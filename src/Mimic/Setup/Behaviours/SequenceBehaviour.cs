namespace Mimic.Setup.Behaviours;

internal sealed class SequenceBehaviour : Behaviour
{
    private readonly MethodCallSetup _setup;
    private readonly Queue<Behaviour> _behaviours = new();

    public int Remaining => _behaviours.Count;

    public SequenceBehaviour(MethodCallSetup setup) => _setup = setup;

    public void AddBehaviour(Behaviour behaviour) => _behaviours.Enqueue(behaviour);

    internal override void Execute(IInvocation invocation)
    {
        if (_behaviours.TryDequeue(out var behaviour))
        {
            behaviour.Execute(invocation);
            return;
        }

        // If we reach this point, there have been more invocations than configured behaviours so handle it like there
        // is no configured return/throw.

        if (invocation.Method.ReturnType != typeof(void))
            _setup.StrictThrowOrReturnDefault(invocation);
    }
}
