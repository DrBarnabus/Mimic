namespace Mimic;

[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface ISetterSetup<TMimic, out TProperty> : ISetterCallback<TProperty>, IDelayable, ILimitable, IExpected, IFluent
    where TMimic : class;
