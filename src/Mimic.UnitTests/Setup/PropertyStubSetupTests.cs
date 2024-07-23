using System.Linq.Expressions;
using Mimic.Core.Extensions;
using Mimic.Setup;

namespace Mimic.UnitTests.Setup;

public class PropertyStubSetupTests
{
    private readonly Mimic<ISubject> _mimic = new();

    [Fact]
    public void Constructor_ShouldSuccessfullyInitialize()
    {
        LambdaExpression expression = (ISubject subject) => subject.StringProperty;
        var property = typeof(ISubject).GetProperty(nameof(ISubject.StringProperty))!;
        property.CanReadProperty(out var getter, out _);
        property.CanWriteProperty(out var setter, out _);

        var setup = new PropertyStubSetup(_mimic, expression, getter!, setter!, null);

        setup.OriginalExpression.ShouldBeNull();
        setup.Mimic.ShouldBeSameAs(_mimic);
        setup.Expectation.ShouldNotBeNull();
        setup.Expression.ShouldBeSameAs(expression);
        setup.Matched.ShouldBeFalse();
        setup.Overridden.ShouldBeFalse();
        setup.Expected.ShouldBeFalse();
    }

    [Fact]
    public void Constructor_ShouldSuccessfullyInitializeExpectation()
    {
        LambdaExpression expression = (ISubject subject) => subject.StringProperty;
        var property = typeof(ISubject).GetProperty(nameof(ISubject.StringProperty))!;
        property.CanReadProperty(out var getter, out _);
        property.CanWriteProperty(out var setter, out _);

        var setup = new PropertyStubSetup(_mimic, expression, getter!, setter!, null);

        setup.Expectation.ShouldNotBeNull();
        setup.Expectation.Expression.ShouldBeSameAs(expression);
    }

    [Fact]
    public void MatchesInvocation_WhenCalledWithInvocationOfPropertyGetter_ShouldReturnTrue()
    {
        LambdaExpression expression = (ISubject subject) => subject.StringProperty;
        var property = typeof(ISubject).GetProperty(nameof(ISubject.StringProperty))!;
        property.CanReadProperty(out var getter, out _);
        property.CanWriteProperty(out var setter, out _);

        var invocation = new InvocationFixture(typeof(ISubject), getter);

        var setup = new PropertyStubSetup(_mimic, expression, getter!, setter!, null);
        setup.MatchesInvocation(invocation).ShouldBeTrue();
    }

    [Fact]
    public void MatchesInvocation_WhenCalledWithInvocationOfPropertySetter_ShouldReturnTrue()
    {
        LambdaExpression expression = (ISubject subject) => subject.StringProperty;
        var property = typeof(ISubject).GetProperty(nameof(ISubject.StringProperty))!;
        property.CanReadProperty(out var getter, out _);
        property.CanWriteProperty(out var setter, out _);

        var invocation = new InvocationFixture(typeof(ISubject), setter, [string.Empty]);

        var setup = new PropertyStubSetup(_mimic, expression, getter!, setter!, null);
        setup.MatchesInvocation(invocation).ShouldBeTrue();
    }

    [Fact]
    public void MatchesInvocation_WhenCalledWithInvocationOfMethod_ShouldReturnFalse()
    {
        LambdaExpression expression = (ISubject subject) => subject.StringProperty;
        var property = typeof(ISubject).GetProperty(nameof(ISubject.StringProperty))!;
        property.CanReadProperty(out var getter, out _);
        property.CanWriteProperty(out var setter, out _);

        var method = typeof(ISubject).GetMethod(nameof(ISubject.IntMethod))!;
        var invocation = new InvocationFixture(typeof(ISubject), method);

        var setup = new PropertyStubSetup(_mimic, expression, getter!, setter!, null);
        setup.MatchesInvocation(invocation).ShouldBeFalse();
    }

    [Fact]
    public void Execute_ShouldSetMatchedToTrueAndSetMatchingSetupOnInvocation()
    {
        LambdaExpression expression = (ISubject subject) => subject.StringProperty;
        var property = typeof(ISubject).GetProperty(nameof(ISubject.StringProperty))!;
        property.CanReadProperty(out var getter, out _);
        property.CanWriteProperty(out var setter, out _);

        var getInvocation = new InvocationFixture(typeof(ISubject), getter);

        var setup = new PropertyStubSetup(_mimic, expression, getter!, setter!, null);
        setup.Execute(getInvocation);

        setup.Matched.ShouldBeTrue();
        getInvocation.MatchedSetup.ShouldBeSameAs(setup);
    }

