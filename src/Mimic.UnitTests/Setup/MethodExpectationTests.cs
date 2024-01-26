using System.Linq.Expressions;
using Mimic.Setup;

namespace Mimic.UnitTests.Setup;

public class MethodExpectationTests
{
    [Fact]
    public void Constructor_WhenArgumentsIsNull_ShouldSuccessfullyConstruct()
    {
        var method = typeof(ISubject).GetMethod(nameof(ISubject.MethodWithNoArguments))!;
        LambdaExpression expression = (ISubject subject) => subject.MethodWithNoArguments();
        var arguments = ((MethodCallExpression)expression.Body).Arguments;

        var expectation = new MethodExpectation(expression, method, arguments, true);

        expectation.Expression.ShouldBeSameAs(expression);
        expectation.MethodInfo.ShouldBeSameAs(method);
        expectation.Arguments.ShouldNotBeNull();
        expectation.Arguments.Length.ShouldBe(0);
    }

    [Fact]
    public void Constructor_WhenArgumentsProvidedWithSkipMatchersTrue_ShouldSuccessfullyConstruct()
    {
        var method = typeof(ISubject).GetMethod(nameof(ISubject.MethodWithArguments))!;
        LambdaExpression expression = (ISubject subject) => subject.MethodWithArguments(150, Arg.Is<string>(s => s == "test"));
        var arguments = ((MethodCallExpression)expression.Body).Arguments;

        var expectation = new MethodExpectation(expression, method, arguments, true);

        expectation.Expression.ShouldBeSameAs(expression);
        expectation.MethodInfo.ShouldBeSameAs(method);
        expectation.Arguments.ShouldNotBeNull();
        expectation.Arguments.Length.ShouldBe(2);
        expectation.Arguments[0].ShouldBeSameAs(arguments[0]);
        expectation.Arguments[1].ShouldBeSameAs(arguments[1]);
    }

    [Fact]
    public void Constructor_WhenArgumentsProvidedWithSkipMatchersFalse_ShouldSuccessfullyConstruct()
    {
        var method = typeof(ISubject).GetMethod(nameof(ISubject.MethodWithArguments))!;
        LambdaExpression expression = (ISubject subject) => subject.MethodWithArguments(150, Arg.Is<string>(s => s == "test"));
        var arguments = ((MethodCallExpression)expression.Body).Arguments;

        var expectation = new MethodExpectation(expression, method, arguments);

        expectation.Expression.ShouldBeSameAs(expression);
        expectation.MethodInfo.ShouldBeSameAs(method);
        expectation.Arguments.ShouldNotBeNull();
        expectation.Arguments.Length.ShouldBe(2);
        expectation.Arguments[0].ShouldBeSameAs(arguments[0]);
        expectation.Arguments[1].ShouldBeSameAs(arguments[1]);
    }

    [Fact]
    public void MatchesInvocation_WhenCalledWithNonMatchingNonGenericMethod_ShouldReturnFalse()
    {
        var expectation = ConstructMethodExpectation(s => s.MethodWithNoArguments());

        var invokedMethod = typeof(ISubject).GetMethod(nameof(ISubject.MethodWithArguments))!;
        var invocation = new InvocationFixture(typeof(Subject), invokedMethod);
        expectation.MatchesInvocation(invocation).ShouldBeFalse();
    }

    [Fact]
    public void MatchesInvocation_WhenCalledWithMatchingNonGenericMethod_WithNoParameters_ShouldReturnTrue()
    {
        var expectation = ConstructMethodExpectation(s => s.MethodWithNoArguments());

        var invokedMethod = typeof(ISubject).GetMethod(nameof(ISubject.MethodWithNoArguments))!;
        var invocation = new InvocationFixture(typeof(ISubject), invokedMethod);
        expectation.MatchesInvocation(invocation).ShouldBeTrue();
    }

    [Theory]
    [AutoData]
    public void MatchesInvocation_WhenCalledWithMatchingNonGenericMethod_WithMatchingParameters_ShouldReturnTrue(
        int intValue, string stringValue)
    {
        var expectation = ConstructMethodExpectation(s => s.MethodWithArguments(intValue, Arg.Is<string>(v => v == stringValue)));

        var invokedMethod = typeof(ISubject).GetMethod(nameof(ISubject.MethodWithArguments))!;
        var invocation = new InvocationFixture(typeof(ISubject), invokedMethod, [intValue, stringValue]);
        expectation.MatchesInvocation(invocation).ShouldBeTrue();
    }

    [Theory]
    [AutoData]
    public void MatchesInvocation_WhenCalledWithMatchingNonGenericMethod_WithPartiallyMatchingParameters_ShouldReturnFalse(
        int intValue, string stringValue, int actualIntValue)
    {
        var expectation = ConstructMethodExpectation(s => s.MethodWithArguments(intValue, Arg.Is<string>(v => v == stringValue)));

        var invokedMethod = typeof(ISubject).GetMethod(nameof(ISubject.MethodWithArguments))!;
        var invocation = new InvocationFixture(typeof(ISubject), invokedMethod, [actualIntValue, stringValue]);
        expectation.MatchesInvocation(invocation).ShouldBeFalse();
    }

    [Theory]
    [AutoData]
    public void MatchesInvocation_WhenCalledWithMatchingNonGenericMethod_WithNoMatchingParameters_ShouldReturnFalse(
        int intValue, string stringValue, int actualIntValue, string actualStringValue)
    {
        var expectation = ConstructMethodExpectation(s => s.MethodWithArguments(intValue, Arg.Is<string>(v => v == stringValue)));

        var invokedMethod = typeof(ISubject).GetMethod(nameof(ISubject.MethodWithArguments))!;
        var invocation = new InvocationFixture(typeof(ISubject), invokedMethod, [actualIntValue, actualStringValue]);
        expectation.MatchesInvocation(invocation).ShouldBeFalse();
    }

