namespace Mimic.Setup.Fluent.Implementations;

internal abstract class SequenceSetupBase<TNext> : IThrows<TNext>, IVerifiable
    where TNext : IFluent
{
    protected MethodCallSetup Setup { get; }

    protected abstract TNext This { get; }

    protected SequenceSetupBase(MethodCallSetup setup)
    {
        Setup = setup;
    }

    public TNext Throws(Exception exception)
    {
        Setup.AddThrowExceptionBehaviour(exception);
        return This;
    }

    public TNext Throws<TException>()
        where TException : Exception, new()
    {
        Setup.AddThrowExceptionBehaviour(new TException());
        return This;
    }

    public TNext Throws(Delegate exceptionFactory)
    {
        Setup.AddThrowComputedExceptionBehaviour(exceptionFactory);
        return This;
    }

    public TNext Throws<TException>(Func<TException?> exceptionFactory)
        where TException : Exception
    {
        Setup.AddThrowComputedExceptionBehaviour(exceptionFactory);
        return This;
    }

    public TNext Throws<T, TException>(Func<T, TException> exceptionFactory)
        where TException : Exception
    {
        Setup.AddThrowComputedExceptionBehaviour(exceptionFactory);
        return This;
    }

    public TNext Throws<T1, T2, TException>(Func<T1, T2, TException> exceptionFactory)
        where TException : Exception
    {
        Setup.AddThrowComputedExceptionBehaviour(exceptionFactory);
        return This;
    }

    public TNext Throws<T1, T2, T3, TException>(Func<T1, T2, T3, TException> exceptionFactory)
        where TException : Exception
    {
        Setup.AddThrowComputedExceptionBehaviour(exceptionFactory);
        return This;
    }

    public TNext Throws<T1, T2, T3, T4, TException>(Func<T1, T2, T3, T4, TException> exceptionFactory)
        where TException : Exception
    {
        Setup.AddThrowComputedExceptionBehaviour(exceptionFactory);
        return This;
    }

    public TNext Throws<T1, T2, T3, T4, T5, TException>(Func<T1, T2, T3, T4, T5, TException> exceptionFactory)
        where TException : Exception
    {
        Setup.AddThrowComputedExceptionBehaviour(exceptionFactory);
        return This;
    }

    public TNext Throws<T1, T2, T3, T4, T5, T6, TException>(Func<T1, T2, T3, T4, T5, T6, TException> exceptionFactory)
        where TException : Exception
    {
        Setup.AddThrowComputedExceptionBehaviour(exceptionFactory);
        return This;
    }

    public TNext Throws<T1, T2, T3, T4, T5, T6, T7, TException>(Func<T1, T2, T3, T4, T5, T6, T7, TException> exceptionFactory)
        where TException : Exception
    {
        Setup.AddThrowComputedExceptionBehaviour(exceptionFactory);
        return This;
    }

    public TNext Throws<T1, T2, T3, T4, T5, T6, T7, T8, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, TException> exceptionFactory)
        where TException : Exception
    {
        Setup.AddThrowComputedExceptionBehaviour(exceptionFactory);
        return This;
    }

    public TNext Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TException> exceptionFactory)
        where TException : Exception
    {
        Setup.AddThrowComputedExceptionBehaviour(exceptionFactory);
        return This;
    }

    public TNext Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TException> exceptionFactory)
        where TException : Exception
    {
        Setup.AddThrowComputedExceptionBehaviour(exceptionFactory);
        return This;
    }

    public TNext Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TException> exceptionFactory)
        where TException : Exception
    {
        Setup.AddThrowComputedExceptionBehaviour(exceptionFactory);
        return This;
    }

    public TNext Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TException> exceptionFactory)
        where TException : Exception
    {
        Setup.AddThrowComputedExceptionBehaviour(exceptionFactory);
        return This;
    }

    public TNext Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TException> exceptionFactory)
        where TException : Exception
    {
        Setup.AddThrowComputedExceptionBehaviour(exceptionFactory);
        return This;
    }

    public TNext Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TException> exceptionFactory)
        where TException : Exception
    {
        Setup.AddThrowComputedExceptionBehaviour(exceptionFactory);
        return This;
    }

    public TNext Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TException> exceptionFactory)
        where TException : Exception
    {
        Setup.AddThrowComputedExceptionBehaviour(exceptionFactory);
        return This;
    }

    public TNext Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TException> exceptionFactory)
        where TException : Exception
    {
        Setup.AddThrowComputedExceptionBehaviour(exceptionFactory);
        return This;
    }

    public void Verifiable() => Setup.FlagAsVerifiable();

    public override string ToString() => Setup.Expression.ToString();
}
