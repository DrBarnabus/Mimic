namespace Mimic;

[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IGetterCallback<in TProperty> : IFluent
{
    IGetterCallbackResult<TProperty> Callback(Action callback);
}
