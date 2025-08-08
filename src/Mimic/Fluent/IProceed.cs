namespace Mimic;

/// <summary>
/// Provides functionality to allow the original method implementation to execute while maintaining the mock setup.
/// This interface enables scenarios where you want to combine real method execution with mock behaviors.
/// </summary>
/// <remarks>
/// This interface is marked with <see cref="EditorBrowsableAttribute"/> to hide it from IntelliSense,
/// as it serves as part of the fluent API chain. It allows the setup to proceed with the original method
/// implementation, which is useful for:
/// <list type="bullet">
/// <item><description>Partial mocking - Where some methods are mocked and others execute normally</description></item>
/// <item><description>Spy scenarios - Where you want to observe method calls while preserving functionality</description></item>
/// <item><description>Decorator patterns - Where you want to add behavior before/after real method execution</description></item>
/// </list>
/// When <see cref="Proceed"/> is called, the original method implementation will be invoked with the provided arguments,
/// and any return value will be preserved. This can be combined with other mock behaviors like callbacks or verification.
/// </remarks>
[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IProceed : IFluent
{
    /// <summary>
    /// Configures the setup to proceed with the original method implementation.
    /// When the mocked method is called, it will execute the actual method implementation.
    /// </summary>
    /// <returns>
    /// A <see cref="IProceedResult"/> instance that allows for further configuration
    /// such as exception throwing behavior after the original method executes.
    /// </returns>
    /// <remarks>
    /// This method enables the original method implementation to be called, making it useful for:
    /// <list type="bullet">
    /// <item><description>Testing wrapper classes where you want to verify the wrapper logic but preserve the wrapped functionality</description></item>
    /// <item><description>Implementing spy patterns where you observe method calls without changing behavior</description></item>
    /// <item><description>Partial mocking scenarios where only specific methods are overridden</description></item>
    /// </list>
    /// The returned interface allows you to configure additional behaviors that occur after the original method executes.
    /// </remarks>
    IProceedResult Proceed();
}
