using System.ComponentModel;
using JetBrains.Annotations;

namespace Mimic.Setup.Fluent;

[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IGetterCallback<TMimic, in TProperty> : IFluent
    where TMimic : class
{
    IGetterCallbackResult<TMimic, TProperty> Callback(Action callback);
}
