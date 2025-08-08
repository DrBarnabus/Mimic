namespace Mimic;

/// <summary>
/// Provides functionality to configure sequential behaviors for void methods in a mimic setup.
/// This interface allows defining different behaviors that will be executed in order on consecutive method calls.
/// </summary>
/// <remarks>
/// This interface is marked with <see cref="EditorBrowsableAttribute"/> to hide it from IntelliSense,
/// as it serves as part of the fluent API chain for sequence setups. It inherits from multiple interfaces
/// to provide comprehensive sequential configuration options:
/// <list type="bullet">
/// <item><description><see cref="IThrows{TNext}"/> - For configuring exception throwing that continues the sequence</description></item>
/// <item><description><see cref="ISequenceDelayable"/> - For configuring delays in the sequence execution</description></item>
/// <item><description><see cref="IExpected"/> - For defining verification expectations on sequence steps</description></item>
/// </list>
/// Sequential setups are useful when you need different behaviors for consecutive calls to the same void method.
/// Each call advances to the next step in the sequence. Once all steps are exhausted, subsequent calls
/// will repeat the last configured behavior.
/// This interface is specifically for void methods and doesn't provide return value configuration.
/// </remarks>
[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface ISequenceSetup : IThrows<ISequenceSetup>, ISequenceDelayable, IExpected, IFluent
{
    /// <summary>
    /// Configures the current step in the sequence to proceed with the original method implementation,
    /// then continues to allow configuration of the next step.
    /// </summary>
    /// <returns>
    /// The same <see cref="ISequenceSetup"/> instance to continue configuring the sequence
    /// or move to the next step using <see cref="Next()"/>.
    /// </returns>
    /// <remarks>
    /// This method allows the real method implementation to execute for this step in the sequence.
    /// After the original method executes, the sequence can continue with additional configuration
    /// or advance to the next step. This is useful for partial mocking scenarios where some
    /// calls should execute normally while others have mocked behavior.
    /// </remarks>
    ISequenceSetup Proceed();

    /// <summary>
    /// Advances to the next step in the sequence, allowing configuration of the behavior
    /// for the subsequent method call.
    /// </summary>
    /// <returns>
    /// The same <see cref="ISequenceSetup"/> instance configured for the next step in the sequence.
    /// </returns>
    /// <remarks>
    /// This method moves the sequence configuration to the next step. The behavior configured
    /// after calling this method will be applied to the next method call in the sequence.
    /// You can continue chaining additional configuration methods or call <see cref="Next()"/> again
    /// to configure further steps in the sequence.
    /// </remarks>
    ISequenceSetup Next();
}
