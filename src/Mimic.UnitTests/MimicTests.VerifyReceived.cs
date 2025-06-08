using Mimic.Exceptions;

namespace Mimic.UnitTests;

public partial class MimicTests
{
    public static class VerifyReceived
    {
        [Theory]
        [AutoData]
        public static void VerifyExpectedReceived_WithAllExpectedCallsReceived_ShouldNotThrow(
            int iValue, string sValue, List<bool> lValue, string rValue)
        {
            var mimic = new Mimic<ISubject> { Strict = false };

            mimic.Setup(m => m.VoidMethod())
                .Expected();

            mimic.Setup(m => m.StringMethod(
                    Arg.Any<int>(), Arg.AnyNotNull<string>(), Arg.Is<List<bool>>(l => l.Count == lValue.Count)))
                .Returns(rValue)
                .Expected();

            mimic.Setup(m => m.StringMethod(Random.Shared.Next(), "Unmatched", Arg.Any<List<bool>>()))
                .Returns("Should not be expected");

            mimic.Setup(m => m.GetNestedSubject().NestedVoidMethod()).Expected();

            mimic.Setup(m => m.GetNestedSubject().NestedStringMethod(
                    Arg.In(Random.Shared.Next(), iValue, Random.Shared.Next()), sValue, Arg.Any<List<bool>>()))
                .Returns(rValue)
                .Expected();

            mimic.Object.VoidMethod();
            mimic.Object.StringMethod(iValue, sValue, lValue);
            mimic.Object.GetNestedSubject().NestedVoidMethod();
            mimic.Object.GetNestedSubject().NestedStringMethod(iValue, sValue, lValue);

            Should.NotThrow(() => mimic.VerifyExpectedReceived());
        }

        [Theory]
        [AutoData]
        public static void VerifyExpectedReceived_WithExpectedVoidMethodCallUnreceived_ShouldThrowMimicException(
            int iValue, string sValue, List<bool> lValue, string rValue)
        {
            var mimic = new Mimic<ISubject> { Strict = false };

            mimic.Setup(m => m.VoidMethod())
                .Expected();

            mimic.Setup(m => m.StringMethod(
                    Arg.Any<int>(), Arg.AnyNotNull<string>(), Arg.Is<List<bool>>(l => l.Count == lValue.Count)))
                .Returns(rValue)
                .Expected();

            mimic.Setup(m => m.StringMethod(Random.Shared.Next(), "Unmatched", Arg.Any<List<bool>>()))
                .Returns("Should not be expected");

            mimic.Setup(m => m.GetNestedSubject().NestedVoidMethod()).Expected();

            mimic.Setup(m => m.GetNestedSubject().NestedStringMethod(
                    Arg.In(Random.Shared.Next(), iValue, Random.Shared.Next()), sValue, Arg.Any<List<bool>>()))
                .Returns(rValue)
                .Expected();

            mimic.Object.StringMethod(iValue, sValue, lValue);
            mimic.Object.GetNestedSubject().NestedVoidMethod();
            mimic.Object.GetNestedSubject().NestedStringMethod(iValue, sValue, lValue);

            var ex = Should.Throw<MimicException>(() => mimic.VerifyExpectedReceived());
            ex.Message.ShouldBe("Setup 'MimicTests.VerifyReceived.ISubject m => m.VoidMethod()' which was marked as expected has not been matched.");
            ex.Reason.ShouldBe(Reason.ExpectationFailed);
        }

        [Theory]
        [AutoData]
        public static void VerifyExpectedReceived_WithExpectedMethodCalledWithDifferentArguments_ShouldThrowMimicException(
            int iValue, string sValue, List<bool> lValue, string rValue)
        {
            var mimic = new Mimic<ISubject> { Strict = false };

            mimic.Setup(m => m.VoidMethod())
                .Expected();

            mimic.Setup(m => m.StringMethod(
                    Arg.Any<int>(), Arg.AnyNotNull<string>(), Arg.Is<List<bool>>(l => l.Count == lValue.Count)))
                .Returns(rValue)
                .Expected();

            mimic.Setup(m => m.StringMethod(Random.Shared.Next(), "Unmatched", Arg.Any<List<bool>>()))
                .Returns("Should not be expected");

            mimic.Setup(m => m.GetNestedSubject().NestedVoidMethod()).Expected();

            mimic.Setup(m => m.GetNestedSubject().NestedStringMethod(
                    Arg.In(Random.Shared.Next(), iValue, Random.Shared.Next()), sValue, Arg.Any<List<bool>>()))
                .Returns(rValue)
                .Expected();

            mimic.Object.VoidMethod();
            mimic.Object.StringMethod(iValue, null!, lValue);
            mimic.Object.GetNestedSubject().NestedVoidMethod();
            mimic.Object.GetNestedSubject().NestedStringMethod(iValue, sValue, lValue);

            var ex = Should.Throw<MimicException>(() => mimic.VerifyExpectedReceived());
            ex.Message.ShouldBe("Setup 'MimicTests.VerifyReceived.ISubject m => m.StringMethod(Any(), AnyNotNull(), Is(l => (l.Count == value(Mimic.UnitTests.MimicTests+VerifyReceived+<>c__DisplayClass2_0).lValue.Count)))' which was marked as expected has not been matched.");
            ex.Reason.ShouldBe(Reason.ExpectationFailed);
        }

