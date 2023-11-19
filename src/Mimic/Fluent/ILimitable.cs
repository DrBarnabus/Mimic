namespace Mimic;

[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface ILimitable : IFluent
{
    IVerifiable Limit(int executionLimit = 1);
}
