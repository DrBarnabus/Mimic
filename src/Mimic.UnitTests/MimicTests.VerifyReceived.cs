using System.ComponentModel.DataAnnotations;
using Mimic.Exceptions;

namespace Mimic.UnitTests;

public partial class MimicTests
{
    public static class VerifyReceived
    {
        #region VerifyExpectedReceived

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

        #endregion

        #region VerifyAllSetupsReceived

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

        #endregion

        #region VerifyNoOtherCallsReceived

        [Theory, AutoData]
        public static void VerifyNoOtherCallsReceived_WhenNoOtherCallsReceived_ShouldNotThrow(
            int iValue, string sValue, List<bool> lValue, string sValueTwo)
        {
            var mimic = new Mimic<ISubject> { Strict = false };

            mimic.Setup(m => m.VoidMethod())
                .Expected();

            mimic.Setup(m => m.StringMethod(iValue, sValue, lValue))
                .Expected();

            mimic.Setup(m => m.GetNestedSubject().NestedStringMethod(iValue, sValueTwo, Arg.Any<List<bool>>()))
                .Expected();

            mimic.Object.VoidMethod();
            mimic.Object.StringMethod(Random.Shared.Next(), sValue, [..lValue, false]);
            mimic.Object.StringMethod(iValue, sValue, lValue);
            mimic.Object.GetNestedSubject().NestedVoidMethod();
            mimic.Object.GetNestedSubject().NestedStringMethod(iValue, Guid.NewGuid().ToString(), []);
            mimic.Object.GetNestedSubject().NestedStringMethod(iValue, sValueTwo, lValue);

            mimic.VerifyExpectedReceived();

            mimic.VerifyReceived(m => m.StringMethod(Arg.Any<int>(), sValue, Arg.Is<List<bool>>(l => l.Count == lValue.Count + 1)));

            mimic.VerifyReceived(m => m.GetNestedSubject().NestedVoidMethod());

            mimic.VerifyReceived(m => m.GetNestedSubject().NestedStringMethod(iValue, Arg.AnyNotNull<string>(), Arg.Is<List<bool>>(l => l.Count == 0)));

            Should.NotThrow(() => mimic.VerifyNoOtherCallsReceived());
        }

        [Theory, AutoData]
        public static void VerifyNoOtherCallsReceived_WhenOtherCallsReceived_ShouldThrow(
            int iValue, string sValue, List<bool> lValue, string sValueTwo)
        {
            var mimic = new Mimic<ISubject> { Strict = false };

            mimic.Setup(m => m.VoidMethod())
                .Expected();

            mimic.Setup(m => m.StringMethod(iValue, sValue, lValue))
                .Expected();

            mimic.Setup(m => m.GetNestedSubject().NestedStringMethod(iValue, sValueTwo, Arg.Any<List<bool>>()))
                .Expected();

            mimic.Object.VoidMethod();
            mimic.Object.StringMethod(Random.Shared.Next(), sValue, [..lValue, false]);
            mimic.Object.StringMethod(iValue, sValue, lValue);

            (int unexpectedInt, string unexpectedString) = (Random.Shared.Next(), Guid.NewGuid().ToString());
            mimic.Object.StringMethod(unexpectedInt, unexpectedString, lValue);

            mimic.Object.GetNestedSubject().NestedVoidMethod();
            mimic.Object.GetNestedSubject().NestedStringMethod(iValue, Guid.NewGuid().ToString(), []);
            mimic.Object.GetNestedSubject().NestedStringMethod(iValue, sValueTwo, lValue);

            mimic.VerifyExpectedReceived();

            mimic.VerifyReceived(m => m.StringMethod(Arg.Any<int>(), sValue, Arg.Is<List<bool>>(l => l.Count == lValue.Count + 1)));

            mimic.VerifyReceived(m => m.GetNestedSubject().NestedVoidMethod());

            mimic.VerifyReceived(m => m.GetNestedSubject().NestedStringMethod(iValue, Arg.AnyNotNull<string>(), Arg.Is<List<bool>>(l => l.Count == 0)));

            var ex = Should.Throw<MimicException>(() => mimic.VerifyNoOtherCallsReceived());
            ex.Message.ShouldBe($"""
                                 {mimic}: Verification failed due to the following unverified invocations:
                                     MimicTests.VerifyReceived.ISubject.StringMethod({unexpectedInt}, "{unexpectedString}", [{string.Join(", ", lValue)}])

                                 """.ReplaceLineEndings(Environment.NewLine));
            ex.Reason.ShouldBe(Reason.ExpectationFailed);
        }

