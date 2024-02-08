namespace Mimic;

[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface ISetup<TMimic, in TResult> : ICallback<TResult>, IReturns<TResult>, ILimitable, IThrows, IExpected, IFluent
    where TMimic : class
{
    ISequenceSetup<TResult> AsSequence();
}
