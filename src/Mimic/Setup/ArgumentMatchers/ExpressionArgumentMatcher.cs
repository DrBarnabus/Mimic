using Mimic.Expressions;

namespace Mimic.Setup.ArgumentMatchers;

internal sealed class ExpressionArgumentMatcher : IArgumentMatcher
{
    private readonly Expression _expression;

    public ExpressionArgumentMatcher(Expression expression) => _expression = expression;

    public bool Matches(object? argument)
    {
        return argument is Expression argumentExpression
               && ExpressionEqualityComparer.Default.Equals(_expression, argumentExpression);
    }
}
