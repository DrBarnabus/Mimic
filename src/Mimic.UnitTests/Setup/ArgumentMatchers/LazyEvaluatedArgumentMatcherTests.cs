using System.Linq.Expressions;
using Mimic.Setup.ArgumentMatchers;

namespace Mimic.UnitTests.Setup.ArgumentMatchers;

public class LazyEvaluatedArgumentMatcherTests
{
    [Fact]
    public void Matches_WhenExpressionDoesNotEvaluateToConstantExpression_ShouldReturnFalse()
    {
        Expression expression = (int m) => m.ToString();
        var argumentMatcher = new LazyEvaluatedArgumentMatcher(expression);

        argumentMatcher.Matches(null, typeof(string)).ShouldBeFalse();
    }

    [Fact]
    public void Matches_WhenExpressionEvaluatesToConstantExpression_AndArgumentMatches_ShouldReturnTrue()
    {
        var argumentMatcher = new LazyEvaluatedArgumentMatcher(Expression.Constant("value"));

        argumentMatcher.Matches("value", typeof(string)).ShouldBeTrue();
    }

    [Fact]
    public void Matches_WhenExpressionEvaluatesToConstantExpression_AndArgumentDoesNotMatch_ShouldReturnFalse()
    {
        var argumentMatcher = new LazyEvaluatedArgumentMatcher(Expression.Constant("value"));

        argumentMatcher.Matches("different", typeof(string)).ShouldBeFalse();
    }
}
