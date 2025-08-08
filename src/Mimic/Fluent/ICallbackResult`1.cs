namespace Mimic;

/// <summary>
/// Represents the result of configuring a callback for a mock setup that returns a value of type <typeparamref name="TResult"/>.
/// This interface combines multiple fluent interfaces to provide method chaining capabilities after a callback has been configured.
/// </summary>
/// <typeparam name="TResult">The type of value returned by the mocked method.</typeparam>
/// <remarks>
/// This interface is marked with <see cref="EditorBrowsableAttribute"/> to hide it from IntelliSense,
/// as it's part of the internal fluent API chain. Users interact with this interface through method chaining
/// after calling <see cref="ICallback{TResult}.Callback(System.Delegate)"/> or its overloads.
/// The interface provides access to configuring return values, throwing exceptions, adding delays, setting call limits, and marking setups as expected.
/// </remarks>
[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface ICallbackResult<in TResult> : IReturns<TResult>, IThrows, IDelayable, ILimitable, IExpected, IFluent;
