namespace Mimic;

/// <summary>
/// Provides functionality to configure sequential behaviors for methods that return values in a mimic setup.
/// This interface allows defining different return values and behaviors that will be executed in order on consecutive method calls.
/// </summary>
/// <typeparam name="TResult">The type of value returned by the method being configured in the sequence.</typeparam>
/// <remarks>
/// This interface is marked with <see cref="EditorBrowsableAttribute"/> to hide it from IntelliSense,
/// as it serves as part of the fluent API chain for sequence setups. It inherits from multiple interfaces
/// to provide comprehensive sequential configuration options for return-value methods:
/// <list type="bullet">
/// <item><description><see cref="IReturns{TResult, TNext}"/> - For configuring return values with sequence continuation</description></item>
/// <item><description><see cref="IThrows{TNext}"/> - For configuring exception throwing that continues the sequence</description></item>
/// <item><description><see cref="ISequenceDelayable"/> - For configuring delays in the sequence execution</description></item>
/// <item><description><see cref="IExpected"/> - For defining verification expectations on sequence steps</description></item>
/// </list>
/// Sequential setups are useful when you need different return values or behaviors for consecutive calls
/// to the same method. Each call advances to the next step in the sequence. Once all steps are exhausted,
/// subsequent calls will repeat the last configured behavior.
/// This interface is specifically for methods that return values and provides full return value configuration
/// capabilities for each step in the sequence.
/// </remarks>
[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface ISequenceSetup<in TResult> : IReturns<TResult, ISequenceSetup<TResult>>, IThrows<ISequenceSetup<TResult>>, ISequenceDelayable, IExpected, IFluent;
