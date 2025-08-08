namespace Mimic;

/// <summary>
/// Provides functionality to configure delays for sequence setups, allowing different delay behaviors for each step in the sequence.
/// This interface enables time-based behavior control in sequential mock setups.
/// </summary>
/// <remarks>
/// This interface is marked with <see cref="EditorBrowsableAttribute"/> to hide it from IntelliSense,
/// as it serves as part of the fluent API chain for sequence setups. It provides delay configuration
/// capabilities specifically designed for sequential behaviors where different steps in the sequence
/// may require different delay characteristics:
/// <list type="bullet">
/// <item><description>Fixed delays - Each step in the sequence uses the same delay duration</description></item>
/// <item><description>Progressive delays - Each step in the sequence can have a different delay based on its position</description></item>
/// </list>
/// This interface is used within sequence setups to control timing between different behaviors
/// that are executed on consecutive method calls.
/// </remarks>
[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface ISequenceDelayable : IFluent
{
    /// <summary>
    /// Configures the current step in the sequence to introduce a fixed delay before execution.
    /// </summary>
    /// <param name="delay">The amount of time to delay before the method executes.</param>
    /// <returns>An <see cref="IExpected"/> instance for further configuration of the sequence step.</returns>
    /// <remarks>
    /// This delay will be applied to the current step in the sequence. Each time this step
    /// is reached during sequential execution, the same delay will be applied.
    /// </remarks>
    IExpected WithDelay(TimeSpan delay);

    /// <summary>
    /// Configures the current step in the sequence to introduce a dynamic delay before execution,
    /// where the delay duration can vary based on the sequence position.
    /// </summary>
    /// <param name="delayFunction">
    /// A function that takes the current sequence position (0-based) and returns the delay duration.
    /// This allows for progressive or custom delay patterns across the sequence.
    /// </param>
    /// <returns>An <see cref="IExpected"/> instance for further configuration of the sequence step.</returns>
    /// <remarks>
    /// The delay function receives the current position in the sequence (starting from 0) and can return
    /// different delay values for each step. This is useful for implementing patterns like:
    /// <list type="bullet">
    /// <item><description>Progressive delays (increasing delay with each step)</description></item>
    /// <item><description>Exponential backoff patterns</description></item>
    /// <item><description>Custom timing sequences based on business logic</description></item>
    /// </list>
    /// </remarks>
    IExpected WithDelay(Func<int, TimeSpan> delayFunction);
}
