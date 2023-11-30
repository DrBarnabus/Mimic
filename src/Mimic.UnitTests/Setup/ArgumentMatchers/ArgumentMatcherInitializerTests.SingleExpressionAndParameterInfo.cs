using System.Linq.Expressions;
using System.Reflection;
using Mimic.Exceptions;
using Mimic.Setup.ArgumentMatchers;

namespace Mimic.UnitTests.Setup.ArgumentMatchers;

public static partial class ArgumentMatcherInitializerTests
{
    public class SingleExpressionAndParameterInfo
    {
        #region Setup

        private struct A;

        private struct B
        {
            public static implicit operator B(A a) => new();
        }

        private static void MethodWithByRef(ref int refValue, in int inValue, out int outValue)
        {
            outValue = default;
        }

        private static int[] MethodWithParamArray(params int[] values) => values;

        private static void MethodWithB(B b) {}

        private MethodInfo MethodWithByRefMethodInfo => GetType().GetMethod(nameof(MethodWithByRef), BindingFlags.NonPublic | BindingFlags.Static)!;
        private MethodInfo MethodWithParamArrayMethodInfo => GetType().GetMethod(nameof(MethodWithParamArray), BindingFlags.NonPublic | BindingFlags.Static)!;
        private MethodInfo MethodWithBMethodInfo => GetType().GetMethod(nameof(MethodWithB), BindingFlags.NonPublic | BindingFlags.Static)!;

        #endregion

        [Fact]
        public void WhenCalledWithOutParameter_ShouldReturnInitializedAnyArgumentMatcher()
        {
            var parameter = MethodWithByRefMethodInfo.GetParameters()[2];

            int outValue = 0;
            var expression = ((LambdaExpression)(() => outValue)).Body;

            var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression, parameter);

            argumentMatcher.ShouldBeOfType<AnyArgumentMatcher>();
            argumentMatcher.ShouldBeSameAs(AnyArgumentMatcher.Instance);
        }

        [Fact]
        public void WhenCalledWithInParameter_AndIsArgRef_ShouldReturnInitializedAnyArgumentMatcher()
        {
            var parameter = MethodWithByRefMethodInfo.GetParameters()[0];
            var expression = ((LambdaExpression)(() => Arg.Ref<int>.Any)).Body;

            var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression, parameter);

            argumentMatcher.ShouldBeOfType<AnyArgumentMatcher>();
            argumentMatcher.ShouldBeSameAs(AnyArgumentMatcher.Instance);
        }

        [Fact]
        public void WhenCalledWithRefParameter_AndIsArgRef_ShouldReturnInitializedAnyArgumentMatcher()
        {
            var parameter = MethodWithByRefMethodInfo.GetParameters()[1];
            var expression = ((LambdaExpression)(() => Arg.Ref<int>.Any)).Body;

            var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression, parameter);

            argumentMatcher.ShouldBeOfType<AnyArgumentMatcher>();
            argumentMatcher.ShouldBeSameAs(AnyArgumentMatcher.Instance);
        }

        [Fact]
        public void WhenCalledWithInParameter_AndIsConstantExpression_ShouldReturnInitializedAnyArgumentMatcher()
        {
            var parameter = MethodWithByRefMethodInfo.GetParameters()[0];
            var expression = Expression.Constant(123);

            var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression, parameter);

            argumentMatcher.ShouldBeOfType<RefArgumentMatcher>();
        }

        [Fact]
        public void WhenCalledWithRefParameter_AndIsConstantExpression_ShouldReturnInitializedAnyArgumentMatcher()
        {
            var parameter = MethodWithByRefMethodInfo.GetParameters()[1];
            var expression = Expression.Constant(123);

            var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression, parameter);

            argumentMatcher.ShouldBeOfType<RefArgumentMatcher>();
        }

        [Fact]
        public void WhenCalledWithParamArrayParameter_AndExpressionIsArrayWithElements_ShouldReturnInitializedParamArrayArgumentMatcher()
        {
            var parameter = MethodWithParamArrayMethodInfo.GetParameters()[0];
            var expression = ((LambdaExpression)(() => new[] { 1, 2, 3 })).Body;

            var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression, parameter);

            argumentMatcher.ShouldBeOfType<ParamArrayArgumentMatcher>();
        }

        [Fact]
        public void WhenCalledWithParamArrayParameter_AndExpressionIsEmptyArrayInitializer_ShouldReturnInitializedParamArrayArgumentMatcher()
        {
            var parameter = MethodWithParamArrayMethodInfo.GetParameters()[0];
            var expression = ((LambdaExpression)(() => new string[] {})).Body;

            var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression, parameter);

            argumentMatcher.ShouldBeOfType<ParamArrayArgumentMatcher>();
        }

        [Fact]
        public void WhenCalledWithParamArrayParameter_AndExpressionIsNotArrayInitializer_ShouldReturnInitializedParamArrayArgumentMatcher()
        {
            var parameter = MethodWithParamArrayMethodInfo.GetParameters()[0];
            var expression = ((LambdaExpression)(() => 10)).Body;

            var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression, parameter);

            argumentMatcher.ShouldBeOfType<ConstantArgumentMatcher>();
        }

        [Fact]
        public void WhenCalledWithArgumentMatcher_AndExpressionIsConvert_AndTypeIsNotAssignable_ShouldThrowMimicException()
        {
            var parameter = MethodWithBMethodInfo.GetParameters()[0];
            var expression = Expression.Convert(Expression.Call(typeof(Arg), nameof(Arg.Any), new[] { typeof(A) }), typeof(B));

            var ex = Should.Throw<MimicException>(() => ArgumentMatcherInitializer.Initialize(expression, parameter));
            ex.Message.ShouldBe("ArgumentMatcher for argument 'Any()' is unmatchable. Due to an implicit conversion of the argument from type 'ArgumentMatcherInitializerTests.SingleExpressionAndParameterInfo.A' to type 'ArgumentMatcherInitializerTests.SingleExpressionAndParameterInfo.B' which is an incompatible assignment");
        }

        [Fact]
        public void WhenCalledWithConstant_AndExpressionIsConvert_AndTypeIsNotAssignable_ShouldReturnInitializedConstantArgumentMatcher()
        {
            var parameter = MethodWithBMethodInfo.GetParameters()[0];
            var expression = Expression.Convert(Expression.Constant(123), typeof(long));

            var (argumentMatcher, _) = ArgumentMatcherInitializer.Initialize(expression, parameter);

            argumentMatcher.ShouldBeOfType<ConstantArgumentMatcher>();
        }
    }
}
