using System.ComponentModel;
using JetBrains.Annotations;

namespace Mimic.Setup.Fluent;

[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface ISetterCallback<TMimic, out TProperty> : IFluent
    where TMimic : class
{
    ICallbackResult Callback(Action<TProperty> callback);
}