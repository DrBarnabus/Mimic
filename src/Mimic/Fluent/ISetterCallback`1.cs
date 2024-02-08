namespace Mimic;

[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface ISetterCallback<out TProperty> : ILimitable, IExpected, IFluent
{
    ICallbackResult Callback(Action<TProperty> callback);
}
