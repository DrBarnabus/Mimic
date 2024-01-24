namespace Mimic.Setup.Behaviours;

internal sealed class ThrowComputedExceptionBehaviour : Behaviour
{
    private readonly Func<Invocation, Exception?> _exceptionFactory;

    public ThrowComputedExceptionBehaviour(Func<Invocation, Exception?> exceptionFactory) => _exceptionFactory = exceptionFactory;

    internal override void Execute(Invocation invocation) => throw _exceptionFactory.Invoke(invocation)!;
}
