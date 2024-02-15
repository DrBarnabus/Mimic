using System.Linq.Expressions;
using Mimic.Expressions;
using Mimic.Setup.ArgumentMatchers;

namespace Mimic.UnitTests.Setup.ArgumentMatchers;

public static partial class ArgumentMatcherInitializerTests
{
    public class SingleExpression
    {
        #region Arg.Any<TValue>

        [Fact]
        public void WhenCalledWithArgAnyMatcher_ShouldReturnInitializedArgumentMatcher()
        {
            var expression = ((LambdaExpression)(() => Arg.Any<object>())).Body;

            var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

            argumentMatcher.ShouldBeOfType<ArgumentMatcher<object>>();
        }

        [Fact]
        public void WhenCalledWithArgAnyMatcher_AndWhenMatcherCalledWithNull_ShouldReturnTrue()
        {
            var expression = ((LambdaExpression)(() => Arg.Any<object>())).Body;

            var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

            argumentMatcher.Matches(null, typeof(object)).ShouldBeTrue();
        }

        [Theory]
        [AutoData]
        public void WhenCalledWithArgAnyMatcher_AndWhenMatcherCalledWithNonNullAssignableType_ShouldReturnTrue(string value)
        {
            var expression = ((LambdaExpression)(() => Arg.Any<object>())).Body;

            var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

            argumentMatcher.Matches(value, typeof(object)).ShouldBeTrue();
        }

        [Theory]
        [AutoData]
        public void WhenCalledWithArgAnyMatcher_AndWhenMatcherCalledWithNonNullAssignableToInterfaceType_ShouldReturnTrue(string value)
        {
            var expression = ((LambdaExpression)(() => Arg.Any<IEnumerable<char>>())).Body;

            var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

            argumentMatcher.Matches(value, typeof(IEnumerable<char>)).ShouldBeTrue();
        }

        [Theory]
        [AutoData]
        public void WhenCalledWithArgAnyMatcher_AndWhenMatcherCalledWithNonAssignableType_ShouldReturnFalse(string value)
        {
            var expression = ((LambdaExpression)(() => Arg.Any<int>())).Body;

            var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

            argumentMatcher.Matches(value, typeof(int)).ShouldBeFalse();
        }

        #endregion

        #region Arg.AnyNotNull<TValue>

        [Fact]
        public void WhenCalledWithArgAnyNotNullMatcher_ShouldReturnInitializedArgumentMatcher()
        {
            var expression = ((LambdaExpression)(() => Arg.AnyNotNull<object>())).Body;

            var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

            argumentMatcher.ShouldBeOfType<ArgumentMatcher<object>>();
        }

        [Theory]
        [AutoData]
        public void WhenCalledWithArgAnyNotNullMatcher_AndWhenMatcherCalledWithNonNullAssignableType_ShouldReturnTrue(string value)
        {
            var expression = ((LambdaExpression)(() => Arg.AnyNotNull<object>())).Body;

            var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

            argumentMatcher.Matches(value, typeof(object)).ShouldBeTrue();
        }

        [Theory]
        [AutoData]
        public void WhenCalledWithArgAnyNotNullMatcher_AndWhenMatcherCalledWithNonNullAssignableToInterfaceType_ShouldReturnTrue(string value)
        {
            var expression = ((LambdaExpression)(() => Arg.AnyNotNull<IEnumerable<char>>())).Body;

            var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

            argumentMatcher.Matches(value, typeof(IEnumerable<char>)).ShouldBeTrue();
        }

        [Theory]
        [AutoData]
        public void WhenCalledWithArgAnyNotNullMatcher_AndWhenMatcherCalledWithNonAssignableType_ShouldReturnFalse(string value)
        {
            var expression = ((LambdaExpression)(() => Arg.AnyNotNull<int>())).Body;

            var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

            argumentMatcher.Matches(value, typeof(int)).ShouldBeFalse();
        }

        [Fact]
        public void WhenCalledWithArgAnyNotNullMatcher_AndWhenMatcherCalledWithNull_ShouldReturnFalse()
        {
            var expression = ((LambdaExpression)(() => Arg.AnyNotNull<object>())).Body;

            var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

            argumentMatcher.Matches(null, typeof(object)).ShouldBeFalse();
        }

