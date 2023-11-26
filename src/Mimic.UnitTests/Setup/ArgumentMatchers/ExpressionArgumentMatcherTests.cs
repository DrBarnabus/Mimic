using System.Linq.Expressions;
using Mimic.Setup.ArgumentMatchers;

namespace Mimic.UnitTests.Setup.ArgumentMatchers;

public class ExpressionArgumentMatcherTests
{
    [Fact]
    public void Matches_WithMatchingExpression_ShouldReturnTrue()
    {
        Expression expression = () => new ExpressionArgumentMatcherTests();
        var argumentMatcher = new ExpressionArgumentMatcher(expression);

        argumentMatcher.Matches(expression, typeof(Expression)).ShouldBeTrue();
    }

    [Fact]
    public void Matches_WithDifferentExpression_ShouldReturnFalse()
    {
        Expression expression = () => new ExpressionArgumentMatcherTests();
        var argumentMatcher = new ExpressionArgumentMatcher(expression);

        Expression differentExpression = (() => new ExpressionArgumentMatcherTests().ToString());
        argumentMatcher.Matches(differentExpression, typeof(Expression)).ShouldBeFalse();
    }

    [Fact]
    public void Matches_WithArgumentThatIsNotAnExpression_ShouldReturnFalse()
    {
        Expression expression = () => new ExpressionArgumentMatcherTests();
        var argumentMatcher = new ExpressionArgumentMatcher(expression);

        argumentMatcher.Matches(100, typeof(int)).ShouldBeFalse();
    }
}
