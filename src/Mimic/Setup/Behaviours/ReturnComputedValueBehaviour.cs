namespace Mimic.Setup.Behaviours;

internal sealed class ReturnComputedValueBehaviour : Behaviour
{
    private readonly Func<Invocation, object?> _valueFactory;

    public ReturnComputedValueBehaviour(Func<Invocation, object?> valueFactory) => _valueFactory = valueFactory;

    internal override void Execute(Invocation invocation) => invocation.SetReturnValue(_valueFactory.Invoke(invocation));
}
