using System.Linq.Expressions;
using Mimic.Core;
using Mimic.Setup;
using Mimic.Setup.Behaviours;
using Mimic.Setup.Fluent;

namespace Mimic.UnitTests.Setup.Fluent;

public static partial class SetupTests
{
    public class OfTMimic
    {
        [Fact]
        public void Constructor_ShouldSuccessfullyConstruct()
        {
            var methodCallSetup = ToMethodCallSetup(m => m.MethodWithNoParameters());

            var setup = new Setup<ISubject>(methodCallSetup);

            setup.ToString().ShouldBe(methodCallSetup.Expression.ToString());
        }

        [Fact]
        public void Constructor_WhenSetupIsNull_ShouldThrowAssertionException()
        {
            var ex = Should.Throw<Guard.AssertionException>(() => new Setup<ISubject>(null!));
            ex.ShouldNotBeNull();
            ex.Message.ShouldContain("setup must not be null");
        }

        [Fact]
        public void Expected_ShouldCorrectlyFlagMethodCallSetupAsExpected()
        {
            var methodCallSetup = ToMethodCallSetup(m => m.MethodWithNoParameters());
            var setup = new Setup<ISubject>(methodCallSetup);

            setup.Expected();

            methodCallSetup.Expected.ShouldBeTrue();
        }

        #region Callback

        [Fact]
        public void Callback_WithDelegate_ShouldCorrectlySetCallbackBehaviour()
        {
            var methodCallSetup = ToMethodCallSetup(m => m.MethodWithNoParameters());
            var setup = new Setup<ISubject>(methodCallSetup);

            Delegate @delegate = () => {};
            setup.Callback(@delegate).ShouldBeSameAs(setup);

            methodCallSetup.ConfiguredBehaviours.PreReturnCallback.ShouldNotBeNull();
        }

        [Fact]
        public void Callback_WithAction_ShouldCorrectlySetCallbackBehaviour()
        {
            var methodCallSetup = ToMethodCallSetup(m => m.MethodWithNoParameters());
            var setup = new Setup<ISubject>(methodCallSetup);

            setup.Callback(() => {}).ShouldBeSameAs(setup);

            methodCallSetup.ConfiguredBehaviours.PreReturnCallback.ShouldNotBeNull();
        }

        [Theory]
        [AutoData]
        public void Callback_WithOneParameterAction_ShouldCorrectlySetCallbackBehaviour(int v1)
        {
            var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1));
            var setup = new Setup<ISubject>(methodCallSetup);

            setup.Callback((int _) => {}).ShouldBeSameAs(setup);

