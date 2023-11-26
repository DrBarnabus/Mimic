using Mimic.Setup.ArgumentMatchers;

namespace Mimic.UnitTests.Setup.ArgumentMatchers;

public class ConstantArgumentMatcherTests
{
    // ReSharper disable once InconsistentNaming
    public static List<object?[]> Matches_WithMatchingValues_TestData =>
    [
        new object?[] { "value", "value", true },
        new object?[] { 100, 100, true },
        new object?[] { new[] { 1, 2, 3 }, new[] { 1, 2, 3 }, true },
        new object?[] { null, null, true },
        new object?[] { "value", "different", false },
        new object?[] { 100, 200, false },
        new object?[] { new[] { 1, 2, 3 }, new[] { 1, 9, 3 }, false },
        new object?[] { "value", null, false },
        new object?[] { 100, null, false },
        new object?[] { 100, null, false },
    ];

    [Theory]
    [MemberData(nameof(Matches_WithMatchingValues_TestData))]
    public void Matches_WithMatchingValues_ShouldReturnCorrectResult(object? value, object? argument, bool expectedResult)
    {
        var argumentMatcher = new ConstantArgumentMatcher(value);

        argumentMatcher.Matches(argument, typeof(void)).ShouldBe(expectedResult);
    }
}