        [Theory, AutoData]
        public static void VerifyNoOtherCallsReceived_WhenOtherCallsReceivedInNestedMimic_ShouldThrow(
            int iValue, string sValue, List<bool> lValue, string sValueTwo)
        {
            var mimic = new Mimic<ISubject> { Strict = false };

            mimic.Setup(m => m.VoidMethod())
                .Expected();

            mimic.Setup(m => m.StringMethod(iValue, sValue, lValue))
                .Expected();

            mimic.Setup(m => m.GetNestedSubject().NestedStringMethod(iValue, sValueTwo, Arg.Any<List<bool>>()))
                .Expected();

            mimic.Object.VoidMethod();
            mimic.Object.StringMethod(Random.Shared.Next(), sValue, [..lValue, false]);
            mimic.Object.StringMethod(iValue, sValue, lValue);

            mimic.Object.GetNestedSubject().NestedVoidMethod();
            mimic.Object.GetNestedSubject().NestedStringMethod(iValue, Guid.NewGuid().ToString(), []);
            mimic.Object.GetNestedSubject().NestedStringMethod(iValue, sValueTwo, lValue);

            (int unexpectedInt, string unexpectedString) = (Random.Shared.Next(), Guid.NewGuid().ToString());
            mimic.Object.GetNestedSubject().NestedStringMethod(unexpectedInt, unexpectedString, lValue);

            mimic.VerifyExpectedReceived();

            mimic.VerifyReceived(m => m.StringMethod(Arg.Any<int>(), sValue, Arg.Is<List<bool>>(l => l.Count == lValue.Count + 1)));

            mimic.VerifyReceived(m => m.GetNestedSubject().NestedVoidMethod());

            mimic.VerifyReceived(m => m.GetNestedSubject().NestedStringMethod(iValue, Arg.AnyNotNull<string>(), Arg.Is<List<bool>>(l => l.Count == 0)));

            var nestedMimic = Mimic<INestedSubject>.FromObject(mimic.Object.GetNestedSubject());
            var ex = Should.Throw<MimicException>(() => nestedMimic.VerifyNoOtherCallsReceived());
            ex.Message.ShouldBe($"""
                                 {nestedMimic}: Verification failed due to the following unverified invocations:
                                     MimicTests.VerifyReceived.INestedSubject.NestedStringMethod({unexpectedInt}, "{unexpectedString}", [{string.Join(", ", lValue)}])

                                 """.ReplaceLineEndings(Environment.NewLine));
            ex.Reason.ShouldBe(Reason.ExpectationFailed);
        }

        #endregion

        public static class WithNoReturnValue
        {
            [Fact]
            public static void VerifyReceived_WithNoCallCountAndExpectedCallReceived_ShouldNotThrow()
            {
                var mimic = new Mimic<ISubject> { Strict = false };

                mimic.Object.VoidMethod();

                Should.NotThrow(() => mimic.VerifyReceived(m => m.VoidMethod()));
            }

            [Fact]
            public static void VerifyReceived_WithNoCallCountAndExpectedCallUnreceived_ShouldThrow()
            {
                var mimic = new Mimic<ISubject> { Strict = false };

                var ex = Should.Throw<MimicException>(() => mimic.VerifyReceived(m => m.VoidMethod()));
                ex.Message.ShouldBe($"""
                                    Verification failed with incorrect matching invocations
                                    Expected at least one invocation on the mimic, but it was never invoked: m => m.VoidMethod()
                                    Actual invocations on {mimic} (m):
                                        There are zero invocations...

                                    """.ReplaceLineEndings(Environment.NewLine));
                ex.Reason.ShouldBe(Reason.ExpectationFailed);
            }

