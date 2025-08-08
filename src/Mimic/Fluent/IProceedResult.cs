namespace Mimic;

/// <summary>
/// Represents the result interface after configuring a method setup to proceed with its original implementation.
/// This interface provides limited additional configuration options for scenarios that combine real method execution with mock behaviors.
/// </summary>
/// <remarks>
/// This interface is marked with <see cref="EditorBrowsableAttribute"/> to hide it from IntelliSense,
/// as it serves as part of the fluent API chain. It inherits from exception throwing interfaces to provide
/// the ability to conditionally throw exceptions even when proceeding with the original implementation:
/// <list type="bullet">
/// <item><description><see cref="IThrows"/> - For configuring exception throwing behavior that can override the proceed behavior</description></item>
/// <item><description><see cref="IThrowsResult"/> - Provides the result interface capabilities after exception configuration</description></item>
/// </list>
/// This interface is typically reached after calling <see cref="IProceed.Proceed()"/> on a setup.
/// It provides a more limited set of configuration options compared to other result interfaces because
/// the method execution is delegated to the original implementation.
/// Note that return value configuration, callbacks, and delays are not available since the behavior
/// is determined by the original method implementation.
/// </remarks>
[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IProceedResult : IThrows, IThrowsResult, IFluent;