        [Theory]
        [AutoData]
        public static void VerifyExpectedReceived_WithExpectedNestedVoidMethodCallUnreceived_ShouldThrowMimicException(
            int iValue, string sValue, List<bool> lValue, string rValue)
        {
            var mimic = new Mimic<ISubject> { Strict = false };

            mimic.Setup(m => m.VoidMethod())
                .Expected();

            mimic.Setup(m => m.StringMethod(
                    Arg.Any<int>(), Arg.AnyNotNull<string>(), Arg.Is<List<bool>>(l => l.Count == lValue.Count)))
                .Returns(rValue)
                .Expected();

            mimic.Setup(m => m.StringMethod(Random.Shared.Next(), "Unmatched", Arg.Any<List<bool>>()))
                .Returns("Should not be expected");

            mimic.Setup(m => m.GetNestedSubject().NestedVoidMethod()).Expected();

            mimic.Setup(m => m.GetNestedSubject().NestedStringMethod(
                    Arg.In(Random.Shared.Next(), iValue, Random.Shared.Next()), sValue, Arg.Any<List<bool>>()))
                .Returns(rValue)
                .Expected();

            mimic.Object.VoidMethod();
            mimic.Object.StringMethod(iValue, sValue, lValue);
            mimic.Object.GetNestedSubject().NestedStringMethod(iValue, sValue, lValue);

            var ex = Should.Throw<MimicException>(() => mimic.VerifyExpectedReceived());
            ex.Message.ShouldBe("Setup 'MimicTests.VerifyReceived.INestedSubject ... => ....NestedVoidMethod()' which was marked as expected has not been matched.");
            ex.Reason.ShouldBe(Reason.ExpectationFailed);
        }

        [Theory]
        [AutoData]
        public static void VerifyExpectedReceived_WithExpectedNestedMethodCalledWithDifferentArguments_ShouldThrowMimicException(
            int iValue, string sValue, List<bool> lValue, string rValue)
        {
            var mimic = new Mimic<ISubject> { Strict = false };

            mimic.Setup(m => m.VoidMethod())
                .Expected();

            mimic.Setup(m => m.StringMethod(
                    Arg.Any<int>(), Arg.AnyNotNull<string>(), Arg.Is<List<bool>>(l => l.Count == lValue.Count)))
                .Returns(rValue)
                .Expected();

            mimic.Setup(m => m.StringMethod(Random.Shared.Next(), "Unmatched", Arg.Any<List<bool>>()))
                .Returns("Should not be expected");

            mimic.Setup(m => m.GetNestedSubject().NestedVoidMethod()).Expected();

            mimic.Setup(m => m.GetNestedSubject().NestedStringMethod(
                    Arg.In(Random.Shared.Next(), iValue, Random.Shared.Next()), Arg.AnyNotNull<string>(), Arg.Any<List<bool>>()))
                .Returns(rValue)
                .Expected();

            mimic.Object.VoidMethod();
            mimic.Object.StringMethod(iValue, sValue, lValue);
            mimic.Object.GetNestedSubject().NestedVoidMethod();
            mimic.Object.GetNestedSubject().NestedStringMethod(Random.Shared.Next(), sValue, lValue);

            var ex = Should.Throw<MimicException>(() => mimic.VerifyExpectedReceived());
            ex.Message.ShouldBe("Setup 'MimicTests.VerifyReceived.INestedSubject ... => ....NestedStringMethod(In(value(System.Int32[])), AnyNotNull(), Any())' which was marked as expected has not been matched.");
            ex.Reason.ShouldBe(Reason.ExpectationFailed);
        }

