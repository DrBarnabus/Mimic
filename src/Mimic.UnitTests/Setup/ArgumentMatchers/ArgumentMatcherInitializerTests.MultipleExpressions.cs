using System.Linq.Expressions;
using System.Reflection;
using Mimic.Core;
using Mimic.Setup.ArgumentMatchers;

namespace Mimic.UnitTests.Setup.ArgumentMatchers;

public static partial class ArgumentMatcherInitializerTests
{
    public class MultipleExpressions
    {
        private static void MethodWithMultipleParameters(int intValue, string stringValue, in byte inValue) {}

        private MethodInfo MethodWithMultipleParametersMethodInfo => GetType().GetMethod(nameof(MethodWithMultipleParameters), BindingFlags.NonPublic | BindingFlags.Static)!;

        [Fact]
        public void WhenArgumentsIsNull_ShouldThrowAssertionException()
        {
            var arguments = (Expression[])null!;
            var parameters = MethodWithMultipleParametersMethodInfo.GetParameters();

            var ex = Should.Throw<Guard.AssertionException>(() => ArgumentMatcherInitializer.Initialize(arguments, parameters));
            ex.Expression.ShouldBe("arguments");
        }

        [Fact]
        public void WhenParametersIsNull_ShouldThrowAssertionException()
        {
            var arguments = Array.Empty<Expression>();
            var parameters = (ParameterInfo[])null!;

            var ex = Should.Throw<Guard.AssertionException>(() => ArgumentMatcherInitializer.Initialize(arguments, parameters));
            ex.Expression.ShouldBe("parameters");
        }

        [Fact]
        public void WhenArgumentsAndParametersHaveDifferentLengths_ArgumentsIsLessThanExpected_ShouldThrowAssertionException()
        {
            var arguments = new Expression[] { Expression.Constant(123) };
            var parameters = MethodWithMultipleParametersMethodInfo.GetParameters();

            var ex = Should.Throw<Guard.AssertionException>(() => ArgumentMatcherInitializer.Initialize(arguments, parameters));
            ex.Expression.ShouldBe("arguments.Length == parameters.Length");
        }

        [Fact]
        public void WhenArgumentsAndParametersHaveDifferentLengths_ParametersIsLessThanGivenArguments_ShouldThrowAssertionException()
        {
            var arguments = new Expression[] { Expression.Constant(123), Expression.Constant("value"), Expression.Constant(321) };
            var parameters = MethodWithMultipleParametersMethodInfo.GetParameters().Take(2).ToArray();

            var ex = Should.Throw<Guard.AssertionException>(() => ArgumentMatcherInitializer.Initialize(arguments, parameters));
            ex.Expression.ShouldBe("arguments.Length == parameters.Length");
        }

        [Fact]
        public void WhenGivenEmptyArgumentsAndParameters_ShouldReturnZeroInitializedArgumentMatchers()
        {
            var arguments = Array.Empty<Expression>();
            var parameters = Array.Empty<ParameterInfo>();

            var (argumentMatchers, argumentExpressions) = ArgumentMatcherInitializer.Initialize(arguments, parameters);

            argumentMatchers.ShouldBeEmpty();
            argumentExpressions.ShouldBeEmpty();
        }

        [Fact]
        public void WhenGivenValidArgumentsAndParameters_ShouldReturnInitializedArgumentMatchers()
        {
            var arguments = new Expression[] { Expression.Constant(123), Expression.Constant("value"), Expression.Constant(321) };
            var parameters = MethodWithMultipleParametersMethodInfo.GetParameters();

            var (argumentMatchers, argumentExpressions) = ArgumentMatcherInitializer.Initialize(arguments, parameters);

            argumentMatchers.Length.ShouldBe(3);
            argumentMatchers[0].ShouldBeOfType<ConstantArgumentMatcher>();
            argumentMatchers[1].ShouldBeOfType<ConstantArgumentMatcher>();
            argumentMatchers[2].ShouldBeOfType<RefArgumentMatcher>();

            argumentExpressions.Length.ShouldBe(3);
        }
    }
}
