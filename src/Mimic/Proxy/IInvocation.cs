using System.Reflection;

namespace Mimic.Proxy;

public interface IInvocation
{
    public Type ProxyType { get; }

    MethodInfo Method { get; }

    object[] Arguments { get; }

    public void SetReturnValue(object? returnValue);

    public void SetException(Exception exception);
}
