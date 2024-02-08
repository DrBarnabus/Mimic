namespace Mimic;

[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface ISetup<TMimic> : ICallback, IThrows, ILimitable, IExpected, IFluent
    where TMimic : class
{
    ISequenceSetup AsSequence();
}
