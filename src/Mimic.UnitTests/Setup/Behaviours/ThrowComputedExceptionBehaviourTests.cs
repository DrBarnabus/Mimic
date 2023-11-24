using Mimic.Proxy;
using Mimic.Setup.Behaviours;

namespace Mimic.UnitTests.Setup.Behaviours;

public class ThrowComputedExceptionBehaviourTests
{
    [Theory]
    [AutoData]
    public void Execute_ShouldSetInvocationExceptionToExceptionReturnedByExceptionFactory(Exception exception)
    {
        var invocation = new InvocationFixture();

        var behaviour = new ThrowComputedExceptionBehaviour(ExceptionFactory);
        var ex = Should.Throw<Exception>(() => behaviour.Execute(invocation));
        ex.Message.ShouldBe(exception.Message);

        Exception ExceptionFactory(IInvocation i)
        {
            i.ShouldBe(invocation);
            return exception;
        }
    }
}
