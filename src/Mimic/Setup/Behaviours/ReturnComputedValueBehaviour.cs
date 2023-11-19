using Mimic.Proxy;

namespace Mimic.Setup.Behaviours;

internal sealed class ReturnComputedValueBehaviour : Behaviour
{
    private readonly Func<IInvocation, object?> _valueFactory;

    public ReturnComputedValueBehaviour(Func<IInvocation, object?> valueFactory) => _valueFactory = valueFactory;

    internal override void Execute(IInvocation invocation) => invocation.SetReturnValue(_valueFactory.Invoke(invocation));
}