            [Theory, AutoData]
            public static void VerifyReceived_WithNoCallCountAndFailureMessageAndExpectedCallUnreceived_ShouldThrowWithCorrectMessage(string failureMessage)
            {
                var mimic = new Mimic<ISubject> { Strict = false };

                var ex = Should.Throw<MimicException>(() => mimic.VerifyReceived(m => m.VoidMethod(), failureMessage));
                ex.Message.ShouldBe($"""
                                     {failureMessage}
                                     Expected at least one invocation on the mimic, but it was never invoked: m => m.VoidMethod()
                                     Actual invocations on {mimic} (m):
                                         There are zero invocations...

                                     """.ReplaceLineEndings(Environment.NewLine));
                ex.Reason.ShouldBe(Reason.ExpectationFailed);
            }

            [Theory, AutoData]
            public static void VerifyReceived_WithCallCountAndExpectedCallsReceived_ShouldNotThrow([Range(1, 5)] int callCount)
            {
                var mimic = new Mimic<ISubject> { Strict = false };

                for (int i = 0; i < callCount; i++)
                    mimic.Object.VoidMethod();

                Should.NotThrow(() => mimic.VerifyReceived(m => m.VoidMethod(), CallCount.AtLeast(callCount)));
            }

            [Theory, AutoData]
            public static void VerifyReceived_WithCallCountAndLessThanExpectedCallsReceived_ShouldThrow([Range(2, 5)] int callCount)
            {
                var mimic = new Mimic<ISubject> { Strict = false };

                for (int i = 0; i < callCount - 1; i++)
                    mimic.Object.VoidMethod();

                var ex = Should.Throw<MimicException>(() => mimic.VerifyReceived(m => m.VoidMethod(), CallCount.AtLeast(callCount)));
                ex.Message.ShouldStartWith($"""
                                     Verification failed with incorrect matching invocations
                                     Expected at least {callCount} invocation(s) on the mimic, but it was invoked {callCount - 1} time(s): m => m.VoidMethod()
                                     Actual invocations on {mimic} (m):
                                     """.ReplaceLineEndings(Environment.NewLine));
                ex.Message.ShouldContain("MimicTests.VerifyReceived.ISubject.VoidMethod()");
                ex.Reason.ShouldBe(Reason.ExpectationFailed);
            }

            [Theory, AutoData]
            public static void VerifyReceived_WithCallCountAndMoreThanExpectedCallsReceived_ShouldThrow([Range(2, 5)] int callCount)
            {
                var mimic = new Mimic<ISubject> { Strict = false };

                for (int i = 0; i < callCount + 1; i++)
                    mimic.Object.VoidMethod();

                var ex = Should.Throw<MimicException>(() => mimic.VerifyReceived(m => m.VoidMethod(), CallCount.AtMost(callCount)));
                ex.Message.ShouldStartWith($"""
                                            Verification failed with incorrect matching invocations
                                            Expected at most {callCount} invocation(s) on the mimic, but it was invoked {callCount + 1} time(s): m => m.VoidMethod()
                                            Actual invocations on {mimic} (m):
                                            """.ReplaceLineEndings(Environment.NewLine));
                ex.Message.ShouldContain("MimicTests.VerifyReceived.ISubject.VoidMethod()");
                ex.Reason.ShouldBe(Reason.ExpectationFailed);
            }

