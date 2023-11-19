namespace Mimic.Setup.Fluent;

[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IVerifiable : IFluent
{
    void Verifiable();
}