            methodCallSetup.ConfiguredBehaviours.PreReturnCallback.ShouldNotBeNull();
        }

        [Theory]
        [AutoData]
        public void Callback_WithTwoParameterAction_ShouldCorrectlySetCallbackBehaviour(int v1, int v2)
        {
            var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2));
            var setup = new Setup<ISubject>(methodCallSetup);

            setup.Callback((int _, int _) => {}).ShouldBeSameAs(setup);

            methodCallSetup.ConfiguredBehaviours.PreReturnCallback.ShouldNotBeNull();
        }

        [Theory]
        [AutoData]
        public void Callback_WithThreeParameterAction_ShouldCorrectlySetCallbackBehaviour(int v1, int v2, int v3)
        {
            var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2, v3));
            var setup = new Setup<ISubject>(methodCallSetup);

            setup.Callback((int _, int _, int _) => {}).ShouldBeSameAs(setup);

            methodCallSetup.ConfiguredBehaviours.PreReturnCallback.ShouldNotBeNull();
        }

        [Theory]
        [AutoData]
        public void Callback_WithFourParameterAction_ShouldCorrectlySetCallbackBehaviour(int v1, int v2, int v3, int v4)
        {
            var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2, v3, v4));
            var setup = new Setup<ISubject>(methodCallSetup);

            setup.Callback((int _, int _, int _, int _) => {}).ShouldBeSameAs(setup);

            methodCallSetup.ConfiguredBehaviours.PreReturnCallback.ShouldNotBeNull();
        }

        [Theory]
        [AutoData]
        public void Callback_WithFiveParameterAction_ShouldCorrectlySetCallbackBehaviour(int v1, int v2, int v3, int v4, int v5)
        {
            var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2, v3, v4, v5));
            var setup = new Setup<ISubject>(methodCallSetup);

            setup.Callback((int _, int _, int _, int _, int _) => {}).ShouldBeSameAs(setup);

            methodCallSetup.ConfiguredBehaviours.PreReturnCallback.ShouldNotBeNull();
        }

        [Theory]
        [AutoData]
        public void Callback_WithSixParameterAction_ShouldCorrectlySetCallbackBehaviour(
            int v1, int v2, int v3, int v4, int v5, int v6)
        {
            var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2, v3, v4, v5, v6));
            var setup = new Setup<ISubject>(methodCallSetup);

            setup.Callback((int _, int _, int _, int _, int _, int _) => {}).ShouldBeSameAs(setup);

            methodCallSetup.ConfiguredBehaviours.PreReturnCallback.ShouldNotBeNull();
        }

        [Theory]
        [AutoData]
        public void Callback_WithSevenParameterAction_ShouldCorrectlySetCallbackBehaviour(
            int v1, int v2, int v3, int v4, int v5, int v6, int v7)
        {
            var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2, v3, v4, v5, v6, v7));
            var setup = new Setup<ISubject>(methodCallSetup);

            setup.Callback((int _, int _, int _, int _, int _, int _, int _) => {}).ShouldBeSameAs(setup);

            methodCallSetup.ConfiguredBehaviours.PreReturnCallback.ShouldNotBeNull();
        }

        [Theory]
        [AutoData]
        public void Callback_WithEightParameterAction_ShouldCorrectlySetCallbackBehaviour(
            int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8)
        {
            var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2, v3, v4, v5, v6, v7, v8));
            var setup = new Setup<ISubject>(methodCallSetup);

            setup.Callback((int _, int _, int _, int _, int _, int _, int _, int _) => {}).ShouldBeSameAs(setup);

            methodCallSetup.ConfiguredBehaviours.PreReturnCallback.ShouldNotBeNull();
        }

        [Theory]
        [AutoData]
        public void Callback_WithNineParameterAction_ShouldCorrectlySetCallbackBehaviour(
            int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9)
        {
            var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2, v3, v4, v5, v6, v7, v8, v9));
            var setup = new Setup<ISubject>(methodCallSetup);

            setup.Callback((int _, int _, int _, int _, int _, int _, int _, int _, int _) => {}).ShouldBeSameAs(setup);

            methodCallSetup.ConfiguredBehaviours.PreReturnCallback.ShouldNotBeNull();
        }

        [Theory]
        [AutoData]
        public void Callback_WithTenParameterAction_ShouldCorrectlySetCallbackBehaviour(
            int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9, int v10)
        {
            var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10));
            var setup = new Setup<ISubject>(methodCallSetup);

            setup.Callback((int _, int _, int _, int _, int _, int _, int _, int _, int _, int _) => {}).ShouldBeSameAs(setup);

            methodCallSetup.ConfiguredBehaviours.PreReturnCallback.ShouldNotBeNull();
        }

        [Theory]
        [AutoData]
        public void Callback_WithElevenParameterAction_ShouldCorrectlySetCallbackBehaviour(
            int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9, int v10, int v11)
        {
            var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11));
            var setup = new Setup<ISubject>(methodCallSetup);

            setup.Callback((int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _) => {}).ShouldBeSameAs(setup);

            methodCallSetup.ConfiguredBehaviours.PreReturnCallback.ShouldNotBeNull();
        }

        [Theory]
        [AutoData]
        public void Callback_WithTwelveParameterAction_ShouldCorrectlySetCallbackBehaviour(
            int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9, int v10, int v11, int v12)
        {
            var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12));
            var setup = new Setup<ISubject>(methodCallSetup);

            setup.Callback((int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _) => {}).ShouldBeSameAs(setup);

            methodCallSetup.ConfiguredBehaviours.PreReturnCallback.ShouldNotBeNull();
        }

        [Theory]
        [AutoData]
        public void Callback_WithThirteenParameterAction_ShouldCorrectlySetCallbackBehaviour(
            int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9, int v10, int v11, int v12, int v13)
        {
            var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13));
            var setup = new Setup<ISubject>(methodCallSetup);

            setup.Callback((int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _) => {}).ShouldBeSameAs(setup);

            methodCallSetup.ConfiguredBehaviours.PreReturnCallback.ShouldNotBeNull();
        }

        [Theory]
        [AutoData]
        public void Callback_WithFourteenParameterAction_ShouldCorrectlySetCallbackBehaviour(
            int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9, int v10, int v11, int v12, int v13, int v14)
        {
            var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14));
            var setup = new Setup<ISubject>(methodCallSetup);

            setup.Callback((int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _) => {}).ShouldBeSameAs(setup);

            methodCallSetup.ConfiguredBehaviours.PreReturnCallback.ShouldNotBeNull();
        }

        [Theory]
        [AutoData]
        public void Callback_WithFifteenParameterAction_ShouldCorrectlySetCallbackBehaviour(
            int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9, int v10, int v11, int v12, int v13, int v14, int v15)
        {
            var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15));
            var setup = new Setup<ISubject>(methodCallSetup);

            setup.Callback((int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _) => {}).ShouldBeSameAs(setup);

            methodCallSetup.ConfiguredBehaviours.PreReturnCallback.ShouldNotBeNull();
        }

        [Theory]
        [AutoData]
        public void Callback_WithSixteenParameterAction_ShouldCorrectlySetCallbackBehaviour(
            int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9, int v10, int v11, int v12, int v13, int v14, int v15, int v16)
        {
            var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16));
            var setup = new Setup<ISubject>(methodCallSetup);

            setup.Callback((int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _) => {}).ShouldBeSameAs(setup);

            methodCallSetup.ConfiguredBehaviours.PreReturnCallback.ShouldNotBeNull();
        }

        #endregion

        #region Throws

        [Fact]
        public void Throws_WithExceptionValue_ShouldCorrectlySetThrowsExceptionBehaviour()
        {
            var methodCallSetup = ToMethodCallSetup(m => m.MethodWithNoParameters());
            var setup = new Setup<ISubject>(methodCallSetup);

            setup.Throws(new Exception()).ShouldBeSameAs(setup);

            (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as ThrowExceptionBehaviour).ShouldNotBeNull();
        }

        [Fact]
        public void Throws_WithExceptionType_ShouldCorrectlySetThrowsExceptionBehaviour()
        {
            var methodCallSetup = ToMethodCallSetup(m => m.MethodWithNoParameters());
            var setup = new Setup<ISubject>(methodCallSetup);

            setup.Throws<Exception>().ShouldBeSameAs(setup);

            (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as ThrowExceptionBehaviour).ShouldNotBeNull();
        }

        [Fact]
        public void Throws_WithDelegate_ShouldCorrectlySetThrowComputedExceptionBehaviour()
        {
            var methodCallSetup = ToMethodCallSetup(m => m.MethodWithNoParameters());
            var setup = new Setup<ISubject>(methodCallSetup);

            Delegate @delegate = () => new Exception();
            setup.Throws(@delegate).ShouldBeSameAs(setup);

            (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as ThrowComputedExceptionBehaviour).ShouldNotBeNull();
        }

        [Fact]
        public void Throws_WithFunc_ShouldCorrectlySetThrowComputedExceptionBehaviour()
        {
            var methodCallSetup = ToMethodCallSetup(m => m.MethodWithNoParameters());
            var setup = new Setup<ISubject>(methodCallSetup);

            setup.Throws(() => new Exception()).ShouldBeSameAs(setup);

            (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as ThrowComputedExceptionBehaviour).ShouldNotBeNull();
        }

        [Theory]
        [AutoData]
        public void Throws_WithOneParameterFunc_ShouldCorrectlySetThrowComputedExceptionBehaviour(int v1)
        {
            var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1));
            var setup = new Setup<ISubject>(methodCallSetup);

            setup.Throws((int _) => new Exception()).ShouldBeSameAs(setup);

            (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as ThrowComputedExceptionBehaviour).ShouldNotBeNull();
        }

        [Theory]
        [AutoData]
        public void Throws_WithTwoParameterFunc_ShouldCorrectlySetThrowComputedExceptionBehaviour(int v1, int v2)
        {
            var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2));
            var setup = new Setup<ISubject>(methodCallSetup);

            setup.Throws((int _, int _) => new Exception()).ShouldBeSameAs(setup);

            (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as ThrowComputedExceptionBehaviour).ShouldNotBeNull();
        }

        [Theory]
        [AutoData]
        public void Throws_WithThreeParameterFunc_ShouldCorrectlySetThrowComputedExceptionBehaviour(int v1, int v2, int v3)
        {
            var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2, v3));
            var setup = new Setup<ISubject>(methodCallSetup);

            setup.Throws((int _, int _, int _) => new Exception()).ShouldBeSameAs(setup);

            (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as ThrowComputedExceptionBehaviour).ShouldNotBeNull();
        }

        [Theory]
        [AutoData]
        public void Throws_WithFourParameterFunc_ShouldCorrectlySetThrowComputedExceptionBehaviour(
            int v1, int v2, int v3, int v4)
        {
            var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2, v3, v4));
            var setup = new Setup<ISubject>(methodCallSetup);

            setup.Throws((int _, int _, int _, int _) => new Exception()).ShouldBeSameAs(setup);

            (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as ThrowComputedExceptionBehaviour).ShouldNotBeNull();
        }

        [Theory]
        [AutoData]
        public void Throws_WithFiveParameterFunc_ShouldCorrectlySetThrowComputedExceptionBehaviour(
            int v1, int v2, int v3, int v4, int v5)
        {
            var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2, v3, v4, v5));
            var setup = new Setup<ISubject>(methodCallSetup);

            setup.Throws((int _, int _, int _, int _, int _) => new Exception()).ShouldBeSameAs(setup);

            (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as ThrowComputedExceptionBehaviour).ShouldNotBeNull();
        }

        [Theory]
        [AutoData]
        public void Throws_WithSixParameterFunc_ShouldCorrectlySetThrowComputedExceptionBehaviour(
            int v1, int v2, int v3, int v4, int v5, int v6)
        {
            var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2, v3, v4, v5, v6));
            var setup = new Setup<ISubject>(methodCallSetup);

            setup.Throws((int _, int _, int _, int _, int _, int _) => new Exception()).ShouldBeSameAs(setup);

            (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as ThrowComputedExceptionBehaviour).ShouldNotBeNull();
        }

        [Theory]
        [AutoData]
        public void Throws_WithSevenParameterFunc_ShouldCorrectlySetThrowComputedExceptionBehaviour(
            int v1, int v2, int v3, int v4, int v5, int v6, int v7)
        {
            var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2, v3, v4, v5, v6, v7));
            var setup = new Setup<ISubject>(methodCallSetup);

            setup.Throws((int _, int _, int _, int _, int _, int _, int _) => new Exception()).ShouldBeSameAs(setup);

            (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as ThrowComputedExceptionBehaviour).ShouldNotBeNull();
        }

        [Theory]
        [AutoData]
        public void Throws_WithEightParameterFunc_ShouldCorrectlySetThrowComputedExceptionBehaviour(
            int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8)
        {
            var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2, v3, v4, v5, v6, v7, v8));
            var setup = new Setup<ISubject>(methodCallSetup);

            setup.Throws((int _, int _, int _, int _, int _, int _, int _, int _) => new Exception()).ShouldBeSameAs(setup);

            (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as ThrowComputedExceptionBehaviour).ShouldNotBeNull();
        }

        [Theory]
        [AutoData]
        public void Throws_WithNineParameterFunc_ShouldCorrectlySetThrowComputedExceptionBehaviour(
            int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9)
        {
            var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2, v3, v4, v5, v6, v7, v8, v9));
            var setup = new Setup<ISubject>(methodCallSetup);

            setup.Throws((int _, int _, int _, int _, int _, int _, int _, int _, int _) => new Exception()).ShouldBeSameAs(setup);

            (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as ThrowComputedExceptionBehaviour).ShouldNotBeNull();
        }

        [Theory]
        [AutoData]
        public void Throws_WithTenParameterFunc_ShouldCorrectlySetThrowComputedExceptionBehaviour(
            int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9, int v10)
        {
            var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10));
            var setup = new Setup<ISubject>(methodCallSetup);

            setup.Throws((int _, int _, int _, int _, int _, int _, int _, int _, int _, int _) => new Exception()).ShouldBeSameAs(setup);

            (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as ThrowComputedExceptionBehaviour).ShouldNotBeNull();
        }

        [Theory]
        [AutoData]
        public void Throws_WithElevenParameterFunc_ShouldCorrectlySetThrowComputedExceptionBehaviour(
            int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9, int v10, int v11)
        {
            var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11));
            var setup = new Setup<ISubject>(methodCallSetup);

            setup.Throws((int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _) => new Exception()).ShouldBeSameAs(setup);

            (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as ThrowComputedExceptionBehaviour).ShouldNotBeNull();
        }

        [Theory]
        [AutoData]
        public void Throws_WithTwelveParameterFunc_ShouldCorrectlySetThrowComputedExceptionBehaviour(
            int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9, int v10, int v11, int v12)
        {
            var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12));
            var setup = new Setup<ISubject>(methodCallSetup);

            setup.Throws((int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _) => new Exception()).ShouldBeSameAs(setup);

            (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as ThrowComputedExceptionBehaviour).ShouldNotBeNull();
        }

        [Theory]
        [AutoData]
        public void Throws_WithThirteenParameterFunc_ShouldCorrectlySetThrowComputedExceptionBehaviour(
            int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9, int v10, int v11, int v12, int v13)
        {
            var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13));
            var setup = new Setup<ISubject>(methodCallSetup);

            setup.Throws((int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _) => new Exception()).ShouldBeSameAs(setup);

            (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as ThrowComputedExceptionBehaviour).ShouldNotBeNull();
        }

        [Theory]
        [AutoData]
        public void Throws_WithFourteenParameterFunc_ShouldCorrectlySetThrowComputedExceptionBehaviour(
            int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9, int v10, int v11, int v12, int v13, int v14)
        {
            var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14));
            var setup = new Setup<ISubject>(methodCallSetup);

            setup.Throws((int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _) => new Exception()).ShouldBeSameAs(setup);

            (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as ThrowComputedExceptionBehaviour).ShouldNotBeNull();
        }

        [Theory]
        [AutoData]
        public void Throws_WithFifteenParameterFunc_ShouldCorrectlySetThrowComputedExceptionBehaviour(
            int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9, int v10, int v11, int v12, int v13, int v14, int v15)
        {
            var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15));
            var setup = new Setup<ISubject>(methodCallSetup);

            setup.Throws((int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _) => new Exception()).ShouldBeSameAs(setup);

            (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as ThrowComputedExceptionBehaviour).ShouldNotBeNull();
        }

        [Theory]
        [AutoData]
        public void Throws_WithSixteenParameterFunc_ShouldCorrectlySetThrowComputedExceptionBehaviour(
            int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9, int v10, int v11, int v12, int v13, int v14, int v15, int v16)
        {
            var methodCallSetup = ToMethodCallSetup(m => m.MethodWithParameters(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16));
            var setup = new Setup<ISubject>(methodCallSetup);

            setup.Throws((int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _, int _) => new Exception()).ShouldBeSameAs(setup);

            (methodCallSetup.ConfiguredBehaviours.ReturnOrThrow as ThrowComputedExceptionBehaviour).ShouldNotBeNull();
        }

        #endregion

        [Theory, AutoData]
        public void WithDelay_WhenCalledWithValue_ShouldCorrectlySetDelayBehaviour(int milliseconds)
        {
            var methodCallSetup = ToMethodCallSetup(m => m.MethodWithNoParameters());
            var setup = new Setup<ISubject>(methodCallSetup);

            setup.WithDelay(TimeSpan.FromMilliseconds(milliseconds)).ShouldBeSameAs(setup);

            methodCallSetup.ConfiguredBehaviours.Delay.ShouldNotBeNull();
        }

        [Theory, AutoData]
        public void WithDelay_WhenCalledWithFunction_ShouldCorrectlySetDelayBehaviour(int milliseconds)
        {
            var methodCallSetup = ToMethodCallSetup(m => m.MethodWithNoParameters());
            var setup = new Setup<ISubject>(methodCallSetup);

            setup.WithDelay(_ => TimeSpan.FromMilliseconds(milliseconds)).ShouldBeSameAs(setup);

            methodCallSetup.ConfiguredBehaviours.Delay.ShouldNotBeNull();
        }

        [Theory]
        [AutoData]
        public void Limit_ShouldCorrectlySetExecutionLimitBehaviour(int executionLimit)
        {
            var methodCallSetup = ToMethodCallSetup(m => m.MethodWithNoParameters());
            var setup = new Setup<ISubject>(methodCallSetup);

            setup.Limit(executionLimit).ShouldBeSameAs(setup);

            methodCallSetup.ConfiguredBehaviours.ExecutionLimit.ShouldNotBeNull();
        }

        [Fact]
        public void Proceed_ShouldReturnInitializedSequenceSetup()
        {
            var methodCallSetup = ToMethodCallSetup<AbstractSubject>(m => m.VirtualMethod());
            var setup = new Setup<ISubject>(methodCallSetup);

            setup.Proceed().ShouldBeSameAs(setup);

            methodCallSetup.ConfiguredBehaviours.ReturnOrThrow.ShouldNotBeNull();
            methodCallSetup.ConfiguredBehaviours.ReturnOrThrow.ShouldBeSameAs(ProceedBehaviour.Instance);
        }

        [Fact]
        public void AsSequence_ShouldReturnInitializedSequenceSetup()
        {
            var methodCallSetup = ToMethodCallSetup(m => m.MethodWithNoParameters());
            var setup = new Setup<ISubject>(methodCallSetup);

            var sequenceSetup = setup.AsSequence();
            sequenceSetup.ShouldNotBeNull();
            sequenceSetup.ShouldBeOfType<SequenceSetup>();

            methodCallSetup.ConfiguredBehaviours.ReturnOrThrow.ShouldBeNull();
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
            public void MethodWithNoParameters();
            public void MethodWithParameters(int v1);
            public void MethodWithParameters(int v1, int v2);
            public void MethodWithParameters(int v1, int v2, int v3);
            public void MethodWithParameters(int v1, int v2, int v3, int v4);
            public void MethodWithParameters(int v1, int v2, int v3, int v4, int v5);
            public void MethodWithParameters(int v1, int v2, int v3, int v4, int v5, int v6);
            public void MethodWithParameters(int v1, int v2, int v3, int v4, int v5, int v6, int v7);
            public void MethodWithParameters(int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8);
            public void MethodWithParameters(int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9);
            public void MethodWithParameters(int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9, int v10);
            public void MethodWithParameters(int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9, int v10, int v11);
            public void MethodWithParameters(int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9, int v10, int v11, int v12);
            public void MethodWithParameters(int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9, int v10, int v11, int v12, int v13);
            public void MethodWithParameters(int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9, int v10, int v11, int v12, int v13, int v14);
            public void MethodWithParameters(int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9, int v10, int v11, int v12, int v13, int v14, int v15);
            public void MethodWithParameters(int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9, int v10, int v11, int v12, int v13, int v14, int v15, int v16);
        }

        // ReSharper disable once MemberCanBePrivate.Global
        internal abstract class AbstractSubject
        {
            public virtual void VirtualMethod()
            {
            }
        }
    }
}
