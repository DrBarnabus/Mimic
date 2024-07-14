using Mimic.Core;
using Mimic.Exceptions;
using Mimic.Setup.Fluent;

namespace Mimic.UnitTests.Setup.Fluent;

public class ConditionalSetupTests
{
    [Fact]
    public void Constructor_ShouldSuccessfullyConstruct()
    {
        var mimic = new Mimic<ISubject>();
        var conditionalSetup = new ConditionalSetup<ISubject>(mimic, () => false);

        conditionalSetup.ShouldNotBeNull();
    }

    [Fact]
    public void Setup_WithActionExpression_ShouldReturnInitializedSetup()
    {
        var mimic = new Mimic<ISubject>();
        var conditionalSetup = new ConditionalSetup<ISubject>(mimic, () => false);

        var setup = conditionalSetup.Setup(m => m.ParameterlessVoidMethod());

        setup.ShouldNotBeNull();
        setup.ShouldBeOfType<Setup<ISubject>>();
    }

    [Fact]
    public void Setup_WithFuncExpression_ShouldReturnInitializedSetup()
    {
        var mimic = new Mimic<ISubject>();
        var conditionalSetup = new ConditionalSetup<ISubject>(mimic, () => false);

        var setup = conditionalSetup.Setup(m => m.ParameterlessStringMethod());

        setup.ShouldNotBeNull();
        setup.ShouldBeOfType<Setup<ISubject, string>>();
    }

    [Fact]
    public void SetupGet_WithValidProperty_ShouldReturnInitializedSetup()
    {
        var mimic = new Mimic<ISubject>();
        var conditionalSetup = new ConditionalSetup<ISubject>(mimic, () => false);

        var setup = conditionalSetup.SetupGet(m => m.Property);

        setup.ShouldNotBeNull();
        setup.ShouldBeOfType<Setup<ISubject, int>>();
    }

    [Fact]
    public void SetupGet_WithMethodCallInsteadOfParameter_ShouldThrow()
    {
        var mimic = new Mimic<ISubject>();
        var conditionalSetup = new ConditionalSetup<ISubject>(mimic, () => false);

        var ex = Should.Throw<MimicException>(() => conditionalSetup.SetupGet(m => m.ParameterlessStringMethod()));
        ex.ShouldNotBeNull();
        ex.Message.ShouldBe("Expression (m => m.ParameterlessStringMethod()) is not a property accessor.");
    }

    [Fact]
    public void SetupGet_WithNullExpression_ShouldThrowAssertionException()
    {
        var mimic = new Mimic<ISubject>();
        var conditionalSetup = new ConditionalSetup<ISubject>(mimic, () => false);

        var ex = Should.Throw<MimicException>(() => conditionalSetup.SetupGet<string>(null!));
        ex.ShouldNotBeNull();
        ex.Message.ShouldContain("expression must not be null");
    }

    [Fact]
    public void SetupSet_WithoutTypeSpecified_AndNullExpression_ShouldReturnInitializedSetup()
    {
        var mimic = new Mimic<ISubject>();
        var conditionalSetup = new ConditionalSetup<ISubject>(mimic, () => false);

        var ex = Should.Throw<Guard.AssertionException>(() => conditionalSetup.SetupSet(null!));
        ex.ShouldNotBeNull();
        ex.Message.ShouldContain("expression must not be null");
    }

    [Fact]
    public void SetupSet_WithoutTypeSpecified_AndExpressionIsNotPropertySetter_ShouldThrow()
    {
        var mimic = new Mimic<ISubject>();
        var conditionalSetup = new ConditionalSetup<ISubject>(mimic, () => false);

        var ex = Should.Throw<MimicException>(() => conditionalSetup.SetupSet(m => m.ParameterlessVoidMethod()));
        ex.ShouldNotBeNull();
        ex.Message.ShouldBe("Expression (m => m.ParameterlessVoidMethod()) is not a property setter.");
    }

    [Fact]
    public void SetupSet_WithoutTypeSpecified_ShouldReturnInitializedSetup()
    {
        var mimic = new Mimic<ISubject>();
        var conditionalSetup = new ConditionalSetup<ISubject>(mimic, () => false);

        var setup = conditionalSetup.SetupSet(m => m.Property = default);

        setup.ShouldNotBeNull();
        setup.ShouldBeOfType<Setup<ISubject>>();
    }

    [Fact]
    public void SetupSet_WithTypeSpecified_AndNullExpression_ShouldThrowAssertionException()
    {
        var mimic = new Mimic<ISubject>();
        var conditionalSetup = new ConditionalSetup<ISubject>(mimic, () => false);

        var ex = Should.Throw<Guard.AssertionException>(() => conditionalSetup.SetupSet<string>(null!));
        ex.ShouldNotBeNull();
        ex.Message.ShouldContain("expression must not be null");
    }

    [Fact]
    public void SetupSet_WithTypeSpecified_ShouldReturnInitializedSetup()
    {
        var mimic = new Mimic<ISubject>();
        var conditionalSetup = new ConditionalSetup<ISubject>(mimic, () => false);

        var setup = conditionalSetup.SetupSet<string>(m => m.Property = default);

        setup.ShouldNotBeNull();
        setup.ShouldBeOfType<SetterSetup<ISubject, string>>();
    }

    [Fact]
    public void SetupSet_WithTypeSpecified_AndExpressionIsNotPropertySetter_ShouldThrow()
    {
        var mimic = new Mimic<ISubject>();
        var conditionalSetup = new ConditionalSetup<ISubject>(mimic, () => false);

        var ex = Should.Throw<MimicException>(() => conditionalSetup.SetupSet(m => m.ParameterlessVoidMethod()));
        ex.ShouldNotBeNull();
        ex.Message.ShouldBe("Expression (m => m.ParameterlessVoidMethod()) is not a property setter.");
    }

    // ReSharper disable once MemberCanBePrivate.Global
    internal interface ISubject
    {
        public void ParameterlessVoidMethod();

        public string ParameterlessStringMethod();

        public int Property { get; set; }
    }
}
