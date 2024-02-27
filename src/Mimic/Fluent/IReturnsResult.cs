namespace Mimic;

[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IReturnsResult : ICallback, ILimitable, IExpected, IFluent;