            [Theory, AutoData]
            public static void VerifyReceived_WithCallCountNeverAndUnexpectedCallReceived_ShouldThrowWithFailureMessage(string failureMessage)
            {
                var mimic = new Mimic<ISubject> { Strict = false };

                mimic.Object.VoidMethod();

                var ex = Should.Throw<MimicException>(() => mimic.VerifyReceived(m => m.VoidMethod(), CallCount.Never(), failureMessage));
                ex.Message.ShouldStartWith($"""
                                            {failureMessage}
                                            Expected zero invocations on the mimic, but it was invoked 1 time(s): m => m.VoidMethod()
                                            Actual invocations on {mimic} (m):
                                            """.ReplaceLineEndings(Environment.NewLine));
                ex.Message.ShouldContain("MimicTests.VerifyReceived.ISubject.VoidMethod()");
                ex.Reason.ShouldBe(Reason.ExpectationFailed);
            }
        }

        public static class WithReturnValue
        {
            [Theory, AutoData]
            public static void VerifyReceived_WithNoCallCountAndExpectedCallReceived_ShouldNotThrow(
                int iValue, string sValue, List<bool> lValue)
            {
                var mimic = new Mimic<ISubject> { Strict = false };

                mimic.Object.StringMethod(iValue, sValue, lValue);

                Should.NotThrow(() => mimic.VerifyReceived(m => m.StringMethod(iValue, sValue, lValue)));
            }

            [Theory, AutoData]
            public static void VerifyReceived_WithNoCallCountAndExpectedCallUnreceived_ShouldThrow(
                int iValue, string sValue, List<bool> lValue)
            {
                var mimic = new Mimic<ISubject> { Strict = false };

                var ex = Should.Throw<MimicException>(() => mimic.VerifyReceived(m => m.StringMethod(iValue, sValue, lValue)));
                ex.Message.ShouldBe($"""
                                    Verification failed with incorrect matching invocations
                                    Expected at least one invocation on the mimic, but it was never invoked: m => m.StringMethod({iValue}, "{sValue}", value(System.Collections.Generic.List`1[System.Boolean]))
                                    Actual invocations on {mimic} (m):
                                        There are zero invocations...

                                    """.ReplaceLineEndings(Environment.NewLine));
                ex.Reason.ShouldBe(Reason.ExpectationFailed);
            }

            [Theory, AutoData]
            public static void VerifyReceived_WithNoCallCountAndFailureMessageAndExpectedCallUnreceived_ShouldThrowWithCorrectMessage(
                int iValue, string sValue, List<bool> lValue, string failureMessage)
            {
                var mimic = new Mimic<ISubject> { Strict = false };

                var ex = Should.Throw<MimicException>(() => mimic.VerifyReceived(m => m.StringMethod(iValue, sValue, lValue), failureMessage));
                ex.Message.ShouldBe($"""
                                     {failureMessage}
                                     Expected at least one invocation on the mimic, but it was never invoked: m => m.StringMethod({iValue}, "{sValue}", value(System.Collections.Generic.List`1[System.Boolean]))
                                     Actual invocations on {mimic} (m):
                                         There are zero invocations...

                                     """.ReplaceLineEndings(Environment.NewLine));
                ex.Reason.ShouldBe(Reason.ExpectationFailed);
            }

            [Theory, AutoData]
            public static void VerifyReceived_WithCallCountAndExpectedCallsReceived_ShouldNotThrow(
                int iValue, string sValue, List<bool> lValue, [Range(1, 5)] int callCount)
            {
                var mimic = new Mimic<ISubject> { Strict = false };

                for (int i = 0; i < callCount; i++)
                    mimic.Object.StringMethod(iValue, sValue, lValue);

                Should.NotThrow(() => mimic.VerifyReceived(m => m.StringMethod(iValue, sValue, lValue), CallCount.AtLeast(callCount)));
            }

