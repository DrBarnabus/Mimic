namespace Mimic;

[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IGetterSetup<TMimic, in TProperty> : IGetterCallback<TProperty>,  IGetterReturns<TProperty>, IThrows, ILimitable, IVerifiable, IFluent
{
}
