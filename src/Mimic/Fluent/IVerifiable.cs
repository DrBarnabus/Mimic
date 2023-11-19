namespace Mimic;

[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IVerifiable : IFluent
{
    void Verifiable();
}
