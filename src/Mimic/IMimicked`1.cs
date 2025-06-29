namespace Mimic;

/// <summary>
/// Represents an interface for accessing the underlying mimic instance for a mocked object.
/// </summary>
/// <typeparam name="T">The type being mocked, which must be a reference type.</typeparam>
[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IMimicked<T>
    where T : class
{
    /// <summary>
    /// Gets the <see cref="Mimic{T}"/> instance associated with the mocked object.
    /// </summary>
    /// <value>
    /// The mimic instance that controls the behaviour and verification of the mocked object.
    /// </value>
    Mimic<T> Mimic { get; }
}
