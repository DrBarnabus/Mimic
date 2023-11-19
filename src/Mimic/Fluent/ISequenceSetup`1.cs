namespace Mimic;

[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface ISequenceSetup<in TResult> : IReturns<TResult, ISequenceSetup<TResult>>, IThrows<ISequenceSetup<TResult>>, IVerifiable, IFluent
{
}
