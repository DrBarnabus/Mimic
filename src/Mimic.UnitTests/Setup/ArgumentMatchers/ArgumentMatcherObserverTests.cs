using Mimic.Core;
using Mimic.Setup.ArgumentMatchers;

namespace Mimic.UnitTests.Setup.ArgumentMatchers;

public class ArgumentMatcherObserverTests
{
    [Fact]
    public void ActivateObserver_ActivatingAndDisposingMultipleObservers_ShouldNotThrowAnException()
    {
        Should.NotThrow(() =>
        {
            using var firstObserver = ArgumentMatcherObserver.ActivateObserver();
            using var secondObserver = ArgumentMatcherObserver.ActivateObserver();
        });
    }

    [Fact]
    public void ActivateObserver_DisposingAnObserverMoreThanOnce_ShouldThrowAnAssertionException()
    {
        var observer = ArgumentMatcherObserver.ActivateObserver();
        observer.Dispose();

        var ex = Should.Throw<Guard.AssertionException>(() => observer.Dispose());
        ex.Expression.ShouldBe("_observers is { Count: > 0 }");
        ex.Message.ShouldContain("Assertion failed");
    }

    [Fact]
    public void HasActiveObserver_WithNoActiveObservers_ShouldReturnFalseWithNullOut()
    {
        ArgumentMatcherObserver.HasActiveObserver(out var activeObserver).ShouldBeFalse();
        activeObserver.ShouldBeNull();
    }

    [Fact]
    public void HasActiveObserver_WithSingleActiveObserver_ShouldReturnTrueWithOutValueSetToTheObserver()
    {
        using var observer = ArgumentMatcherObserver.ActivateObserver();

        ArgumentMatcherObserver.HasActiveObserver(out var activeObserver).ShouldBeTrue();
        activeObserver.ShouldBeSameAs(observer);
    }

    [Fact]
    public void HasActiveObserver_WithMultipleActiveObservers_ShouldReturnTrueWithOutValueSetToTheSecondObserver()
    {
        using var firstObserver = ArgumentMatcherObserver.ActivateObserver();
        using var secondObserver = ArgumentMatcherObserver.ActivateObserver();

        ArgumentMatcherObserver.HasActiveObserver(out var activeObserver).ShouldBeTrue();
        activeObserver.ShouldBeSameAs(secondObserver);
    }

    [Fact]
    public void AddArgumentMatcher_ShouldAddArgumentMatcherToListOfInvocationsWithCounterOne()
    {
        var argumentMatcher = new ArgumentMatcher<string>(_ => true);
        using var observer = ArgumentMatcherObserver.ActivateObserver();

        observer.AddArgumentMatcher(argumentMatcher);

        observer.Observations.Count.ShouldBe(1);
        observer.Observations[0].Counter.ShouldBe(1);
        observer.Observations[0].ArgumentMatcher.ShouldBeSameAs(argumentMatcher);
    }

    [Fact]
    public void AddArgumentMatcher_WhenCalledMultipleTimes_ShouldContinuallyIncrementCounter()
    {
        var argumentMatcher = new ArgumentMatcher<string>(_ => true);
        using var observer = ArgumentMatcherObserver.ActivateObserver();

        observer.AddArgumentMatcher(argumentMatcher);
        observer.AddArgumentMatcher(argumentMatcher);
        observer.AddArgumentMatcher(argumentMatcher);

        observer.Observations.Count.ShouldBe(3);
        observer.Observations[2].Counter.ShouldBe(3);
        observer.Observations[2].ArgumentMatcher.ShouldBeSameAs(argumentMatcher);
    }

    [Fact]
    public void TryGetLastArgumentMatcher_WithZeroObservations_ShouldReturnFalseWithNullOut()
    {
        using var observer = ArgumentMatcherObserver.ActivateObserver();

        observer.TryGetLastArgumentMatcher(out var lastArgumentMatcher).ShouldBeFalse();
        lastArgumentMatcher.ShouldBeNull();
    }

    [Fact]
    public void TryGetLastArgumentMatcher_WithOneObservation_ShouldReturnTrueWithOutValueSetToTheArgumentMatcherFromLastObservation()
    {
        var argumentMatcher = new ArgumentMatcher<string>(_ => true);
        using var observer = ArgumentMatcherObserver.ActivateObserver();
        observer.AddArgumentMatcher(argumentMatcher);

        observer.TryGetLastArgumentMatcher(out var lastArgumentMatcher).ShouldBeTrue();
        lastArgumentMatcher.ShouldBeSameAs(argumentMatcher);
    }

    [Fact]
    public void TryGetLastArgumentMatcher_WithTwoObservations_ShouldReturnTrueWithOutValueSetToTheArgumentMatcherFromLastObservation()
    {
        var argumentMatcher = new ArgumentMatcher<string>(_ => true);
        using var observer = ArgumentMatcherObserver.ActivateObserver();
        observer.AddArgumentMatcher(new ArgumentMatcher<int>(_ => false));
        observer.AddArgumentMatcher(argumentMatcher);

        observer.TryGetLastArgumentMatcher(out var lastArgumentMatcher).ShouldBeTrue();
        lastArgumentMatcher.ShouldBeSameAs(argumentMatcher);
    }

    [Fact]
    public void GetArgumentMatchersBetween_WithZeroObservations_ShouldReturnAnEmptyEnumberable()
    {
        using var observer = ArgumentMatcherObserver.ActivateObserver();

        observer.GetArgumentMatchersBetween(0, 2).ShouldBeEmpty();
    }

    [Fact]
    public void GetArgumentMatchersBetween_WithMultipleObservations_ShouldReturnCorrectArgumentMatchers()
    {
        using var observer = ArgumentMatcherObserver.ActivateObserver();

        observer.AddArgumentMatcher(new ArgumentMatcher<string>(_ => true)); // not returned

        int fromCounter = observer.GetCounter();

        var argumentMatcher1 = new ArgumentMatcher<string>(_ => true);
        observer.AddArgumentMatcher(argumentMatcher1);
        var argumentMatcher2 = new ArgumentMatcher<string>(_ => true);
        observer.AddArgumentMatcher(argumentMatcher2);
        var argumentMatcher3 = new ArgumentMatcher<string>(_ => true);
        observer.AddArgumentMatcher(argumentMatcher3);

        int toCounter = observer.GetCounter();

        observer.AddArgumentMatcher(new ArgumentMatcher<string>(_ => true)); // not returned

        var result = observer.GetArgumentMatchersBetween(fromCounter, toCounter).ToList();
        result.ShouldNotBeEmpty();
        result.Count.ShouldBe(3);
        result[0].ShouldBeSameAs(argumentMatcher1);
        result[1].ShouldBeSameAs(argumentMatcher2);
        result[2].ShouldBeSameAs(argumentMatcher3);
    }
}