            [Theory, AutoData]
            public static void VerifyReceived_WithCallCountAndLessThanExpectedCallsReceived_ShouldThrow(
                int iValue, string sValue, List<bool> lValue, [Range(2, 5)] int callCount)
            {
                var mimic = new Mimic<ISubject> { Strict = false };

                for (int i = 0; i < callCount - 1; i++)
                    mimic.Object.StringMethod(iValue, sValue, lValue);

                var ex = Should.Throw<MimicException>(() => mimic.VerifyReceived(m => m.StringMethod(iValue, sValue, lValue), CallCount.AtLeast(callCount)));
                ex.Message.ShouldStartWith($"""
                                     Verification failed with incorrect matching invocations
                                     Expected at least {callCount} invocation(s) on the mimic, but it was invoked {callCount - 1} time(s): m => m.StringMethod({iValue}, "{sValue}", value(System.Collections.Generic.List`1[System.Boolean]))
                                     Actual invocations on {mimic} (m):
                                     """.ReplaceLineEndings(Environment.NewLine));
                ex.Message.ShouldContain($"MimicTests.VerifyReceived.ISubject.StringMethod({iValue}, \"{sValue}\", [{string.Join(", ", lValue)}])");
                ex.Reason.ShouldBe(Reason.ExpectationFailed);
            }

            [Theory, AutoData]
            public static void VerifyReceived_WithCallCountAndMoreThanExpectedCallsReceived_ShouldThrow(
                int iValue, string sValue, List<bool> lValue, [Range(2, 5)] int callCount)
            {
                var mimic = new Mimic<ISubject> { Strict = false };

                for (int i = 0; i < callCount + 1; i++)
                    mimic.Object.StringMethod(iValue, sValue, lValue);

                var ex = Should.Throw<MimicException>(() => mimic.VerifyReceived(m => m.StringMethod(iValue, sValue, lValue), CallCount.AtMost(callCount)));
                ex.Message.ShouldStartWith($"""
                                            Verification failed with incorrect matching invocations
                                            Expected at most {callCount} invocation(s) on the mimic, but it was invoked {callCount + 1} time(s): m => m.StringMethod({iValue}, "{sValue}", value(System.Collections.Generic.List`1[System.Boolean]))
                                            Actual invocations on {mimic} (m):
                                            """.ReplaceLineEndings(Environment.NewLine));
                ex.Message.ShouldContain($"MimicTests.VerifyReceived.ISubject.StringMethod({iValue}, \"{sValue}\", [{string.Join(", ", lValue)}])");
                ex.Reason.ShouldBe(Reason.ExpectationFailed);
            }

            [Theory, AutoData]
            public static void VerifyReceived_WithCallCountNeverAndUnexpectedCallReceived_ShouldThrowWithFailureMessage(
                int iValue, string sValue, List<bool> lValue, string failureMessage)
            {
                var mimic = new Mimic<ISubject> { Strict = false };

                mimic.Object.StringMethod(iValue, sValue, lValue);

                var ex = Should.Throw<MimicException>(() => mimic.VerifyReceived(m => m.StringMethod(iValue, sValue, lValue), CallCount.Never(), failureMessage));
                ex.Message.ShouldStartWith($"""
                                            {failureMessage}
                                            Expected zero invocations on the mimic, but it was invoked 1 time(s): m => m.StringMethod({iValue}, "{sValue}", value(System.Collections.Generic.List`1[System.Boolean]))
                                            Actual invocations on {mimic} (m):
                                            """.ReplaceLineEndings(Environment.NewLine));
                ex.Message.ShouldContain($"MimicTests.VerifyReceived.ISubject.StringMethod({iValue}, \"{sValue}\", [{string.Join(", ", lValue)}])");
                ex.Reason.ShouldBe(Reason.ExpectationFailed);
            }
        }

        public static class WithGetter
        {
            [Fact]
            public static void VerifyGetReceived_WithNoCallCountAndExpectedCallReceived_ShouldNotThrow()
            {
                var mimic = new Mimic<ISubject> { Strict = false };

                _ = mimic.Object.Property;

                Should.NotThrow(() => mimic.VerifyGetReceived(m => m.Property));
            }

