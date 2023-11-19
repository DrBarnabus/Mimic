namespace Mimic;

[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface ICallback<in TResult> : IFluent
{
    ICallbackResult<TResult> Callback(Delegate callback);

    ICallbackResult<TResult> Callback(Action callback);

    ICallbackResult<TResult> Callback<T>(Action<T> callback);

    ICallbackResult<TResult> Callback<T1, T2>(Action<T1, T2> callback);

    ICallbackResult<TResult> Callback<T1, T2, T3>(Action<T1, T2, T3> callback);

    ICallbackResult<TResult> Callback<T1, T2, T3, T4>(Action<T1, T2, T3, T4> callback);

    ICallbackResult<TResult> Callback<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> callback);

    ICallbackResult<TResult> Callback<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> callback);

    ICallbackResult<TResult> Callback<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> callback);

    ICallbackResult<TResult> Callback<T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8> callback);

    ICallbackResult<TResult> Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> callback);

    ICallbackResult<TResult> Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> callback);

    ICallbackResult<TResult> Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> callback);

    ICallbackResult<TResult> Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> callback);

    ICallbackResult<TResult> Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> callback);

    ICallbackResult<TResult> Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> callback);

    ICallbackResult<TResult> Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> callback);

    ICallbackResult<TResult> Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> callback);
}
