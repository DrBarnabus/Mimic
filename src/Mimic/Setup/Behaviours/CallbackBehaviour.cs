namespace Mimic.Setup.Behaviours;

internal sealed class CallbackBehaviour : Behaviour
{
    private readonly Action<IInvocation> _callbackFunction;

    public CallbackBehaviour(Action<IInvocation> callbackFunction) => _callbackFunction = callbackFunction;

    internal override void Execute(IInvocation invocation) => _callbackFunction.Invoke(invocation);
}
