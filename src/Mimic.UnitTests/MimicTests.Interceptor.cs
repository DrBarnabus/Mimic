using System.Collections;
using Mimic.Exceptions;

namespace Mimic.UnitTests;

public partial class MimicTests
{
    public class Interceptor
    {
        [Fact]
        public void GetterForIMimicked_ShouldReturnTheOriginalMimicInstance()
        {
            var mimic = new Mimic<ISubject>();

            // ReSharper disable once SuspiciousTypeConversion.Global
            (mimic.Object as IMimicked)?.Mimic.ShouldBeSameAs(mimic);
        }

        [Fact]
        public void GetterForIMimickedOfT_ShouldReturnTheOriginalMimicInstance()
        {
            var mimic = new Mimic<ISubject>();

            // ReSharper disable once SuspiciousTypeConversion.Global
            (mimic.Object as IMimicked<ISubject>)?.Mimic.ShouldBeSameAs(mimic);
        }

        [Fact]
        public void ToString_WhenThereIsNoOverridingSetup_ShouldReturnTheNameOfTheOriginalMimicInstance()
        {
            var mimic = new Mimic<ISubject>();

            string? result = mimic.Object.ToString();
            result.ShouldNotBeNull();
            result.ShouldStartWith("Mimic<MimicTests.Interceptor.ISubject>:");
            result.ShouldEndWith(".Object");
        }

        [Theory, AutoData]
        public void ToString_WhenThereIsAnOverridingSetup_ShouldReturnTheReturnValueOfTheSetup(string returnValue)
        {
            var mimic = new Mimic<ISubject>();
            mimic.Setup(m => m.ToString()).Returns(returnValue);

            string? result = mimic.Object.ToString();
            result.ShouldNotBeNull();
            result.ShouldBe(returnValue);
        }

        [Fact]
        public void Equals_WhenThereIsNoOverridingSetup_AndInstancesMatch_ShouldReturnTrue()
        {
            var mimic = new Mimic<ISubject>();

            // ReSharper disable once EqualExpressionComparison
            mimic.Object.Equals(mimic.Object).ShouldBeTrue();
        }

        [Fact]
        public void Equals_WhenThereIsNoOverridingSetup_AndInstancesAreDifferent_ShouldReturnTrue()
        {
            var mimic = new Mimic<ISubject>();

            mimic.Object.Equals(new Mimic<ISubject>().Object).ShouldBeFalse();
        }

        [Theory, AutoData]
        public void Equals_WhenThereIsAnOverridingSetup_ShouldReturnTheReturnValueOfTheSetup(bool returnValue)
        {
            var mimic = new Mimic<ISubject>();
            mimic.Setup(m => m.Equals(Arg.Any<object>())).Returns(returnValue);

            // ReSharper disable once EqualExpressionComparison
            mimic.Object.Equals(mimic.Object).ShouldBe(returnValue);
        }

        [Fact]
        public void GetHashCode_WhenThereIsNoOverridingSetup_ShouldReturnTheHashCodeOfTheOriginalMimicInstance()
        {
            var mimic = new Mimic<ISubject>();

            mimic.Object.GetHashCode().ShouldBe(mimic.GetHashCode());
        }

        [Theory, AutoData]
        public void GetHashCode_WhenThereIsAnOverridingSetup_ShouldReturnTheReturnValueOfTheSetup(int hashCode)
        {
            var mimic = new Mimic<ISubject>();
            mimic.Setup(m => m.GetHashCode()).Returns(hashCode);

            mimic.Object.GetHashCode().ShouldBe(hashCode);
        }

        [Fact]
        public void StrictEnabled_WhenThereIsNoMatchingSetup_ShouldThrowMimicException()
        {
            var mimic = new Mimic<ISubject>();

            Should.Throw<MimicException>(() => mimic.Object.VoidMethod()).Message.
                ShouldBe("Invocation of 'MimicTests.Interceptor.ISubject.VoidMethod()' failed. Mimic is configured in Strict mode so all invocations must match a setup or this error will be thrown.");

            Should.Throw<MimicException>(() => mimic.Object.GenericMethod<string>()).Message.
                ShouldBe("Invocation of 'MimicTests.Interceptor.ISubject.GenericMethod<string>()' failed. Mimic is configured in Strict mode so all invocations must match a setup or this error will be thrown.");
        }

        [Fact]
        public void StrictDisabled_WhenThereIsNoMatchingSetup_ShouldReturnTheDefaultValueForThatType()
        {
            var mimic = new Mimic<ISubject>(false);

            // Void
            mimic.Object.VoidMethod();

            // Built-in types
            mimic.Object.GenericMethod<bool>().ShouldBe(default);
            mimic.Object.GenericMethod<byte>().ShouldBe(default);
            mimic.Object.GenericMethod<sbyte>().ShouldBe(default);
            mimic.Object.GenericMethod<char>().ShouldBe(default);
            mimic.Object.GenericMethod<decimal>().ShouldBe(default);
            mimic.Object.GenericMethod<double>().ShouldBe(default);
            mimic.Object.GenericMethod<float>().ShouldBe(default);
            mimic.Object.GenericMethod<int>().ShouldBe(default);
            mimic.Object.GenericMethod<uint>().ShouldBe(default);
            mimic.Object.GenericMethod<nint>().ShouldBe(default);
            mimic.Object.GenericMethod<nuint>().ShouldBe(default);
            mimic.Object.GenericMethod<long>().ShouldBe(default);
            mimic.Object.GenericMethod<ulong>().ShouldBe(default);
            mimic.Object.GenericMethod<short>().ShouldBe(default);
            mimic.Object.GenericMethod<ushort>().ShouldBe(default);
            mimic.Object.GenericMethod<object>().ShouldBe(default);
            mimic.Object.GenericMethod<string>().ShouldBe(default);

            // Array
            mimic.Object.GenericMethod<int[]>().ShouldBe([]);
            mimic.Object.GenericMethod<int[,]>().ShouldBe(new int[0, 0]);

            // Enumerable
            mimic.Object.GenericMethod<IEnumerable>().ShouldBe(Array.Empty<object>());
            mimic.Object.GenericMethod<IEnumerable<int>>().ShouldBe(Array.Empty<int>());

            // Tuple
            mimic.Object.GenericMethod<(int, int)>().ShouldBe(new ValueTuple<int, int>());
            mimic.Object.GenericMethod<(int, int, int)>().ShouldBe(new ValueTuple<int, int, int>());
            mimic.Object.GenericMethod<(int, int, int, int)>().ShouldBe(new ValueTuple<int, int, int, int>());
            mimic.Object.GenericMethod<(int, int, int, int, int)>().ShouldBe(new ValueTuple<int, int, int, int, int>());
            mimic.Object.GenericMethod<(int, int, int, int, int, int)>().ShouldBe(new ValueTuple<int, int, int, int, int, int>());
            mimic.Object.GenericMethod<(int, int, int, int, int, int, int)>().ShouldBe(new ValueTuple<int, int, int, int, int, int, int>());
        }

        // ReSharper disable once MemberHidesStaticFromOuterClass
        internal interface ISubject
        {
            void VoidMethod();

            T GenericMethod<T>();
        }
    }
}
