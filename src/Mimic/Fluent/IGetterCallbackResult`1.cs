namespace Mimic;

/// <summary>
/// Represents the result of configuring a callback for a property getter mock setup.
/// This interface combines multiple fluent interfaces to provide method chaining capabilities after a getter callback has been configured.
/// </summary>
/// <typeparam name="TProperty">The type of the property being mocked.</typeparam>
/// <remarks>
/// This interface is marked with <see cref="EditorBrowsableAttribute"/> to hide it from IntelliSense,
/// as it's part of the internal fluent API chain. Users interact with this interface through method chaining
/// after calling <see cref="IGetterCallback{TProperty}.Callback(System.Action)"/>.
/// The interface provides access to configuring return values, throwing exceptions, adding delays, setting call limits, and marking setups as expected.
/// </remarks>
[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IGetterCallbackResult<in TProperty> : IGetterReturns<TProperty>, IThrows, IDelayable, ILimitable, IExpected, IFluent;
