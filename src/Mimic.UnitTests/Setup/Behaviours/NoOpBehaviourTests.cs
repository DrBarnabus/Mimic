using Mimic.Setup.Behaviours;

namespace Mimic.UnitTests.Setup.Behaviours;

public class NoOpBehaviourTests
{
    [Fact]
    public void Instance_ShouldBeAlwaysReturnTheSameReference()
    {
        var instance = NoOpBehaviour.Instance;
        instance.ShouldBeSameAs(NoOpBehaviour.Instance);
    }

    [Fact]
    public void Execute_WithLessExecutionsThanLimit_DoesNotThrow()
    {
        var invocation = new InvocationFixture();

        Should.NotThrow(() => NoOpBehaviour.Instance.Execute(invocation));
    }
}
