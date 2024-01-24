namespace Mimic.Setup.Behaviours;

internal sealed class CallbackBehaviour : Behaviour
{
    private readonly Action<Invocation> _callbackFunction;

    public CallbackBehaviour(Action<Invocation> callbackFunction) => _callbackFunction = callbackFunction;

    internal override void Execute(Invocation invocation) => _callbackFunction.Invoke(invocation);
}