            [Fact]
            public static void VerifyGetReceived_WithNoCallCountAndExpectedCallUnreceived_ShouldThrow()
            {
                var mimic = new Mimic<ISubject> { Strict = false };

                var ex = Should.Throw<MimicException>(() => mimic.VerifyGetReceived(m => m.Property));
                ex.Message.ShouldBe($"""
                                          Verification failed with incorrect matching invocations
                                          Expected at least one invocation on the mimic, but it was never invoked: m => m.Property
                                          Actual invocations on {mimic} (m):
                                              There are zero invocations...

                                          """.ReplaceLineEndings(Environment.NewLine));
                ex.Reason.ShouldBe(Reason.ExpectationFailed);
            }

            [Fact]
            public static void VerifyGetReceived_WithNonProperty_ShouldThrow()
            {
                var mimic = new Mimic<ISubject> { Strict = false };

                var ex = Should.Throw<MimicException>(() => mimic.VerifyGetReceived(m => m.GetNestedSubject()));
                ex.Message.ShouldBe("Expression (m => m.GetNestedSubject()) is not a property accessor.");
                ex.Reason.ShouldBe(Reason.UnsupportedExpression);
            }

            [Theory, AutoData]
            public static void VerifyGetReceived_WithNoCallCountAndFailureMessageAndExpectedCallUnreceived_ShouldThrowWithCorrectMessage(string failureMessage)
            {
                var mimic = new Mimic<ISubject> { Strict = false };

                var ex = Should.Throw<MimicException>(() => mimic.VerifyGetReceived(m => m.Property, failureMessage));
                ex.Message.ShouldBe($"""
                                          {failureMessage}
                                          Expected at least one invocation on the mimic, but it was never invoked: m => m.Property
                                          Actual invocations on {mimic} (m):
                                              There are zero invocations...

                                          """.ReplaceLineEndings(Environment.NewLine));
                ex.Reason.ShouldBe(Reason.ExpectationFailed);
            }

            [Theory, AutoData]
            public static void VerifyGetReceived_WithCallCountAndExpectedCallsReceived_ShouldNotThrow([Range(1, 5)] int callCount)
            {
                var mimic = new Mimic<ISubject> { Strict = false };

                for (int i = 0; i < callCount; i++)
                    _ = mimic.Object.Property;

                Should.NotThrow(() => mimic.VerifyGetReceived(m => m.Property, CallCount.Exactly(callCount)));
            }

            [Theory, AutoData]
            public static void VerifyGetReceived_WithCallCountAndLessThanExpectedCallsReceived_ShouldThrow([Range(2, 5)] int callCount)
            {
                var mimic = new Mimic<ISubject> { Strict = false };

                for (int i = 0; i < callCount - 1; i++)
                    _ = mimic.Object.Property;

                var ex = Should.Throw<MimicException>(() => mimic.VerifyGetReceived(m => m.Property, CallCount.AtLeast(callCount)));
                ex.Message.ShouldStartWith($"""
                                     Verification failed with incorrect matching invocations
                                     Expected at least {callCount} invocation(s) on the mimic, but it was invoked {callCount - 1} time(s): m => m.Property
                                     Actual invocations on {mimic} (m):
                                     """.ReplaceLineEndings(Environment.NewLine));
                ex.Message.ShouldContain("MimicTests.VerifyReceived.ISubject.Property");
                ex.Reason.ShouldBe(Reason.ExpectationFailed);
            }

            [Theory, AutoData]
            public static void VerifyGetReceived_WithCallCountAndMoreThanExpectedCallsReceived_ShouldThrow([Range(1, 5)] int callCount)
            {
                var mimic = new Mimic<ISubject> { Strict = false };

                for (int i = 0; i < callCount + 1; i++)
                    _ = mimic.Object.Property;

                var ex = Should.Throw<MimicException>(() => mimic.VerifyGetReceived(m => m.Property, CallCount.AtMost(callCount)));
                ex.Message.ShouldStartWith($"""
                                            Verification failed with incorrect matching invocations
                                            Expected at most {callCount} invocation(s) on the mimic, but it was invoked {callCount + 1} time(s): m => m.Property
                                            Actual invocations on {mimic} (m):
                                            """.ReplaceLineEndings(Environment.NewLine));
                ex.Message.ShouldContain("MimicTests.VerifyReceived.ISubject.Property");
                ex.Reason.ShouldBe(Reason.ExpectationFailed);
            }

