using Mimic.Exceptions;
using Mimic.Setup.Behaviours;

namespace Mimic.UnitTests.Setup.Behaviours;

public class ExecutionLimitBehaviourTests
{
    [Fact]
    public void Execute_WithLessExecutionsThanLimit_DoesNotThrow()
    {
        var invocation = new InvocationFixture();

        var behaviour = new ExecutionLimitBehaviour(null!, 3);

        Should.NotThrow(() => behaviour.Execute(invocation));
        Should.NotThrow(() => behaviour.Execute(invocation));
        Should.NotThrow(() => behaviour.Execute(invocation));
    }

    [Fact]
    public void Execute_WithMoreExecutionsThanLimit_ShouldThrow()
    {
        var invocation = new InvocationFixture();

        var behaviour = new ExecutionLimitBehaviour(null!, 3);

        behaviour.Execute(invocation);
        behaviour.Execute(invocation);
        behaviour.Execute(invocation);

        var ex = Should.Throw<MimicException>(() => behaviour.Execute(invocation));
        ex.Message.ShouldBe("Setup '' has been limited to 3 executions but was actually executed 4 times");
    }
}