        [Theory]
        [AutoData]
        public static void VerifyAllSetupsReceived_WithAllSetupCallsReceived_ShouldNotThrow(
            int iValue, string sValue, List<bool> lValue, string rValue)
        {
            var mimic = new Mimic<ISubject> { Strict = false };

            mimic.Setup(m => m.VoidMethod());

            mimic.Setup(m => m.StringMethod(Arg.Any<int>(), Arg.AnyNotNull<string>(), Arg.Any<List<bool>>()))
                .Returns(rValue);

            mimic.Setup(m => m.GetNestedSubject().NestedVoidMethod());

            mimic.Setup(m => m.GetNestedSubject().NestedStringMethod(Arg.Any<int>(), Arg.AnyNotNull<string>(), Arg.Any<List<bool>>()))
                .Returns(rValue);

            mimic.Object.VoidMethod();
            mimic.Object.StringMethod(iValue, sValue, lValue);
            mimic.Object.GetNestedSubject().NestedVoidMethod();
            mimic.Object.GetNestedSubject().NestedStringMethod(iValue, sValue, lValue);

            Should.NotThrow(() => mimic.VerifyAllSetupsReceived());
        }

        [Theory]
        [AutoData]
        public static void VerifyAllSetupsReceived_WithNotAllSetupCallsReceived_ShouldThrowMimicException(
            int iValue, string sValue, List<bool> lValue, string rValue)
        {
            var mimic = new Mimic<ISubject> { Strict = false };

            mimic.Setup(m => m.VoidMethod());

            mimic.Setup(m => m.StringMethod(Arg.Any<int>(), Arg.AnyNotNull<string>(), Arg.Any<List<bool>>()))
                .Returns(rValue);

            mimic.Setup(m => m.GetNestedSubject().NestedVoidMethod());

            mimic.Setup(m => m.GetNestedSubject().NestedStringMethod(Arg.Any<int>(), Arg.AnyNotNull<string>(), Arg.Any<List<bool>>()))
                .Returns(rValue);

            mimic.Object.StringMethod(iValue, sValue, lValue);
            mimic.Object.GetNestedSubject().NestedVoidMethod();
            mimic.Object.GetNestedSubject().NestedStringMethod(iValue, sValue, lValue);

            var ex = Should.Throw<MimicException>(() => mimic.VerifyAllSetupsReceived());
            ex.Message.ShouldBe("Setup 'MimicTests.VerifyReceived.ISubject m => m.VoidMethod()' which was marked as expected has not been matched.");
            ex.Reason.ShouldBe(Reason.ExpectationFailed);
        }

        [Theory]
        [AutoData]
        public static void VerifyAllSetupsReceived_WithNotAllNestedSetupCallsReceived_ShouldThrowMimicException(
            int iValue, string sValue, List<bool> lValue, string rValue)
        {
            var mimic = new Mimic<ISubject> { Strict = false };

            mimic.Setup(m => m.VoidMethod());

            mimic.Setup(m => m.StringMethod(Arg.Any<int>(), Arg.AnyNotNull<string>(), Arg.Any<List<bool>>()))
                .Returns(rValue);

            mimic.Setup(m => m.GetNestedSubject().NestedVoidMethod());

            mimic.Setup(m => m.GetNestedSubject().NestedStringMethod(Arg.Any<int>(), Arg.AnyNotNull<string>(), Arg.Any<List<bool>>()))
                .Returns(rValue);

            mimic.Object.VoidMethod();
            mimic.Object.StringMethod(iValue, sValue, lValue);
            mimic.Object.GetNestedSubject().NestedVoidMethod();
            mimic.Object.GetNestedSubject().NestedStringMethod(iValue, null!, lValue);

            var ex = Should.Throw<MimicException>(() => mimic.VerifyAllSetupsReceived());
            ex.Message.ShouldBe("Setup 'MimicTests.VerifyReceived.INestedSubject ... => ....NestedStringMethod(Any(), AnyNotNull(), Any())' which was marked as expected has not been matched.");
            ex.Reason.ShouldBe(Reason.ExpectationFailed);
        }

        // ReSharper disable once MemberHidesStaticFromOuterClass
        internal interface ISubject
        {
            void VoidMethod();

            string StringMethod(int iValue, string sValue, List<bool> lValue);

            INestedSubject GetNestedSubject();
        }

        internal interface INestedSubject
        {
            void NestedVoidMethod();

            string NestedStringMethod(int iValue, string sValue, List<bool> lValue);
        }
    }
}
