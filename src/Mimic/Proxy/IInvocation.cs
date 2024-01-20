using Mimic.Setup;

namespace Mimic.Proxy;

internal interface IInvocation
{
    public Type ProxyType { get; }

    MethodInfo Method { get; }

    MethodInfo MethodImplementation { get; }

    object?[] Arguments { get; }

    bool Verified { get; }

    public void SetReturnValue(object? returnValue);

    public void MarkMatchedBy(SetupBase setup);

    public void MarkVerified();

    public void MarkVerified(Predicate<SetupBase> predicate);
}
