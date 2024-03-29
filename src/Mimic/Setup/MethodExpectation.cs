﻿using Mimic.Setup.ArgumentMatchers;

namespace Mimic.Setup;

internal sealed class MethodExpectation : IExpectation
{
    private readonly IArgumentMatcher[] _argumentMatchers;
    private MethodInfo? _methodImplementation;

    public LambdaExpression Expression { get; }

    public MethodInfo MethodInfo { get; }

    public Expression[] Arguments { get; }

    internal MethodExpectation(
        LambdaExpression expression,
        MethodInfo methodInfo,
        IEnumerable<Expression>? arguments = null,
        bool skipMatchers = false)

    {
        Guard.NotNull(expression);
        Guard.NotNull(methodInfo);

        ValidateMethodIsOverridable(methodInfo, expression);

        Expression = expression;
        MethodInfo = methodInfo;

        if (arguments is not null && !skipMatchers)
            (_argumentMatchers, Arguments) = ArgumentMatcherInitializer.Initialize(arguments.ToArray(), MethodInfo.GetParameters());
        else
            (_argumentMatchers, Arguments) = (Array.Empty<IArgumentMatcher>(), arguments?.ToArray() ?? Array.Empty<Expression>());
    }

    public bool MatchesInvocation(Invocation invocation)
    {
        if (invocation.Method != MethodInfo && !IsMatchedGenericMethod(invocation))
            return false;

        object?[] arguments = invocation.Arguments;
        var parameters = invocation.Method.GetParameters();
        for (int i = 0; i < _argumentMatchers.Length; i++)
        {
            if (!_argumentMatchers[i].Matches(arguments[i], parameters[i].ParameterType))
                return false;
        }

        return true;
    }

    private bool IsMatchedGenericMethod(Invocation invocation)
    {
        Guard.Assert(invocation.Method != MethodInfo);

        _methodImplementation ??= MethodInfo.GetImplementingMethod(invocation.ProxyType);
        if (invocation.MethodImplementation != _methodImplementation)
            return false;

        if (!MethodInfo.IsGenericMethod && !invocation.Method.IsGenericMethod)
            return true;

        return MethodInfo.GetGenericArguments().CompareWith(invocation.Method.GetGenericArguments());
    }

    private static void ValidateMethodIsOverridable(MethodInfo method, Expression expression)
    {
        if (method.IsStatic)
            throw new UnsupportedExpressionException(expression,
                method.IsDefined(typeof(ExtensionAttribute))
                    ? UnsupportedExpressionException.UnsupportedReason.MemberIsExtension
                    : UnsupportedExpressionException.UnsupportedReason.MemberIsStatic);

        if (method is not { IsVirtual: true, IsFinal: false, IsPrivate: false })
            throw new UnsupportedExpressionException(expression, UnsupportedExpressionException.UnsupportedReason.MemberIsNotOverridable);

        ProxyGenerator.ThrowIfMethodIsInaccessible(method);
    }
}
