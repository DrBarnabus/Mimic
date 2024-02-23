namespace Mimic;

[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface ISetup<TMimic> : ICallback, IProceed, IThrows, ILimitable, IExpected, IFluent
    where TMimic : class
{
    ISequenceSetup AsSequence();
}
