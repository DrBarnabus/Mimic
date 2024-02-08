namespace Mimic;

[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface ILimitable : IFluent
{
    IExpected Limit(int executionLimit = 1);
}
