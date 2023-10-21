using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using Mimic.Core;
using Mimic.Proxy;

namespace Mimic.Setup;

internal sealed class MethodCallSetup
{
    private Flags _flags;
    private Behaviour? _returnOrThrow;

    public Expression OriginalExpression { get; }

    public IMimic Mimic { get; }

    public MethodExpectation Expectation { get; }

    public LambdaExpression Expression => Expectation.Expression;

    public MethodInfo MethodInfo => Expectation.MethodInfo;

    public bool Matched => (_flags & Flags.Matched) != 0;

    public bool Overriden => (_flags & Flags.Overriden) != 0;

    public MethodCallSetup(Expression originalExpression, IMimic mimic, MethodExpectation expectation)
    {
        OriginalExpression = originalExpression;
        Mimic = mimic;
        Expectation = expectation;
    }

    public bool MatchesInvocation(IInvocation invocation) => Expectation.MatchesInvocation(invocation);

    public void Execute(IInvocation invocation)
    {
        _flags |= Flags.Matched;

        if (_returnOrThrow is not null)
        {
            _returnOrThrow.Execute(invocation);
        }
        else if (invocation.Method.ReturnType != typeof(void))
        {
            throw new NotImplementedException(); // TODO: Create a specialized exception for this purpose
        }
    }

    public void Override()
    {
        Guard.Assert(!Overriden);
        _flags |= Flags.Overriden;
    }

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

        var expectedReturnType = MethodInfo.ReturnType;

        if (expectedReturnType == typeof(Delegate))
            _returnOrThrow = new ReturnValueBehaviour(valueFactory);
        else
        {
            ValidateDelegateArgumentCount(valueFactory);
            ValidateDelegateReturnType(valueFactory);

            _returnOrThrow = valueFactory.CompareParameterTypesTo(Type.EmptyTypes)
                ? new ReturnComputedValueBehaviour(_ => valueFactory.Invoke())
                : new ReturnComputedValueBehaviour(invocation => valueFactory.Invoke(invocation.Arguments));
        }
    }

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
                throw new ArgumentException($"Setup on method with {expectedNumberOfArguments} expected argument(s) cannot cannot invoke a callback method with only {actualNumberOfArguments} argument(s)");
            }
        }
    }

    private void ValidateDelegateReturnType(Delegate delegateFunction)
    {
        var actualReturnType = delegateFunction.GetMethodInfo().ReturnType;
        var expectedReturnType = MethodInfo.ReturnType;

        // TODO: See if we need this check at all
        // if (actualReturnType == typeof(void))
        //     throw new ArgumentException($"Setup on method with return type '{TypeNameFormatter.GetFormattedName(expectedReturnType)}' cannot invoke a callback method with a void return type");

        if (!expectedReturnType.IsAssignableFrom(actualReturnType))
            throw new ArgumentException($"Setup on method with return type '{TypeNameFormatter.GetFormattedName(expectedReturnType)}' cannot invoke a callback method with return type '{TypeNameFormatter.GetFormattedName(actualReturnType)}'");
    }

    [Flags]
    private enum Flags : byte
    {
        None = 0,
        Matched = 1 << 0,
        Overriden = 1 << 1,
    }

    private abstract class Behaviour
    {
        internal abstract void Execute(IInvocation invocation);
    }

    private sealed class ReturnValueBehaviour : Behaviour
    {
        private readonly object? _value;

        public ReturnValueBehaviour(object? value) => _value = value;

        internal override void Execute(IInvocation invocation) => invocation.SetReturnValue(_value);
    }

    private sealed class ReturnComputedValueBehaviour : Behaviour
    {
        private readonly Func<IInvocation, object?> _valueFactory;

        public ReturnComputedValueBehaviour(Func<IInvocation, object?> valueFactory) => _valueFactory = valueFactory;

        internal override void Execute(IInvocation invocation) => invocation.SetReturnValue(_valueFactory.Invoke(invocation));
    }
}
