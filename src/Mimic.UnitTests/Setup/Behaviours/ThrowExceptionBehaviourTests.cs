using Mimic.Setup.Behaviours;

namespace Mimic.UnitTests.Setup.Behaviours;

public class ThrowExceptionBehaviourTests
{
    [Theory]
    [AutoData]
    public void Execute_ShouldSetInvocationExceptionToExceptionReturnedByExceptionFactory(Exception exception)
    {
        var invocation = new InvocationFixture();

        var behaviour = new ThrowExceptionBehaviour(exception);
        var ex = Should.Throw<Exception>(() => behaviour.Execute(invocation));
        ex.Message.ShouldBe(exception.Message);
    }
}
