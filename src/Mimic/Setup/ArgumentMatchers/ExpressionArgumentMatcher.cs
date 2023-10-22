using System.Linq.Expressions;
using Mimic.Expressions;

namespace Mimic.Setup.ArgumentMatchers;

internal sealed class ExpressionArgumentMatcher : IArgumentMatcher
{
    private readonly Expression _expression;

    public ExpressionArgumentMatcher(Expression expression) => _expression = expression;

    public bool Matches(object? argument, Type type)
    {
        return argument is Expression argumentExpression
               && ExpressionEqualityComparer.Default.Equals(_expression, argumentExpression);
    }
}
