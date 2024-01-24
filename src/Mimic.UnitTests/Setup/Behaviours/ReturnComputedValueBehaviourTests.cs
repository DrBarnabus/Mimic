using Mimic.Proxy;
using Mimic.Setup.Behaviours;

namespace Mimic.UnitTests.Setup.Behaviours;

public class ReturnComputedValueBehaviourTests
{
    [Theory]
    [AutoData]
    public void Execute_ShouldSetInvocationReturnValueToValueReturnedByValueFactory(string value)
    {
        var invocation = new InvocationFixture();

        var behaviour = new ReturnComputedValueBehaviour(ValueFactory);
        behaviour.Execute(invocation);

        invocation.ReturnValue.ShouldBe(value);

        object? ValueFactory(Invocation i)
        {
            i.ShouldBe(invocation);
            return value;
        }
    }
}
