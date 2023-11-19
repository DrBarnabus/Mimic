using Mimic.Proxy;

namespace Mimic.Setup.Behaviours;

internal sealed class NoOpBehaviour : Behaviour
{
    public static readonly NoOpBehaviour Instance = new();

    internal override void Execute(IInvocation invocation)
    {
        // intentionally empty
    }
}
