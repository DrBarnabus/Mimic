namespace Mimic;

[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface ICallbackResult : IThrows, IThrowsResult, IDelayable, ILimitable, IExpected, IFluent;
