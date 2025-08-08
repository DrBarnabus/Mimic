namespace Mimic;

/// <summary>
/// Represents the result interface after configuring exception throwing behavior in a mimic setup.
/// This interface provides additional configuration options that can be applied after defining what exception a method should throw.
/// </summary>
/// <remarks>
/// This interface is marked with <see cref="EditorBrowsableAttribute"/> to hide it from IntelliSense,
/// as it serves as part of the fluent API chain. It inherits from multiple interfaces to provide
/// additional configuration capabilities after an exception throwing behavior has been specified:
/// <list type="bullet">
/// <item><description><see cref="IDelayable"/> - For introducing delays before method execution</description></item>
/// <item><description><see cref="ILimitable"/> - For limiting the number of times the setup can be matched</description></item>
/// <item><description><see cref="IExpected"/> - For defining verification expectations</description></item>
/// </list>
/// This interface is typically reached after calling a <c>Throws</c> method on a setup, allowing you to
/// further customize the behavior with delays, occurrence limits, or verification requirements.
/// It serves as the continuation point in the fluent API chain for exception-throwing-configured setups.
/// Note that callback functionality is not available for exception-throwing setups, as the method
/// execution is terminated by the exception.
/// </remarks>
[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IThrowsResult : IDelayable, ILimitable, IExpected, IFluent;
