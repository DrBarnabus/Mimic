using Mimic.Setup.ArgumentMatchers;

namespace Mimic.UnitTests.Setup.ArgumentMatchers;

public class ConstantArgumentMatcherTests
{
    // ReSharper disable once InconsistentNaming
    public static List<object?[]> Matches_WithMatchingValues_TestData =>
    [
        new object?[] { "value", "value", typeof(string), true },
        new object?[] { 100, 100, typeof(int), true },
        new object?[] { new[] { 1, 2, 3 }, new[] { 1, 2, 3 }, typeof(int[]), true },
        new object?[] { null, null, typeof(object), true },
        new object?[] { "value", "different", typeof(string), false },
        new object?[] { 100, 200, typeof(int), false },
        new object?[] { new[] { 1, 2, 3 }, new[] { 1, 9, 3 }, typeof(int[]), false },
        new object?[] { "value", null, typeof(string), false },
        new object?[] { 100, null, typeof(int?), false },
    ];

    [Theory]
    [MemberData(nameof(Matches_WithMatchingValues_TestData))]
    public void Matches_WithMatchingValues_ShouldReturnCorrectResult(object? value, object? argument, Type parameterType, bool expectedResult)
    {
        var argumentMatcher = new ConstantArgumentMatcher(value);

        argumentMatcher.Matches(argument, parameterType).ShouldBe(expectedResult);
    }
}
