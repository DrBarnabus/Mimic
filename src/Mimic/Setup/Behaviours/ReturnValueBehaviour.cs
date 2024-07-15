namespace Mimic.Setup.Behaviours;

internal sealed class ReturnValueBehaviour : Behaviour
{
    internal object? Value { get; }

    public ReturnValueBehaviour(object? value) => Value = value;

    internal override void Execute(Invocation invocation) => invocation.SetReturnValue(Value);
}
