namespace Mimic;

/// <summary>
/// Provides extension methods for adding random delays to mimic setups, enhancing the realism of mock behaviours by introducing timing variability.
/// </summary>
[PublicAPI]
public static class DelayableExtensions
{
    #region IDelayable

    /// <summary>
    /// Adds a random delay between the specified minimum and maximum values to the mimic setup using <see cref="Random.Shared"/>.
    /// </summary>
    /// <param name="mimic">The mimic instance to add the delay to.</param>
    /// <param name="minDelay">The minimum delay duration.</param>
    /// <param name="maxDelay">The maximum delay duration.</param>
    /// <returns>An <see cref="IDelayableResult"/> that can be used to continue configuring the mimic setup.</returns>
    /// <exception cref="Guard.AssertionException">Thrown when <paramref name="minDelay"/> is not less than <paramref name="maxDelay"/>.</exception>
    /// <remarks>
    /// This method uses <see cref="Random.Shared"/> for generating random delays, making it suitable for most scenarios.
    /// For deterministic testing or when you need control over the randomisation, use the overload that accepts a <see cref="Random"/> parameter.
    /// </remarks>
    public static IDelayableResult WithDelay(this IDelayable mimic, TimeSpan minDelay, TimeSpan maxDelay)
    {
        return mimic.WithDelay(minDelay, maxDelay, Random.Shared);
    }

    /// <summary>
    /// Adds a random delay between the specified minimum and maximum values to the mimic setup using the provided random number generator.
    /// </summary>
    /// <param name="mimic">The mimic instance to add the delay to.</param>
    /// <param name="minDelay">The minimum delay duration.</param>
    /// <param name="maxDelay">The maximum delay duration.</param>
    /// <param name="random">The random number generator to use for generating delays.</param>
    /// <returns>An <see cref="IDelayableResult"/> that can be used to continue configuring the mimic setup.</returns>
    /// <exception cref="Guard.AssertionException">Thrown when <paramref name="minDelay"/> is not less than <paramref name="maxDelay"/>.</exception>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="random"/> is <c>null</c>.</exception>
    /// <remarks>
    /// This overload allows you to provide your own <see cref="Random"/> instance, which is useful for deterministic testing
    /// or when you need to control the seed for reproducible random delays.
    /// </remarks>
    public static IDelayableResult WithDelay(this IDelayable mimic, TimeSpan minDelay, TimeSpan maxDelay, Random random)
    {
        Guard.Assert(minDelay < maxDelay, $"{nameof(minDelay)} must be less than {nameof(maxDelay)}");
        Guard.NotNull(random);

        return mimic.WithDelay(_ => GetRandomDelay(random, minDelay, maxDelay));
    }

    #endregion

    #region ISequenceDelayable

    /// <summary>
    /// Adds a random delay between the specified minimum and maximum values to the sequence mimic setup using <see cref="Random.Shared"/>.
    /// </summary>
    /// <param name="mimic">The sequence Mimic instance to add the delay to.</param>
    /// <param name="minDelay">The minimum delay duration.</param>
    /// <param name="maxDelay">The maximum delay duration.</param>
    /// <returns>An <see cref="IExpected"/> that can be used to continue configuring the sequence mimic setup.</returns>
    /// <exception cref="Guard.AssertionException">Thrown when <paramref name="minDelay"/> is not less than <paramref name="maxDelay"/>.</exception>
    /// <remarks>
    /// This method uses <see cref="Random.Shared"/> for generating random delays, making it suitable for most scenarios.
    /// For deterministic testing or when you need control over the randomisation, use the overload that accepts a <see cref="Random"/> parameter.
    /// This overload is specifically designed for sequence-based mimic setups where the delay applies to sequential calls.
    /// </remarks>
    public static IExpected WithDelay(this ISequenceDelayable mimic, TimeSpan minDelay, TimeSpan maxDelay)
    {
        return mimic.WithDelay(minDelay, maxDelay, Random.Shared);
    }

    /// <summary>
    /// Adds a random delay between the specified minimum and maximum values to the sequence mimic setup using the provided random number generator.
    /// </summary>
    /// <param name="mimic">The sequence Mimic instance to add the delay to.</param>
    /// <param name="minDelay">The minimum delay duration.</param>
    /// <param name="maxDelay">The maximum delay duration.</param>
    /// <param name="random">The random number generator to use for generating delays.</param>
    /// <returns>An <see cref="IExpected"/> that can be used to continue configuring the sequence mimic setup.</returns>
    /// <exception cref="Guard.AssertionException">Thrown when <paramref name="minDelay"/> is not less than <paramref name="maxDelay"/>.</exception>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="random"/> is <c>null</c>.</exception>
    /// <remarks>
    /// This overload allows you to provide your own <see cref="Random"/> instance, which is useful for deterministic testing
    /// or when you need to control the seed for reproducible random delays.
    /// This method is specifically designed for sequence-based mimic setups where the delay applies to sequential calls.
    /// </remarks>
    public static IExpected WithDelay(this ISequenceDelayable mimic, TimeSpan minDelay, TimeSpan maxDelay, Random random)
    {
        Guard.Assert(minDelay < maxDelay, $"{nameof(minDelay)} must be less than {nameof(maxDelay)}");
        Guard.NotNull(random);

        return mimic.WithDelay(_ => GetRandomDelay(random, minDelay, maxDelay));
    }

    #endregion

    /// <summary>
    /// Generates a random <see cref="TimeSpan"/> between the specified minimum and maximum delays.
    /// </summary>
    /// <param name="random">The random number generator to use.</param>
    /// <param name="minDelay">The minimum delay duration.</param>
    /// <param name="maxDelay">The maximum delay duration.</param>
    /// <returns>A random <see cref="TimeSpan"/> between <paramref name="minDelay"/> and <paramref name="maxDelay"/>.</returns>
    private static TimeSpan GetRandomDelay(Random random, TimeSpan minDelay, TimeSpan maxDelay) =>
        new(random.Next((int)minDelay.Ticks, (int)maxDelay.Ticks));
}
