using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Mimic.Setup.Behaviours;

namespace Mimic.UnitTests.Setup.Behaviours;

public class DelayBehaviourTests
{
    [Theory, AutoData]
    public void Execute_ReturnsAfterRoughlyTheSpecifiedTimeSpanHasElapsed(
        [Range(100, 1000)] int firstDelayMs, [Range(100, 1000)] int secondDelayMs)
    {
        var invocation = new InvocationFixture();

        var behaviour = new DelayBehaviour(execution => TimeSpan.FromMilliseconds(execution == 1 ? firstDelayMs : secondDelayMs));

        var stopwatch = Stopwatch.StartNew();
        behaviour.Execute(invocation);
        stopwatch.ElapsedMilliseconds.ShouldBeGreaterThanOrEqualTo(firstDelayMs);

        stopwatch = Stopwatch.StartNew();
        behaviour.Execute(invocation);
        stopwatch.ElapsedMilliseconds.ShouldBeGreaterThanOrEqualTo(secondDelayMs);
    }
}
