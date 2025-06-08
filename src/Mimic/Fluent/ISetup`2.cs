namespace Mimic;

[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface ISetup<TMimic, in TResult> : ICallback<TResult>, IReturns<TResult>, IDelayable, ILimitable, IThrows, IExpected, IFluent
    where TMimic : class
{
    ISequenceSetup<TResult> AsSequence();
}
