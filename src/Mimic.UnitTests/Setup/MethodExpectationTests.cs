using System.Linq.Expressions;
using Mimic.Exceptions;
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
    public void Constructor_WithNonOverridableStaticMethod_ShouldThrowUnsupportedExpressionException()
    {
        var method = typeof(Subject).GetMethod(nameof(Subject.StaticMethod))!;
        LambdaExpression expression = (Subject _) => Subject.StaticMethod();
        var arguments = ((MethodCallExpression)expression.Body).Arguments;

        var ex = Should.Throw<MimicException>(() => new MethodExpectation(expression, method, arguments, true));
        ex.Message.ShouldContain("is unsupported as a static method is not overridable");
    }

    [Fact]
    public void Constructor_WithNonOverridableExtensionMethod_ShouldThrowUnsupportedExpressionException()
    {
        var method = typeof(SubjectExtensions).GetMethod(nameof(SubjectExtensions.ExtensionMethod))!;
        LambdaExpression expression = (ISubject subject) => subject.ExtensionMethod();
        var arguments = ((MethodCallExpression)expression.Body).Arguments;

        var ex = Should.Throw<MimicException>(() => new MethodExpectation(expression, method, arguments, true));
        ex.Message.ShouldContain("is unsupported as an extension method is not overridable");
    }

    [Fact]
    public void Constructor_WithNonVirtualMethod_ShouldThrowUnsupportedExpressionException()
    {
        var method = typeof(AbstractSubject).GetMethod(nameof(AbstractSubject.RegularMethod))!;
        LambdaExpression expression = (AbstractSubject subject) => subject.RegularMethod();
        var arguments = ((MethodCallExpression)expression.Body).Arguments;

        var ex = Should.Throw<MimicException>(() => new MethodExpectation(expression, method, arguments, true));
        ex.Message.ShouldContain("is unsupported as the specified method is not overridable");
    }

    [Fact]
    public void Constructor_WithSealedVirtualMethod_ShouldThrowUnsupportedExpressionException()
    {
        var method = typeof(ConcreteSubject).GetMethod(nameof(ConcreteSubject.VirtualMethod))!;
        LambdaExpression expression = (ConcreteSubject subject) => subject.VirtualMethod();
        var arguments = ((MethodCallExpression)expression.Body).Arguments;

        var ex = Should.Throw<MimicException>(() => new MethodExpectation(expression, method, arguments, true));
        ex.Message.ShouldContain("is unsupported as the specified method is not overridable");
    }

    [Fact]
    public void Constructor_WithInaccessibleMethod_ShouldThrowMimicException()
    {
        var method = typeof(NonAccessibleSubject).GetMethod(nameof(NonAccessibleSubject.NonAccessibleMethod))!;
        LambdaExpression expression = (NonAccessibleSubject subject) => subject.NonAccessibleMethod();
        var arguments = ((MethodCallExpression)expression.Body).Arguments;

        var ex = Should.Throw<MimicException>(() => new MethodExpectation(expression, method, arguments, true));
        ex.Message.ShouldStartWith("Method NonAccessibleMethod in type MethodExpectationTests.NonAccessibleSubject cannot be setup because it is not accessible by our proxy generator (Castle.DynamicProxy). Message returned from proxy generator: ");
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

    [Theory]
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

    [Theory]
    [AutoData]
    public void Equals_WhenCalledWithMatchingObject_ShouldReturnTrue(int iValue)
    {
        var expectationOne = ConstructMethodExpectation(s => s.MethodWithArguments(iValue, Arg.Any<string>()));
        var expectationTwo = ConstructMethodExpectation(s => s.MethodWithArguments(iValue, Arg.Any<string>()));

        expectationOne.Equals(expectationTwo).ShouldBeTrue();
    }

    [Fact]
    public void Equals_WhenCalledWithWrongObjectType_ShouldReturnFalse()
    {
        var expectation = ConstructMethodExpectation(s => s.MethodWithArguments(Arg.Any<int>(), Arg.Any<string>()));

        // ReSharper disable once SuspiciousTypeConversion.Global
        expectation.Equals("obviously wrong type").ShouldBeFalse();
    }

    [Fact]
    public void Equals_WhenCalledWithWrongExpectationType_ShouldReturnFalse()
    {
        var expectation = ConstructMethodExpectation(s => s.MethodWithArguments(Arg.Any<int>(), Arg.Any<string>()));
        var allPropertiesStubSetupExpectation = new AllPropertiesStubSetup(new Mimic<ISubject>()).Expectation;

        expectation.Equals(allPropertiesStubSetupExpectation).ShouldBeFalse();
    }

    [Fact]
    public void Equals_WhenExpectationsHaveDifferentMethods_ShouldReturnFalse()
    {
        var expectationOne = ConstructMethodExpectation(s => s.MethodWithArguments(Arg.Any<int>(), Arg.Any<string>()));
        var expectationTwo = ConstructMethodExpectation(s => s.MethodWithNoArguments());

        expectationOne.Equals(expectationTwo).ShouldBeFalse();
    }

    [Theory]
    [AutoData]
    public void Equals_WhenExpectationsHaveDifferentArgumentValues_ShouldReturnFalse(int iValue)
    {
        var expectationOne = ConstructMethodExpectation(s => s.MethodWithArguments(Arg.Any<int>(), Arg.Any<string>()));
        var expectationTwo = ConstructMethodExpectation(s => s.MethodWithArguments(iValue, Arg.Any<string>()));

        expectationOne.Equals(expectationTwo).ShouldBeFalse();
    }

    [Fact]
    public void GetHashCode_WhenExpectationsHaveSameMethods_ShouldReturnTrue()
    {
        var expectationOne = ConstructMethodExpectation(s => s.MethodWithArguments(Arg.Any<int>(), Arg.Any<string>()));
        var expectationTwo = ConstructMethodExpectation(s => s.MethodWithArguments(Arg.Any<int>(), Arg.Any<string>()));

        expectationOne.GetHashCode().ShouldBe(expectationTwo.GetHashCode());
    }

    [Fact]
    public void GetHashCode_WhenExpectationsHaveDifferentMethods_ShouldReturnFalse()
    {
        var expectationOne = ConstructMethodExpectation(s => s.MethodWithArguments(Arg.Any<int>(), Arg.Any<string>()));
        var expectationTwo = ConstructMethodExpectation(s => s.MethodWithNoArguments());

        expectationOne.GetHashCode().ShouldNotBe(expectationTwo.GetHashCode());
    }

    internal static MethodExpectation ConstructMethodExpectation(Expression<Action<ISubject>> expression)
    {
        var methodCallExpression = (MethodCallExpression)expression.Body;
        return new MethodExpectation(expression, methodCallExpression.Method, methodCallExpression.Arguments);
    }

    // ReSharper disable once MemberCanBePrivate.Global
    internal interface ISubject
    {
        public void MethodWithNoArguments();

        public void MethodWithArguments(int intValue, string stringValue);

        public void GenericWithNoArguments<T>();

        public void GenericWithArguments<T>(T value, double doubleValue);
    }

    // ReSharper disable once MemberCanBePrivate.Global
    internal class Subject : ISubject
    {
        public static void StaticMethod() => throw new NotSupportedException();

        public void MethodWithNoArguments() => throw new NotSupportedException();

        public void MethodWithArguments(int intValue, string stringValue) => throw new NotSupportedException();

        public void GenericWithNoArguments<T>() => throw new NotSupportedException();

        public void GenericWithArguments<T>(T value, double doubleValue) => throw new NotSupportedException();
    }

    // ReSharper disable once MemberCanBePrivate.Global
    internal abstract class AbstractSubject
    {
        public abstract void AbstractMethod();

        public virtual void VirtualMethod()
        {
        }

        public void RegularMethod()
        {
        }
    }

    // ReSharper disable once MemberCanBePrivate.Global
    internal class ConcreteSubject : AbstractSubject
    {
        public override void AbstractMethod()
        {
        }

        public sealed override void VirtualMethod()
        {
        }
    }

    private abstract class NonAccessibleSubject
    {
        public virtual void NonAccessibleMethod()
        {
        }
    }
}

file static class SubjectExtensions
{
    public static void ExtensionMethod(this MethodExpectationTests.ISubject subject) => subject.MethodWithNoArguments();
}
