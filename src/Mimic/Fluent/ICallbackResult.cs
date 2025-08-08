namespace Mimic;

/// <summary>
/// Represents the result of configuring a callback for a mock setup that does not return a value.
/// This interface combines multiple fluent interfaces to provide method chaining capabilities after a callback has been configured.
/// </summary>
/// <remarks>
/// This interface is marked with <see cref="EditorBrowsableAttribute"/> to hide it from IntelliSense,
/// as it's part of the internal fluent API chain. Users interact with this interface through method chaining
/// after calling <see cref="ICallback.Callback(System.Delegate)"/> or its overloads.
/// The interface provides access to throwing exceptions, adding delays, setting call limits, and marking setups as expected.
/// </remarks>
[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface ICallbackResult : IThrows, IThrowsResult, IDelayable, ILimitable, IExpected, IFluent;