        #endregion

        #region Arg.Is<TValue>

        [Fact]
        public void WhenCalledWithArgIsMatcherWithVariable_ShouldReturnInitializedArgumentMatcher()
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
        public void WhenCalledWithArgIsMatcherWithVariable_AndWhenMatcherCalledWithMatchingValue_ShouldReturnTrue(string? value)
        {
            var expression = ((LambdaExpression)(() => Arg.Is(value))).Body;

            var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

            argumentMatcher.Matches(value, typeof(string)).ShouldBeTrue();
        }

        [Theory]
        [InlineData(null, "non-null")]
        [InlineData("not-null", null)]
        [InlineData("foo", "bar")]
        public void WhenCalledWithArgIsMatcherWithVariable_AndWhenMatcherCalledWithNonMatchingValue_ShouldReturnFalse(string? expectedValue, string? value)
        {
            var expression = ((LambdaExpression)(() => Arg.Is(expectedValue))).Body;

            var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

            argumentMatcher.Matches(value, typeof(string)).ShouldBeFalse();
        }

        [Fact]
        public void WhenCalledWithArgIsMatcherWithVariableAndComparer_ShouldReturnInitializedArgumentMatcher()
        {
            object value = new();
            var expression = ((LambdaExpression)(() => Arg.Is(value, EqualityComparer<object>.Default))).Body;

            var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

            argumentMatcher.ShouldBeOfType<ArgumentMatcher<object>>();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("mimic")]
        [InlineData("bar")]
        public void WhenCalledWithArgIsMatcherWithVariableAndComparer_AndWhenMatcherCalledWithMatchingValue_ShouldReturnTrue(string? value)
        {
            var expression = ((LambdaExpression)(() => Arg.Is(value, EqualityComparer<string?>.Default))).Body;

            var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

            argumentMatcher.Matches(value, typeof(string)).ShouldBeTrue();
        }

        [Theory]
        [InlineData(null, "non-null")]
        [InlineData("not-null", null)]
        [InlineData("foo", "bar")]
        public void WhenCalledWithArgIsMatcherWithVariableAndComparer_AndWhenMatcherCalledWithNonMatchingValue_ShouldReturnFalse(string? expectedValue, string? value)
        {
            var expression = ((LambdaExpression)(() => Arg.Is(expectedValue, EqualityComparer<string?>.Default))).Body;

            var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

            argumentMatcher.Matches(value, typeof(string)).ShouldBeFalse();
        }

        [Fact]
        public void WhenCalledWithArgIsMatcherWithExpression_ShouldReturnInitializedArgumentMatcher()
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
        public void WhenCalledWithArgIsMatcherWithExpression_AndWhenMatcherCalledWithMatchingValue_ShouldReturnTrue(string? value)
        {
            var expression = ((LambdaExpression)(() => Arg.Is<string>(v => v == value))).Body;

            var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

            argumentMatcher.Matches(value, typeof(string)).ShouldBeTrue();
        }

        [Theory]
        [InlineData(null, "non-null")]
        [InlineData("not-null", null)]
        [InlineData("foo", "bar")]
        public void WhenCalledWithArgIsMatcherWithExpression_AndWhenMatcherCalledWithNonMatchingValue_ShouldReturnFalse(string? expectedValue, string? value)
        {
            var expression = ((LambdaExpression)(() => Arg.Is<string>(v => v == expectedValue))).Body;

            var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

            argumentMatcher.Matches(value, typeof(string)).ShouldBeFalse();
        }

        #endregion

        #region Arg.In<TValue>

        [Fact]
        public void WhenCalledWithArgInMatcherWithEnumerable_ShouldReturnInitializedMatcher()
        {
            List<object> values = [];
            var expression = ((LambdaExpression)(() => Arg.In<object>(values))).Body;

            var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

            argumentMatcher.ShouldBeOfType<ArgumentMatcher<object>>();
        }

