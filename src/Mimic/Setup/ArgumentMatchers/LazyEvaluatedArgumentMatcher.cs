using Mimic.Expressions;

namespace Mimic.Setup.ArgumentMatchers;

internal sealed class LazyEvaluatedArgumentMatcher : IArgumentMatcher
{
    private readonly Expression _expression;

    public LazyEvaluatedArgumentMatcher(Expression expression) => _expression = expression;

    public bool Matches(object? argument)
    {
        var evaluatedExpression = ExpressionEvaluator.PartiallyEvaluate(_expression);
        if (evaluatedExpression is not ConstantExpression constantExpression)
            return false;

        var constantArgumentMatcher = new ConstantArgumentMatcher(constantExpression.Value);
        return constantArgumentMatcher.Matches(argument);
    }
}
