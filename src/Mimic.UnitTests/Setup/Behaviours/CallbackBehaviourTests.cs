using Mimic.Setup.Behaviours;

namespace Mimic.UnitTests.Setup.Behaviours;

public class CallbackBehaviourTests
{
    [Fact]
    public void Execute_InvokesTheProvidedCallbackFunction()
    {
        var invocation = new InvocationFixture();

        bool callbackCalled = false;
        var behaviour = new CallbackBehaviour(i =>
        {
            i.ShouldBe(invocation);
            callbackCalled = true;
        });

        behaviour.Execute(invocation);

        callbackCalled.ShouldBeTrue();
    }
}
