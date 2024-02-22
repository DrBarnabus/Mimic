using System.Reflection;
using AutoFixture;
using Mimic.Core;
using Mimic.Exceptions;
using Mimic.Proxy;
using Mimic.Setup;

namespace Mimic.UnitTests.Proxy;

public class ProxyGeneratorTests
{
    [Fact]
    public void Instance_ShouldAlwaysReturnTheSameReference()
    {
        var instance = ProxyGenerator.Instance;
        instance.ShouldBeSameAs(ProxyGenerator.Instance);
    }

    [Fact]
    public void GenerateProxy_WhenCalledWithInterfaceType_ShouldReturnNonNullTypeThatInheritsInterface()
    {
        var interceptor = new InterceptorFixture();

        object result = ProxyGenerator.Instance.GenerateProxy(typeof(IA), Type.EmptyTypes, Array.Empty<object>(), interceptor);

        result.ShouldNotBeNull();
        result.ShouldBeAssignableTo<IA>();
    }

    [Fact]
    public void GenerateProxy_WhenCalledWithSealedClassType_ShouldThrowMimicException()
    {
        var interceptor = new InterceptorFixture();

        var ex = Should.Throw<MimicException>(() =>
            ProxyGenerator.Instance.GenerateProxy(typeof(string), Type.EmptyTypes, Array.Empty<object>(), interceptor));

        ex.ShouldNotBeNull();
        ex.Message.ShouldBe("Type string cannot be mimicked. It must be an interface or a non-sealed/non-static class.");
        ex.InnerException.ShouldNotBeNull();
        ex.InnerException.ShouldBeOfType<TypeLoadException>();
    }

    [Fact]
    public void GenerateProxy_WhenCalledWithStaticClassType_ShouldThrowMimicException()
    {
        var interceptor = new InterceptorFixture();

        // Note: This exception comes from Castle.Core so we're just verifying it is indeed thrown and it get's wrapped
        var ex = Should.Throw<MimicException>(() =>
            ProxyGenerator.Instance.GenerateProxy(typeof(TypeExtensions), Type.EmptyTypes, Array.Empty<object>(), interceptor));

        ex.ShouldNotBeNull();
        ex.Message.ShouldBe("Type TypeExtensions cannot be mimicked. It must be an interface or a non-sealed/non-static class.");
        ex.InnerException.ShouldNotBeNull();
        ex.InnerException.Message.ShouldBe("Parent does not have a default constructor. The default constructor must be explicitly defined.");
    }

    [Fact]
    public void GenerateProxy_WhenCalledWithAbstractClassType_ButCallingParameterlessConstructorThatDoesNotExist_ShouldThrowMimicException()
    {
        var interceptor = new InterceptorFixture();

        // Note: This exception comes from Castle.Core so we're just verifying it is indeed thrown and it get's wrapped
        var ex = Should.Throw<MimicException>(() =>
            ProxyGenerator.Instance.GenerateProxy(typeof(C), Type.EmptyTypes, Array.Empty<object>(), interceptor));

        ex.ShouldNotBeNull();
        ex.Message.ShouldBe("Unable to find a constructor in type ProxyGeneratorTests.C matching given constructor arguments.");
        ex.InnerException.ShouldNotBeNull();
        ex.InnerException.Message.ShouldContain("Could not find a parameterless constructor.");
    }

    [Theory]
    [AutoData]
    public void GenerateProxy_WhenCalledWithAbstractClassType_ButCallingConstructorThatDoesNotExist_ShouldThrowMimicException(
        string sValue)
    {
        var interceptor = new InterceptorFixture();

        // Note: This exception comes from Castle.Core so we're just verifying it is indeed thrown and it get's wrapped
        var ex = Should.Throw<MimicException>(() =>
            ProxyGenerator.Instance.GenerateProxy(typeof(C), Type.EmptyTypes, [sValue], interceptor));

        ex.ShouldNotBeNull();
        ex.Message.ShouldBe("Unable to find a constructor in type ProxyGeneratorTests.C matching given constructor arguments.");
        ex.InnerException.ShouldNotBeNull();
        ex.InnerException.Message.ShouldContain("Could not find a constructor that would match given arguments:");
    }

    [Theory]
    [AutoData]
    public void GenerateProxy_WhenCalledWithAbstractClassType_ShouldReturnNonNullTypeThatInheritsParentClass(
        string sValue, int iValue)
    {
        var interceptor = new InterceptorFixture();

        object result = ProxyGenerator.Instance.GenerateProxy(typeof(C), Type.EmptyTypes, [sValue, iValue], interceptor);

        result.ShouldNotBeNull();
        result.ShouldBeAssignableTo<C>();
    }

    public class InvocationTests
    {
        private readonly InterceptorFixture _interceptor;
        private readonly IA _proxyObject;

