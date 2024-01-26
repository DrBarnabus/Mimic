using Mimic.Setup;

namespace Mimic.UnitTests.Setup;

public class AllPropertiesStubSetupTests
{
    private readonly Mimic<ISubject> _mimic = new();

    [Fact]
    public void Constructor_ShouldSuccessfullyConstruct()
    {
        var setup = new AllPropertiesStubSetup(_mimic);

        setup.OriginalExpression.ShouldBeNull();
        setup.Mimic.ShouldBeSameAs(_mimic);
        setup.Expectation.ShouldNotBeNull();
        setup.Expression.ShouldNotBeNull();
        setup.Matched.ShouldBeFalse();
        setup.Overriden.ShouldBeFalse();
        setup.Verifiable.ShouldBeTrue();
    }

    [Fact]
    public void Constructor_ShouldSuccessfullyInitializeExpectation()
    {
        var setup = new AllPropertiesStubSetup(_mimic);

        setup.Expectation.ShouldNotBeNull();
        setup.Expectation.Expression.ToString().ShouldBe("m => FromObject(m).SetupAllProperties()");
    }

    [Fact]
    public void MatchesInvocation_WhenCalledWithInvocationOfPropertyGetter_ShouldReturnTrue()
    {
        var method = typeof(ISubject).GetProperty(nameof(ISubject.StringProperty))!.GetGetMethod(true)!;
        var invocation = new InvocationFixture(typeof(ISubject), method);

        var setup = new AllPropertiesStubSetup(_mimic);
        setup.MatchesInvocation(invocation).ShouldBeTrue();
    }

    [Fact]
    public void MatchesInvocation_WhenCalledWithInvocationOfPropertySetter_ShouldReturnTrue()
    {
        var method = typeof(ISubject).GetProperty(nameof(ISubject.StringProperty))!.GetSetMethod(true)!;
        var invocation = new InvocationFixture(typeof(ISubject), method, [string.Empty]);

        var setup = new AllPropertiesStubSetup(_mimic);
        setup.MatchesInvocation(invocation).ShouldBeTrue();
    }

    [Fact]
    public void MatchesInvocation_WhenCalledWithInvocationOfMethod_ShouldReturnFalse()
    {
        var method = typeof(ISubject).GetMethod(nameof(ISubject.VoidMethod))!;
        var invocation = new InvocationFixture(typeof(ISubject), method);

        var setup = new AllPropertiesStubSetup(_mimic);
        setup.MatchesInvocation(invocation).ShouldBeFalse();
    }

    [Fact]
    public void Execute_ShouldSetMatchedToTrueAndSetMatchingSetupOnInvocation()
    {
        var method = typeof(ISubject).GetProperty(nameof(ISubject.StringProperty))!.GetGetMethod(true)!;
        var invocation = new InvocationFixture(typeof(ISubject), method);

        var setup = new AllPropertiesStubSetup(_mimic);
        setup.Execute(invocation);

        setup.Matched.ShouldBeTrue();
        invocation.MatchedSetup.ShouldBeSameAs(setup);
    }

    [Theory]
    [AutoData]
    public void Execute_WhenCalledWithSetterAndGetterMethods_ShouldStoreAndRetriveProvidedValue(string value)
    {
        var setMethod = typeof(ISubject).GetProperty(nameof(ISubject.StringProperty))!.GetSetMethod(true)!;
        var setInvocation = new InvocationFixture(typeof(ISubject), setMethod, [value]);

        var getMethod = typeof(ISubject).GetProperty(nameof(ISubject.StringProperty))!.GetGetMethod(true)!;
        var getInvocation = new InvocationFixture(typeof(ISubject), getMethod);

        var setup = new AllPropertiesStubSetup(_mimic);
        setup.Execute(setInvocation);
        setup.Execute(getInvocation);

        getInvocation.ReturnValue.ShouldBe(value);
    }

    [Fact]
    public void Execute_WhenCalledWithGetterMethodButNoValueStored_ShouldReturnDefaultValueForThatType()
    {
        var stringMethod = typeof(ISubject).GetProperty(nameof(ISubject.StringProperty))!.GetGetMethod(true)!;
        var stringInvocation = new InvocationFixture(typeof(ISubject), stringMethod);

        var intMethod = typeof(ISubject).GetProperty(nameof(ISubject.IntProperty))!.GetGetMethod(true)!;
        var intInvocation = new InvocationFixture(typeof(ISubject), intMethod);

        var setup = new AllPropertiesStubSetup(_mimic);
        setup.Execute(stringInvocation);
        setup.Execute(intInvocation);

        stringInvocation.ReturnValue.ShouldBeNull();
        intInvocation.ReturnValue.ShouldBe(default(int));
    }

    [Fact]
    public void Override_ShouldSetOverridenToTrue()
    {
        var setup = new AllPropertiesStubSetup(_mimic);
        setup.Override();

        setup.Overriden.ShouldBeTrue();
    }

    [Fact]
    public void Verify_ShouldNotThrow()
    {
        var setup = new AllPropertiesStubSetup(_mimic);
        Should.NotThrow(() => setup.Verify());
    }

    [Fact]
    public void ToString_ShouldReturnCorrectStringRepresentationOfSetup()
    {
        var setup = new AllPropertiesStubSetup(_mimic);
        setup.ToString().ShouldBe("AllPropertiesStubSetupTests.ISubject m => FromObject(m).SetupAllProperties()");
    }

    private interface ISubject
    {
        public string StringProperty { get; set; }

        public int IntProperty { get; set; }

        public void VoidMethod();
    }
}
