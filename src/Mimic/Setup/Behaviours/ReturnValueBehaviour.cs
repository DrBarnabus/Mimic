using Mimic.Proxy;

namespace Mimic.Setup.Behaviours;

internal sealed class ReturnValueBehaviour : Behaviour
{
    private readonly object? _value;

    public ReturnValueBehaviour(object? value) => _value = value;

    internal override void Execute(IInvocation invocation) => invocation.SetReturnValue(_value);
}
