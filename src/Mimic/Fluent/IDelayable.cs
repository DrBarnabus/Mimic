namespace Mimic;

/// <summary>
/// Provides delay functionality for mock setups, allowing you to introduce timing delays before method execution or return values.
/// This interface enables realistic timing behavior in mock scenarios by adding configurable delays.
/// </summary>
/// <remarks>
/// This interface is marked with <see cref="EditorBrowsableAttribute"/> to hide it from IntelliSense,
/// as it's part of the internal fluent API chain. Users typically access delay functionality
/// through method chaining on setup expressions. Delays can be fixed or dynamic based on call count.
/// </remarks>
[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IDelayable : IFluent
{
    /// <summary>
    /// Adds a fixed delay before the mocked method execution completes.
    /// </summary>
    /// <param name="delay">The amount of time to delay before completing the method call.</param>
    /// <returns>A <see cref="IDelayableResult"/> that allows further configuration of the mock setup.</returns>
    IDelayableResult WithDelay(TimeSpan delay);

    /// <summary>
    /// Adds a dynamic delay before the mocked method execution completes, based on the call count.
    /// </summary>
    /// <param name="delayFunction">A function that takes the call count (starting from 0) and returns the delay duration for that call.</param>
    /// <returns>A <see cref="IDelayableResult"/> that allows further configuration of the mock setup.</returns>
    /// <remarks>
    /// The delay function receives the zero-based call count, allowing for different delays based on how many times the method has been called.
    /// This enables scenarios like simulating increasing latency or timeout patterns.
    /// </remarks>
    IDelayableResult WithDelay(Func<int, TimeSpan> delayFunction);
}
