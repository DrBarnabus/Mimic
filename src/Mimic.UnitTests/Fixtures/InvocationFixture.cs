using System.Reflection;
using Mimic.Proxy;

namespace Mimic.UnitTests.Fixtures;

internal sealed class InvocationFixture : Invocation
{
    private static readonly MethodInfo DefaultMethod = typeof(InvocationFixture).GetMethod("ToString")!;

    public bool ProceededToBase { get; private set; }

    public InvocationFixture(MethodInfo? method = null)
        : base(typeof(InvocationFixture), method ?? DefaultMethod, Array.Empty<object?>())
    {
    }

    public InvocationFixture(Type proxyType, MethodInfo? method)
        : base(proxyType, method ?? DefaultMethod, Array.Empty<object?>())
    {
    }

    public InvocationFixture(Type proxyType, MethodInfo? method, object?[] arguments)
        : base(proxyType, method ?? DefaultMethod, arguments)
    {
    }

    public override object Proceed() => ProceededToBase = true;

    public static InvocationFixture ForMethod<T>(string name, object?[]? arguments = null)
    {
        var method = typeof(T).GetMethod(name);
        return new InvocationFixture(typeof(T), method, arguments ?? Array.Empty<object?>());
    }
}
