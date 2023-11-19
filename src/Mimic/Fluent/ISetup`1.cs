namespace Mimic;

[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface ISetup<TMimic> : ICallback, IThrows, ILimitable, IVerifiable, IFluent
    where TMimic : class
{
    ISequenceSetup AsSequence();
}
