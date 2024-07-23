using Mimic.Exceptions;
using Mimic.Setup.Fluent;

namespace Mimic.UnitTests;

public partial class MimicTests
{
    [Fact]
    public void Constructor_Parameterless_ShouldSuccessfullyConstruct()
    {
        var mimic = new Mimic<ISubject>();

        mimic.Name.ShouldStartWith("Mimic<MimicTests.ISubject>:");
        mimic.Strict.ShouldBeTrue();
        mimic.ConstructorArguments.ShouldBeNull();
        mimic.Invocations.ShouldBeEmpty();

        (mimic as IMimic).Setups.ShouldBeEmpty();
        (mimic as IMimic).Invocations.ShouldBeEmpty();
    }

    [Fact]
    public void Constructor_Parameterless_WhenTypeCanNotBeMimicked_ShouldThrowMimicException()
    {
        var ex = Should.Throw<MimicException>(() => new Mimic<string>());

        ex.Message.ShouldBe("Type string cannot be mimicked. It must be an interface or a non-sealed/non-static class.");
    }

    [Theory, InlineData(true), InlineData(false)]
    public void Constructor_WithStrict_ShouldSuccessfullyConstruct(bool strict)
    {
        var mimic = new Mimic<ISubject>(strict);

        mimic.Strict.ShouldBe(strict);
    }

    [Theory, AutoData]
    public void ConstructorArguments_WhenTypeIsNotAnInterface_ShouldSuccessfullyGetAndSetValues(int intValue, string stringValue)
    {
        var mimic = new Mimic<SubjectClassWithConstructor> { ConstructorArguments = [intValue, stringValue] };

        mimic.ConstructorArguments.ShouldBe([intValue, stringValue]);
    }

    [Theory, AutoData]
    public void ConstructorArguments_WhenTypeIsAnInterface_ShouldThrowArgumentException(int intValue, string stringValue)
    {
        var ex = Should.Throw<ArgumentException>(() => new Mimic<ISubject> { ConstructorArguments = [intValue, stringValue] });

        ex.Message.ShouldBe("ConstructorArguments should not be set when mimicking an interface.");
    }

    [Fact]
    public void ConstructorArguments_WhenTypeIsAnInterfaceButArrayIsEmpty_ShouldNotThrowArgumentException()
    {
        Should.NotThrow(() => new Mimic<ISubject> { ConstructorArguments = [] });
    }

    [Fact]
    public void Object_WhenTypeIsInterface_ShouldReturnMimickedInstance()
    {
        var mimic = new Mimic<ISubject>();

        var mimickedObject = mimic.Object;
        mimickedObject.ShouldNotBeNull();
        mimickedObject.ShouldBeAssignableTo<ISubject>();
        mimickedObject.ShouldBeAssignableTo<IMimicked>();
        mimickedObject.ShouldBeAssignableTo<IMimicked<ISubject>>();
        mimickedObject.ShouldBeAssignableTo<Mimic.Proxy.IProxy>();
    }

    [Fact]
    public void Object_WhenTypeIsClassWithoutConstructor_AndNoConstructorArgumentsProvided_ShouldReturnMimickedInstance()
    {
        var mimic = new Mimic<SubjectClassWithoutConstructor>();

        var mimickedObject = mimic.Object;
        mimickedObject.ShouldNotBeNull();
        mimickedObject.ShouldBeAssignableTo<SubjectClassWithoutConstructor>();
        mimickedObject.ShouldBeAssignableTo<IMimicked>();
        mimickedObject.ShouldBeAssignableTo<IMimicked<SubjectClassWithoutConstructor>>();
        mimickedObject.ShouldBeAssignableTo<Mimic.Proxy.IProxy>();
    }

    [Theory, AutoData]
    public void Object_WhenTypeIsClassWithConstructor_AndConstructorArgumentsProvided_ShouldReturnMimickedInstance(int intValue, string stringValue)
    {
        var mimic = new Mimic<SubjectClassWithConstructor> { ConstructorArguments = [intValue, stringValue] };

        var mimickedObject = mimic.Object;
        mimickedObject.ShouldNotBeNull();
        mimickedObject.ShouldBeAssignableTo<SubjectClassWithConstructor>();
        mimickedObject.ShouldBeAssignableTo<IMimicked>();
        mimickedObject.ShouldBeAssignableTo<IMimicked<SubjectClassWithConstructor>>();
        mimickedObject.ShouldBeAssignableTo<Mimic.Proxy.IProxy>();
    }

