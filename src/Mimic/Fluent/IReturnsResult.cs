namespace Mimic;

[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IReturnsResult : ICallback, IDelayable, ILimitable, IExpected, IFluent;
