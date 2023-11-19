using Mimic.Proxy;

namespace Mimic.Setup.Behaviours;

internal abstract class Behaviour
{
    internal abstract void Execute(IInvocation invocation);
}
