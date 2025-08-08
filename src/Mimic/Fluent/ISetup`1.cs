namespace Mimic;

/// <summary>
/// Defines the setup interface for configuring expectations on void methods and property setters of a mimic object.
/// This interface provides the entry point for setting up method call expectations that do not return values.
/// </summary>
/// <typeparam name="TMimic">The type of the mimic object being configured.</typeparam>
/// <remarks>
/// This interface is marked with <see cref="EditorBrowsableAttribute"/> to hide it from IntelliSense,
/// as it serves as part of the fluent API chain. It inherits from multiple interfaces to provide
/// comprehensive configuration options:
/// <list type="bullet">
/// <item><description><see cref="ICallback"/> - For defining callback actions when the method is invoked</description></item>
/// <item><description><see cref="IProceed"/> - For allowing the original method implementation to execute</description></item>
/// <item><description><see cref="IThrows"/> - For configuring exception throwing behavior</description></item>
/// <item><description><see cref="IDelayable"/> - For introducing delays before method execution</description></item>
/// <item><description><see cref="ILimitable"/> - For limiting the number of times the setup can be matched</description></item>
/// <item><description><see cref="IExpected"/> - For defining verification expectations</description></item>
/// </list>
/// This interface is typically used when setting up expectations for void methods, property setters,
/// or other operations that do not return values.
/// </remarks>
[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface ISetup<TMimic> : ICallback, IProceed, IThrows, IDelayable, ILimitable, IExpected, IFluent
    where TMimic : class
{
    /// <summary>
    /// Converts this setup into a sequence setup, allowing for ordered configuration of multiple behaviors
    /// that will be executed in sequence on subsequent method calls.
    /// </summary>
    /// <returns>
    /// A <see cref="ISequenceSetup"/> instance that allows configuring behaviors to be executed
    /// in sequence for consecutive method calls.
    /// </returns>
    /// <remarks>
    /// Sequential setups are useful when you need different behaviors for consecutive calls to the same method.
    /// Each call to the method will advance to the next configured behavior in the sequence.
    /// Once all behaviors in the sequence have been used, subsequent calls will use the last behavior.
    /// </remarks>
    ISequenceSetup AsSequence();
}
