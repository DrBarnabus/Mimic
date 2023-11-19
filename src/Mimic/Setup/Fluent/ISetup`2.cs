namespace Mimic.Setup.Fluent;

[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface ISetup<TMimic, in TResult> : ICallback<TResult>, IReturns<TResult>, ILimitable, IThrows, IVerifiable, IFluent
    where TMimic : class
{
    ISequenceSetup<TResult> AsSequence();
}
