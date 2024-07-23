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
        setup.Overridden.ShouldBeFalse();
        setup.Expected.ShouldBeFalse();
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
    public void Execute_WhenCalledWithSetterAndGetterMethods_ShouldStoreAndRetrieveProvidedValue(string value)
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
    public void Override_ShouldSetOverriddenToTrue()
    {
        var setup = new AllPropertiesStubSetup(_mimic);
        setup.Override();

        setup.Overridden.ShouldBeTrue();
    }

    [Fact]
    public void VerifyMatched_ShouldNotThrow()
    {
        var setup = new AllPropertiesStubSetup(_mimic);
        Should.NotThrow(() => setup.VerifyMatched(_ => true, []));
    }

    [Fact]
    public void GetNested_ShouldReturnListContainingOnlyTheMimicObject()
    {
        var nestedMimic = new Mimic<INestedSubject>();
        var setMethod = typeof(ISubject).GetProperty(nameof(ISubject.NestedSubject))!.GetSetMethod(true)!;
        var setInvocation = new InvocationFixture(typeof(ISubject), setMethod, [nestedMimic.Object]);

        var setup = new AllPropertiesStubSetup(_mimic);
        setup.Execute(setInvocation);

        var nestedMimics = setup.GetNested();
        nestedMimics.ShouldNotBeNull();
        nestedMimics.ShouldNotBeEmpty();
        nestedMimics.Count.ShouldBe(1);
        nestedMimics[0].ShouldBeSameAs(nestedMimic);
    }

    [Fact]
    public void ToString_ShouldReturnCorrectStringRepresentationOfSetup()
    {
        var setup = new AllPropertiesStubSetup(_mimic);
        setup.ToString().ShouldBe("AllPropertiesStubSetupTests.ISubject m => FromObject(m).SetupAllProperties()");
    }

    [Fact]
    public void Equals_WhenCalledWithMatchingObject_ShouldReturnTrue()
    {
        var allPropertiesStubSetupExpectationOne = new AllPropertiesStubSetup(_mimic).Expectation;
        var allPropertiesStubSetupExpectationTwo = new AllPropertiesStubSetup(_mimic).Expectation;

        allPropertiesStubSetupExpectationOne.Equals(allPropertiesStubSetupExpectationTwo).ShouldBeTrue();
    }

    [Fact]
    public void Equals_WhenCalledWithWrongObjectType_ShouldReturnFalse()
    {
        var allPropertiesStubSetupExpectation = new AllPropertiesStubSetup(_mimic).Expectation;

        // ReSharper disable once SuspiciousTypeConversion.Global
        allPropertiesStubSetupExpectation.Equals("obviously wrong type").ShouldBeFalse();
    }

    [Fact]
    public void Equals_WhenCalledWithWrongExpectationType_ShouldReturnFalse()
    {
        var allPropertiesStubSetupExpectation = new AllPropertiesStubSetup(_mimic).Expectation;
        var methodExpectation = MethodExpectationTests.ConstructMethodExpectation(m => m.MethodWithNoArguments());

        allPropertiesStubSetupExpectation.Equals(methodExpectation).ShouldBeFalse();
    }

    [Fact]
    public void Equals_WhenExpectationIsForDifferentMimicObject_ShouldReturnFalse()
    {
        var allPropertiesStubSetupExpectationOne = new AllPropertiesStubSetup(_mimic).Expectation;
        var allPropertiesStubSetupExpectationTwo = new AllPropertiesStubSetup(new Mimic<INestedSubject>()).Expectation;

        allPropertiesStubSetupExpectationOne.Equals(allPropertiesStubSetupExpectationTwo).ShouldBeFalse();
    }

    [Fact]
    public void GetHashCode_ShouldAlwaysReturnMatchingValues()
    {
        var allPropertiesStubSetupExpectationOne = new AllPropertiesStubSetup(_mimic).Expectation;
        var allPropertiesStubSetupExpectationTwo = new AllPropertiesStubSetup(new Mimic<INestedSubject>()).Expectation;

        allPropertiesStubSetupExpectationOne.GetHashCode().ShouldBe(allPropertiesStubSetupExpectationTwo.GetHashCode());
    }

    private interface ISubject
    {
        public string StringProperty { get; set; }

        public int IntProperty { get; set; }

        public void VoidMethod();

        public INestedSubject NestedSubject { get; set; }
    }

    // ReSharper disable once MemberCanBePrivate.Global
    internal interface INestedSubject;
}
