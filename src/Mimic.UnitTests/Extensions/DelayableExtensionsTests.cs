using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Mimic.UnitTests.Extensions;

public static class DelayableExtensionsTests
{
    [Theory, AutoData]
    public static void WithDelay_ForDelayable_UsingSharedRandom_ShouldApplyRandomDelayBetweenMiniumAndMaximum(
        [Range(100 ,500)] int fromMilliseconds, [Range(501, 1000)] int toMilliseconds)
    {
        var mimic = new Mimic<MimicTests.ISubject>();

        mimic.Setup(m => m.ConditionalMethod())
            .Returns("After Delay")
            .WithDelay(TimeSpan.FromMilliseconds(fromMilliseconds), TimeSpan.FromMilliseconds(toMilliseconds));

        var stopwatch = Stopwatch.StartNew();
        mimic.Object.ConditionalMethod();
        stopwatch.ElapsedMilliseconds.ShouldBeGreaterThanOrEqualTo(fromMilliseconds);
    }

    [Theory, AutoData]
    public static void WithDelay_ForDelayable_UsingSpecificRandom_ShouldApplyRandomDelayBetweenMiniumAndMaximum(
        [Range(100 ,500)] int fromMilliseconds, [Range(501, 1000)] int toMilliseconds)
    {
        var mimic = new Mimic<MimicTests.ISubject>();

        mimic.Setup(m => m.ConditionalMethod())
            .Returns("After Delay")
            .WithDelay(TimeSpan.FromMilliseconds(fromMilliseconds), TimeSpan.FromMilliseconds(toMilliseconds), new Random());

        var stopwatch = Stopwatch.StartNew();
        mimic.Object.ConditionalMethod();
        stopwatch.ElapsedMilliseconds.ShouldBeGreaterThanOrEqualTo(fromMilliseconds);
    }

    [Theory, AutoData]
    public static void WithDelay_ForSequenceDelayable_UsingSharedRandom_ShouldApplyRandomDelayBetweenMiniumAndMaximum(
        [Range(100 ,500)] int fromMilliseconds, [Range(501, 1000)] int toMilliseconds)
    {
        var mimic = new Mimic<MimicTests.ISubject>();

        mimic.Setup(m => m.ConditionalMethod()).AsSequence()
            .Returns("After Delay")
            .WithDelay(TimeSpan.FromMilliseconds(fromMilliseconds), TimeSpan.FromMilliseconds(toMilliseconds));

        var stopwatch = Stopwatch.StartNew();
        mimic.Object.ConditionalMethod();
        stopwatch.ElapsedMilliseconds.ShouldBeGreaterThanOrEqualTo(fromMilliseconds);
    }

    [Theory, AutoData]
    public static void WithDelay_ForSequenceDelayable_UsingSpecificRandom_ShouldApplyRandomDelayBetweenMiniumAndMaximum(
        [Range(100 ,500)] int fromMilliseconds, [Range(501, 1000)] int toMilliseconds)
    {
        var mimic = new Mimic<MimicTests.ISubject>();

        mimic.Setup(m => m.ConditionalMethod()).AsSequence()
            .Returns("After Delay")
            .WithDelay(TimeSpan.FromMilliseconds(fromMilliseconds), TimeSpan.FromMilliseconds(toMilliseconds), new Random());

        var stopwatch = Stopwatch.StartNew();
        mimic.Object.ConditionalMethod();
        stopwatch.ElapsedMilliseconds.ShouldBeGreaterThanOrEqualTo(fromMilliseconds);
    }
}
