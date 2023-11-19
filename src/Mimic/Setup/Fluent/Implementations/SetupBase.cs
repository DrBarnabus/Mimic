using Mimic.Core;

namespace Mimic.Setup.Fluent.Implementations;

internal class SetupBase : ICallback, ICallbackResult, IThrows, IThrowsResult
{
    protected SetupBase(MethodCallSetup setup)
    {
        Guard.NotNull(setup);

        Setup = setup;
    }

    protected MethodCallSetup Setup { get; }

    #region Callback

    public ICallbackResult Callback(Delegate callback)
    {
        Setup.SetCallbackBehaviour(callback);
        return this;
    }

    public ICallbackResult Callback(Action callback)
    {
        Setup.SetCallbackBehaviour(callback);
        return this;
    }

    public ICallbackResult Callback<T>(Action<T> callback)
    {
        Setup.SetCallbackBehaviour(callback);
        return this;
    }

    public ICallbackResult Callback<T1, T2>(Action<T1, T2> callback)
    {
        Setup.SetCallbackBehaviour(callback);
        return this;
    }

    public ICallbackResult Callback<T1, T2, T3>(Action<T1, T2, T3> callback)
    {
        Setup.SetCallbackBehaviour(callback);
        return this;
    }

    public ICallbackResult Callback<T1, T2, T3, T4>(Action<T1, T2, T3, T4> callback)
    {
        Setup.SetCallbackBehaviour(callback);
        return this;
    }

    public ICallbackResult Callback<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> callback)
    {
        Setup.SetCallbackBehaviour(callback);
        return this;
    }

    public ICallbackResult Callback<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> callback)
    {
        Setup.SetCallbackBehaviour(callback);
        return this;
    }

    public ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> callback)
    {
        Setup.SetCallbackBehaviour(callback);
        return this;
    }

    public ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8> callback)
    {
        Setup.SetCallbackBehaviour(callback);
        return this;
    }

    public ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> callback)
    {
        Setup.SetCallbackBehaviour(callback);
        return this;
    }

    public ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> callback)
    {
        Setup.SetCallbackBehaviour(callback);
        return this;
    }

    public ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> callback)
    {
        Setup.SetCallbackBehaviour(callback);
        return this;
    }

    public ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> callback)
    {
        Setup.SetCallbackBehaviour(callback);
        return this;
    }

    public ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> callback)
    {
        Setup.SetCallbackBehaviour(callback);
        return this;
    }

    public ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> callback)
    {
        Setup.SetCallbackBehaviour(callback);
        return this;
    }

    public ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> callback)
    {
        Setup.SetCallbackBehaviour(callback);
        return this;
    }

    public ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> callback)
    {
        Setup.SetCallbackBehaviour(callback);
        return this;
    }

    #endregion

    #region Throws

    public IThrowsResult Throws(Exception exception)
    {
        Setup.SetThrowExceptionBehaviour(exception);
        return this;
    }

    public IThrowsResult Throws<TException>()
        where TException : Exception, new()
    {
        Setup.SetThrowExceptionBehaviour(new TException());
        return this;
    }

    public IThrowsResult Throws(Delegate exceptionFactory)
    {
        Setup.SetThrowComputedExceptionBehaviour(exceptionFactory);
        return this;
    }

    public IThrowsResult Throws<TException>(Func<TException?> exceptionFactory)
        where TException : Exception
    {
        Setup.SetThrowComputedExceptionBehaviour(exceptionFactory);
        return this;
    }

    public IThrowsResult Throws<T, TException>(Func<T, TException> exceptionFactory)
        where TException : Exception
    {
        Setup.SetThrowComputedExceptionBehaviour(exceptionFactory);
        return this;
    }

    public IThrowsResult Throws<T1, T2, TException>(Func<T1, T2, TException> exceptionFactory)
        where TException : Exception
    {
        Setup.SetThrowComputedExceptionBehaviour(exceptionFactory);
        return this;
    }

    public IThrowsResult Throws<T1, T2, T3, TException>(Func<T1, T2, T3, TException> exceptionFactory)
        where TException : Exception
    {
        Setup.SetThrowComputedExceptionBehaviour(exceptionFactory);
        return this;
    }

    public IThrowsResult Throws<T1, T2, T3, T4, TException>(Func<T1, T2, T3, T4, TException> exceptionFactory)
        where TException : Exception
    {
        Setup.SetThrowComputedExceptionBehaviour(exceptionFactory);
        return this;
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, TException>(Func<T1, T2, T3, T4, T5, TException> exceptionFactory)
        where TException : Exception
    {
        Setup.SetThrowComputedExceptionBehaviour(exceptionFactory);
        return this;
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, TException>(Func<T1, T2, T3, T4, T5, T6, TException> exceptionFactory)
        where TException : Exception
    {
        Setup.SetThrowComputedExceptionBehaviour(exceptionFactory);
        return this;
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, TException>(Func<T1, T2, T3, T4, T5, T6, T7, TException> exceptionFactory)
        where TException : Exception
    {
        Setup.SetThrowComputedExceptionBehaviour(exceptionFactory);
        return this;
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, T8, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, TException> exceptionFactory)
        where TException : Exception
    {
        Setup.SetThrowComputedExceptionBehaviour(exceptionFactory);
        return this;
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TException> exceptionFactory)
        where TException : Exception
    {
        Setup.SetThrowComputedExceptionBehaviour(exceptionFactory);
        return this;
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TException> exceptionFactory)
        where TException : Exception
    {
        Setup.SetThrowComputedExceptionBehaviour(exceptionFactory);
        return this;
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TException> exceptionFactory)
        where TException : Exception
    {
        Setup.SetThrowComputedExceptionBehaviour(exceptionFactory);
        return this;
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TException> exceptionFactory)
        where TException : Exception
    {
        Setup.SetThrowComputedExceptionBehaviour(exceptionFactory);
        return this;
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TException> exceptionFactory)
        where TException : Exception
    {
        Setup.SetThrowComputedExceptionBehaviour(exceptionFactory);
        return this;
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TException> exceptionFactory)
        where TException : Exception
    {
        Setup.SetThrowComputedExceptionBehaviour(exceptionFactory);
        return this;
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TException> exceptionFactory)
        where TException : Exception
    {
        Setup.SetThrowComputedExceptionBehaviour(exceptionFactory);
        return this;
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TException> exceptionFactory)
        where TException : Exception
    {
        Setup.SetThrowComputedExceptionBehaviour(exceptionFactory);
        return this;
    }

    #endregion

    public IVerifiable Limit(int executionLimit = 1)
    {
        Setup.SetExecutionLimitBehaviour(executionLimit);
        return this;
    }

    public void Verifiable()
    {
        Setup.FlagAsVerifiable();
    }

    public override string ToString()
    {
        return Setup.Expression.ToString();
    }
}
