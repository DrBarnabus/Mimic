using Mimic.Setup.ArgumentMatchers;

namespace Mimic.UnitTests.Setup.ArgumentMatchers;

public class RefArgumentMatcherTests
{
    [Fact]
    public void Matches_WhenReferenceIsValueType_AndArgumentIsEqual_ShouldReturnTrue()
    {
        var argumentMatcher = new RefArgumentMatcher(100);
        argumentMatcher.Matches(100).ShouldBeTrue();
    }

    [Fact]
    public void Matches_WhenReferenceIsValueType_AndArgumentIsNotEqual_ShouldReturnFalse()
    {
        var argumentMatcher = new RefArgumentMatcher(100);
        argumentMatcher.Matches(101).ShouldBeFalse();
    }

    [Fact]
    public void Matches_WhenReferenceIsReferenceType_AndArgumentIsEqual_ShouldReturnTrue()
    {
        var argumentMatcher = new RefArgumentMatcher("value");
        argumentMatcher.Matches("value").ShouldBeTrue();
    }

    [Fact]
    public void Matches_WhenReferenceIsReferenceType_AndArgumentIsNotEqual_ShouldReturnFalse()
    {
        var argumentMatcher = new RefArgumentMatcher("value");
        argumentMatcher.Matches("different").ShouldBeFalse();
    }

    [Fact]
    public void Matches_WhenReferenceIsNull_AndArgumentIsEqual_ShouldReturnTrue()
    {
        var argumentMatcher = new RefArgumentMatcher(null);
        argumentMatcher.Matches(null).ShouldBeTrue();
    }

    [Fact]
    public void Matches_WhenReferenceIsNull_AndArgumentIsNotEqual_ShouldReturnFalse()
    {
        var argumentMatcher = new RefArgumentMatcher(null);
        argumentMatcher.Matches("notnull").ShouldBeFalse();
    }
}
