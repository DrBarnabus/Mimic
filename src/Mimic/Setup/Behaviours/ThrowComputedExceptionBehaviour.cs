using Mimic.Proxy;

namespace Mimic.Setup.Behaviours;

internal sealed class ThrowComputedExceptionBehaviour : Behaviour
{
    private readonly Func<IInvocation, Exception?> _exceptionFactory;

    public ThrowComputedExceptionBehaviour(Func<IInvocation, Exception?> exceptionFactory) => _exceptionFactory = exceptionFactory;

    internal override void Execute(IInvocation invocation) => throw _exceptionFactory.Invoke(invocation)!;
}
