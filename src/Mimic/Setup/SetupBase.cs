using Mimic.Core;
using Mimic.Setup.Fluent;

namespace Mimic.Setup;

internal class SetupBase : IThrows, IThrowsResult
{
    protected SetupBase(MethodCallSetup setup)
    {
        Guard.NotNull(setup);

        Setup = setup;
    }

    protected MethodCallSetup Setup { get; }

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

    public override string ToString()
    {
        return Setup.Expression.ToString();
    }
}