    [Fact]
    public void MatchesInvocation_WhenCalledWithNonMatchingGenericMethod_ShouldReturnFalse()
    {
        var expectation = ConstructMethodExpectation(s => s.GenericWithNoArguments<Generic.AnyType>());

        var invokedMethod = typeof(ISubject).GetMethod(nameof(ISubject.GenericWithArguments))!.MakeGenericMethod([typeof(int)]);
        var invocation = new InvocationFixture(typeof(Subject), invokedMethod);
        expectation.MatchesInvocation(invocation).ShouldBeFalse();
    }

    [Fact]
    public void MatchesInvocation_WhenCalledWithMatchingGenericMethod_WithSpecificTypeAndNoParameters_ShouldReturnTrue()
    {
        var expectation = ConstructMethodExpectation(s => s.GenericWithNoArguments<int>());

        var invokedMethod = typeof(ISubject).GetMethod(nameof(ISubject.GenericWithNoArguments))!.MakeGenericMethod([typeof(int)]);
        var invocation = new InvocationFixture(typeof(ISubject), invokedMethod);
        expectation.MatchesInvocation(invocation).ShouldBeTrue();
    }

    [Fact]
    public void MatchesInvocation_WhenCalledWithMatchingGenericMethod_WithAnyTypeAndNoParameters_ShouldReturnTrue()
    {
        var expectation = ConstructMethodExpectation(s => s.GenericWithNoArguments<Generic.AnyType>());

        var invokedMethod = typeof(ISubject).GetMethod(nameof(ISubject.GenericWithNoArguments))!.MakeGenericMethod([typeof(int)]);
        var invocation = new InvocationFixture(typeof(Subject), invokedMethod);
        expectation.MatchesInvocation(invocation).ShouldBeTrue();
    }

    [Fact]
    public void MatchesInvocation_WhenCalledWithMatchingGenericMethod_WithAnyReferenceTypeButValueType_ShouldReturnFalse()
    {
        var expectation = ConstructMethodExpectation(s => s.GenericWithNoArguments<Generic.AnyReferenceType>());

        var invokedMethod = typeof(ISubject).GetMethod(nameof(ISubject.GenericWithNoArguments))!.MakeGenericMethod([typeof(int)]);
        var invocation = new InvocationFixture(typeof(Subject), invokedMethod);
        expectation.MatchesInvocation(invocation).ShouldBeFalse();
    }

    [Theory]
    [AutoData]
    public void MatchesInvocation_WhenCalledWithMatchingGenericMethod_WithSpecificTypeAndMatchingParameters_ShouldReturnTrue(
        int intValue, double doubleValue)
    {
        var expectation = ConstructMethodExpectation(s => s.GenericWithArguments(intValue, doubleValue));

        var invokedMethod = typeof(ISubject).GetMethod(nameof(ISubject.GenericWithArguments))!.MakeGenericMethod([typeof(int)]);
        var invocation = new InvocationFixture(typeof(ISubject), invokedMethod, [intValue, doubleValue]);
        expectation.MatchesInvocation(invocation).ShouldBeTrue();
    }

    [Theory(Skip = "This currently is not working, needs a fix in the argument matchers to properly support `Generic.AnyType`")]
    [AutoData]
    public void MatchesInvocation_WhenCalledWithMatchingGenericMethod_WithAnyTypeAndMatchingParameters_ShouldReturnTrue(
        int intValue, double doubleValue)
    {
        var expectation = ConstructMethodExpectation(s => s.GenericWithArguments(Arg.Any<Generic.AnyType>(), doubleValue));

        var invokedMethod = typeof(ISubject).GetMethod(nameof(ISubject.GenericWithArguments))!.MakeGenericMethod([typeof(int)]);
        var invocation = new InvocationFixture(typeof(Subject), invokedMethod, [intValue, doubleValue]);
        expectation.MatchesInvocation(invocation).ShouldBeTrue();
    }

    [Theory]
    [AutoData]
    public void MatchesInvocation_WhenCalledWithMatchingGenericMethod_WithAnyValueTypeAndMatchingParametersButReferenceType_ShouldReturnFalse(
        string stringValue, double doubleValue)
    {
        var expectation = ConstructMethodExpectation(s => s.GenericWithArguments(Arg.Any<Generic.AnyValueType>(), doubleValue));

        var invokedMethod = typeof(ISubject).GetMethod(nameof(ISubject.GenericWithArguments))!.MakeGenericMethod([typeof(string)]);
        var invocation = new InvocationFixture(typeof(Subject), invokedMethod, [stringValue, doubleValue]);
        expectation.MatchesInvocation(invocation).ShouldBeFalse();
    }

    private static MethodExpectation ConstructMethodExpectation(Expression<Action<ISubject>> expression)
    {
        var methodCallExpression = (MethodCallExpression)expression.Body;
        return new MethodExpectation(expression, methodCallExpression.Method, methodCallExpression.Arguments);
    }

    private interface ISubject
    {
        public void MethodWithNoArguments();

        public void MethodWithArguments(int intValue, string stringValue);

        public void GenericWithNoArguments<T>();

        public void GenericWithArguments<T>(T value, double doubleValue);
    }

    private class Subject : ISubject
    {
        public void MethodWithNoArguments() => throw new NotImplementedException();

        public void MethodWithArguments(int intValue, string stringValue) => throw new NotImplementedException();

        public void GenericWithNoArguments<T>() => throw new NotImplementedException();

        public void GenericWithArguments<T>(T value, double doubleValue) => throw new NotImplementedException();
    }
}
