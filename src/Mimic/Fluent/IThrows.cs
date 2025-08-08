namespace Mimic;

/// <summary>
/// Provides functionality to configure exception throwing behavior for methods in a mimic setup.
/// This is a simplified version of <see cref="IThrows{TNext}"/> that returns <see cref="IThrowsResult"/>.
/// </summary>
/// <remarks>
/// This interface is marked with <see cref="EditorBrowsableAttribute"/> to hide it from IntelliSense,
/// as it serves as part of the fluent API chain. It inherits from <see cref="IThrows{TNext}"/>
/// with <see cref="IThrowsResult"/> as the continuation type, providing a simplified interface for
/// exception throwing configuration where the next step in the fluent chain is predetermined.
/// This interface is typically used in setups where exception throwing configuration is the primary concern,
/// and the user doesn't need access to additional configuration options.
/// </remarks>
[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IThrows : IThrows<IThrowsResult>;
