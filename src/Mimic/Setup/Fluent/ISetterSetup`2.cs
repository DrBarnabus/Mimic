namespace Mimic.Setup.Fluent;

[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface ISetterSetup<TMimic, out TProperty> : ISetterCallback<TProperty>, ILimitable, IVerifiable, IFluent
    where TMimic : class
{
}
