using System.Linq.Expressions;
using Mimic.Core;
using Mimic.Setup;
using Mimic.Setup.Behaviours;
using Mimic.Setup.Fluent;

namespace Mimic.UnitTests.Setup.Fluent;

public class SequenceSetupOfTResultTests
{
    [Fact]
    public void Constructor_ShouldSuccessfullyConstruct()
    {
        var methodCallSetup = ToMethodCallSetup(m => m.MethodWithNoParameters());

        var setup = new SequenceSetup<string>(methodCallSetup);

        setup.ToString().ShouldBe(methodCallSetup.Expression.ToString());
    }

    [Fact]
    public void Constructor_WhenSetupIsNull_ShouldThrowAssertionException()
    {
        var ex = Should.Throw<Guard.AssertionException>(() => new SequenceSetup<string>(null!));
        ex.ShouldNotBeNull();
        ex.Message.ShouldContain("setup must not be null");
    }

    [Fact]
    public void Expected_ShouldCorrectlyFlagMethodCallSetupAsExpected()
    {
        var methodCallSetup = ToMethodCallSetup(m => m.MethodWithNoParameters());
        var setup = new SequenceSetup<string>(methodCallSetup);

        setup.Expected();

        methodCallSetup.Expected.ShouldBeTrue();
    }

    #region Throws

