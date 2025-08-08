namespace Mimic;

/// <summary>
/// Represents the result of configuring a delay for a mock setup.
/// This interface combines multiple fluent interfaces to provide method chaining capabilities after a delay has been configured.
/// </summary>
/// <remarks>
/// This interface is marked with <see cref="EditorBrowsableAttribute"/> to hide it from IntelliSense,
/// as it's part of the internal fluent API chain. Users interact with this interface through method chaining
/// after calling <see cref="IDelayable.WithDelay(TimeSpan)"/> or its overloads.
/// The interface provides access to setting call limits and marking setups as expected.
/// </remarks>
[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IDelayableResult : ILimitable, IExpected, IFluent;
