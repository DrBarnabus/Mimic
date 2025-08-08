namespace Mimic;

/// <summary>
/// Provides functionality to mark a mock setup as expected, indicating that the setup must be called during testing.
/// This interface is used to enforce verification that certain mock behaviours are actually invoked.
/// </summary>
/// <remarks>
/// This interface is marked with <see cref="EditorBrowsableAttribute"/> to hide it from IntelliSense,
/// as it's part of the internal fluent API chain. Users typically access expected functionality
/// through method chaining on setup expressions. Marking a setup as expected means verification will fail
/// if the setup is not called during test execution.
/// </remarks>
[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IExpected : IFluent
{
    /// <summary>
    /// Marks this mock setup as expected, meaning it must be called during testing for verification to pass.
    /// </summary>
    /// <remarks>
    /// When a setup is marked as expected, the mock framework will track whether this particular setup
    /// was invoked during test execution. If verification is performed and an expected setup was not called,
    /// the verification will fail. This is useful for ensuring that critical code paths are executed.
    /// </remarks>
    void Expected();
}