    [Fact]
    public void Throws_WithExceptionValue_ShouldCorrectlySetSequenceBehaviour()
    {
        var methodCallSetup = ToMethodCallSetup(m => m.MethodWithNoParameters());
        var setup = new SequenceSetup<string>(methodCallSetup);

        setup.Throws(new Exception()).ShouldBeSameAs(setup);

        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour).ShouldNotBeNull();
        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour)!.Remaining.ShouldBe(1);
    }

    [Fact]
    public void Throws_WithExceptionType_ShouldCorrectlySetSequenceBehaviour()
    {
        var methodCallSetup = ToMethodCallSetup(m => m.MethodWithNoParameters());
        var setup = new SequenceSetup<string>(methodCallSetup);

        setup.Throws<Exception>().ShouldBeSameAs(setup);

        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour).ShouldNotBeNull();
        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour)!.Remaining.ShouldBe(1);
    }

    [Fact]
    public void Throws_WithDelegate_ShouldCorrectlySetSequenceBehaviour()
    {
        var methodCallSetup = ToMethodCallSetup(m => m.MethodWithNoParameters());
        var setup = new SequenceSetup<string>(methodCallSetup);

        Delegate @delegate = () => new Exception();
        setup.Throws(@delegate).ShouldBeSameAs(setup);

        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour).ShouldNotBeNull();
        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour)!.Remaining.ShouldBe(1);
    }

    [Fact]
    public void Throws_WithFunc_ShouldCorrectlySetSequenceBehaviour()
    {
        var methodCallSetup = ToMethodCallSetup(m => m.MethodWithNoParameters());
        var setup = new SequenceSetup<string>(methodCallSetup);

        setup.Throws(() => new Exception()).ShouldBeSameAs(setup);

        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour).ShouldNotBeNull();
        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour)!.Remaining.ShouldBe(1);
    }

    [Theory]
    [AutoData]
    public void Throws_WithOneParameterFunc_ShouldCorrectlySetSequenceBehaviour(int v1)
    {
        var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1));
        var setup = new SequenceSetup<string>(methodCallSetup);

        setup.Throws((int _) => new Exception()).ShouldBeSameAs(setup);

        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour).ShouldNotBeNull();
        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour)!.Remaining.ShouldBe(1);
    }

    [Theory]
    [AutoData]
    public void Throws_WithTwoParameterFunc_ShouldCorrectlySetSequenceBehaviour(int v1, int v2)
    {
        var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2));
        var setup = new SequenceSetup<string>(methodCallSetup);

        setup.Throws((int _, int _) => new Exception()).ShouldBeSameAs(setup);

        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour).ShouldNotBeNull();
        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour)!.Remaining.ShouldBe(1);
    }

    [Theory]
    [AutoData]
    public void Throws_WithThreeParameterFunc_ShouldCorrectlySetSequenceBehaviour(int v1, int v2, int v3)
    {
        var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2, v3));
        var setup = new SequenceSetup<string>(methodCallSetup);

        setup.Throws((int _, int _, int _) => new Exception()).ShouldBeSameAs(setup);

        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour).ShouldNotBeNull();
        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour)!.Remaining.ShouldBe(1);
    }

    [Theory]
    [AutoData]
    public void Throws_WithFourParameterFunc_ShouldCorrectlySetSequenceBehaviour(
        int v1, int v2, int v3, int v4)
    {
        var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2, v3, v4));
        var setup = new SequenceSetup<string>(methodCallSetup);

        setup.Throws((int _, int _, int _, int _) => new Exception()).ShouldBeSameAs(setup);

        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour).ShouldNotBeNull();
        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour)!.Remaining.ShouldBe(1);
    }

    [Theory]
    [AutoData]
    public void Throws_WithFiveParameterFunc_ShouldCorrectlySetSequenceBehaviour(
        int v1, int v2, int v3, int v4, int v5)
    {
        var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2, v3, v4, v5));
        var setup = new SequenceSetup<string>(methodCallSetup);

        setup.Throws((int _, int _, int _, int _, int _) => new Exception()).ShouldBeSameAs(setup);

        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour).ShouldNotBeNull();
        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour)!.Remaining.ShouldBe(1);
    }

    [Theory]
    [AutoData]
    public void Throws_WithSixParameterFunc_ShouldCorrectlySetSequenceBehaviour(
        int v1, int v2, int v3, int v4, int v5, int v6)
    {
        var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2, v3, v4, v5, v6));
        var setup = new SequenceSetup<string>(methodCallSetup);

        setup.Throws((int _, int _, int _, int _, int _, int _) => new Exception()).ShouldBeSameAs(setup);

        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour).ShouldNotBeNull();
        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour)!.Remaining.ShouldBe(1);
    }

    [Theory]
    [AutoData]
    public void Throws_WithSevenParameterFunc_ShouldCorrectlySetSequenceBehaviour(
        int v1, int v2, int v3, int v4, int v5, int v6, int v7)
    {
        var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2, v3, v4, v5, v6, v7));
        var setup = new SequenceSetup<string>(methodCallSetup);

        setup.Throws((int _, int _, int _, int _, int _, int _, int _) => new Exception()).ShouldBeSameAs(setup);

        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour).ShouldNotBeNull();
        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour)!.Remaining.ShouldBe(1);
    }

    [Theory]
    [AutoData]
    public void Throws_WithEightParameterFunc_ShouldCorrectlySetSequenceBehaviour(
        int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8)
    {
        var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2, v3, v4, v5, v6, v7, v8));
        var setup = new SequenceSetup<string>(methodCallSetup);

        setup.Throws((int _, int _, int _, int _, int _, int _, int _, int _) => new Exception()).ShouldBeSameAs(setup);

        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour).ShouldNotBeNull();
        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour)!.Remaining.ShouldBe(1);
    }

    [Theory]
    [AutoData]
    public void Throws_WithNineParameterFunc_ShouldCorrectlySetSequenceBehaviour(
        int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9)
    {
        var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2, v3, v4, v5, v6, v7, v8, v9));
        var setup = new SequenceSetup<string>(methodCallSetup);

        setup.Throws((int _, int _, int _, int _, int _, int _, int _, int _, int _) => new Exception()).ShouldBeSameAs(setup);

        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour).ShouldNotBeNull();
        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour)!.Remaining.ShouldBe(1);
    }

    [Theory]
    [AutoData]
    public void Throws_WithTenParameterFunc_ShouldCorrectlySetSequenceBehaviour(
        int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9, int v10)
    {
        var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10));
        var setup = new SequenceSetup<string>(methodCallSetup);

        setup.Throws((int _, int _, int _, int _, int _, int _, int _, int _, int _, int _) => new Exception()).ShouldBeSameAs(setup);

        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour).ShouldNotBeNull();
        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour)!.Remaining.ShouldBe(1);
    }

    [Theory]
    [AutoData]
    public void Throws_WithElevenParameterFunc_ShouldCorrectlySetSequenceBehaviour(
        int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9, int v10, int v11)
    {
        var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11));
        var setup = new SequenceSetup<string>(methodCallSetup);

        setup.Throws((int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _) => new Exception()).ShouldBeSameAs(setup);

        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour).ShouldNotBeNull();
        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour)!.Remaining.ShouldBe(1);
    }

    [Theory]
    [AutoData]
    public void Throws_WithTwelveParameterFunc_ShouldCorrectlySetSequenceBehaviour(
        int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9, int v10, int v11, int v12)
    {
        var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12));
        var setup = new SequenceSetup<string>(methodCallSetup);

        setup.Throws((int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _) => new Exception()).ShouldBeSameAs(setup);

        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour).ShouldNotBeNull();
        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour)!.Remaining.ShouldBe(1);
    }

    [Theory]
    [AutoData]
    public void Throws_WithThirteenParameterFunc_ShouldCorrectlySetSequenceBehaviour(
        int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9, int v10, int v11, int v12, int v13)
    {
        var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13));
        var setup = new SequenceSetup<string>(methodCallSetup);

        setup.Throws((int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _) => new Exception()).ShouldBeSameAs(setup);

        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour).ShouldNotBeNull();
        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour)!.Remaining.ShouldBe(1);
    }

    [Theory]
    [AutoData]
    public void Throws_WithFourteenParameterFunc_ShouldCorrectlySetSequenceBehaviour(
        int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9, int v10, int v11, int v12, int v13, int v14)
    {
        var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14));
        var setup = new SequenceSetup<string>(methodCallSetup);

        setup.Throws((int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _) => new Exception()).ShouldBeSameAs(setup);

        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour).ShouldNotBeNull();
        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour)!.Remaining.ShouldBe(1);
    }

    [Theory]
    [AutoData]
    public void Throws_WithFifteenParameterFunc_ShouldCorrectlySetSequenceBehaviour(
        int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9, int v10, int v11, int v12, int v13, int v14, int v15)
    {
        var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15));
        var setup = new SequenceSetup<string>(methodCallSetup);

        setup.Throws((int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _) => new Exception()).ShouldBeSameAs(setup);

        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour).ShouldNotBeNull();
        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour)!.Remaining.ShouldBe(1);
    }

    [Theory]
    [AutoData]
    public void Throws_WithSixteenParameterFunc_ShouldCorrectlySetSequenceBehaviour(
        int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9, int v10, int v11, int v12, int v13, int v14, int v15, int v16)
    {
        var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16));
        var setup = new SequenceSetup<string>(methodCallSetup);

        setup.Throws((int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _) => new Exception()).ShouldBeSameAs(setup);

        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour).ShouldNotBeNull();
        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour)!.Remaining.ShouldBe(1);
    }

    #endregion

    #region Returns

    [Theory]
    [AutoData]
    public void Returns_WithReturnValue_ShouldCorrectlySetSequenceBehaviour(string returnValue)
    {
        var methodCallSetup = ToMethodCallSetup(m => m.MethodWithNoParameters());
        var setup = new SequenceSetup<string>(methodCallSetup);

        setup.Returns(returnValue).ShouldBeSameAs(setup);

        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour).ShouldNotBeNull();
        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour)!.Remaining.ShouldBe(1);
    }

    [Theory]
    [AutoData]
    public void Returns_WithFunc_ShouldCorrectlySetSequenceBehaviour(string returnValue)
    {
        var methodCallSetup = ToMethodCallSetup(m => m.MethodWithNoParameters());
        var setup = new SequenceSetup<string>(methodCallSetup);

        setup.Returns(() => returnValue).ShouldBeSameAs(setup);

        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour).ShouldNotBeNull();
        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour)!.Remaining.ShouldBe(1);
    }

    [Theory]
    [AutoData]
    public void Returns_WithOneParameterFunc_ShouldCorrectlySetSequenceBehaviour(int v1, string returnValue)
    {
        var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1));
        var setup = new SequenceSetup<string>(methodCallSetup);

        setup.Returns((int _) => returnValue).ShouldBeSameAs(setup);

        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour).ShouldNotBeNull();
        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour)!.Remaining.ShouldBe(1);
    }

    [Theory]
    [AutoData]
    public void Returns_WithTwoParameterFunc_ShouldCorrectlySetSequenceBehaviour(int v1, int v2, string returnValue)
    {
        var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2));
        var setup = new SequenceSetup<string>(methodCallSetup);

        setup.Returns((int _, int _) => returnValue).ShouldBeSameAs(setup);

        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour).ShouldNotBeNull();
        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour)!.Remaining.ShouldBe(1);
    }

    [Theory]
    [AutoData]
    public void Returns_WithThreeParameterFunc_ShouldCorrectlySetSequenceBehaviour(int v1, int v2, int v3, string returnValue)
    {
        var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2, v3));
        var setup = new SequenceSetup<string>(methodCallSetup);

        setup.Returns((int _, int _, int _) => returnValue).ShouldBeSameAs(setup);

        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour).ShouldNotBeNull();
        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour)!.Remaining.ShouldBe(1);
    }

    [Theory]
    [AutoData]
    public void Returns_WithFourParameterFunc_ShouldCorrectlySetSequenceBehaviour(
        int v1, int v2, int v3, int v4, string returnValue)
    {
        var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2, v3, v4));
        var setup = new SequenceSetup<string>(methodCallSetup);

        setup.Returns((int _, int _, int _, int _) => returnValue).ShouldBeSameAs(setup);

        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour).ShouldNotBeNull();
        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour)!.Remaining.ShouldBe(1);
    }

    [Theory]
    [AutoData]
    public void Returns_WithFiveParameterFunc_ShouldCorrectlySetSequenceBehaviour(
        int v1, int v2, int v3, int v4, int v5, string returnValue)
    {
        var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2, v3, v4, v5));
        var setup = new SequenceSetup<string>(methodCallSetup);

        setup.Returns((int _, int _, int _, int _, int _) => returnValue).ShouldBeSameAs(setup);

        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour).ShouldNotBeNull();
        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour)!.Remaining.ShouldBe(1);
    }

    [Theory]
    [AutoData]
    public void Returns_WithSixParameterFunc_ShouldCorrectlySetSequenceBehaviour(
        int v1, int v2, int v3, int v4, int v5, int v6, string returnValue)
    {
        var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2, v3, v4, v5, v6));
        var setup = new SequenceSetup<string>(methodCallSetup);

        setup.Returns((int _, int _, int _, int _, int _, int _) => returnValue).ShouldBeSameAs(setup);

        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour).ShouldNotBeNull();
        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour)!.Remaining.ShouldBe(1);
    }

    [Theory]
    [AutoData]
    public void Returns_WithSevenParameterFunc_ShouldCorrectlySetSequenceBehaviour(
        int v1, int v2, int v3, int v4, int v5, int v6, int v7, string returnValue)
    {
        var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2, v3, v4, v5, v6, v7));
        var setup = new SequenceSetup<string>(methodCallSetup);

        setup.Returns((int _, int _, int _, int _, int _, int _, int _) => returnValue).ShouldBeSameAs(setup);

        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour).ShouldNotBeNull();
        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour)!.Remaining.ShouldBe(1);
    }

    [Theory]
    [AutoData]
    public void Returns_WithEightParameterFunc_ShouldCorrectlySetSequenceBehaviour(
        int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, string returnValue)
    {
        var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2, v3, v4, v5, v6, v7, v8));
        var setup = new SequenceSetup<string>(methodCallSetup);

        setup.Returns((int _, int _, int _, int _, int _, int _, int _, int _) => returnValue).ShouldBeSameAs(setup);

        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour).ShouldNotBeNull();
        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour)!.Remaining.ShouldBe(1);
    }

    [Theory]
    [AutoData]
    public void Returns_WithNineParameterFunc_ShouldCorrectlySetSequenceBehaviour(
        int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9, string returnValue)
    {
        var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2, v3, v4, v5, v6, v7, v8, v9));
        var setup = new SequenceSetup<string>(methodCallSetup);

        setup.Returns((int _, int _, int _, int _, int _, int _, int _, int _, int _) => returnValue).ShouldBeSameAs(setup);

        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour).ShouldNotBeNull();
        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour)!.Remaining.ShouldBe(1);
    }

    [Theory]
    [AutoData]
    public void Returns_WithTenParameterFunc_ShouldCorrectlySetSequenceBehaviour(
        int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9, int v10, string returnValue)
    {
        var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10));
        var setup = new SequenceSetup<string>(methodCallSetup);

        setup.Returns((int _, int _, int _, int _, int _, int _, int _, int _, int _, int _) => returnValue).ShouldBeSameAs(setup);

        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour).ShouldNotBeNull();
        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour)!.Remaining.ShouldBe(1);
    }

    [Theory]
    [AutoData]
    public void Returns_WithElevenParameterFunc_ShouldCorrectlySetSequenceBehaviour(
        int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9, int v10, int v11, string returnValue)
    {
        var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11));
        var setup = new SequenceSetup<string>(methodCallSetup);

        setup.Returns((int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _) => returnValue).ShouldBeSameAs(setup);

        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour).ShouldNotBeNull();
        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour)!.Remaining.ShouldBe(1);
    }

    [Theory]
    [AutoData]
    public void Returns_WithTwelveParameterFunc_ShouldCorrectlySetSequenceBehaviour(
        int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9, int v10, int v11, int v12, string returnValue)
    {
        var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12));
        var setup = new SequenceSetup<string>(methodCallSetup);

        setup.Returns((int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _) => returnValue).ShouldBeSameAs(setup);

        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour).ShouldNotBeNull();
        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour)!.Remaining.ShouldBe(1);
    }

    [Theory]
    [AutoData]
    public void Returns_WithThirteenParameterFunc_ShouldCorrectlySetSequenceBehaviour(
        int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9, int v10, int v11, int v12, int v13, string returnValue)
    {
        var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13));
        var setup = new SequenceSetup<string>(methodCallSetup);

        setup.Returns((int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _) => returnValue).ShouldBeSameAs(setup);

        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour).ShouldNotBeNull();
        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour)!.Remaining.ShouldBe(1);
    }

    [Theory]
    [AutoData]
    public void Returns_WithFourteenParameterFunc_ShouldCorrectlySetSequenceBehaviour(
        int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9, int v10, int v11, int v12, int v13, int v14, string returnValue)
    {
        var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14));
        var setup = new SequenceSetup<string>(methodCallSetup);

        setup.Returns((int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _) => returnValue).ShouldBeSameAs(setup);

        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour).ShouldNotBeNull();
        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour)!.Remaining.ShouldBe(1);
    }

    [Theory]
    [AutoData]
    public void Returns_WithFifteenParameterFunc_ShouldCorrectlySetSequenceBehaviour(
        int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9, int v10, int v11, int v12, int v13, int v14, int v15, string returnValue)
    {
        var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15));
        var setup = new SequenceSetup<string>(methodCallSetup);

        setup.Returns((int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _) => returnValue).ShouldBeSameAs(setup);

        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour).ShouldNotBeNull();
        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour)!.Remaining.ShouldBe(1);
    }

    [Theory]
    [AutoData]
    public void Returns_WithSixteenParameterFunc_ShouldCorrectlySetSequenceBehaviour(
        int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9, int v10, int v11, int v12, int v13, int v14, int v15, int v16, string returnValue)
    {
        var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16));
        var setup = new SequenceSetup<string>(methodCallSetup);

        setup.Returns((int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _) => returnValue).ShouldBeSameAs(setup);

        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour).ShouldNotBeNull();
        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour)!.Remaining.ShouldBe(1);
    }

    #endregion

    [Fact]
    public void Proceed_ShouldCorrectlySetProceedBehaviour()
    {
        var methodCallSetup = ToMethodCallSetup<AbstractSubject>(m => m.VirtualMethod());
        var setup = new SequenceSetup<string>(methodCallSetup);

        setup.Proceed().ShouldBeSameAs(setup);

        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour).ShouldNotBeNull();
        (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as SequenceBehaviour)!.Remaining.ShouldBe(1);
    }

    private static MethodCallSetup ToMethodCallSetup(Expression<Action<ISubject>> expression) => ToMethodCallSetup<ISubject>(expression);

    private static MethodCallSetup ToMethodCallSetup<T>(Expression<Action<T>> expression)
        where T : class
    {
        var mimic = new Mimic<T>();
        var methodCallExpression = (MethodCallExpression)expression.Body;
        var methodExpectation = new MethodExpectation(expression, methodCallExpression.Method, methodCallExpression.Arguments);

        return new MethodCallSetup(methodCallExpression, mimic, methodExpectation, null);
    }

    // ReSharper disable once MemberCanBePrivate.Global
    internal interface ISubject
    {
        public string MethodWithNoParameters();
        public string MethodWithParameters(int v1);
        public string MethodWithParameters(int v1, int v2);
        public string MethodWithParameters(int v1, int v2, int v3);
        public string MethodWithParameters(int v1, int v2, int v3, int v4);
        public string MethodWithParameters(int v1, int v2, int v3, int v4, int v5);
        public string MethodWithParameters(int v1, int v2, int v3, int v4, int v5, int v6);
        public string MethodWithParameters(int v1, int v2, int v3, int v4, int v5, int v6, int v7);
        public string MethodWithParameters(int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8);
        public string MethodWithParameters(int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9);
        public string MethodWithParameters(int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9, int v10);
        public string MethodWithParameters(int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9, int v10, int v11);
        public string MethodWithParameters(int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9, int v10, int v11, int v12);
        public string MethodWithParameters(int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9, int v10, int v11, int v12, int v13);
        public string MethodWithParameters(int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9, int v10, int v11, int v12, int v13, int v14);
        public string MethodWithParameters(int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9, int v10, int v11, int v12, int v13, int v14, int v15);
        public string MethodWithParameters(int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9, int v10, int v11, int v12, int v13, int v14, int v15, int v16);
    }

    // ReSharper disable once MemberCanBePrivate.Global
    internal abstract class AbstractSubject
    {
        public virtual string VirtualMethod() => default!;
    }
}