        public InvocationTests()
        {
            _interceptor = new InterceptorFixture();
            _proxyObject = (IA)ProxyGenerator.Instance.GenerateProxy(typeof(IA), Type.EmptyTypes, Array.Empty<object>(), _interceptor);
        }

        [Fact]
        public void ProxyType_ShouldReturnTypeOfProxyObject()
        {
            _interceptor.Callback = invocation =>
            {
                invocation.ProxyType.ShouldBe(_proxyObject.GetType());
            };

            _proxyObject.ReturnsVoid();

            _interceptor.Intercepted.ShouldBeTrue();
        }

        [Fact]
        public void Method_ShouldReturnCorrectMethodInfoForInterceptedInterfaceMethod()
        {
            _interceptor.Callback = invocation =>
            {
                invocation.Method.ReturnType.ShouldBe(typeof(void));
                invocation.Method.Name.ShouldBe(nameof(IA.ReturnsVoid));
                invocation.Method.DeclaringType.ShouldBe(typeof(IA));
            };

            _proxyObject.ReturnsVoid();

            _interceptor.Intercepted.ShouldBeTrue();
        }

        [Fact]
        public void MethodImplementation_ShouldReturnCorrectMethodInfoForInterceptedProxyMethod()
        {
            _interceptor.Callback = invocation =>
            {
                invocation.MethodImplementation.ReturnType.ShouldBe(typeof(void));
                invocation.MethodImplementation.Name.ShouldBe(nameof(IA.ReturnsVoid));
                invocation.MethodImplementation.DeclaringType.ShouldBe(_proxyObject.GetType());
            };

            _proxyObject.ReturnsVoid();

            _interceptor.Intercepted.ShouldBeTrue();
        }

        [Fact]
        public void MethodImplementation_ShouldReturnCachedMethodInfoOnSecondCall()
        {
            MethodInfo? previousMethodImplementation = null;
            _interceptor.Callback = invocation =>
            {
                if (previousMethodImplementation == null)
                {
                    previousMethodImplementation = invocation.MethodImplementation;
                }
                else
                {
                    invocation.MethodImplementation.ShouldBeSameAs(previousMethodImplementation);
                }
            };

            _proxyObject.ReturnsVoid();
            _proxyObject.ReturnsVoid();

            _interceptor.InterceptCount.ShouldBe(2);
            previousMethodImplementation.ShouldNotBeNull();
        }

        [Fact]
        public void Arguments_WhenCalledWithMethodWithZeroParameters_ShouldReturnEmptyArray()
        {
            _interceptor.Callback = invocation =>
            {
                invocation.Arguments.ShouldNotBeNull();
                invocation.Arguments.Length.ShouldBe(0);
            };

            _proxyObject.ReturnsVoid();

            _interceptor.Intercepted.ShouldBeTrue();
        }

        [Theory]
        [AutoData]
        public void Arguments_WhenCalledWithMethodThatHasParameters_ShouldReturnCorrectArgumentValues(
            int intValue, string stringValue)
        {
            _interceptor.Callback = invocation =>
            {
                invocation.Arguments.ShouldNotBeNull();
                invocation.Arguments.Length.ShouldBe(2);
                invocation.Arguments[0].ShouldBe(intValue);
                invocation.Arguments[1].ShouldBe(stringValue);
            };

            _proxyObject.ReturnsString(intValue, stringValue);

            _interceptor.Intercepted.ShouldBeTrue();
        }

        [Theory]
        [AutoData]
        public void SetReturnValue_WhenCalledWithNonVoidMethod_ShouldReturnTheSetReturnValue(
            int intValue, string stringValue, string returnValue)
        {
            _interceptor.Callback = invocation =>
            {
                invocation.SetReturnValue(returnValue);
            };

            _proxyObject.ReturnsString(intValue, stringValue).ShouldBe(returnValue);

            _interceptor.Intercepted.ShouldBeTrue();
        }

        [Theory]
        [AutoData]
        public void SetReturnValue_WhenCalledWithNonVoidMethod_AndSetReturnValueIsCalledMoreThanOnce_ShouldThrowAssertionException(
            int intValue, string stringValue, string returnValue)
        {
            _interceptor.Callback = invocation =>
            {
                invocation.SetReturnValue(returnValue);

                var ex = Should.Throw<Guard.AssertionException>(() => invocation.SetReturnValue(returnValue));
                ex.Expression.ShouldBe("_result is null");
            };

            _proxyObject.ReturnsString(intValue, stringValue).ShouldBe(returnValue);

            _interceptor.Intercepted.ShouldBeTrue();
        }

        [Fact]
        public void MarkVerified_WithNoArgument_ShouldSetVerifiedToTrue()
        {
            _interceptor.Callback = invocation =>
            {
                invocation.MarkVerified();
                invocation.Verified.ShouldBe(true);
            };

            _proxyObject.ReturnsVoid();
            _interceptor.Intercepted.ShouldBeTrue();
        }

