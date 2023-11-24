using System.Reflection;
using Mimic.Core;
using Mimic.Core.Extensions;

namespace Mimic.UnitTests.Core.Extensions;

public class MethodInfoExtensionsTests
{
    #region IsGetter

    [Fact]
    public void IsGetter_WhenMethodIsGetter_ReturnsTrue()
    {
        var getMethod = typeof(A).GetProperty(nameof(A.B))?.GetMethod!;
        getMethod.IsGetter().ShouldBeTrue();
    }

    [Fact]
    public void IsGetter_WhenMethodIsNotGetterButIsSpecialMethod_ReturnsFalse()
    {
        var specialMethod = typeof(A).GetProperty(nameof(A.B))?.SetMethod!;
        specialMethod.IsGetter().ShouldBeFalse();
    }

    [Fact]
    public void IsGetter_WhenMethodIsNotGetterAndNotSpecialMethod_ReturnsFalse()
    {
        var method = typeof(A).GetMethod(nameof(A.M))!;
        method.IsGetter().ShouldBeFalse();
    }

    #endregion

    #region IsSetter

    [Fact]
    public void IsSetter_WhenMethodIsSetter_ReturnsTrue()
    {
        var setMethod = typeof(A).GetProperty(nameof(A.B))?.SetMethod!;
        setMethod.IsSetter().ShouldBeTrue();
    }

    [Fact]
    public void IsSetter_WhenMethodIsNotGetterButIsSpecialMethod_ReturnsFalse()
    {
        var specialMethod = typeof(A).GetProperty(nameof(A.B))?.GetMethod!;
        specialMethod.IsSetter().ShouldBeFalse();
    }

    [Fact]
    public void IsSetter_WhenMethodIsNotGetterAndNotSpecialMethod_ReturnsFalse()
    {
        var method = typeof(A).GetMethod(nameof(A.M))!;
        method.IsSetter().ShouldBeFalse();
    }

    #endregion

    #region GetImplementingMethod

    [Fact]
    public void GetImplementingMethod_WithNullMethod_ThrowsAssertionException()
    {
        var ex = Should.Throw<Guard.AssertionException>(() => ((MethodInfo)null!).GetImplementingMethod(typeof(A)));
        ex.Expression.ShouldBe("method");
        ex.Message.ShouldContain("method must not be null");
    }

    [Fact]
    public void GetImplementingMethod_WithNullProxyType_ThrowsAssertionException()
    {
        var method = typeof(I).GetMethod(nameof(I.M))!;

        var ex = Should.Throw<Guard.AssertionException>(() => method.GetImplementingMethod(null!));
        ex.Expression.ShouldBe("proxyType");
        ex.Message.ShouldContain("proxyType must not be null");
    }

    [Fact]
    public void GetImplementingMethod_WithProxyTypeThatIsNotAClass_ThrowsAssertionException()
    {
        var method = typeof(I).GetMethod(nameof(I.M))!;

        var ex = Should.Throw<Guard.AssertionException>(() => method.GetImplementingMethod(typeof(I)));
        ex.Expression.ShouldBe("proxyType.IsClass");
        ex.Message.ShouldContain("Assertion failed");
    }

    [Fact]
    public void GetImplementingMethod_WithNonGenericMethod_AndDeclaringTypeIsInterface_ReturnsTheCorrectImplementingMethod()
    {
        var method = typeof(I).GetMethod(nameof(I.M))!;

        var implementingMethod = method.GetImplementingMethod(typeof(A));
        implementingMethod.ShouldBe(typeof(A).GetMethod(nameof(A.M)));
    }

    [Fact]
    public void GetImplementingMethod_WithNonGenericMethod_AndDeclaringTypeIsClass_ReturnsTheCorrectImplementingMethod()
    {
        var method = typeof(A).GetMethod(nameof(A.M))!;

        var implementingMethod = method.GetImplementingMethod(typeof(A));
        implementingMethod.ShouldBe(typeof(A).GetMethod(nameof(A.M)));
    }

    [Fact]
    public void GetImplementingMethod_WithGenericMethod_AndDeclaringTypeIsInterface_ReturnsTheCorrectImplementingMethod()
    {
        var method = typeof(I).GetMethod(nameof(I.G))!.MakeGenericMethod(typeof(string));

        var implementingMethod = method.GetImplementingMethod(typeof(A));
        implementingMethod.ShouldBe(typeof(A).GetMethod(nameof(A.G)));
    }

    [Fact]
    public void GetImplementingMethod_WithGenericMethod_AndDeclaringTypeIsClass_ReturnsTheCorrectImplementingMethod()
    {
        var method = typeof(A).GetMethod(nameof(A.G))!.MakeGenericMethod(typeof(string));

        var implementingMethod = method.GetImplementingMethod(typeof(A));
        implementingMethod.ShouldBe(typeof(A).GetMethod(nameof(A.G)));
    }

    [Fact]
    public void GetImplementingMethod_WithGenericMethodDefinition_AndDeclaringTypeIsInterface_ReturnsTheCorrectImplementingMethod()
    {
        var method = typeof(I).GetMethod(nameof(I.G))!;

        var implementingMethod = method.GetImplementingMethod(typeof(A));
        implementingMethod.ShouldBe(typeof(A).GetMethod(nameof(A.G)));
    }

    [Fact]
    public void GetImplementingMethod_WithGenericMethodDefinition_AndDeclaringTypeIsClass_ReturnsTheCorrectImplementingMethod()
    {
        var method = typeof(A).GetMethod(nameof(A.G))!;

        var implementingMethod = method.GetImplementingMethod(typeof(A));
        implementingMethod.ShouldBe(typeof(A).GetMethod(nameof(A.G)));
    }

    #endregion

    private interface I
    {
        void M();

        void G<T>();
    }

    private sealed class A : I
    {
        public string? B { get; set; }

        public void M() {}

        public void G<T>() {}
    }
}
