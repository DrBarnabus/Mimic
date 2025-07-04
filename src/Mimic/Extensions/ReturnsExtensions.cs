﻿namespace Mimic;

[PublicAPI]
public static class ReturnsExtensions
{
    #region Task<T>

    public static IReturnsResult Returns<TResult>(this IReturns<Task<TResult>> mimic, TResult value)
    {
        return mimic.Returns(() => Task.FromResult(value));
    }

    public static IReturnsResult Returns<TResult>(this IReturns<Task<TResult>> mimic, Func<TResult> valueFunction)
    {
        return mimic.Returns(() => Task.FromResult(valueFunction()));
    }

    public static IReturnsResult Returns<T, TResult>(this IReturns<Task<TResult>> mimic, Func<T, TResult> valueFunction)
    {
        return mimic.Returns((T t) => Task.FromResult(valueFunction(t)));
    }

    public static IReturnsResult Returns<T1, T2, TResult>(this IReturns<Task<TResult>> mimic, Func<T1, T2, TResult> valueFunction)
    {
        return mimic.Returns((T1 t1, T2 t2) => Task.FromResult(valueFunction(t1, t2)));
    }

    public static IReturnsResult Returns<T1, T2, T3, TResult>(this IReturns<Task<TResult>> mimic, Func<T1, T2, T3, TResult> valueFunction)
    {
        return mimic.Returns((T1 t1, T2 t2, T3 t3) => Task.FromResult(valueFunction(t1, t2, t3)));
    }

    public static IReturnsResult Returns<T1, T2, T3, T4, TResult>(this IReturns<Task<TResult>> mimic, Func<T1, T2, T3, T4, TResult> valueFunction)
    {
        return mimic.Returns((T1 t1, T2 t2, T3 t3, T4 t4) => Task.FromResult(valueFunction(t1, t2, t3, t4)));
    }

    public static IReturnsResult Returns<T1, T2, T3, T4, T5, TResult>(this IReturns<Task<TResult>> mimic, Func<T1, T2, T3, T4, T5, TResult> valueFunction)
    {
        return mimic.Returns((T1 t1, T2 t2, T3 t3, T4 t4, T5 t5) => Task.FromResult(valueFunction(t1, t2, t3, t4, t5)));
    }

    public static IReturnsResult Returns<T1, T2, T3, T4, T5, T6, TResult>(this IReturns<Task<TResult>> mimic, Func<T1, T2, T3, T4, T5, T6, TResult> valueFunction)
    {
        return mimic.Returns((T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6) => Task.FromResult(valueFunction(t1, t2, t3, t4, t5, t6)));
    }

    public static IReturnsResult Returns<T1, T2, T3, T4, T5, T6, T7, TResult>(this IReturns<Task<TResult>> mimic, Func<T1, T2, T3, T4, T5, T6, T7, TResult> valueFunction)
    {
        return mimic.Returns((T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7) => Task.FromResult(valueFunction(t1, t2, t3, t4, t5, t6, t7)));
    }

    public static IReturnsResult Returns<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(this IReturns<Task<TResult>> mimic, Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> valueFunction)
    {
        return mimic.Returns((T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8) => Task.FromResult(valueFunction(t1, t2, t3, t4, t5, t6, t7, t8)));
    }

    public static IReturnsResult Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(this IReturns<Task<TResult>> mimic, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> valueFunction)
    {
        return mimic.Returns((T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9) => Task.FromResult(valueFunction(t1, t2, t3, t4, t5, t6, t7, t8, t9)));
    }

    public static IReturnsResult Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(this IReturns<Task<TResult>> mimic, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> valueFunction)
    {
        return mimic.Returns((T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10) => Task.FromResult(valueFunction(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10)));
    }

    public static IReturnsResult Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(this IReturns<Task<TResult>> mimic, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> valueFunction)
    {
        return mimic.Returns((T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11) => Task.FromResult(valueFunction(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11)));
    }

    public static IReturnsResult Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(this IReturns<Task<TResult>> mimic, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> valueFunction)
    {
        return mimic.Returns((T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12) => Task.FromResult(valueFunction(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12)));
    }

    public static IReturnsResult Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(this IReturns<Task<TResult>> mimic, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> valueFunction)
    {
        return mimic.Returns((T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13) => Task.FromResult(valueFunction(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13)));
    }

    public static IReturnsResult Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(this IReturns<Task<TResult>> mimic, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> valueFunction)
    {
        return mimic.Returns((T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14) => Task.FromResult(valueFunction(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14)));
    }

    public static IReturnsResult Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(this IReturns<Task<TResult>> mimic, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> valueFunction)
    {
        return mimic.Returns((T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, T15 t15) => Task.FromResult(valueFunction(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15)));
    }

    #endregion

    #region ValueTask<T>