    [Theory]
    [AutoData]
    public void Execute_WhenCalledWithGetterAndSetterMethods_ShouldStoreAndRetrieveProvidedValue(string initialValue, string newValue)
    {
        LambdaExpression expression = (ISubject subject) => subject.StringProperty;
        var property = typeof(ISubject).GetProperty(nameof(ISubject.StringProperty))!;
        property.CanReadProperty(out var getter, out _);
        property.CanWriteProperty(out var setter, out _);

        var getInvocation = new InvocationFixture(typeof(ISubject), getter);
        var setInvocation = new InvocationFixture(typeof(ISubject), setter, [newValue]);

        var setup = new PropertyStubSetup(_mimic, expression, getter!, setter!, initialValue);
        setup.Execute(setInvocation);
        setup.Execute(getInvocation);

        getInvocation.ReturnValue.ShouldBe(newValue);
    }

    [Theory]
    [AutoData]
    public void Execute_WhenCalledWithGetterMethodButNoValueStored_ShouldReturnInitialValue(string initialValue)
    {
        LambdaExpression expression = (ISubject subject) => subject.StringProperty;
        var property = typeof(ISubject).GetProperty(nameof(ISubject.StringProperty))!;
        property.CanReadProperty(out var getter, out _);
        property.CanWriteProperty(out var setter, out _);

        var getInvocation = new InvocationFixture(typeof(ISubject), getter);

        var setup = new PropertyStubSetup(_mimic, expression, getter!, setter!, initialValue);
        setup.Execute(getInvocation);

        getInvocation.ReturnValue.ShouldBe(initialValue);
    }

    [Fact]
    public void Override_ShouldSetOverriddenToTrue()
    {
        LambdaExpression expression = (ISubject subject) => subject.StringProperty;
        var property = typeof(ISubject).GetProperty(nameof(ISubject.StringProperty))!;
        property.CanReadProperty(out var getter, out _);
        property.CanWriteProperty(out var setter, out _);

        var setup = new PropertyStubSetup(_mimic, expression, getter!, setter!, null);
        setup.Override();

        setup.Overridden.ShouldBeTrue();
    }

    [Fact]
    public void VerifyMatched_ShouldNotThrow()
    {
        LambdaExpression expression = (ISubject subject) => subject.StringProperty;
        var property = typeof(ISubject).GetProperty(nameof(ISubject.StringProperty))!;
        property.CanReadProperty(out var getter, out _);
        property.CanWriteProperty(out var setter, out _);

        var setup = new PropertyStubSetup(_mimic, expression, getter!, setter!, null);
        Should.NotThrow(() => setup.VerifyMatched(_ => true, []));
    }

    [Fact]
    public void GetNested_WhenCurrentValueIsNotMimicked_ShouldReturnAnEmptyList()
    {
        LambdaExpression expression = (ISubject subject) => subject.StringProperty;
        var property = typeof(ISubject).GetProperty(nameof(ISubject.StringProperty))!;
        property.CanReadProperty(out var getter, out _);
        property.CanWriteProperty(out var setter, out _);

        var setup = new PropertyStubSetup(_mimic, expression, getter!, setter!, string.Empty);

        var nestedMimics = setup.GetNested();
        nestedMimics.ShouldNotBeNull();
        nestedMimics.ShouldBeEmpty();
    }

    [Fact]
    public void GetNested_WhenCurrentValueIsMimicked_ShouldReturnListContainingOnlyTheMimicObject()
    {
        LambdaExpression expression = (ISubject subject) => subject.NestedSubject;
        var property = typeof(ISubject).GetProperty(nameof(ISubject.NestedSubject))!;
        property.CanReadProperty(out var getter, out _);
        property.CanWriteProperty(out var setter, out _);

        var nestedMimic = new Mimic<INestedSubject>();
        var setup = new PropertyStubSetup(_mimic, expression, getter!, setter!, nestedMimic.Object);

        var nestedMimics = setup.GetNested();
        nestedMimics.ShouldNotBeNull();
        nestedMimics.ShouldNotBeEmpty();
        nestedMimics.Count.ShouldBe(1);
        nestedMimics[0].ShouldBeSameAs(nestedMimic);
    }

    [Fact]
    public void ToString_ShouldReturnCorrectStringRepresentationOfSetup()
    {
        LambdaExpression expression = (ISubject subject) => subject.StringProperty;
        var property = typeof(ISubject).GetProperty(nameof(ISubject.StringProperty))!;
        property.CanReadProperty(out var getter, out _);
        property.CanWriteProperty(out var setter, out _);

        var setup = new PropertyStubSetup(_mimic, expression, getter!, setter!, null);
        setup.ToString().ShouldBe("PropertyStubSetupTests.ISubject subject => subject.StringProperty");
    }

