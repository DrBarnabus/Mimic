namespace Mimic;

[PublicAPI]
public static class DelayableExtensions
{
    #region IDelayable

    public static IDelayableResult WithDelay(this IDelayable mimic, TimeSpan minDelay, TimeSpan maxDelay)
    {
        return mimic.WithDelay(minDelay, maxDelay, Random.Shared);
    }

    public static IDelayableResult WithDelay(this IDelayable mimic, TimeSpan minDelay, TimeSpan maxDelay, Random random)
    {
        Guard.Assert(minDelay < maxDelay, $"{nameof(minDelay)} must be less than {nameof(maxDelay)}");
        Guard.NotNull(random);

        return mimic.WithDelay(_ => GetRandomDelay(random, minDelay, maxDelay));
    }

    #endregion

    #region ISequenceDelayable

    public static IExpected WithDelay(this ISequenceDelayable mimic, TimeSpan minDelay, TimeSpan maxDelay)
    {
        return mimic.WithDelay(minDelay, maxDelay, Random.Shared);
    }

    public static IExpected WithDelay(this ISequenceDelayable mimic, TimeSpan minDelay, TimeSpan maxDelay, Random random)
    {
        Guard.Assert(minDelay < maxDelay, $"{nameof(minDelay)} must be less than {nameof(maxDelay)}");
        Guard.NotNull(random);

        return mimic.WithDelay(_ => GetRandomDelay(random, minDelay, maxDelay));
    }

    #endregion

    private static TimeSpan GetRandomDelay(Random random, TimeSpan minDelay, TimeSpan maxDelay) =>
        new(random.Next((int)minDelay.Ticks, (int)maxDelay.Ticks));
}
