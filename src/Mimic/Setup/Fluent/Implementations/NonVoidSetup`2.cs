namespace Mimic.Setup.Fluent.Implementations;

internal sealed class NonVoidSetup<TMimic, TResult> : SetupBase, ISetup<TMimic, TResult>, IGetterSetup<TMimic, TResult>, ICallbackResult<TMimic, TResult>, IGetterCallbackResult<TMimic, TResult>, IReturnsResult<TMimic>
    where TMimic : class
{
    public NonVoidSetup(MethodCallSetup setup)
        : base(setup)
    {
    }

    #region Callback

    public new ICallbackResult<TMimic, TResult> Callback(Delegate callback)
    {
        Setup.SetCallbackBehaviour(callback);
        return this;
    }

    public new ICallbackResult<TMimic, TResult> Callback(Action callback)
    {
        Setup.SetCallbackBehaviour(callback);
        return this;
    }

    public new ICallbackResult<TMimic, TResult> Callback<T1>(Action<T1> callback)
    {
        Setup.SetCallbackBehaviour(callback);
        return this;
    }

    public new ICallbackResult<TMimic, TResult> Callback<T1, T2>(Action<T1, T2> callback)
    {
        Setup.SetCallbackBehaviour(callback);
        return this;
    }

    public new ICallbackResult<TMimic, TResult> Callback<T1, T2, T3>(Action<T1, T2, T3> callback)
    {
        Setup.SetCallbackBehaviour(callback);
        return this;
    }

    public new ICallbackResult<TMimic, TResult> Callback<T1, T2, T3, T4>(Action<T1, T2, T3, T4> callback)
    {
        Setup.SetCallbackBehaviour(callback);
        return this;
    }

    public new ICallbackResult<TMimic, TResult> Callback<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> callback)
    {
        Setup.SetCallbackBehaviour(callback);
        return this;
    }

    public new ICallbackResult<TMimic, TResult> Callback<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> callback)
    {
        Setup.SetCallbackBehaviour(callback);
        return this;
    }

    public new ICallbackResult<TMimic, TResult> Callback<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> callback)
    {
        Setup.SetCallbackBehaviour(callback);
        return this;
    }

    public new ICallbackResult<TMimic, TResult> Callback<T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8> callback)
    {
        Setup.SetCallbackBehaviour(callback);
        return this;
    }

    public new ICallbackResult<TMimic, TResult> Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> callback)
    {
        Setup.SetCallbackBehaviour(callback);
        return this;
    }

    public new ICallbackResult<TMimic, TResult> Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> callback)
    {
        Setup.SetCallbackBehaviour(callback);
        return this;
    }

    public new ICallbackResult<TMimic, TResult> Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> callback)
    {
        Setup.SetCallbackBehaviour(callback);
        return this;
    }

    public new ICallbackResult<TMimic, TResult> Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> callback)
    {
        Setup.SetCallbackBehaviour(callback);
        return this;
    }

    public new ICallbackResult<TMimic, TResult> Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> callback)
    {
        Setup.SetCallbackBehaviour(callback);
        return this;
    }

    public new ICallbackResult<TMimic, TResult> Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> callback)
    {
        Setup.SetCallbackBehaviour(callback);
        return this;
    }

    public new ICallbackResult<TMimic, TResult> Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> callback)
    {
        Setup.SetCallbackBehaviour(callback);
        return this;
    }

    public new ICallbackResult<TMimic, TResult> Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> callback)
    {
        Setup.SetCallbackBehaviour(callback);
        return this;
    }

    #endregion

    #region GetterCallback

    IGetterCallbackResult<TMimic, TResult> IGetterCallback<TMimic, TResult>.Callback(Action callback)
    {
        Setup.SetCallbackBehaviour(callback);
        return this;
    }

    #endregion

    #region Returns

    public IReturnsResult<TMimic> Returns(TResult? value)
    {
        Setup.SetReturnValueBehaviour(value);
        return this;
    }

    public IReturnsResult<TMimic> Returns(Delegate valueFactory)
    {
        Setup.SetReturnComputedValueBehaviour(valueFactory);
        return this;
    }

    public IReturnsResult<TMimic> Returns(Func<TResult?> valueFactory)
    {
        Setup.SetReturnComputedValueBehaviour(valueFactory);
        return this;
    }

    public IReturnsResult<TMimic> Returns<T1>(Func<T1, TResult> valueFactory)
    {
        Setup.SetReturnComputedValueBehaviour(valueFactory);
        return this;
    }

    public IReturnsResult<TMimic> Returns<T1, T2>(Func<T1, T2, TResult> valueFactory)
    {
        Setup.SetReturnComputedValueBehaviour(valueFactory);
        return this;
    }

    public IReturnsResult<TMimic> Returns<T1, T2, T3>(Func<T1, T2, T3, TResult> valueFactory)
    {
        Setup.SetReturnComputedValueBehaviour(valueFactory);
        return this;
    }

    public IReturnsResult<TMimic> Returns<T1, T2, T3, T4>(Func<T1, T2, T3, T4, TResult> valueFactory)
    {
        Setup.SetReturnComputedValueBehaviour(valueFactory);
        return this;
    }

    public IReturnsResult<TMimic> Returns<T1, T2, T3, T4, T5>(Func<T1, T2, T3, T4, T5, TResult> valueFactory)
    {
        Setup.SetReturnComputedValueBehaviour(valueFactory);
        return this;
    }

    public IReturnsResult<TMimic> Returns<T1, T2, T3, T4, T5, T6>(Func<T1, T2, T3, T4, T5, T6, TResult> valueFactory)
    {
        Setup.SetReturnComputedValueBehaviour(valueFactory);
        return this;
    }

    public IReturnsResult<TMimic> Returns<T1, T2, T3, T4, T5, T6, T7>(Func<T1, T2, T3, T4, T5, T6, T7, TResult> valueFactory)
    {
        Setup.SetReturnComputedValueBehaviour(valueFactory);
        return this;
    }

    public IReturnsResult<TMimic> Returns<T1, T2, T3, T4, T5, T6, T7, T8>(Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> valueFactory)
    {
        Setup.SetReturnComputedValueBehaviour(valueFactory);
        return this;
    }

    public IReturnsResult<TMimic> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> valueFactory)
    {
        Setup.SetReturnComputedValueBehaviour(valueFactory);
        return this;
    }

    public IReturnsResult<TMimic> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> valueFactory)
    {
        Setup.SetReturnComputedValueBehaviour(valueFactory);
        return this;
    }

    public IReturnsResult<TMimic> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> valueFactory)
    {
        Setup.SetReturnComputedValueBehaviour(valueFactory);
        return this;
    }

    public IReturnsResult<TMimic> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> valueFactory)
    {
        Setup.SetReturnComputedValueBehaviour(valueFactory);
        return this;
    }

    public IReturnsResult<TMimic> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> valueFactory)
    {
        Setup.SetReturnComputedValueBehaviour(valueFactory);
        return this;
    }

    public IReturnsResult<TMimic> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> valueFactory)
    {
        Setup.SetReturnComputedValueBehaviour(valueFactory);
        return this;
    }

    public IReturnsResult<TMimic> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> valueFactory)
    {
        Setup.SetReturnComputedValueBehaviour(valueFactory);
        return this;
    }

    public IReturnsResult<TMimic> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> valueFactory)
    {
        Setup.SetReturnComputedValueBehaviour(valueFactory);
        return this;
    }

    #endregion
}
