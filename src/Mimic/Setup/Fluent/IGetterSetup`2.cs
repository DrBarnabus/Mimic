using System.ComponentModel;
using JetBrains.Annotations;

namespace Mimic.Setup.Fluent;

[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IGetterSetup<TMimic, in TProperty> : IGetterCallback<TMimic, TProperty>,  IGetterReturns<TMimic, TProperty>, IThrows, ILimitable, IVerifiable, IFluent
    where TMimic : class
{
}
