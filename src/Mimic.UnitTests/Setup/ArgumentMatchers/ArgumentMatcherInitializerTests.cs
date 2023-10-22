using System.Linq.Expressions;
using Mimic.Setup.ArgumentMatchers;

namespace Mimic.UnitTests.Setup.ArgumentMatchers;

public class ArgumentMatcherInitializerTests
{
    [Fact]
    public void Initialize_WhenCalledWithArgAnyMatcher_ShouldReturnInitializedArgumentMatcher()
    {
        var expression = ((LambdaExpression)(() => Arg.Any<object>())).Body;

        var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

        argumentMatcher.ShouldBeOfType<ArgumentMatcher<object>>();
    }

    [Fact]
    public void Initialize_WhenCalledWithArgAnyMatcher_AndWhenMatcherCalledWithNull_ShouldReturnTrue()
    {
        var expression = ((LambdaExpression)(() => Arg.Any<object>())).Body;

        var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

        argumentMatcher.Matches(null, typeof(object)).ShouldBeTrue();
    }

    [Theory]
    [AutoData]
    public void Initialize_WhenCalledWithArgAnyMatcher_AndWhenMatcherCalledWithNonNullAssignableType_ShouldReturnTrue(string value)
    {
        var expression = ((LambdaExpression)(() => Arg.Any<object>())).Body;

        var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

        argumentMatcher.Matches(value, typeof(object)).ShouldBeTrue();
    }

    [Theory]
    [AutoData]
    public void Initialize_WhenCalledWithArgAnyMatcher_AndWhenMatcherCalledWithNonNullAssignableToInterfaceType_ShouldReturnTrue(string value)
    {
        var expression = ((LambdaExpression)(() => Arg.Any<IEnumerable<char>>())).Body;

        var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

        argumentMatcher.Matches(value, typeof(IEnumerable<char>)).ShouldBeTrue();
    }

    [Theory]
    [AutoData]
    public void Initialize_WhenCalledWithArgAnyMatcher_AndWhenMatcherCalledWithNonAssignableType_ShouldReturnFalse(string value)
    {
        var expression = ((LambdaExpression)(() => Arg.Any<int>())).Body;

        var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

        argumentMatcher.Matches(value, typeof(int)).ShouldBeFalse();
    }

    [Fact]
    public void Initialize_WhenCalledWithArgAnyNotNullMatcher_ShouldReturnInitializedArgumentMatcher()
    {
        var expression = ((LambdaExpression)(() => Arg.AnyNotNull<object>())).Body;

        var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

        argumentMatcher.ShouldBeOfType<ArgumentMatcher<object>>();
    }

    [Theory]
    [AutoData]
    public void Initialize_WhenCalledWithArgAnyNotNullMatcher_AndWhenMatcherCalledWithNonNullAssignableType_ShouldReturnTrue(string value)
    {
        var expression = ((LambdaExpression)(() => Arg.AnyNotNull<object>())).Body;

        var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

        argumentMatcher.Matches(value, typeof(object)).ShouldBeTrue();
    }

    [Theory]
    [AutoData]
    public void Initialize_WhenCalledWithArgAnyNotNullMatcher_AndWhenMatcherCalledWithNonNullAssignableToInterfaceType_ShouldReturnTrue(string value)
    {
        var expression = ((LambdaExpression)(() => Arg.AnyNotNull<IEnumerable<char>>())).Body;

        var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

        argumentMatcher.Matches(value, typeof(IEnumerable<char>)).ShouldBeTrue();
    }

    [Theory]
    [AutoData]
    public void Initialize_WhenCalledWithArgAnyNotNullMatcher_AndWhenMatcherCalledWithNonAssignableType_ShouldReturnFalse(string value)
    {
        var expression = ((LambdaExpression)(() => Arg.AnyNotNull<int>())).Body;

        var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

        argumentMatcher.Matches(value, typeof(int)).ShouldBeFalse();
    }

    [Fact]
    public void Initialize_WhenCalledWithArgAnyNotNullMatcher_AndWhenMatcherCalledWithNull_ShouldReturnFalse()
    {
        var expression = ((LambdaExpression)(() => Arg.AnyNotNull<object>())).Body;

        var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

        argumentMatcher.Matches(null, typeof(object)).ShouldBeFalse();
    }

    [Fact]
    public void Initialize_WhenCalledWithArgIsMatcherWithVariable_ShouldReturnInitializedArgumentMatcher()
    {
        object value = new();
        var expression = ((LambdaExpression)(() => Arg.Is(value))).Body;

        var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

        argumentMatcher.ShouldBeOfType<ArgumentMatcher<object>>();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("mimic")]
    [InlineData("bar")]
    public void Initialize_WhenCalledWithArgIsMatcherWithVariable_AndWhenMatcherCalledWithMatchingValue_ShouldReturnTrue(string value)
    {
        var expression = ((LambdaExpression)(() => Arg.Is(value))).Body;

        var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

        argumentMatcher.Matches(value, typeof(string)).ShouldBeTrue();
    }

    [Theory]
    [InlineData(null, "non-null")]
    [InlineData("not-null", null)]
    [InlineData("foo", "bar")]
    public void Initialize_WhenCalledWithArgIsMatcherWithVariable_AndWhenMatcherCalledWithNonMatchingValue_ShouldReturnFalse(string expectedValue, string value)
    {
        var expression = ((LambdaExpression)(() => Arg.Is(expectedValue))).Body;

        var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

        argumentMatcher.Matches(value, typeof(string)).ShouldBeFalse();
    }

    [Fact]
    public void Initialize_WhenCalledWithArgIsMatcherWithExpression_ShouldReturnInitializedArgumentMatcher()
    {
        object value = new();
        var expression = ((LambdaExpression)(() => Arg.Is<object>(v => v == value))).Body;

        var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

        argumentMatcher.ShouldBeOfType<ArgumentMatcher<object>>();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("mimic")]
    [InlineData("bar")]
    public void Initialize_WhenCalledWithArgIsMatcherWithExpression_AndWhenMatcherCalledWithMatchingValue_ShouldReturnTrue(string value)
    {
        var expression = ((LambdaExpression)(() => Arg.Is<string>(v => v == value))).Body;

        var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

        argumentMatcher.Matches(value, typeof(string)).ShouldBeTrue();
    }

    [Theory]
    [InlineData(null, "non-null")]
    [InlineData("not-null", null)]
    [InlineData("foo", "bar")]
    public void Initialize_WhenCalledWithArgIsMatcherWithExpression_AndWhenMatcherCalledWithNonMatchingValue_ShouldReturnFalse(string expectedValue, string value)
    {
        var expression = ((LambdaExpression)(() => Arg.Is<string>(v => v == expectedValue))).Body;

        var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

        argumentMatcher.Matches(value, typeof(string)).ShouldBeFalse();
    }

    // TODO: Additional tests for ArgumentMatcherInitializer.Initialize(expression) covering; Convert, get_Methods, Member/Index, Constant & Quote
}