        [Theory]
        [InlineData("foo")]
        [InlineData("bar")]
        [InlineData(null)]
        public void WhenCalledWithArgInMatcherWithEnumerable_AndMatcherCalledWithContainedValue_ShouldReturnTrue(string? value)
        {
            List<string?> values = ["foo", null, "bar"];
            var expression = ((LambdaExpression)(() => Arg.In<string?>(values))).Body;

            var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

            argumentMatcher.Matches(value, typeof(string)).ShouldBeTrue();
        }

        [Theory]
        [InlineData("foo")]
        [InlineData("bar")]
        [InlineData(null)]
        public void WhenCalledWithArgInMatcherWithEnumerable_AndMatcherCalledWithNonContainedValue_ShouldReturnFalse(string? value)
        {
            List<string?> values = ["baz"];
            var expression = ((LambdaExpression)(() => Arg.In<string?>(values))).Body;

            var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

            argumentMatcher.Matches(value, typeof(string)).ShouldBeFalse();
        }

        [Fact]
        public void WhenCalledWithArgInMatcherWithEnumerableAndComparer_ShouldReturnInitializedMatcher()
        {
            List<object> values = [];
            var expression = ((LambdaExpression)(() => Arg.In(values, EqualityComparer<object>.Default))).Body;

            var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

            argumentMatcher.ShouldBeOfType<ArgumentMatcher<object>>();
        }

        [Theory]
        [InlineData("foo")]
        [InlineData("bar")]
        [InlineData(null)]
        public void WhenCalledWithArgInMatcherWithEnumerableAndComparer_AndMatcherCalledWithContainedValue_ShouldReturnTrue(string? value)
        {
            List<string?> values = ["foo", null, "bar"];
            var expression = ((LambdaExpression)(() => Arg.In(values, EqualityComparer<string?>.Default))).Body;

            var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

            argumentMatcher.Matches(value, typeof(string)).ShouldBeTrue();
        }

        [Theory]
        [InlineData("foo")]
        [InlineData("bar")]
        [InlineData(null)]
        public void WhenCalledWithArgInMatcherWithEnumerableAndComparer_AndMatcherCalledWithNonContainedValue_ShouldReturnFalse(string? value)
        {
            List<string?> values = ["baz"];
            var expression = ((LambdaExpression)(() => Arg.In(values, EqualityComparer<string?>.Default))).Body;

            var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

            argumentMatcher.Matches(value, typeof(string)).ShouldBeFalse();
        }

        [Fact]
        public void WhenCalledWithArgInMatcherWithParams_ShouldReturnInitializedMatcher()
        {
            var expression = ((LambdaExpression)(() => Arg.In<object>())).Body;

            var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

            argumentMatcher.ShouldBeOfType<ArgumentMatcher<object>>();
        }

        [Theory]
        [InlineData("foo")]
        [InlineData("bar")]
        [InlineData(null)]
        public void WhenCalledWithArgInMatcherWithParams_AndMatcherCalledWithContainedValue_ShouldReturnTrue(string? value)
        {
            var expression = ((LambdaExpression)(() => Arg.In("foo", null, "bar"))).Body;

            var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

            argumentMatcher.Matches(value, typeof(string)).ShouldBeTrue();
        }

        [Theory]
        [InlineData("foo")]
        [InlineData("bar")]
        [InlineData(null)]
        public void WhenCalledWithArgInMatcherWithParams_AndMatcherCalledWithNonContainedValue_ShouldReturnFalse(string? value)
        {
            var expression = ((LambdaExpression)(() => Arg.In<string?>("baz"))).Body;

            var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

            argumentMatcher.Matches(value, typeof(string)).ShouldBeFalse();
        }

        #endregion

        #region Arg.NotIn<TValue>

        [Fact]
        public void WhenCalledWithArgNotInMatcherWithEnumerable_ShouldReturnInitializedMatcher()
        {
            List<object> values = [];
            var expression = ((LambdaExpression)(() => Arg.NotIn<object>(values))).Body;

            var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

            argumentMatcher.ShouldBeOfType<ArgumentMatcher<object>>();
        }

        [Theory]
        [InlineData("foo")]
        [InlineData("bar")]
        [InlineData(null)]
        public void WhenCalledWithArgNotInMatcherWithEnumerable_AndMatcherCalledWithNonContainedValue_ShouldReturnTrue(string? value)
        {
            List<string?> values = ["baz"];
            var expression = ((LambdaExpression)(() => Arg.NotIn<string?>(values))).Body;

            var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

            argumentMatcher.Matches(value, typeof(string)).ShouldBeTrue();
        }

