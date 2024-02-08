namespace Mimic;

[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IGetterCallbackResult<in TProperty> : IGetterReturns<TProperty>, IThrows, ILimitable, IExpected, IFluent
{
}
