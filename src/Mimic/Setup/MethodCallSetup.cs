﻿using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using Mimic.Core;
using Mimic.Core.Extensions;
using Mimic.Exceptions;
using Mimic.Proxy;

namespace Mimic.Setup;

internal sealed class MethodCallSetup : SetupBase
{
    private readonly Func<bool>? _condition;
    private Behaviour? _returnOrThrow;
    private CallbackBehaviour? _preReturnCallback;
    private CallbackBehaviour? _postReturnCallback;
    private ExecutionLimitBehaviour? _executionLimit;

    public MethodInfo MethodInfo => ((MethodExpectation)Expectation).MethodInfo;

    public MethodCallSetup(Expression originalExpression, IMimic mimic, MethodExpectation expectation, Func<bool>? condition)
        : base(originalExpression, mimic, expectation)
    {
        _condition = condition;
    }

    public override bool MatchesInvocation(IInvocation invocation) =>
        base.MatchesInvocation(invocation) && (_condition == null || _condition.Invoke());

    protected override void ExecuteCore(IInvocation invocation)
    {
        _executionLimit?.Execute(invocation);
        _preReturnCallback?.Execute(invocation);

        if (_returnOrThrow is not null)
        {
            _returnOrThrow.Execute(invocation);
        }
        else if (invocation.Method.ReturnType != typeof(void))
        {
            if (Mimic.Strict)
                throw MimicException.ReturnRequired(invocation);

            object? defaultValue = DefaultValueFactory.GetDefaultValue(invocation.Method.ReturnType);
            invocation.SetReturnValue(defaultValue);
        }

        _postReturnCallback?.Execute(invocation);
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
        {
            _returnOrThrow = new ReturnValueBehaviour(valueFactory);
        }
        else
        {
            ValidateDelegateArgumentCount(valueFactory);
            ValidateReturnDelegateReturnType(valueFactory, MethodInfo.ReturnType);

            _returnOrThrow = valueFactory.CompareParameterTypesTo(Type.EmptyTypes)
                ? new ReturnComputedValueBehaviour(_ => valueFactory.Invoke())
                : new ReturnComputedValueBehaviour(invocation => valueFactory.Invoke(invocation.Arguments));
        }
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

        ValidateDelegateArgumentCount(exceptionFactory);
        ValidateReturnDelegateReturnType(exceptionFactory, typeof(Exception));

        _returnOrThrow = exceptionFactory.CompareParameterTypesTo(Type.EmptyTypes)
            ? new ThrowComputedExceptionBehaviour(_ => exceptionFactory.Invoke() as Exception)
            : new ThrowComputedExceptionBehaviour(invocation => exceptionFactory.Invoke(invocation.Arguments) as Exception);
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

    private static void ValidateReturnDelegateReturnType(Delegate delegateFunction, Type expectedReturnType)
    {
        var actualReturnType = delegateFunction.GetMethodInfo().ReturnType;

        if (actualReturnType == typeof(void))
            throw MimicException.WrongReturnCallbackReturnType(expectedReturnType, null);

        if (!expectedReturnType.IsAssignableFrom(actualReturnType))
            throw MimicException.WrongReturnCallbackReturnType(expectedReturnType, actualReturnType);
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

    private sealed class ThrowExceptionBehaviour : Behaviour
    {
        private readonly Exception _exception;

        public ThrowExceptionBehaviour(Exception exception) => _exception = exception;

        internal override void Execute(IInvocation invocation)
        {
            throw _exception;
        }
    }

    private sealed class ThrowComputedExceptionBehaviour : Behaviour
    {
        private readonly Func<IInvocation, Exception?> _exceptionFactory;

        public ThrowComputedExceptionBehaviour(Func<IInvocation, Exception?> exceptionFactory) => _exceptionFactory = exceptionFactory;

        internal override void Execute(IInvocation invocation)
        {
            throw _exceptionFactory.Invoke(invocation)!;
        }
    }

    private sealed class CallbackBehaviour : Behaviour
    {
        private readonly Action<IInvocation> _callbackFunction;

        public CallbackBehaviour(Action<IInvocation> callbackFunction) => _callbackFunction = callbackFunction;

        internal override void Execute(IInvocation invocation)
        {
            _callbackFunction.Invoke(invocation);
        }
    }

    private sealed class ExecutionLimitBehaviour : Behaviour
    {
        private readonly MethodCallSetup _setup;
        private readonly int _executionLimit;
        private int _executionCount = 0;

        public ExecutionLimitBehaviour(MethodCallSetup setup, int executionLimit) => (_setup, _executionLimit) = (setup, executionLimit);

        internal override void Execute(IInvocation invocation)
        {
            _executionCount++;
            if (_executionCount > _executionLimit)
                throw MimicException.ExecutionLimitExceeded(_setup, _executionLimit, _executionCount);
        }
    }
}
