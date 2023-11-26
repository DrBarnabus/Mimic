using Mimic.Core;
using Mimic.Setup.ArgumentMatchers;

namespace Mimic.UnitTests.Setup.ArgumentMatchers;

public class ParamArrayArgumentMatcherTests
{
    [Fact]
    public void Constructor_WhenArgumentMatchersIsNull_ShouldThrowAssertionException()
    {
        var ex = Should.Throw<Guard.AssertionException>(() => new ParamArrayArgumentMatcher(null!));
        ex.Expression.ShouldBe("argumentMatchers");
        ex.Message.ShouldContain("argumentMatchers must not be null");
    }

    [Fact]
    public void Matches_WithArgumentThatIsNotAnArray_ShouldReturnFalse()
    {
        IArgumentMatcher[] argumentMatchers = { AnyArgumentMatcher.Instance, AnyArgumentMatcher.Instance };
        var argumentMatcher = new ParamArrayArgumentMatcher(argumentMatchers);

        argumentMatcher.Matches(100).ShouldBeFalse();
    }

    [Fact]
    public void Matches_WithArgumentThatIsNotExpectedLength_ShouldReturnFalse()
    {
        IArgumentMatcher[] argumentMatchers = { AnyArgumentMatcher.Instance, AnyArgumentMatcher.Instance };
        var argumentMatcher = new ParamArrayArgumentMatcher(argumentMatchers);

        int[] argument = { 100 };
        argumentMatcher.Matches(argument).ShouldBeFalse();
    }

    [Fact]
    public void Matches_WithNonMatchingArguments_ShouldReturnFalse()
    {
        IArgumentMatcher[] argumentMatchers = { new ArgumentMatcher<int>(i => i == 100), new ArgumentMatcher<int>(i => i == 100) };
        var argumentMatcher = new ParamArrayArgumentMatcher(argumentMatchers);

        int[] argument = { 100, 200 };
        argumentMatcher.Matches(argument).ShouldBeFalse();
    }

    [Fact]
    public void Matches_WithMatchingArguments_ShouldReturnTrue()
    {
        IArgumentMatcher[] argumentMatchers = { AnyArgumentMatcher.Instance, AnyArgumentMatcher.Instance };
        var argumentMatcher = new ParamArrayArgumentMatcher(argumentMatchers);

        int[] argument = { 100, 200 };
        argumentMatcher.Matches(argument).ShouldBeTrue();
    }
}
