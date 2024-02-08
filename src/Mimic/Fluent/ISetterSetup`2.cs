namespace Mimic;

[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface ISetterSetup<TMimic, out TProperty> : ISetterCallback<TProperty>, ILimitable, IExpected, IFluent
    where TMimic : class
{
}
