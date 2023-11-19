namespace Mimic.Setup.Fluent;

[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface ISetterCallback<out TProperty> : ILimitable, IVerifiable, IFluent
{
    ICallbackResult Callback(Action<TProperty> callback);
}
