namespace Mimic;

/// <summary>
/// Provides comprehensive setup functionality for property getter mock configurations.
/// This interface combines all available property getter configuration options including callbacks, return values, exceptions, delays, call limits, and expectations.
/// </summary>
/// <typeparam name="TMimic">The type being mocked.</typeparam>
/// <typeparam name="TProperty">The type of the property being mocked.</typeparam>
/// <remarks>
/// This interface is marked with <see cref="EditorBrowsableAttribute"/> to hide it from IntelliSense,
/// as it's part of the internal fluent API chain. Users typically access this interface through method chaining
/// after calling property getter setup methods. This interface serves as the main entry point for configuring
/// property getter behavior, combining all available configuration options into a single fluent interface.
/// </remarks>
[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IGetterSetup<TMimic, in TProperty> : IGetterCallback<TProperty>,  IGetterReturns<TProperty>, IThrows, IDelayable, ILimitable, IExpected, IFluent;
