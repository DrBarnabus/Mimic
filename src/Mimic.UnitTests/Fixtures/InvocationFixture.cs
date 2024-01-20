using System.Reflection;
using Mimic.Proxy;
using Mimic.Setup;

namespace Mimic.UnitTests.Fixtures;

internal sealed class InvocationFixture : IInvocation
{
    public Type ProxyType { get; set; } = default!;

    public MethodInfo Method { get; set; } = default!;

    public MethodInfo MethodImplementation { get; set; } = default!;

    public object?[] Arguments { get; set; } = default!;

    public bool Verified { get; set; }

    public void SetReturnValue(object? returnValue) => ReturnValue = returnValue;

    public void MarkMatchedBy(SetupBase setup) => MatchedSetup = setup;

    public void MarkVerified() => Verified = true;

    public void MarkVerified(Predicate<SetupBase> predicate)
    {
        if (MatchedSetup != null && predicate(MatchedSetup))
            Verified = true;
    }

    public object? ReturnValue { get; set; }

    public SetupBase? MatchedSetup { get; set; }
}