            [Theory, AutoData]
            public static void VerifyGetReceived_WithCallCountAtMostOneAndUnexpectedCallReceived_ShouldThrowWithFailureMessage(string failureMessage)
            {
                var mimic = new Mimic<ISubject> { Strict = false };

                _ = mimic.Object.Property;
                _ = mimic.Object.Property;

                var ex = Should.Throw<MimicException>(() => mimic.VerifyGetReceived(m => m.Property, CallCount.AtMostOnce(), failureMessage));
                ex.Message.ShouldStartWith($"""
                                            {failureMessage}
                                            Expected at most one invocation on the mimic, but it was invoked 2 times: m => m.Property
                                            Actual invocations on {mimic} (m):
                                            """.ReplaceLineEndings(Environment.NewLine));
                ex.Message.ShouldContain("MimicTests.VerifyReceived.ISubject.Property");
                ex.Reason.ShouldBe(Reason.ExpectationFailed);
            }
        }

        public static class WithSetter
        {
            [Fact]
            public static void VerifySetReceived_WithNoCallCountAndExpectedCallReceived_ShouldNotThrow()
            {
                var mimic = new Mimic<ISubject> { Strict = false };

                _ = mimic.Object.Property = "value";

                Should.NotThrow(() => mimic.VerifySetReceived(m => m.Property = Arg.Any<string>()));
            }

            [Fact]
            public static void VerifySetReceived_WithNoCallCountAndExpectedCallUnreceived_ShouldThrow()
            {
                var mimic = new Mimic<ISubject> { Strict = false };

                var ex = Should.Throw<MimicException>(() => mimic.VerifySetReceived(m => m.Property = Arg.Any<string>()));
                ex.Message.ShouldBe($"""
                                          Verification failed with incorrect matching invocations
                                          Expected at least one invocation on the mimic, but it was never invoked: m => m.set_Property([Mimic.Expressions.ArgumentMatcherExpression])
                                          Actual invocations on {mimic} (m):
                                              There are zero invocations...

                                          """.ReplaceLineEndings(Environment.NewLine));
                ex.Reason.ShouldBe(Reason.ExpectationFailed);
            }

            [Fact]
            public static void VerifySetReceived_WithNonProperty_ShouldThrow()
            {
                var mimic = new Mimic<ISubject> { Strict = false };

                var ex = Should.Throw<MimicException>(() => mimic.VerifySetReceived(m => m.GetNestedSubject()));
                ex.Message.ShouldBe("Expression (m => m.GetNestedSubject()...) is unsupported, the expression threw an exception while Mimic tried to resolve the method to intercept.");
                ex.Reason.ShouldBe(Reason.UnsupportedExpression);
            }

            [Theory, AutoData]
            public static void VerifySetReceived_WithNoCallCountAndFailureMessageAndExpectedCallUnreceived_ShouldThrowWithCorrectMessage(string failureMessage)
            {
                var mimic = new Mimic<ISubject> { Strict = false };

                var ex = Should.Throw<MimicException>(() => mimic.VerifySetReceived(m => m.Property = Arg.Any<string>(), failureMessage));
                ex.Message.ShouldBe($"""
                                          {failureMessage}
                                          Expected at least one invocation on the mimic, but it was never invoked: m => m.set_Property([Mimic.Expressions.ArgumentMatcherExpression])
                                          Actual invocations on {mimic} (m):
                                              There are zero invocations...

                                          """.ReplaceLineEndings(Environment.NewLine));
                ex.Reason.ShouldBe(Reason.ExpectationFailed);
            }

