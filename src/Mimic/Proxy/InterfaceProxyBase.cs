namespace Mimic.Proxy;

internal abstract class InterfaceProxyBase
{
    private static readonly MethodInfo ToStringMethod = typeof(object).GetMethod(nameof(ToString), BindingFlags.Public | BindingFlags.Instance)!;
    private static readonly MethodInfo EqualsMethod = typeof(object).GetMethod(nameof(Equals), BindingFlags.Public | BindingFlags.Instance)!;
    private static readonly MethodInfo GetHashCodeMethod = typeof(object).GetMethod(nameof(GetHashCode), BindingFlags.Public | BindingFlags.Instance)!;

    [DebuggerHidden]
    public sealed override string ToString()
    {
        var invocation = new Invocation(GetType(), ToStringMethod, Array.Empty<object?>());
        ((IProxy)this).Interceptor.Intercept(invocation);
        return (string)invocation.ReturnValue!;
    }

    [DebuggerHidden]
    public sealed override bool Equals(object? obj)
    {
        var invocation = new Invocation(GetType(), EqualsMethod, [obj]);
        ((IProxy)this).Interceptor.Intercept(invocation);
        return (bool)invocation.ReturnValue!;
    }

    [DebuggerHidden]
    public sealed override int GetHashCode()
    {
        var invocation = new Invocation(GetType(), GetHashCodeMethod, Array.Empty<object?>());
        ((IProxy)this).Interceptor.Intercept(invocation);
        return (int)invocation.ReturnValue!;
    }

    private sealed class Invocation(Type proxyType, MethodInfo method, object?[] arguments)
        : Mimic.Proxy.Invocation(proxyType, method, arguments);
}
