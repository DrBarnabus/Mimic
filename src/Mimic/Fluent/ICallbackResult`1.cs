namespace Mimic;

[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface ICallbackResult<in TResult> : IReturns<TResult>, IThrows, IDelayable, ILimitable, IExpected, IFluent;
