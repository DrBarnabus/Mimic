namespace Mimic;

/// <summary>
/// Base interface for all fluent API interfaces in the Mimic library.
/// This interface provides a clean fluent interface experience by hiding common Object methods from IntelliSense.
/// </summary>
/// <remarks>
/// This interface is marked with <see cref="EditorBrowsableAttribute"/> to hide it from IntelliSense,
/// as it serves as the foundation for the fluent API chain. It explicitly declares and hides common Object methods
/// (GetType, GetHashCode, ToString, Equals) to prevent them from appearing in IntelliSense when using the fluent API,
/// providing a cleaner development experience focused on the actual mock configuration methods.
/// All other fluent interfaces in the library inherit from this interface.
/// </remarks>
[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IFluent
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    Type GetType();

    [EditorBrowsable(EditorBrowsableState.Never)]
    int GetHashCode();

    [EditorBrowsable(EditorBrowsableState.Never)]
    string ToString();

    [EditorBrowsable(EditorBrowsableState.Never)]
    bool Equals(object obj);
}
