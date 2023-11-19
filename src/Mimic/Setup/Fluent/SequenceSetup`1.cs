namespace Mimic.Setup.Fluent;

internal sealed class SequenceSetup<TResult> : SequenceSetupBase<ISequenceSetup<TResult>>, ISequenceSetup<TResult>
{
    protected override ISequenceSetup<TResult> This => this;

    public SequenceSetup(MethodCallSetup setup) : base(setup)
    {
    }

    public ISequenceSetup<TResult> Returns(TResult? value)
    {
        Setup.AddReturnValueBehaviour(value);
        return this;
    }

    public ISequenceSetup<TResult> Returns(Func<TResult?> valueFunction)
    {
        Setup.AddReturnComputedValueBehaviour(valueFunction);
        return this;
    }

    public ISequenceSetup<TResult> Returns<T>(Func<T, TResult> valueFunction)
    {
        Setup.AddReturnComputedValueBehaviour(valueFunction);
        return this;
    }

    public ISequenceSetup<TResult> Returns<T1, T2>(Func<T1, T2, TResult> valueFunction)
    {
        Setup.AddReturnComputedValueBehaviour(valueFunction);
        return this;
    }

    public ISequenceSetup<TResult> Returns<T1, T2, T3>(Func<T1, T2, T3, TResult> valueFunction)
    {
        Setup.AddReturnComputedValueBehaviour(valueFunction);
        return this;
    }

    public ISequenceSetup<TResult> Returns<T1, T2, T3, T4>(Func<T1, T2, T3, T4, TResult> valueFunction)
    {
        Setup.AddReturnComputedValueBehaviour(valueFunction);
        return this;
    }

    public ISequenceSetup<TResult> Returns<T1, T2, T3, T4, T5>(Func<T1, T2, T3, T4, T5, TResult> valueFunction)
    {
        Setup.AddReturnComputedValueBehaviour(valueFunction);
        return this;
    }

    public ISequenceSetup<TResult> Returns<T1, T2, T3, T4, T5, T6>(Func<T1, T2, T3, T4, T5, T6, TResult> valueFunction)
    {
        Setup.AddReturnComputedValueBehaviour(valueFunction);
        return this;
    }

    public ISequenceSetup<TResult> Returns<T1, T2, T3, T4, T5, T6, T7>(Func<T1, T2, T3, T4, T5, T6, T7, TResult> valueFunction)
    {
        Setup.AddReturnComputedValueBehaviour(valueFunction);
        return this;
    }

    public ISequenceSetup<TResult> Returns<T1, T2, T3, T4, T5, T6, T7, T8>(Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> valueFunction)
    {
        Setup.AddReturnComputedValueBehaviour(valueFunction);
        return this;
    }

    public ISequenceSetup<TResult> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> valueFunction)
    {
        Setup.AddReturnComputedValueBehaviour(valueFunction);
        return this;
    }

    public ISequenceSetup<TResult> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> valueFunction)
    {
        Setup.AddReturnComputedValueBehaviour(valueFunction);
        return this;
    }

    public ISequenceSetup<TResult> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> valueFunction)
    {
        Setup.AddReturnComputedValueBehaviour(valueFunction);
        return this;
    }

    public ISequenceSetup<TResult> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> valueFunction)
    {
        Setup.AddReturnComputedValueBehaviour(valueFunction);
        return this;
    }

    public ISequenceSetup<TResult> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> valueFunction)
    {
        Setup.AddReturnComputedValueBehaviour(valueFunction);
        return this;
    }

    public ISequenceSetup<TResult> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> valueFunction)
    {
        Setup.AddReturnComputedValueBehaviour(valueFunction);
        return this;
    }

    public ISequenceSetup<TResult> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> valueFunction)
    {
        Setup.AddReturnComputedValueBehaviour(valueFunction);
        return this;
    }

    public ISequenceSetup<TResult> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> valueFunction)
    {
        Setup.AddReturnComputedValueBehaviour(valueFunction);
        return this;
    }
}