        [Theory]
        [InlineData("foo")]
        [InlineData("bar")]
        [InlineData(null)]
        public void WhenCalledWithArgNotInMatcherWithEnumerable_AndMatcherCalledWithContainedValue_ShouldReturnFalse(string? value)
        {
            List<string?> values = [null, "foo", "bar"];
            var expression = ((LambdaExpression)(() => Arg.NotIn<string?>(values))).Body;

            var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

            argumentMatcher.Matches(value, typeof(string)).ShouldBeFalse();
        }

        [Fact]
        public void WhenCalledWithArgNotInMatcherWithEnumerableAndComparer_ShouldReturnInitializedMatcher()
        {
            List<object> values = [];
            var expression = ((LambdaExpression)(() => Arg.NotIn(values, EqualityComparer<object>.Default))).Body;

            var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

            argumentMatcher.ShouldBeOfType<ArgumentMatcher<object>>();
        }

        [Theory]
        [InlineData("foo")]
        [InlineData("bar")]
        [InlineData(null)]
        public void WhenCalledWithArgNotInMatcherWithEnumerableAndComparer_AndMatcherCalledWithNonContainedValue_ShouldReturnTrue(string? value)
        {
            List<string?> values = ["baz"];
            var expression = ((LambdaExpression)(() => Arg.NotIn(values, EqualityComparer<string?>.Default))).Body;

            var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

            argumentMatcher.Matches(value, typeof(string)).ShouldBeTrue();
        }

        [Theory]
        [InlineData("foo")]
        [InlineData("bar")]
        [InlineData(null)]
        public void WhenCalledWithArgNotInMatcherWithEnumerableAndComparer_AndMatcherCalledWithContainedValue_ShouldReturnFalse(string? value)
        {
            List<string?> values = [null, "foo", "bar"];
            var expression = ((LambdaExpression)(() => Arg.NotIn(values, EqualityComparer<string?>.Default))).Body;

            var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

            argumentMatcher.Matches(value, typeof(string)).ShouldBeFalse();
        }

        [Fact]
        public void WhenCalledWithArgNotInMatcherWithParams_ShouldReturnInitializedMatcher()
        {
            var expression = ((LambdaExpression)(() => Arg.NotIn<object>())).Body;

            var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

            argumentMatcher.ShouldBeOfType<ArgumentMatcher<object>>();
        }

        [Theory]
        [InlineData("foo")]
        [InlineData("bar")]
        [InlineData(null)]
        public void WhenCalledWithArgNotInMatcherWithParams_AndMatcherCalledWithNonContainedValue_ShouldReturnTrue(string? value)
        {
            var expression = ((LambdaExpression)(() => Arg.NotIn("baz"))).Body;

            var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

            argumentMatcher.Matches(value, typeof(string)).ShouldBeTrue();
        }

        [Theory]
        [InlineData("foo")]
        [InlineData("bar")]
        [InlineData(null)]
        public void WhenCalledWithArgNotInMatcherWithParams_AndMatcherCalledWithContainedValue_ShouldReturnFalse(string? value)
        {
            var expression = ((LambdaExpression)(() => Arg.NotIn(null, "foo", "bar"))).Body;

            var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

            argumentMatcher.Matches(value, typeof(string)).ShouldBeFalse();
        }

        #endregion

        private static string GetArgumentValue() => "value";
        private static string ArgumentValue { get; } = "value";

        [Fact]
        public void WhenCalledWithMethodThatIsNotAnArgumentMatcher_ShouldReturnInitializedLazyEvaluatedArgumentMatcher()
        {
            var expression = ((LambdaExpression)(() => GetArgumentValue())).Body;

            var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

            argumentMatcher.ShouldBeOfType<LazyEvaluatedArgumentMatcher>();
        }

        [Fact]
        public void WhenCalledWithMethodThatIsNotAnArgumentMatcher_AndMatcherCalledWithMatchingValue_ShouldReturnTrue()
        {
            var expression = ((LambdaExpression)(() => GetArgumentValue())).Body;

            var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

            argumentMatcher.Matches("value", typeof(string)).ShouldBeTrue();
        }

