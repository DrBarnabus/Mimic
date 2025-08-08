namespace Mimic;

/// <summary>
/// Provides functionality to configure return values for methods in a mimic setup.
/// This is a simplified version of <see cref="IReturns{TResult, TNext}"/> that returns <see cref="IReturnsResult"/>.
/// </summary>
/// <typeparam name="TResult">The type of value that the configured method should return.</typeparam>
/// <remarks>
/// This interface is marked with <see cref="EditorBrowsableAttribute"/> to hide it from IntelliSense,
/// as it serves as part of the fluent API chain. It inherits from <see cref="IReturns{TResult, TNext}"/>
/// with <see cref="IReturnsResult"/> as the continuation type, providing a simplified interface for
/// return value configuration where the next step in the fluent chain is predetermined.
/// This interface is typically used in setups where return value configuration is the primary concern,
/// and the user doesn't need access to additional configuration options.
/// </remarks>
[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IReturns<in TResult> : IReturns<TResult, IReturnsResult>;
