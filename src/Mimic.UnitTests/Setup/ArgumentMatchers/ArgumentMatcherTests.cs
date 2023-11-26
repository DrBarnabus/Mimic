using Mimic.Setup.ArgumentMatchers;

namespace Mimic.UnitTests.Setup.ArgumentMatchers;

public class ArgumentMatcherTests
{
    [Fact]
    public void Constructor_ShouldAppropriatelySetProperties()
    {
        var argumentMatcher = new ArgumentMatcher<string>(_ => true);

        argumentMatcher.Type.ShouldBe(typeof(string));
    }

    [Theory]
    [AutoData]
    public void Matches_WithMatchingNonNullValue_ShouldReturnTrue(string value)
    {
        var argumentMatcher = new ArgumentMatcher<string>(s => s == value) as IArgumentMatcher;

        argumentMatcher.Matches(value, typeof(string));
    }

    [Theory]
    [AutoData]
    public void Matches_WithNonMatchingNonNullValue_ShouldReturnTrue(string value)
    {
        var argumentMatcher = new ArgumentMatcher<string>(s => s != value) as IArgumentMatcher;

        argumentMatcher.Matches(value, typeof(string));
    }

    [Fact]
    public void Matches_WithMatchingNullReferenceTypeValue_ShouldReturnTrue()
    {
        var argumentMatcher = new ArgumentMatcher<string?>(s => s == null) as IArgumentMatcher;

        argumentMatcher.Matches(null, typeof(string)).ShouldBeTrue();
    }

    [Fact]
    public void Matches_WithNonMatchingNullReferenceTypeValue_ShouldReturnFalse()
    {
        var argumentMatcher = new ArgumentMatcher<string?>(s => s != null) as IArgumentMatcher;

        argumentMatcher.Matches(null, typeof(string)).ShouldBeFalse();
    }

    [Fact]
    public void Matches_WithMatchingNullableValueTypeValue_ShouldReturnTrue()
    {
        var argumentMatcher = new ArgumentMatcher<int?>(i => i == null) as IArgumentMatcher;

        argumentMatcher.Matches(null, typeof(int?)).ShouldBeTrue();
    }

    [Fact]
    public void Matches_WithNonMatchingNullableValueTypeValue_ShouldReturnFalse()
    {
        var argumentMatcher = new ArgumentMatcher<int?>(i => i != null) as IArgumentMatcher;

        argumentMatcher.Matches(null, typeof(int?)).ShouldBeFalse();
    }

    [Fact]
    public void Matches_WithNonNullableValueType_ButCalledWithNull_ShouldReturnTrue()
    {
        var argumentMatcher = new ArgumentMatcher<int>(_ => true) as IArgumentMatcher;

        argumentMatcher.Matches(null, typeof(int)).ShouldBeFalse();
    }
}
