using System.ComponentModel;
using JetBrains.Annotations;

namespace Mimic.Setup.Fluent;

[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IGetterCallbackResult<TMimic, in TProperty> : IGetterReturns<TMimic, TProperty>, IThrows, ILimitable, IVerifiable, IFluent
    where TMimic : class
{
}