    public static IReturnsResult Returns<TResult>(this IReturns<ValueTask<TResult>> mimic, TResult value)
    {
        return mimic.Returns(() => new ValueTask<TResult>(value));
    }

    public static IReturnsResult Returns<TResult>(this IReturns<ValueTask<TResult>> mimic, Func<TResult> valueFunction)
    {
        return mimic.Returns(() => new ValueTask<TResult>(valueFunction()));
    }

    public static IReturnsResult Returns<T, TResult>(this IReturns<ValueTask<TResult>> mimic, Func<T, TResult> valueFunction)
    {
        return mimic.Returns((T t) => new ValueTask<TResult>(valueFunction(t)));
    }

    public static IReturnsResult Returns<T1, T2, TResult>(this IReturns<ValueTask<TResult>> mimic, Func<T1, T2, TResult> valueFunction)
    {
        return mimic.Returns((T1 t1, T2 t2) => new ValueTask<TResult>(valueFunction(t1, t2)));
    }

    public static IReturnsResult Returns<T1, T2, T3, TResult>(this IReturns<ValueTask<TResult>> mimic, Func<T1, T2, T3, TResult> valueFunction)
    {
        return mimic.Returns((T1 t1, T2 t2, T3 t3) => new ValueTask<TResult>(valueFunction(t1, t2, t3)));
    }

    public static IReturnsResult Returns<T1, T2, T3, T4, TResult>(this IReturns<ValueTask<TResult>> mimic, Func<T1, T2, T3, T4, TResult> valueFunction)
    {
        return mimic.Returns((T1 t1, T2 t2, T3 t3, T4 t4) => new ValueTask<TResult>(valueFunction(t1, t2, t3, t4)));
    }

    public static IReturnsResult Returns<T1, T2, T3, T4, T5, TResult>(this IReturns<ValueTask<TResult>> mimic, Func<T1, T2, T3, T4, T5, TResult> valueFunction)
    {
        return mimic.Returns((T1 t1, T2 t2, T3 t3, T4 t4, T5 t5) => new ValueTask<TResult>(valueFunction(t1, t2, t3, t4, t5)));
    }

    public static IReturnsResult Returns<T1, T2, T3, T4, T5, T6, TResult>(this IReturns<ValueTask<TResult>> mimic, Func<T1, T2, T3, T4, T5, T6, TResult> valueFunction)
    {
        return mimic.Returns((T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6) => new ValueTask<TResult>(valueFunction(t1, t2, t3, t4, t5, t6)));
    }

    public static IReturnsResult Returns<T1, T2, T3, T4, T5, T6, T7, TResult>(this IReturns<ValueTask<TResult>> mimic, Func<T1, T2, T3, T4, T5, T6, T7, TResult> valueFunction)
    {
        return mimic.Returns((T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7) => new ValueTask<TResult>(valueFunction(t1, t2, t3, t4, t5, t6, t7)));
    }

    public static IReturnsResult Returns<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(this IReturns<ValueTask<TResult>> mimic, Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> valueFunction)
    {
        return mimic.Returns((T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8) => new ValueTask<TResult>(valueFunction(t1, t2, t3, t4, t5, t6, t7, t8)));
    }

    public static IReturnsResult Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(this IReturns<ValueTask<TResult>> mimic, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> valueFunction)
    {
        return mimic.Returns((T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9) => new ValueTask<TResult>(valueFunction(t1, t2, t3, t4, t5, t6, t7, t8, t9)));
    }

    public static IReturnsResult Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(this IReturns<ValueTask<TResult>> mimic, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> valueFunction)
    {
        return mimic.Returns((T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10) => new ValueTask<TResult>(valueFunction(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10)));
    }

    public static IReturnsResult Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(this IReturns<ValueTask<TResult>> mimic, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> valueFunction)
    {
        return mimic.Returns((T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11) => new ValueTask<TResult>(valueFunction(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11)));
    }

    public static IReturnsResult Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(this IReturns<ValueTask<TResult>> mimic, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> valueFunction)
    {
        return mimic.Returns((T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12) => new ValueTask<TResult>(valueFunction(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12)));
    }

    public static IReturnsResult Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(this IReturns<ValueTask<TResult>> mimic, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> valueFunction)
    {
        return mimic.Returns((T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13) => new ValueTask<TResult>(valueFunction(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13)));
    }

    public static IReturnsResult Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(this IReturns<ValueTask<TResult>> mimic, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> valueFunction)
    {
        return mimic.Returns((T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14) => new ValueTask<TResult>(valueFunction(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14)));
    }

    public static IReturnsResult Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(this IReturns<ValueTask<TResult>> mimic, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> valueFunction)
    {
        return mimic.Returns((T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, T15 t15) => new ValueTask<TResult>(valueFunction(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15)));
    }

    #endregion
}