            [Theory, AutoData]
            public static void VerifySetReceived_WithCallCountAndExpectedCallsReceived_ShouldNotThrow([Range(1, 5)] int callCount)
            {
                var mimic = new Mimic<ISubject> { Strict = false };

                for (int i = 0; i < callCount; i++)
                    _ = mimic.Object.Property = "value";

                Should.NotThrow(() => mimic.VerifySetReceived(m => m.Property = Arg.Any<string>(), CallCount.Exactly(callCount)));
            }

            [Theory, AutoData]
            public static void VerifySetReceived_WithCallCountAndLessThanExpectedCallsReceived_ShouldThrow([Range(2, 5)] int callCount)
            {
                var mimic = new Mimic<ISubject> { Strict = false };

                for (int i = 0; i < callCount - 1; i++)
                    _ = mimic.Object.Property = "value";

                var ex = Should.Throw<MimicException>(() => mimic.VerifySetReceived(m => m.Property = Arg.Any<string>(), CallCount.AtLeast(callCount)));
                ex.Message.ShouldStartWith($"""
                                     Verification failed with incorrect matching invocations
                                     Expected at least {callCount} invocation(s) on the mimic, but it was invoked {callCount - 1} time(s): m => m.set_Property([Mimic.Expressions.ArgumentMatcherExpression])
                                     Actual invocations on {mimic} (m):
                                     """.ReplaceLineEndings(Environment.NewLine));
                ex.Message.ShouldContain("MimicTests.VerifyReceived.ISubject.Property");
                ex.Reason.ShouldBe(Reason.ExpectationFailed);
            }

            [Theory, AutoData]
            public static void VerifySetReceived_WithCallCountAndMoreThanExpectedCallsReceived_ShouldThrow([Range(1, 5)] int callCount)
            {
                var mimic = new Mimic<ISubject> { Strict = false };

                for (int i = 0; i < callCount + 1; i++)
                    _ = mimic.Object.Property = "value";

                var ex = Should.Throw<MimicException>(() => mimic.VerifySetReceived(m => m.Property = Arg.Any<string>(), CallCount.AtMost(callCount)));
                ex.Message.ShouldStartWith($"""
                                            Verification failed with incorrect matching invocations
                                            Expected at most {callCount} invocation(s) on the mimic, but it was invoked {callCount + 1} time(s): m => m.set_Property([Mimic.Expressions.ArgumentMatcherExpression])
                                            Actual invocations on {mimic} (m):
                                            """.ReplaceLineEndings(Environment.NewLine));
                ex.Message.ShouldContain("MimicTests.VerifyReceived.ISubject.Property");
                ex.Reason.ShouldBe(Reason.ExpectationFailed);
            }

            [Theory, AutoData]
            public static void VerifySetReceived_WithCallCountAtMostOneAndUnexpectedCallReceived_ShouldThrowWithFailureMessage(string failureMessage)
            {
                var mimic = new Mimic<ISubject> { Strict = false };

                _ = mimic.Object.Property = "value";
                _ = mimic.Object.Property = "value";

                var ex = Should.Throw<MimicException>(() => mimic.VerifySetReceived(m => m.Property = Arg.Any<string>(), CallCount.AtMostOnce(), failureMessage));
                ex.Message.ShouldStartWith($"""
                                            {failureMessage}
                                            Expected at most one invocation on the mimic, but it was invoked 2 times: m => m.set_Property([Mimic.Expressions.ArgumentMatcherExpression])
                                            Actual invocations on {mimic} (m):
                                            """.ReplaceLineEndings(Environment.NewLine));
                ex.Message.ShouldContain("MimicTests.VerifyReceived.ISubject.Property");
                ex.Reason.ShouldBe(Reason.ExpectationFailed);
            }
        }

        // ReSharper disable once MemberHidesStaticFromOuterClass
        internal interface ISubject
        {
            void VoidMethod();

            string StringMethod(int iValue, string sValue, List<bool> lValue);

            INestedSubject GetNestedSubject();

            string Property { get; set;  }
        }

        internal interface INestedSubject
        {
            void NestedVoidMethod();

            string NestedStringMethod(int iValue, string sValue, List<bool> lValue);
        }
    }
}
