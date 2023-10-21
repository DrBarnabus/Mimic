using System.Linq.Expressions;
using System.Reflection;
using Mimic.Core;
using Mimic.Proxy;
using Mimic.Setup.ArgumentMatchers;

namespace Mimic.Setup;

internal sealed class MethodExpectation
{
    private readonly IArgumentMatcher[] _argumentMatchers;

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
        if (invocation.Method != MethodInfo)
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
}
