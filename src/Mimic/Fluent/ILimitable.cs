namespace Mimic;

/// <summary>
/// Provides functionality to limit the number of times a mock setup can be executed.
/// This interface allows you to specify execution constraints on mock behaviors for precise control over call expectations.
/// </summary>
/// <remarks>
/// This interface is marked with <see cref="EditorBrowsableAttribute"/> to hide it from IntelliSense,
/// as it's part of the internal fluent API chain. Users typically access limit functionality
/// through method chaining on setup expressions. Setting execution limits helps ensure that mocked
/// methods are called the expected number of times during testing.
/// </remarks>
[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface ILimitable : IFluent
{
    /// <summary>
    /// Sets the maximum number of times this mock setup can be executed.
    /// </summary>
    /// <param name="executionLimit">The maximum number of times the setup can be executed. Default is 1.</param>
    /// <returns>An <see cref="IExpected"/> that allows marking the setup as expected for verification.</returns>
    /// <remarks>
    /// When a setup reaches its execution limit, subsequent calls will not match this setup and may result
    /// in different behavior depending on the mock's configuration. This is useful for testing scenarios
    /// where you want to ensure a method is called a specific number of times.
    /// </remarks>
    IExpected Limit(int executionLimit = 1);
}
