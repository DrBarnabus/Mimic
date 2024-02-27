namespace Mimic.Setup.Behaviours;

internal sealed class ProceedBehaviour : Behaviour
{
    public static readonly ProceedBehaviour Instance = new();

    internal override void Execute(Invocation invocation) => invocation.SetReturnValue(invocation.Proceed());
}
