namespace Mimic.Setup.Fluent;

[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IReturns<in TResult> : IReturns<TResult, IReturnsResult>
{
}
