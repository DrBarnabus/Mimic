using Mimic.Expressions;
using Mimic.Setup.Behaviours;

namespace Mimic.Setup;

using OutValue = (int Position, object? Value);

internal sealed class MethodCallSetup : SetupBase
{
    private readonly Func<bool>? _condition;
    private readonly List<OutValue> _outValues;

    private Behaviour? _returnOrThrow;
    private CallbackBehaviour? _preReturnCallback;
    private CallbackBehaviour? _postReturnCallback;
    private ExecutionLimitBehaviour? _executionLimit;

    public MethodInfo MethodInfo => ((MethodExpectation)Expectation).MethodInfo;

    public MethodCallSetup(Expression originalExpression, IMimic mimic, MethodExpectation expectation, Func<bool>? condition)
        : base(originalExpression, mimic, expectation)
    {
        _condition = condition;

        _outValues = FindAndEvaluateOutValues(expectation.Arguments, expectation.MethodInfo.GetParameters());
    }

    public override bool MatchesInvocation(Invocation invocation) =>
        base.MatchesInvocation(invocation) && (_condition == null || _condition.Invoke());

    protected override void ExecuteCore(Invocation invocation)
    {
        SetOutParameters(invocation);

        _executionLimit?.Execute(invocation);
        _preReturnCallback?.Execute(invocation);

        if (_returnOrThrow is not null)
            _returnOrThrow.Execute(invocation);
        else if (invocation.Method.ReturnType != typeof(void))
            StrictThrowOrReturnDefault(invocation);

        _postReturnCallback?.Execute(invocation);
    }

    #region SetBehaviours

    public void SetReturnValueBehaviour(object? value)
    {
        Guard.Assert(MethodInfo.ReturnType != typeof(void));
        Guard.Assert(_returnOrThrow is null);

        _returnOrThrow = new ReturnValueBehaviour(value);
    }

    public void SetReturnComputedValueBehaviour(Delegate valueFactory)
    {
        Guard.Assert(MethodInfo.ReturnType != typeof(void));
        Guard.Assert(_returnOrThrow is null);

        _returnOrThrow = ReturnValueBehaviourFromValueFactory(valueFactory);
    }

    public void SetThrowExceptionBehaviour(Exception exception)
    {
        Guard.NotNull(exception);
        Guard.Assert(_returnOrThrow is null);

        _returnOrThrow = new ThrowExceptionBehaviour(exception);
    }

    public void SetThrowComputedExceptionBehaviour(Delegate exceptionFactory)
    {
        Guard.NotNull(exceptionFactory);
        Guard.Assert(_returnOrThrow is null);

        _returnOrThrow = ThrowExceptionBehaviourFromExecptionFactory(exceptionFactory);
    }

    public void SetCallbackBehaviour(Delegate callbackFunction)
    {
        Guard.NotNull(callbackFunction);

        ref var callbackBehaviour = ref _returnOrThrow == null ? ref _preReturnCallback : ref _postReturnCallback;

        if (callbackFunction is Action callbackFunctionWithoutArgs)
        {
            callbackBehaviour = new CallbackBehaviour(_ => callbackFunctionWithoutArgs());
        }
        else
        {
            ValidateDelegateArgumentCount(callbackFunction);

            var expectedArguments = MethodInfo.GetParameters();
            if (!callbackFunction.CompareParameterTypesTo(expectedArguments.Select(p => p.ParameterType).ToArray()))
                throw MimicException.WrongCallbackArgumentTypes(expectedArguments, callbackFunction.GetMethodInfo().GetParameters());

            var callbackReturnType = callbackFunction.GetMethodInfo().ReturnType;
            if (callbackReturnType != typeof(void))
                throw MimicException.WrongCallbackReturnType();

            callbackBehaviour = new CallbackBehaviour(invocation => callbackFunction.Invoke(invocation.Arguments));
        }
    }

    public void SetExecutionLimitBehaviour(int executionLimit)
    {
        Guard.Assert(executionLimit >= 1);
        Guard.Assert(_executionLimit is null);

        _executionLimit = new ExecutionLimitBehaviour(this, executionLimit);
    }

    #endregion

    #region AddBehaviours

    public void AddReturnValueBehaviour(object? value)
    {
        Guard.Assert(MethodInfo.ReturnType != typeof(void));

        AddBehaviour(new ReturnValueBehaviour(value));
    }

    public void AddReturnComputedValueBehaviour(Delegate valueFactory)
    {
        Guard.Assert(MethodInfo.ReturnType != typeof(void));

        AddBehaviour(ReturnValueBehaviourFromValueFactory(valueFactory));
    }

    public void AddThrowExceptionBehaviour(Exception exception)
    {
        Guard.NotNull(exception);

        AddBehaviour(new ThrowExceptionBehaviour(exception));
    }

