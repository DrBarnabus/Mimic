using Mimic.Setup.Fluent;

namespace Mimic.Setup;

internal sealed class NonVoidSetup<T, TResult> : SetupBase, ISetup<T, TResult>, IReturnsResult<T>
    where T : class
{
    public NonVoidSetup(MethodCallSetup setup)
        : base(setup)
    {
    }

    public IReturnsResult<T> Returns(TResult? value)
    {
        Setup.SetReturnValueBehaviour(value);
        return this;
    }

    public IReturnsResult<T> Returns(Func<TResult?> valueFactory)
    {
        Setup.SetReturnComputedValueBehaviour(valueFactory);
        return this;
    }

    public IReturnsResult<T> Returns<T1>(Func<T1, TResult> valueFactory)
    {
        Setup.SetReturnComputedValueBehaviour(valueFactory);
        return this;
    }

    public IReturnsResult<T> Returns<T1, T2>(Func<T1, T2, TResult> valueFactory)
    {
        Setup.SetReturnComputedValueBehaviour(valueFactory);
        return this;
    }

    public IReturnsResult<T> Returns<T1, T2, T3>(Func<T1, T2, T3, TResult> valueFactory)
    {
        Setup.SetReturnComputedValueBehaviour(valueFactory);
        return this;
    }

    public IReturnsResult<T> Returns<T1, T2, T3, T4>(Func<T1, T2, T3, T4, TResult> valueFactory)
    {
        Setup.SetReturnComputedValueBehaviour(valueFactory);
        return this;
    }

    public IReturnsResult<T> Returns<T1, T2, T3, T4, T5>(Func<T1, T2, T3, T4, T5, TResult> valueFactory)
    {
        Setup.SetReturnComputedValueBehaviour(valueFactory);
        return this;
    }

    public IReturnsResult<T> Returns<T1, T2, T3, T4, T5, T6>(Func<T1, T2, T3, T4, T5, T6, TResult> valueFactory)
    {
        Setup.SetReturnComputedValueBehaviour(valueFactory);
        return this;
    }

    public IReturnsResult<T> Returns<T1, T2, T3, T4, T5, T6, T7>(Func<T1, T2, T3, T4, T5, T6, T7, TResult> valueFactory)
    {
        Setup.SetReturnComputedValueBehaviour(valueFactory);
        return this;
    }

    public IReturnsResult<T> Returns<T1, T2, T3, T4, T5, T6, T7, T8>(Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> valueFactory)
    {
        Setup.SetReturnComputedValueBehaviour(valueFactory);
        return this;
    }

    public IReturnsResult<T> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> valueFactory)
    {
        Setup.SetReturnComputedValueBehaviour(valueFactory);
        return this;
    }

    public IReturnsResult<T> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> valueFactory)
    {
        Setup.SetReturnComputedValueBehaviour(valueFactory);
        return this;
    }

    public IReturnsResult<T> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> valueFactory)
    {
        Setup.SetReturnComputedValueBehaviour(valueFactory);
        return this;
    }

    public IReturnsResult<T> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> valueFactory)
    {
        Setup.SetReturnComputedValueBehaviour(valueFactory);
        return this;
    }

    public IReturnsResult<T> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> valueFactory)
    {
        Setup.SetReturnComputedValueBehaviour(valueFactory);
        return this;
    }

    public IReturnsResult<T> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> valueFactory)
    {
        Setup.SetReturnComputedValueBehaviour(valueFactory);
        return this;
    }

    public IReturnsResult<T> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> valueFactory)
    {
        Setup.SetReturnComputedValueBehaviour(valueFactory);
        return this;
    }

    public IReturnsResult<T> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> valueFactory)
    {
        Setup.SetReturnComputedValueBehaviour(valueFactory);
        return this;
    }
}
