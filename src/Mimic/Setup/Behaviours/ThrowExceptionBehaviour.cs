using Mimic.Proxy;

namespace Mimic.Setup.Behaviours;

internal sealed class ThrowExceptionBehaviour : Behaviour
{
    private readonly Exception _exception;

    public ThrowExceptionBehaviour(Exception exception) => _exception = exception;

    internal override void Execute(IInvocation invocation) => throw _exception;
}
