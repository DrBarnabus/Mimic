namespace Mimic.Setup.Behaviours;

internal sealed class ExecutionLimitBehaviour : Behaviour
{
    private readonly MethodCallSetup _setup;
    private readonly int _executionLimit;
    private int _executionCount;

    public ExecutionLimitBehaviour(MethodCallSetup setup, int executionLimit) => (_setup, _executionLimit) = (setup, executionLimit);

    internal override void Execute(IInvocation invocation)
    {
        _executionCount++;
        if (_executionCount > _executionLimit)
            throw MimicException.ExecutionLimitExceeded(_setup, _executionLimit, _executionCount);
    }
}