    [Theory, AutoData]
    public void Object_WhenTypeIsClassWithoutConstructor_AndConstructorArgumentsProvided_ShouldThrowMimicException(int intValue, string stringValue)
    {
        var mimic = new Mimic<SubjectClassWithoutConstructor> { ConstructorArguments = [intValue, stringValue] };

        var ex = Should.Throw<MimicException>(() => mimic.Object);

        ex.Message.ShouldBe("Unable to find a constructor in type MimicTests.SubjectClassWithoutConstructor matching given constructor arguments.");
    }

    [Fact]
    public void Object_WhenTypeIsClassWithConstructor_AndNoConstructorArgumentsProvided_ShouldThrowMimicException()
    {
        var mimic = new Mimic<SubjectClassWithConstructor>();

        var ex = Should.Throw<MimicException>(() => mimic.Object);

        ex.Message.ShouldBe("Unable to find a constructor in type MimicTests.SubjectClassWithConstructor matching given constructor arguments.");
    }

    [Fact]
    public void FromObject_WhenCalledWithMimickedObjectInstance_ShouldReturnTheOriginalMimicInstance()
    {
        var mimic = new Mimic<ISubject>();
        var result = Mimic<ISubject>.FromObject(mimic.Object);

        result.ShouldNotBeNull();
        result.ShouldBeSameAs(mimic);
    }

    [Fact]
    public void FromObject_WhenCalledWithNonMimickedInstance_ShouldThrowMimicException()
    {
        var ex = Should.Throw<MimicException>(() => Mimic<ISubject>.FromObject(new SubjectImpl()));

        ex.Message.ShouldBe("Object was not created by Mimic, it does not implement IMimicked<T>.");
    }

    [Fact]
    public void ToString_ShouldReturnGeneratedNameOfInstance()
    {
        var mimic = new Mimic<ISubject>();
        mimic.ToString().ShouldStartWith("Mimic<MimicTests.ISubject>:");
    }

    [Theory, AutoData]
    public void ToString_WhenNameIsExplicitlySet_ShouldTheProvidedName(string name)
    {
        var mimic = new Mimic<ISubject> { Name = name };
        mimic.ToString().ShouldBe(name);
    }

    #region When

    [Theory, InlineData(true), InlineData(false)]
    public void When_ShouldReturnAnInstanceOfConditionalSetup(bool condition)
    {
        var mimic = new Mimic<ISubject>();
        var conditionalSetup = mimic.When(() => condition);

        conditionalSetup.ShouldNotBeNull();
        conditionalSetup.ShouldBeAssignableTo<ConditionalSetup<ISubject>>();
    }

    [Theory, AutoData]
    public void When_ConditionIsTrue_ShouldMatchTheProvidedSetup(string returnValue)
    {
        var mimic = new Mimic<ISubject>();

        mimic.When(() => true).Setup(m => m.ConditionalMethod())
            .Returns(returnValue);

        mimic.Object.ConditionalMethod().ShouldBe(returnValue);
    }

    [Theory, AutoData]
    public void When_ConditionIsFalse_ShouldNotMatchTheProvidedSetup(string returnValue)
    {
        var mimic = new Mimic<ISubject>();

        mimic.When(() => false).Setup(m => m.ConditionalMethod())
            .Returns(returnValue);

        var ex = Should.Throw<MimicException>(() => mimic.Object.ConditionalMethod());

        ex.Message.ShouldBe("Invocation of 'MimicTests.ISubject.ConditionalMethod()' failed. Mimic is configured in Strict mode so all invocations must match a setup or this error will be thrown.");
    }

    #endregion

    // ReSharper disable once MemberCanBePrivate.Global
    internal interface ISubject
    {
        string ConditionalMethod();
    }

    // ReSharper disable once MemberCanBePrivate.Global
    internal class SubjectImpl : ISubject
    {
        public string ConditionalMethod() => throw new NotSupportedException();
    }

    // ReSharper disable once MemberCanBePrivate.Global
    internal abstract class SubjectClassWithoutConstructor;

    // ReSharper disable once MemberCanBePrivate.Global
    internal abstract class SubjectClassWithConstructor
    {
        protected SubjectClassWithConstructor(int intValue, string stringValue)
        {
            _ = intValue;
            _ = stringValue;
        }
    }
}