    public void AddThrowComputedExceptionBehaviour(Delegate exceptionFactory)
    {
        Guard.NotNull(exceptionFactory);

        AddBehaviour(ThrowExceptionBehaviourFromExecptionFactory(exceptionFactory));
    }

    public void AddNoOpBehaviour()
    {
        Guard.Assert(MethodInfo.ReturnType == typeof(void));

        AddBehaviour(NoOpBehaviour.Instance);
    }

    private void AddBehaviour(Behaviour behaviour)
    {
        Guard.Assert(_returnOrThrow is null or SequenceBehaviour);

        _returnOrThrow ??= new SequenceBehaviour(this);
        if (_returnOrThrow is SequenceBehaviour sequenceBehaviour)
            sequenceBehaviour.AddBehaviour(behaviour);
    }

    #endregion

    internal override void VerifyMatched()
    {
        base.VerifyMatched();

        if (_returnOrThrow is SequenceBehaviour { Remaining: >0 } sequenceBehaviour)
            throw MimicException.ExpectedSequenceSetupNotMatched(this, sequenceBehaviour.Remaining);
    }

    internal void StrictThrowOrReturnDefault(Invocation invocation)
    {
        if (Mimic.Strict)
            throw MimicException.ReturnRequired(invocation);

        object? defaultValue = DefaultValueFactory.GetDefaultValue(invocation.Method.ReturnType);
        invocation.SetReturnValue(defaultValue);
    }

    private Behaviour ReturnValueBehaviourFromValueFactory(Delegate valueFactory)
    {
        var expectedReturnType = MethodInfo.ReturnType;
        if (expectedReturnType == typeof(Delegate))
            return new ReturnValueBehaviour(valueFactory);

        ValidateDelegateArgumentCount(valueFactory);
        ValidateReturnDelegateReturnType(valueFactory, expectedReturnType);

        return valueFactory.CompareParameterTypesTo(Type.EmptyTypes)
            ? new ReturnComputedValueBehaviour(_ => valueFactory.Invoke())
            : new ReturnComputedValueBehaviour(invocation => valueFactory.Invoke(invocation.Arguments));
    }

    private ThrowComputedExceptionBehaviour ThrowExceptionBehaviourFromExecptionFactory(Delegate exceptionFactory)
    {
        ValidateDelegateArgumentCount(exceptionFactory);
        ValidateReturnDelegateReturnType(exceptionFactory, typeof(Exception));

        return exceptionFactory.CompareParameterTypesTo(Type.EmptyTypes)
            ? new ThrowComputedExceptionBehaviour(_ => exceptionFactory.Invoke() as Exception)
            : new ThrowComputedExceptionBehaviour(invocation => exceptionFactory.Invoke(invocation.Arguments) as Exception);
    }

    private void SetOutParameters(Invocation invocation)
    {
        foreach ((int position, object? value) in _outValues)
            invocation.Arguments[position] = value;
    }

    // TODO: Move this out into `DelegateExtensions`
    private void ValidateDelegateArgumentCount(Delegate delegateFunction)
    {
        var methodInfo = delegateFunction.GetMethodInfo();

        int actualNumberOfArguments = methodInfo.GetParameters().Length;
        if (methodInfo.IsStatic && (methodInfo.IsDefined(typeof(ExtensionAttribute)) || delegateFunction.Target != null))
            actualNumberOfArguments--;

        if (actualNumberOfArguments > 0)
        {
            int expectedNumberOfArguments = MethodInfo.GetParameters().Length;
            if (actualNumberOfArguments != expectedNumberOfArguments)
            {
                throw MimicException.WrongCallbackArgumentCount(expectedNumberOfArguments, actualNumberOfArguments);
            }
        }
    }

    // TODO: Move this out into `DelegateExtensions`
    private static void ValidateReturnDelegateReturnType(Delegate delegateFunction, Type expectedReturnType)
    {
        var actualReturnType = delegateFunction.GetMethodInfo().ReturnType;

        if (actualReturnType == typeof(void))
            throw MimicException.WrongReturnCallbackReturnType(expectedReturnType, null);

        if (!expectedReturnType.IsAssignableFrom(actualReturnType))
            throw MimicException.WrongReturnCallbackReturnType(expectedReturnType, actualReturnType);
    }

    private static List<OutValue> FindAndEvaluateOutValues(IReadOnlyList<Expression> arguments, IReadOnlyList<ParameterInfo> parameters)
    {
        var outValues = new List<OutValue>();

        for (int i = 0; i < parameters.Count; i++)
        {
            var parameter = parameters[i];
            if (!parameter.ParameterType.IsByRef || (parameter.Attributes & (ParameterAttributes.In | ParameterAttributes.Out)) != ParameterAttributes.Out)
                continue;

            if (ExpressionEvaluator.PartiallyEvaluate(arguments[i]) is not ConstantExpression constantExpression)
                throw MimicException.OutExpressionMustBeConstantValue();

            outValues.Add((i, constantExpression.Value));
        }

        return outValues;
    }
}
