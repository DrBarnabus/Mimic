using Mimic.Setup.ArgumentMatchers;

namespace Mimic.UnitTests.Setup.ArgumentMatchers;

public class AnyArgumentMatcherTests
{
    [Fact]
    public void Instance_ShouldAlwaysReturnTheSameReference()
    {
        var instance = AnyArgumentMatcher.Instance;
        instance.ShouldBeSameAs(AnyArgumentMatcher.Instance);
    }

    [Theory]
    [AutoData]
    public void Matches_ShouldAlwaysReturnTrue(string argument)
    {
        AnyArgumentMatcher.Instance.Matches(argument).ShouldBeTrue();
    }
}