    [Fact]
    public void Equals_WhenCalledWithMatchingObject_ShouldReturnTrue()
    {
        var stringProperty = typeof(ISubject).GetProperty(nameof(ISubject.StringProperty))!;
        stringProperty.CanReadProperty(out var stringGetter, out _);
        stringProperty.CanWriteProperty(out var stringSetter, out _);

        var expectationOne = new PropertyStubSetup(_mimic, (ISubject subject) => subject.StringProperty, stringGetter!, stringSetter!, null).Expectation;
        var expectationTwo = new PropertyStubSetup(_mimic, (ISubject subject) => subject.StringProperty, stringGetter!, stringSetter!, null).Expectation;

        expectationOne.Equals(expectationTwo).ShouldBeTrue();
    }

    [Fact]
    public void Equals_WhenCalledWithWrongObjectType_ShouldReturnFalse()
    {
        var stringProperty = typeof(ISubject).GetProperty(nameof(ISubject.StringProperty))!;
        stringProperty.CanReadProperty(out var stringGetter, out _);
        stringProperty.CanWriteProperty(out var stringSetter, out _);

        var expectation = new PropertyStubSetup(_mimic, (ISubject subject) => subject.StringProperty, stringGetter!, stringSetter!, null).Expectation;

        // ReSharper disable once SuspiciousTypeConversion.Global
        expectation.Equals("obviously wrong type").ShouldBeFalse();
    }

    [Fact]
    public void Equals_WhenCalledWithWrongExpectationType_ShouldReturnFalse()
    {
        var stringProperty = typeof(ISubject).GetProperty(nameof(ISubject.StringProperty))!;
        stringProperty.CanReadProperty(out var stringGetter, out _);
        stringProperty.CanWriteProperty(out var stringSetter, out _);

        var propertyStubSetupExpectation = new PropertyStubSetup(_mimic, (ISubject subject) => subject.StringProperty, stringGetter!, stringSetter!, null).Expectation;
        var methodExpectation = MethodExpectationTests.ConstructMethodExpectation(m => m.MethodWithNoArguments());

        propertyStubSetupExpectation.Equals(methodExpectation).ShouldBeFalse();
    }

    [Fact]
    public void Equals_WhenExpectationIsForDifferentProperty_ShouldReturnTrue()
    {
        var stringProperty = typeof(ISubject).GetProperty(nameof(ISubject.StringProperty))!;
        stringProperty.CanReadProperty(out var stringGetter, out _);
        stringProperty.CanWriteProperty(out var stringSetter, out _);

        var doubleProperty = typeof(ISubject).GetProperty(nameof(ISubject.DoubleProperty))!;
        doubleProperty.CanReadProperty(out var doubleGetter, out _);
        doubleProperty.CanWriteProperty(out var doubleSetter, out _);

        var expectationOne = new PropertyStubSetup(_mimic, (ISubject subject) => subject.StringProperty, stringGetter!, stringSetter!, null).Expectation;
        var expectationTwo = new PropertyStubSetup(_mimic, (ISubject subject) => subject.DoubleProperty, doubleGetter!, doubleSetter!, null).Expectation;

        expectationOne.Equals(expectationTwo).ShouldBeFalse();
    }

    [Fact]
    public void GetHashCode_ShouldAlwaysReturnMatchingValues()
    {
        var stringProperty = typeof(ISubject).GetProperty(nameof(ISubject.StringProperty))!;
        stringProperty.CanReadProperty(out var stringGetter, out _);
        stringProperty.CanWriteProperty(out var stringSetter, out _);

        var doubleProperty = typeof(ISubject).GetProperty(nameof(ISubject.DoubleProperty))!;
        doubleProperty.CanReadProperty(out var doubleGetter, out _);
        doubleProperty.CanWriteProperty(out var doubleSetter, out _);

        var expectationOne = new PropertyStubSetup(_mimic, (ISubject subject) => subject.StringProperty, stringGetter!, stringSetter!, null).Expectation;
        var expectationTwo = new PropertyStubSetup(_mimic, (ISubject subject) => subject.DoubleProperty, doubleGetter!, doubleSetter!, null).Expectation;

        expectationOne.GetHashCode().ShouldBe(expectationTwo.GetHashCode());
    }

    private interface ISubject
    {
        public double DoubleProperty { get; set; }

        public string StringProperty { get; set; }

        public int IntMethod();

        public INestedSubject NestedSubject { get; set; }
    }

    // ReSharper disable once MemberCanBePrivate.Global
    internal interface INestedSubject;
}
