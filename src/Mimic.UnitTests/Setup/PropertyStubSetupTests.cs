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
        setup.Overriden.ShouldBeFalse();
        setup.Verifiable.ShouldBeTrue();
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
    public void Override_ShouldSetOverridenToTrue()
    {
        LambdaExpression expression = (ISubject subject) => subject.StringProperty;
        var property = typeof(ISubject).GetProperty(nameof(ISubject.StringProperty))!;
        property.CanReadProperty(out var getter, out _);
        property.CanWriteProperty(out var setter, out _);

        var setup = new PropertyStubSetup(_mimic, expression, getter!, setter!, null);
        setup.Override();

        setup.Overriden.ShouldBeTrue();
    }

    [Fact]
    public void Verify_ShouldNotThrow()
    {
        LambdaExpression expression = (ISubject subject) => subject.StringProperty;
        var property = typeof(ISubject).GetProperty(nameof(ISubject.StringProperty))!;
        property.CanReadProperty(out var getter, out _);
        property.CanWriteProperty(out var setter, out _);

        var setup = new PropertyStubSetup(_mimic, expression, getter!, setter!, null);
        Should.NotThrow(() => setup.Verify());
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

    private interface ISubject
    {
        public double DoubleProperty { get; set; }

        public string StringProperty { get; set; }

        public int IntMethod();
    }
}
