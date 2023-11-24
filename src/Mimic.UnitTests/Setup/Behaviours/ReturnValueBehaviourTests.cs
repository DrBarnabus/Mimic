using Mimic.Setup.Behaviours;

namespace Mimic.UnitTests.Setup.Behaviours;

public class ReturnValueBehaviourTests
{
    [Theory]
    [AutoData]
    public void Execute_ShouldSetInvocationReturnValueToValueSetInConstructor(string value)
    {
        var invocation = new InvocationFixture();

        var behaviour = new ReturnValueBehaviour(value);
        behaviour.Execute(invocation);

        invocation.ReturnValue.ShouldBe(value);
    }
}
