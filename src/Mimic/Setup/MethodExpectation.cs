using Mimic.Setup.ArgumentMatchers;

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

        Expression = expression;
        MethodInfo = methodInfo;

        if (arguments is not null && !skipMatchers)
        {
            (_argumentMatchers, Arguments) = ArgumentMatcherInitializer.Initialize(arguments.ToArray(), MethodInfo.GetParameters());
        }
        else
        {
            Arguments = arguments?.ToArray() ?? Array.Empty<Expression>();
            _argumentMatchers = Array.Empty<IArgumentMatcher>();
        }
    }

    public bool MatchesInvocation(IInvocation invocation)
    {
        if (invocation.Method != MethodInfo && !IsMatchedGenericMethod(invocation))
            return false;

        object[] arguments = invocation.Arguments;
        var parameters = invocation.Method.GetParameters();

        for (int i = 0; i < _argumentMatchers.Length; i++)
        {
            if (!_argumentMatchers[i].Matches(arguments[i], parameters[i].ParameterType))
                return false;
        }

        return true;
    }

    private bool IsMatchedGenericMethod(IInvocation invocation)
    {
        Guard.Assert(invocation.Method != MethodInfo);

        _methodImplementation ??= MethodInfo.GetImplementingMethod(invocation.ProxyType);
        if (invocation.MethodImplementation != _methodImplementation)
            return false;

        if (!MethodInfo.IsGenericMethod && !invocation.Method.IsGenericMethod)
            return true;

        return MethodInfo.GetGenericArguments().CompareWith(invocation.Method.GetGenericArguments());
    }
}
