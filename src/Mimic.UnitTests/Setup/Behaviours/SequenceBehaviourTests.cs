using Mimic.Setup.Behaviours;

namespace Mimic.UnitTests.Setup.Behaviours;

public class SequenceBehaviourTests
{
    [Fact]
    public void AddBehaviour_ShouldIncreaseRemainingCount()
    {
        var behaviour = new SequenceBehaviour(null!);
        behaviour.Remaining.ShouldBe(0);

        behaviour.AddBehaviour(new NoOpBehaviour());
        behaviour.Remaining.ShouldBe(1);

        behaviour.AddBehaviour(new NoOpBehaviour());
        behaviour.Remaining.ShouldBe(2);

        behaviour.AddBehaviour(new NoOpBehaviour());
        behaviour.Remaining.ShouldBe(3);
    }

    [Fact]
    public void Execute_WithQueuedBehaviour_ShouldExecuteBehaviourAndReturn()
    {
        var invocation = new InvocationFixture();

        var behaviour = new SequenceBehaviour(null!);
        behaviour.AddBehaviour(new ReturnValueBehaviour(300));

        behaviour.Execute(invocation);

        invocation.ReturnValue.ShouldBe(300);
    }

    [Fact]
    public void Execute_WithNoQueuedBehaviour_AndMethodHasReturnTypeOfVoid_ShouldReturnWithoutCallingSetupThrowOrReturnDefault()
    {
        var invocation = new InvocationFixture(typeof(SequenceBehaviour).GetMethod(nameof(SequenceBehaviour.AddBehaviour)));
        var behaviour = new SequenceBehaviour(null!);

        Should.NotThrow(() => behaviour.Execute(invocation));
    }

    // TODO: Add test for case when there is no queued behaviour but method is not a void return type
}
