namespace Mimic;

[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IMimicked<T>
    where T : class
{
    Mimic<T> Mimic { get; }
}