        [Fact]
        public void WhenCalledWithMethodThatIsNotAnArgumentMatcher_AndMatcherCalledWithNonMatchingValue_ShouldReturnFalse()
        {
            var expression = ((LambdaExpression)(() => GetArgumentValue())).Body;

            var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

            argumentMatcher.Matches("wrong", typeof(string)).ShouldBeFalse();
        }

        [Fact]
        public void WhenCalledWithGetterMethod_ShouldReturnInitializedConstantArgumentMatcher()
        {
            var expression = ((LambdaExpression)(() => ArgumentValue)).Body;

            var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

            argumentMatcher.ShouldBeOfType<ConstantArgumentMatcher>();
        }

        [Fact]
        public void WhenCalledWithGetterMethod_AndMatcherCalledWithMatchingValue_ShouldReturnTrue()
        {
            var expression = ((LambdaExpression)(() => ArgumentValue)).Body;

            var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

            argumentMatcher.Matches("value", typeof(string)).ShouldBeTrue();
        }

        [Fact]
        public void WhenCalledWithGetterMethod_AndMatcherCalledWithNonMatchingValue_ShouldReturnFalse()
        {
            var expression = ((LambdaExpression)(() => ArgumentValue)).Body;

            var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

            argumentMatcher.Matches("wrong", typeof(string)).ShouldBeFalse();
        }

        [Fact]
        public void WhenCalledWithConstant_ShouldReturnInitializedConstantArgumentMatcher()
        {
            var expression = Expression.Constant(123);

            var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

            argumentMatcher.ShouldBeOfType<ConstantArgumentMatcher>();
        }

        [Fact]
        public void WhenCalledWithConstant_AndMatcherCalledWithMatchingValue_ShouldReturnTrue()
        {
            var expression = Expression.Constant(123);

            var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

            argumentMatcher.Matches(123, typeof(int)).ShouldBeTrue();
        }

        [Fact]
        public void WhenCalledWithConstant_AndMatcherCalledWithNonMatchingValue_ShouldReturnFalse()
        {
            var expression = Expression.Constant(123);

            var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

            argumentMatcher.Matches(321, typeof(int)).ShouldBeFalse();
        }

        [Fact]
        public void WhenCalledWithConvertedConstant_ShouldReturnInitializedConstantArgumentMatcher()
        {
            var expression = Expression.Convert(Expression.Constant(123d), typeof(int));

            var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

            argumentMatcher.ShouldBeOfType<ConstantArgumentMatcher>();
        }

        [Fact]
        public void WhenCalledWithConvertedConstant_AndMatcherCalledWithMatchingValue_ShouldReturnTrue()
        {
            var expression = Expression.Convert(Expression.Constant(123d), typeof(int));

            var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

            argumentMatcher.Matches(123, typeof(int)).ShouldBeTrue();
        }

        [Fact]
        public void WhenCalledWithConvertedConstant_AndMatcherCalledWithNonMatchingValue_ShouldReturnFalse()
        {
            var expression = Expression.Convert(Expression.Constant(123d), typeof(int));

            var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

            argumentMatcher.Matches(321, typeof(int)).ShouldBeFalse();
        }

        [Fact]
        public void WhenCalledWithQuote_ShouldReturnInitializedExpressionArgumentMatcher()
        {
            var i = Expression.Parameter(typeof(int), "i");
            var expression = Expression.Quote(Expression.Lambda(Expression.Equal(Expression.Constant(1), i), i));

            var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

            argumentMatcher.ShouldBeOfType<ExpressionArgumentMatcher>();
        }

        [Fact]
        public void WhenCalledWithArgumentMatcherExpression_ShouldReturnArgumentMatcherFromExpression()
        {
            var expectedArgumentMatcher = new ArgumentMatcher<string>(_ => true);
            var expression = new ArgumentMatcherExpression(expectedArgumentMatcher);
            var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression);

            argumentMatcher.ShouldBeSameAs(expectedArgumentMatcher);
        }

        // TODO: Additional tests for ArgumentMatcherInitializer.Initialize(expression) covering; Member/Index
    }
}
