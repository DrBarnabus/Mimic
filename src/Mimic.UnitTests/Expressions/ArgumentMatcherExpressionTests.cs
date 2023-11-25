using System.Linq.Expressions;
using Mimic.Expressions;
using Mimic.Setup.ArgumentMatchers;

namespace Mimic.UnitTests.Expressions;

public class ArgumentMatcherExpressionTests
{
    [Fact]
    public void Constructor_ShouldAppropriatelySetProperties()
    {
        var argumentMatcher = new ArgumentMatcher<string>(_ => true);

        var expression = new ArgumentMatcherExpression(argumentMatcher);

        expression.ShouldNotBeNull();
        expression.ArgumentMatcher.ShouldBeSameAs(argumentMatcher);
        expression.NodeType.ShouldBe(ExpressionType.Extension);
        expression.Type.ShouldBe(argumentMatcher.Type);
        expression.CanReduce.ShouldBeFalse();
    }
}