        [Fact]
        public void MarkVerified_WithSetupPredicate_AndMarkMatchedByNotPreviouslyCalled_ShouldNotSetVerifiedToTrue()
        {
            _interceptor.Callback = invocation =>
            {
                var setup = new AllPropertiesStubSetup(new Mimic<IA>());

                invocation.MarkVerified(s => s == setup);
                invocation.Verified.ShouldBe(false);
            };

            _proxyObject.ReturnsVoid();
            _interceptor.Intercepted.ShouldBeTrue();
        }

        [Fact]
        public void MarkVerified_WithSetupPredicate_AndMarkMatchedByWasPreviouslyCalledWithNonMatching_ShouldNotSetVerifiedToTrue()
        {
            _interceptor.Callback = invocation =>
            {
                var setup = new AllPropertiesStubSetup(new Mimic<IA>());
                invocation.MarkMatchedBy(setup);

                invocation.MarkVerified(s => s != setup);
                invocation.Verified.ShouldBe(false);
            };

            _proxyObject.ReturnsVoid();
            _interceptor.Intercepted.ShouldBeTrue();
        }

        [Fact]
        public void MarkVerified_WithSetupPredicate_AndMarkMatchedByWasPreviouslyCalledWithMatching_ShouldSetVerifiedToTrue()
        {
            _interceptor.Callback = invocation =>
            {
                var setup = new AllPropertiesStubSetup(new Mimic<IA>());
                invocation.MarkMatchedBy(setup);

                invocation.MarkVerified(s => s == setup);
                invocation.Verified.ShouldBe(true);
            };

            _proxyObject.ReturnsVoid();
            _interceptor.Intercepted.ShouldBeTrue();
        }

        [Fact]
        public void ToString_WithGetter_ShouldReturnOnlyPropertyName()
        {
            _interceptor.Callback = invocation =>
            {
                invocation.SetReturnValue(default(int));
                invocation.ToString().ShouldBe($"{nameof(ProxyGeneratorTests)}.{nameof(IA)}.{nameof(IA.IntProperty)}");
            };

            _ = _proxyObject.IntProperty;

            _interceptor.Intercepted.ShouldBeTrue();
        }

        [Theory]
        [AutoData]
        public void ToString_WithSetter_ShouldReturnOnlyPropertyName(int value)
        {
            _interceptor.Callback = invocation =>
            {
                invocation.ToString().ShouldBe($"{nameof(ProxyGeneratorTests)}.{nameof(IA)}.{nameof(IA.IntProperty)} = {value}");
            };

            _proxyObject.IntProperty = value;

            _interceptor.Intercepted.ShouldBeTrue();
        }

        [Fact]
        public void ToString_WithNonGenericMethod_ShouldReturnCorrectlyFormattedMethodNameWithArguments()
        {
            const string Ia = $"{nameof(ProxyGeneratorTests)}.{nameof(IA)}";
            const string MethodName = $"{Ia}.{nameof(IA.NonGenericMethod)}";

            var fixture = new Fixture();
            string stringValue = fixture.Create<string>();
            var enumValue = fixture.Create<IA.E>();
            int[] intArray = fixture.CreateMany<int>(15).ToArray();
            var stringList = fixture.CreateMany<string>(8).ToList();

            string arrayString = $"[{string.Join(", ", intArray.Take(10))}, ...]";
            string listString = $"[{string.Join(", ", stringList.Select(s => $"\"{s}\""))}]";

            _interceptor.Callback = invocation =>
            {
                invocation.ToString().ShouldBe(
                    $"{MethodName}(\"{stringValue}\", null, {Ia}.E.{enumValue}, {arrayString}, {listString})");
            };

            _proxyObject.NonGenericMethod(stringValue, null, enumValue, intArray, stringList);

            _interceptor.Intercepted.ShouldBeTrue();
        }

        [Fact]
        public void ToString_WithGenericMethod_ShouldReturnCorrectlyFormattedMethodNameWithTypeParameters()
        {
            _interceptor.Callback = invocation =>
            {
                invocation.ToString()
                    .ShouldBe($"{nameof(ProxyGeneratorTests)}.{nameof(IA)}.{nameof(IA.GenericMethod)}<string, int, byte>()");
            };

            _proxyObject.GenericMethod<string, int, byte>();

            _interceptor.Intercepted.ShouldBeTrue();
        }
    }

    public interface IA
    {
        public int IntProperty { get; set; }

        public void ReturnsVoid();

        public string ReturnsString(int intValue, string stringValue);

        public void NonGenericMethod(string stringValue, int? nullableIntValue, E enumValue, int[] intArray, List<string> stringList);

        public void GenericMethod<T1, T2, T3>();

        public enum E { None = 0, One = 1, Two = 2 }
    }

    public abstract class C
    {
        public C(string sValue, int iValue)
        {
        }
    }
}
