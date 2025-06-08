namespace Mimic.Setup.Behaviours;

internal sealed class DelayBehaviour : Behaviour
{
    private readonly Func<int, TimeSpan> _delayFunction;
    private int _executionCount;

    public DelayBehaviour(Func<int, TimeSpan> delayFunction) => _delayFunction = delayFunction;

    internal override void Execute(Invocation invocation)
    {
        _executionCount++;

        var delay = _delayFunction.Invoke(_executionCount);
        Thread.Sleep(delay);
    }
}
