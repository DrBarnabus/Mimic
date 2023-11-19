using System.ComponentModel;
using JetBrains.Annotations;

namespace Mimic.Setup.Fluent;

[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IGetterCallback<in TProperty> : IFluent
{
    IGetterCallbackResult<TProperty> Callback(Action callback);
}
