namespace Mimic;

[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IReturns<in TResult> : IReturns<TResult, IReturnsResult>
{
}
