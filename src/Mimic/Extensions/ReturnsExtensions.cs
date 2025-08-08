namespace Mimic;

/// <summary>
/// Provides extension methods for simplifying the configuration of async return values in mimic setups.
/// These methods eliminate the need to manually wrap values in <c>Task.FromResult()</c> or create <c>ValueTask&lt;T&gt;</c> instances.
/// </summary>
[PublicAPI]
public static class ReturnsExtensions
{
    #region Task<T>

    /// <summary>
    /// Configures the setup to return the specified value wrapped in a completed <see cref="Task{TResult}"/>.
    /// </summary>
    /// <typeparam name="TResult">The type of the value to be returned.</typeparam>
    /// <param name="mimic">The returns configuration instance.</param>
    /// <param name="value">The value to return wrapped in a task.</param>
    /// <returns>An <see cref="IReturnsResult"/> that can be used to configure additional return behaviours.</returns>
    /// <remarks>
    /// This method simplifies async setup by automatically wrapping the value in <c>Task.FromResult(value)</c>.
    /// Instead of writing <c>.Returns(() =&gt; Task.FromResult(42))</c>, you can simply write <c>.Returns(42)</c>.
    /// </remarks>
    public static IReturnsResult Returns<TResult>(this IReturns<Task<TResult>> mimic, TResult value)
    {
        return mimic.Returns(() => Task.FromResult(value));
    }

    /// <summary>
    /// Configures the setup to return a value computed by the specified function, wrapped in a completed <see cref="Task{TResult}"/>.
    /// </summary>
    /// <typeparam name="TResult">The type of the value to be returned.</typeparam>
    /// <param name="mimic">The returns configuration instance.</param>
    /// <param name="valueFunction">A function that computes the value to return wrapped in a task.</param>
    /// <returns>An <see cref="IReturnsResult"/> that can be used to configure additional return behaviours.</returns>
    /// <remarks>
    /// This method allows for dynamic value computation at invocation time while automatically handling the task wrapping.
    /// The function is executed each time the mocked method is called.
    /// </remarks>
    public static IReturnsResult Returns<TResult>(this IReturns<Task<TResult>> mimic, Func<TResult> valueFunction)
    {
        return mimic.Returns(() => Task.FromResult(valueFunction()));
    }

    /// <summary>
    /// Configures the setup to return a value computed by the specified function using the method's single argument,
    /// wrapped in a completed <see cref="Task{TResult}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the method's argument.</typeparam>
    /// <typeparam name="TResult">The type of the value to be returned.</typeparam>
    /// <param name="mimic">The returns configuration instance.</param>
    /// <param name="valueFunction">A function that takes the method's argument and computes the return value.</param>
    /// <returns>An <see cref="IReturnsResult"/> that can be used to configure additional return behaviours.</returns>
    /// <remarks>
    /// This method allows the return value to be computed based on the method's argument while automatically handling the task wrapping.
    /// The function receives the argument passed to the mocked method at invocation time.
    /// </remarks>
    public static IReturnsResult Returns<T, TResult>(this IReturns<Task<TResult>> mimic, Func<T, TResult> valueFunction)
    {
        return mimic.Returns((T t) => Task.FromResult(valueFunction(t)));
    }

    /// <summary>
    /// Configures the setup to return a value computed by the specified function using the method's two arguments,
    /// wrapped in a completed <see cref="Task{TResult}"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the method's first argument.</typeparam>
    /// <typeparam name="T2">The type of the method's second argument.</typeparam>
    /// <typeparam name="TResult">The type of the value to be returned.</typeparam>
    /// <param name="mimic">The returns configuration instance.</param>
    /// <param name="valueFunction">A function that takes the method's arguments and computes the return value.</param>
    /// <returns>An <see cref="IReturnsResult"/> that can be used to configure additional return behaviours.</returns>
    /// <remarks>
    /// This method allows the return value to be computed based on the method's arguments while automatically handling the task wrapping.
    /// The function receives the arguments passed to the mocked method at invocation time.
    /// </remarks>
    public static IReturnsResult Returns<T1, T2, TResult>(this IReturns<Task<TResult>> mimic, Func<T1, T2, TResult> valueFunction)
    {
        return mimic.Returns((T1 t1, T2 t2) => Task.FromResult(valueFunction(t1, t2)));
    }

    /// <summary>
    /// Configures the setup to return a value computed by the specified function using the method's three arguments,
    /// wrapped in a completed <see cref="Task{TResult}"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the method's first argument.</typeparam>
    /// <typeparam name="T2">The type of the method's second argument.</typeparam>
    /// <typeparam name="T3">The type of the method's third argument.</typeparam>
    /// <typeparam name="TResult">The type of the value to be returned.</typeparam>
    /// <param name="mimic">The returns configuration instance.</param>
    /// <param name="valueFunction">A function that takes the method's arguments and computes the return value.</param>
    /// <returns>An <see cref="IReturnsResult"/> that can be used to configure additional return behaviours.</returns>
    /// <remarks>
    /// This method allows the return value to be computed based on the method's arguments while automatically handling the task wrapping.
    /// Additional overloads are available for methods with up to 15 parameters following the same pattern.
    /// </remarks>
    public static IReturnsResult Returns<T1, T2, T3, TResult>(this IReturns<Task<TResult>> mimic, Func<T1, T2, T3, TResult> valueFunction)
    {
        return mimic.Returns((T1 t1, T2 t2, T3 t3) => Task.FromResult(valueFunction(t1, t2, t3)));
    }

    /// <summary>
    /// Configures the setup to return a value computed by the specified function using the method's four arguments,
    /// wrapped in a completed <see cref="Task{TResult}"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the method's first argument.</typeparam>
    /// <typeparam name="T2">The type of the method's second argument.</typeparam>
    /// <typeparam name="T3">The type of the method's third argument.</typeparam>
    /// <typeparam name="T4">The type of the method's fourth argument.</typeparam>
    /// <typeparam name="TResult">The type of the value to be returned.</typeparam>
    /// <param name="mimic">The returns configuration instance.</param>
    /// <param name="valueFunction">A function that takes the method's arguments and computes the return value.</param>
    /// <returns>An <see cref="IReturnsResult"/> that can be used to configure additional return behaviours.</returns>
    /// <remarks>
    /// This method allows the return value to be computed based on the method's arguments while automatically handling the task wrapping.
    /// The function receives the arguments passed to the mocked method at invocation time.
    /// </remarks>
    public static IReturnsResult Returns<T1, T2, T3, T4, TResult>(this IReturns<Task<TResult>> mimic, Func<T1, T2, T3, T4, TResult> valueFunction)
    {
        return mimic.Returns((T1 t1, T2 t2, T3 t3, T4 t4) => Task.FromResult(valueFunction(t1, t2, t3, t4)));
    }

    /// <summary>
    /// Configures the setup to return a value computed by the specified function using the method's five arguments,
    /// wrapped in a completed <see cref="Task{TResult}"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the method's first argument.</typeparam>
    /// <typeparam name="T2">The type of the method's second argument.</typeparam>
    /// <typeparam name="T3">The type of the method's third argument.</typeparam>
    /// <typeparam name="T4">The type of the method's fourth argument.</typeparam>
    /// <typeparam name="T5">The type of the method's fifth argument.</typeparam>
    /// <typeparam name="TResult">The type of the value to be returned.</typeparam>
    /// <param name="mimic">The returns configuration instance.</param>
    /// <param name="valueFunction">A function that takes the method's arguments and computes the return value.</param>
    /// <returns>An <see cref="IReturnsResult"/> that can be used to configure additional return behaviours.</returns>
    /// <remarks>
    /// This method allows the return value to be computed based on the method's arguments while automatically handling the task wrapping.
    /// The function receives the arguments passed to the mocked method at invocation time.
    /// </remarks>
    public static IReturnsResult Returns<T1, T2, T3, T4, T5, TResult>(this IReturns<Task<TResult>> mimic, Func<T1, T2, T3, T4, T5, TResult> valueFunction)
    {
        return mimic.Returns((T1 t1, T2 t2, T3 t3, T4 t4, T5 t5) => Task.FromResult(valueFunction(t1, t2, t3, t4, t5)));
    }

    /// <summary>
    /// Configures the setup to return a value computed by the specified function using the method's six arguments,
    /// wrapped in a completed <see cref="Task{TResult}"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the method's first argument.</typeparam>
    /// <typeparam name="T2">The type of the method's second argument.</typeparam>
    /// <typeparam name="T3">The type of the method's third argument.</typeparam>
    /// <typeparam name="T4">The type of the method's fourth argument.</typeparam>
    /// <typeparam name="T5">The type of the method's fifth argument.</typeparam>
    /// <typeparam name="T6">The type of the method's sixth argument.</typeparam>
    /// <typeparam name="TResult">The type of the value to be returned.</typeparam>
    /// <param name="mimic">The returns configuration instance.</param>
    /// <param name="valueFunction">A function that takes the method's arguments and computes the return value.</param>
    /// <returns>An <see cref="IReturnsResult"/> that can be used to configure additional return behaviours.</returns>
    /// <remarks>
    /// This method allows the return value to be computed based on the method's arguments while automatically handling the task wrapping.
    /// The function receives the arguments passed to the mocked method at invocation time.
    /// </remarks>
    public static IReturnsResult Returns<T1, T2, T3, T4, T5, T6, TResult>(this IReturns<Task<TResult>> mimic, Func<T1, T2, T3, T4, T5, T6, TResult> valueFunction)
    {
        return mimic.Returns((T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6) => Task.FromResult(valueFunction(t1, t2, t3, t4, t5, t6)));
    }

    /// <summary>
    /// Configures the setup to return a value computed by the specified function using the method's seven arguments,
    /// wrapped in a completed <see cref="Task{TResult}"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the method's first argument.</typeparam>
    /// <typeparam name="T2">The type of the method's second argument.</typeparam>
    /// <typeparam name="T3">The type of the method's third argument.</typeparam>
    /// <typeparam name="T4">The type of the method's fourth argument.</typeparam>
    /// <typeparam name="T5">The type of the method's fifth argument.</typeparam>
    /// <typeparam name="T6">The type of the method's sixth argument.</typeparam>
    /// <typeparam name="T7">The type of the method's seventh argument.</typeparam>
    /// <typeparam name="TResult">The type of the value to be returned.</typeparam>
    /// <param name="mimic">The returns configuration instance.</param>
    /// <param name="valueFunction">A function that takes the method's arguments and computes the return value.</param>
    /// <returns>An <see cref="IReturnsResult"/> that can be used to configure additional return behaviours.</returns>
    /// <remarks>
    /// This method allows the return value to be computed based on the method's arguments while automatically handling the task wrapping.
    /// The function receives the arguments passed to the mocked method at invocation time.
    /// </remarks>
    public static IReturnsResult Returns<T1, T2, T3, T4, T5, T6, T7, TResult>(this IReturns<Task<TResult>> mimic, Func<T1, T2, T3, T4, T5, T6, T7, TResult> valueFunction)
    {
        return mimic.Returns((T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7) => Task.FromResult(valueFunction(t1, t2, t3, t4, t5, t6, t7)));
    }

    /// <summary>
    /// Configures the setup to return a value computed by the specified function using the method's eight arguments,
    /// wrapped in a completed <see cref="Task{TResult}"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the method's first argument.</typeparam>
    /// <typeparam name="T2">The type of the method's second argument.</typeparam>
    /// <typeparam name="T3">The type of the method's third argument.</typeparam>
    /// <typeparam name="T4">The type of the method's fourth argument.</typeparam>
    /// <typeparam name="T5">The type of the method's fifth argument.</typeparam>
    /// <typeparam name="T6">The type of the method's sixth argument.</typeparam>
    /// <typeparam name="T7">The type of the method's seventh argument.</typeparam>
    /// <typeparam name="T8">The type of the method's eighth argument.</typeparam>
    /// <typeparam name="TResult">The type of the value to be returned.</typeparam>
    /// <param name="mimic">The returns configuration instance.</param>
    /// <param name="valueFunction">A function that takes the method's arguments and computes the return value.</param>
    /// <returns>An <see cref="IReturnsResult"/> that can be used to configure additional return behaviours.</returns>
    /// <remarks>
    /// This method allows the return value to be computed based on the method's arguments while automatically handling the task wrapping.
    /// The function receives the arguments passed to the mocked method at invocation time.
    /// </remarks>
    public static IReturnsResult Returns<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(this IReturns<Task<TResult>> mimic, Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> valueFunction)
    {
        return mimic.Returns((T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8) => Task.FromResult(valueFunction(t1, t2, t3, t4, t5, t6, t7, t8)));
    }

    /// <summary>
    /// Configures the setup to return a value computed by the specified function using the method's nine arguments,
    /// wrapped in a completed <see cref="Task{TResult}"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the method's first argument.</typeparam>
    /// <typeparam name="T2">The type of the method's second argument.</typeparam>
    /// <typeparam name="T3">The type of the method's third argument.</typeparam>
    /// <typeparam name="T4">The type of the method's fourth argument.</typeparam>
    /// <typeparam name="T5">The type of the method's fifth argument.</typeparam>
    /// <typeparam name="T6">The type of the method's sixth argument.</typeparam>
    /// <typeparam name="T7">The type of the method's seventh argument.</typeparam>
    /// <typeparam name="T8">The type of the method's eighth argument.</typeparam>
    /// <typeparam name="T9">The type of the method's ninth argument.</typeparam>
    /// <typeparam name="TResult">The type of the value to be returned.</typeparam>
    /// <param name="mimic">The returns configuration instance.</param>
    /// <param name="valueFunction">A function that takes the method's arguments and computes the return value.</param>
    /// <returns>An <see cref="IReturnsResult"/> that can be used to configure additional return behaviours.</returns>
    /// <remarks>
    /// This method allows the return value to be computed based on the method's arguments while automatically handling the task wrapping.
    /// The function receives the arguments passed to the mocked method at invocation time.
    /// </remarks>
    public static IReturnsResult Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(this IReturns<Task<TResult>> mimic, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> valueFunction)
    {
        return mimic.Returns((T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9) => Task.FromResult(valueFunction(t1, t2, t3, t4, t5, t6, t7, t8, t9)));
    }

    /// <summary>
    /// Configures the setup to return a value computed by the specified function using the method's ten arguments,
    /// wrapped in a completed <see cref="Task{TResult}"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the method's first argument.</typeparam>
    /// <typeparam name="T2">The type of the method's second argument.</typeparam>
    /// <typeparam name="T3">The type of the method's third argument.</typeparam>
    /// <typeparam name="T4">The type of the method's fourth argument.</typeparam>
    /// <typeparam name="T5">The type of the method's fifth argument.</typeparam>
    /// <typeparam name="T6">The type of the method's sixth argument.</typeparam>
    /// <typeparam name="T7">The type of the method's seventh argument.</typeparam>
    /// <typeparam name="T8">The type of the method's eighth argument.</typeparam>
    /// <typeparam name="T9">The type of the method's ninth argument.</typeparam>
    /// <typeparam name="T10">The type of the method's tenth argument.</typeparam>
    /// <typeparam name="TResult">The type of the value to be returned.</typeparam>
    /// <param name="mimic">The returns configuration instance.</param>
    /// <param name="valueFunction">A function that takes the method's arguments and computes the return value.</param>
    /// <returns>An <see cref="IReturnsResult"/> that can be used to configure additional return behaviours.</returns>
    /// <remarks>
    /// This method allows the return value to be computed based on the method's arguments while automatically handling the task wrapping.
    /// The function receives the arguments passed to the mocked method at invocation time.
    /// </remarks>
    public static IReturnsResult Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(this IReturns<Task<TResult>> mimic, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> valueFunction)
    {
        return mimic.Returns((T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10) => Task.FromResult(valueFunction(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10)));
    }

    /// <summary>
    /// Configures the setup to return a value computed by the specified function using the method's eleven arguments,
    /// wrapped in a completed <see cref="Task{TResult}"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the method's first argument.</typeparam>
    /// <typeparam name="T2">The type of the method's second argument.</typeparam>
    /// <typeparam name="T3">The type of the method's third argument.</typeparam>
    /// <typeparam name="T4">The type of the method's fourth argument.</typeparam>
    /// <typeparam name="T5">The type of the method's fifth argument.</typeparam>
    /// <typeparam name="T6">The type of the method's sixth argument.</typeparam>
    /// <typeparam name="T7">The type of the method's seventh argument.</typeparam>
    /// <typeparam name="T8">The type of the method's eighth argument.</typeparam>
    /// <typeparam name="T9">The type of the method's ninth argument.</typeparam>
    /// <typeparam name="T10">The type of the method's tenth argument.</typeparam>
    /// <typeparam name="T11">The type of the method's eleventh argument.</typeparam>
    /// <typeparam name="TResult">The type of the value to be returned.</typeparam>
    /// <param name="mimic">The returns configuration instance.</param>
    /// <param name="valueFunction">A function that takes the method's arguments and computes the return value.</param>
    /// <returns>An <see cref="IReturnsResult"/> that can be used to configure additional return behaviours.</returns>
    /// <remarks>
    /// This method allows the return value to be computed based on the method's arguments while automatically handling the task wrapping.
    /// The function receives the arguments passed to the mocked method at invocation time.
    /// </remarks>
    public static IReturnsResult Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(this IReturns<Task<TResult>> mimic, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> valueFunction)
    {
        return mimic.Returns((T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11) => Task.FromResult(valueFunction(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11)));
    }

    /// <summary>
    /// Configures the setup to return a value computed by the specified function using the method's twelve arguments,
    /// wrapped in a completed <see cref="Task{TResult}"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the method's first argument.</typeparam>
    /// <typeparam name="T2">The type of the method's second argument.</typeparam>
    /// <typeparam name="T3">The type of the method's third argument.</typeparam>
    /// <typeparam name="T4">The type of the method's fourth argument.</typeparam>
    /// <typeparam name="T5">The type of the method's fifth argument.</typeparam>
    /// <typeparam name="T6">The type of the method's sixth argument.</typeparam>
    /// <typeparam name="T7">The type of the method's seventh argument.</typeparam>
    /// <typeparam name="T8">The type of the method's eighth argument.</typeparam>
    /// <typeparam name="T9">The type of the method's ninth argument.</typeparam>
    /// <typeparam name="T10">The type of the method's tenth argument.</typeparam>
    /// <typeparam name="T11">The type of the method's eleventh argument.</typeparam>
    /// <typeparam name="T12">The type of the method's twelfth argument.</typeparam>
    /// <typeparam name="TResult">The type of the value to be returned.</typeparam>
    /// <param name="mimic">The returns configuration instance.</param>
    /// <param name="valueFunction">A function that takes the method's arguments and computes the return value.</param>
    /// <returns>An <see cref="IReturnsResult"/> that can be used to configure additional return behaviours.</returns>
    /// <remarks>
    /// This method allows the return value to be computed based on the method's arguments while automatically handling the task wrapping.
    /// The function receives the arguments passed to the mocked method at invocation time.
    /// </remarks>
    public static IReturnsResult Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(this IReturns<Task<TResult>> mimic, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> valueFunction)
    {
        return mimic.Returns((T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12) => Task.FromResult(valueFunction(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12)));
    }

    /// <summary>
    /// Configures the setup to return a value computed by the specified function using the method's thirteen arguments,
    /// wrapped in a completed <see cref="Task{TResult}"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the method's first argument.</typeparam>
    /// <typeparam name="T2">The type of the method's second argument.</typeparam>
    /// <typeparam name="T3">The type of the method's third argument.</typeparam>
    /// <typeparam name="T4">The type of the method's fourth argument.</typeparam>
    /// <typeparam name="T5">The type of the method's fifth argument.</typeparam>
    /// <typeparam name="T6">The type of the method's sixth argument.</typeparam>
    /// <typeparam name="T7">The type of the method's seventh argument.</typeparam>
    /// <typeparam name="T8">The type of the method's eighth argument.</typeparam>
    /// <typeparam name="T9">The type of the method's ninth argument.</typeparam>
    /// <typeparam name="T10">The type of the method's tenth argument.</typeparam>
    /// <typeparam name="T11">The type of the method's eleventh argument.</typeparam>
    /// <typeparam name="T12">The type of the method's twelfth argument.</typeparam>
    /// <typeparam name="T13">The type of the method's thirteenth argument.</typeparam>
    /// <typeparam name="TResult">The type of the value to be returned.</typeparam>
    /// <param name="mimic">The returns configuration instance.</param>
    /// <param name="valueFunction">A function that takes the method's arguments and computes the return value.</param>
    /// <returns>An <see cref="IReturnsResult"/> that can be used to configure additional return behaviours.</returns>
    /// <remarks>
    /// This method allows the return value to be computed based on the method's arguments while automatically handling the task wrapping.
    /// The function receives the arguments passed to the mocked method at invocation time.
    /// </remarks>
    public static IReturnsResult Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(this IReturns<Task<TResult>> mimic, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> valueFunction)
    {
        return mimic.Returns((T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13) => Task.FromResult(valueFunction(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13)));
    }

    /// <summary>
    /// Configures the setup to return a value computed by the specified function using the method's fourteen arguments,
    /// wrapped in a completed <see cref="Task{TResult}"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the method's first argument.</typeparam>
    /// <typeparam name="T2">The type of the method's second argument.</typeparam>
    /// <typeparam name="T3">The type of the method's third argument.</typeparam>
    /// <typeparam name="T4">The type of the method's fourth argument.</typeparam>
    /// <typeparam name="T5">The type of the method's fifth argument.</typeparam>
    /// <typeparam name="T6">The type of the method's sixth argument.</typeparam>
    /// <typeparam name="T7">The type of the method's seventh argument.</typeparam>
    /// <typeparam name="T8">The type of the method's eighth argument.</typeparam>
    /// <typeparam name="T9">The type of the method's ninth argument.</typeparam>
    /// <typeparam name="T10">The type of the method's tenth argument.</typeparam>
    /// <typeparam name="T11">The type of the method's eleventh argument.</typeparam>
    /// <typeparam name="T12">The type of the method's twelfth argument.</typeparam>
    /// <typeparam name="T13">The type of the method's thirteenth argument.</typeparam>
    /// <typeparam name="T14">The type of the method's fourteenth argument.</typeparam>
    /// <typeparam name="TResult">The type of the value to be returned.</typeparam>
    /// <param name="mimic">The returns configuration instance.</param>
    /// <param name="valueFunction">A function that takes the method's arguments and computes the return value.</param>
    /// <returns>An <see cref="IReturnsResult"/> that can be used to configure additional return behaviours.</returns>
    /// <remarks>
    /// This method allows the return value to be computed based on the method's arguments while automatically handling the task wrapping.
    /// The function receives the arguments passed to the mocked method at invocation time.
    /// </remarks>
    public static IReturnsResult Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(this IReturns<Task<TResult>> mimic, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> valueFunction)
    {
        return mimic.Returns((T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14) => Task.FromResult(valueFunction(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14)));
    }

    /// <summary>
    /// Configures the setup to return a value computed by the specified function using the method's fifteen arguments,
    /// wrapped in a completed <see cref="Task{TResult}"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the method's first argument.</typeparam>
    /// <typeparam name="T2">The type of the method's second argument.</typeparam>
    /// <typeparam name="T3">The type of the method's third argument.</typeparam>
    /// <typeparam name="T4">The type of the method's fourth argument.</typeparam>
    /// <typeparam name="T5">The type of the method's fifth argument.</typeparam>
    /// <typeparam name="T6">The type of the method's sixth argument.</typeparam>
    /// <typeparam name="T7">The type of the method's seventh argument.</typeparam>
    /// <typeparam name="T8">The type of the method's eighth argument.</typeparam>
    /// <typeparam name="T9">The type of the method's ninth argument.</typeparam>
    /// <typeparam name="T10">The type of the method's tenth argument.</typeparam>
    /// <typeparam name="T11">The type of the method's eleventh argument.</typeparam>
    /// <typeparam name="T12">The type of the method's twelfth argument.</typeparam>
    /// <typeparam name="T13">The type of the method's thirteenth argument.</typeparam>
    /// <typeparam name="T14">The type of the method's fourteenth argument.</typeparam>
    /// <typeparam name="T15">The type of the method's fifteenth argument.</typeparam>
    /// <typeparam name="TResult">The type of the value to be returned.</typeparam>
    /// <param name="mimic">The returns configuration instance.</param>
    /// <param name="valueFunction">A function that takes the method's arguments and computes the return value.</param>
    /// <returns>An <see cref="IReturnsResult"/> that can be used to configure additional return behaviours.</returns>
    /// <remarks>
    /// This method allows the return value to be computed based on the method's arguments while automatically handling the task wrapping.
    /// This is the maximum parameter count supported by these extension methods.
    /// </remarks>
    public static IReturnsResult Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(this IReturns<Task<TResult>> mimic, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> valueFunction)
    {
        return mimic.Returns((T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, T15 t15) => Task.FromResult(valueFunction(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15)));
    }

    #endregion

    #region ValueTask<T>

    /// <summary>
    /// Configures the setup to return the specified value wrapped in a <see cref="ValueTask{TResult}"/>.
    /// </summary>
    /// <typeparam name="TResult">The type of the value to be returned.</typeparam>
    /// <param name="mimic">The returns configuration instance.</param>
    /// <param name="value">The value to return wrapped in a ValueTask.</param>
    /// <returns>An <see cref="IReturnsResult"/> that can be used to configure additional return behaviours.</returns>
    /// <remarks>
    /// This method simplifies async setup by automatically wrapping the value in a <c>ValueTask&lt;TResult&gt;</c>.
    /// Instead of writing <c>.Returns(() =&gt; new ValueTask&lt;int&gt;(42))</c>, you can simply write <c>.Returns(42)</c>.
    /// ValueTask provides better performance than Task for scenarios where the result is immediately available.
    /// </remarks>
    public static IReturnsResult Returns<TResult>(this IReturns<ValueTask<TResult>> mimic, TResult value)
    {
        return mimic.Returns(() => new ValueTask<TResult>(value));
    }

    /// <summary>
    /// Configures the setup to return a value computed by the specified function, wrapped in a <see cref="ValueTask{TResult}"/>.
    /// </summary>
    /// <typeparam name="TResult">The type of the value to be returned.</typeparam>
    /// <param name="mimic">The returns configuration instance.</param>
    /// <param name="valueFunction">A function that computes the value to return wrapped in a ValueTask.</param>
    /// <returns>An <see cref="IReturnsResult"/> that can be used to configure additional return behaviours.</returns>
    /// <remarks>
    /// This method allows for dynamic value computation at invocation time while automatically handling the ValueTask wrapping.
    /// The function is executed each time the mocked method is called.
    /// </remarks>
    public static IReturnsResult Returns<TResult>(this IReturns<ValueTask<TResult>> mimic, Func<TResult> valueFunction)
    {
        return mimic.Returns(() => new ValueTask<TResult>(valueFunction()));
    }

    /// <summary>
    /// Configures the setup to return a value computed by the specified function using the method's single argument,
    /// wrapped in a <see cref="ValueTask{TResult}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the method's argument.</typeparam>
    /// <typeparam name="TResult">The type of the value to be returned.</typeparam>
    /// <param name="mimic">The returns configuration instance.</param>
    /// <param name="valueFunction">A function that takes the method's argument and computes the return value.</param>
    /// <returns>An <see cref="IReturnsResult"/> that can be used to configure additional return behaviours.</returns>
    /// <remarks>
    /// This method allows the return value to be computed based on the method's argument while automatically handling the ValueTask wrapping.
    /// The function receives the argument passed to the mocked method at invocation time.
    /// </remarks>
    public static IReturnsResult Returns<T, TResult>(this IReturns<ValueTask<TResult>> mimic, Func<T, TResult> valueFunction)
    {
        return mimic.Returns((T t) => new ValueTask<TResult>(valueFunction(t)));
    }

    /// <summary>
    /// Configures the setup to return a value computed by the specified function using the method's two arguments,
    /// wrapped in a <see cref="ValueTask{TResult}"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the method's first argument.</typeparam>
    /// <typeparam name="T2">The type of the method's second argument.</typeparam>
    /// <typeparam name="TResult">The type of the value to be returned.</typeparam>
    /// <param name="mimic">The returns configuration instance.</param>
    /// <param name="valueFunction">A function that takes the method's arguments and computes the return value.</param>
    /// <returns>An <see cref="IReturnsResult"/> that can be used to configure additional return behaviours.</returns>
    /// <remarks>
    /// This method allows the return value to be computed based on the method's arguments while automatically handling the ValueTask wrapping.
    /// The function receives the arguments passed to the mocked method at invocation time.
    /// </remarks>
    public static IReturnsResult Returns<T1, T2, TResult>(this IReturns<ValueTask<TResult>> mimic, Func<T1, T2, TResult> valueFunction)
    {
        return mimic.Returns((T1 t1, T2 t2) => new ValueTask<TResult>(valueFunction(t1, t2)));
    }

    /// <summary>
    /// Configures the setup to return a value computed by the specified function using the method's three arguments,
    /// wrapped in a <see cref="ValueTask{TResult}"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the method's first argument.</typeparam>
    /// <typeparam name="T2">The type of the method's second argument.</typeparam>
    /// <typeparam name="T3">The type of the method's third argument.</typeparam>
    /// <typeparam name="TResult">The type of the value to be returned.</typeparam>
    /// <param name="mimic">The returns configuration instance.</param>
    /// <param name="valueFunction">A function that takes the method's arguments and computes the return value.</param>
    /// <returns>An <see cref="IReturnsResult"/> that can be used to configure additional return behaviours.</returns>
    /// <remarks>
    /// This method allows the return value to be computed based on the method's arguments while automatically handling the ValueTask wrapping.
    /// Additional overloads are available for methods with up to 15 parameters following the same pattern.
    /// </remarks>
    public static IReturnsResult Returns<T1, T2, T3, TResult>(this IReturns<ValueTask<TResult>> mimic, Func<T1, T2, T3, TResult> valueFunction)
    {
        return mimic.Returns((T1 t1, T2 t2, T3 t3) => new ValueTask<TResult>(valueFunction(t1, t2, t3)));
    }

    /// <summary>
    /// Configures the setup to return a value computed by the specified function using the method's four arguments,
    /// wrapped in a <see cref="ValueTask{TResult}"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the method's first argument.</typeparam>
    /// <typeparam name="T2">The type of the method's second argument.</typeparam>
    /// <typeparam name="T3">The type of the method's third argument.</typeparam>
    /// <typeparam name="T4">The type of the method's fourth argument.</typeparam>
    /// <typeparam name="TResult">The type of the value to be returned.</typeparam>
    /// <param name="mimic">The returns configuration instance.</param>
    /// <param name="valueFunction">A function that takes the method's arguments and computes the return value.</param>
    /// <returns>An <see cref="IReturnsResult"/> that can be used to configure additional return behaviours.</returns>
    /// <remarks>
    /// This method allows the return value to be computed based on the method's arguments while automatically handling the ValueTask wrapping.
    /// The function receives the arguments passed to the mocked method at invocation time.
    /// </remarks>
    public static IReturnsResult Returns<T1, T2, T3, T4, TResult>(this IReturns<ValueTask<TResult>> mimic, Func<T1, T2, T3, T4, TResult> valueFunction)
    {
        return mimic.Returns((T1 t1, T2 t2, T3 t3, T4 t4) => new ValueTask<TResult>(valueFunction(t1, t2, t3, t4)));
    }

    /// <summary>
    /// Configures the setup to return a value computed by the specified function using the method's five arguments,
    /// wrapped in a <see cref="ValueTask{TResult}"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the method's first argument.</typeparam>
    /// <typeparam name="T2">The type of the method's second argument.</typeparam>
    /// <typeparam name="T3">The type of the method's third argument.</typeparam>
    /// <typeparam name="T4">The type of the method's fourth argument.</typeparam>
    /// <typeparam name="T5">The type of the method's fifth argument.</typeparam>
    /// <typeparam name="TResult">The type of the value to be returned.</typeparam>
    /// <param name="mimic">The returns configuration instance.</param>
    /// <param name="valueFunction">A function that takes the method's arguments and computes the return value.</param>
    /// <returns>An <see cref="IReturnsResult"/> that can be used to configure additional return behaviours.</returns>
    /// <remarks>
    /// This method allows the return value to be computed based on the method's arguments while automatically handling the ValueTask wrapping.
    /// The function receives the arguments passed to the mocked method at invocation time.
    /// </remarks>
    public static IReturnsResult Returns<T1, T2, T3, T4, T5, TResult>(this IReturns<ValueTask<TResult>> mimic, Func<T1, T2, T3, T4, T5, TResult> valueFunction)
    {
        return mimic.Returns((T1 t1, T2 t2, T3 t3, T4 t4, T5 t5) => new ValueTask<TResult>(valueFunction(t1, t2, t3, t4, t5)));
    }

    /// <summary>
    /// Configures the setup to return a value computed by the specified function using the method's six arguments,
    /// wrapped in a <see cref="ValueTask{TResult}"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the method's first argument.</typeparam>
    /// <typeparam name="T2">The type of the method's second argument.</typeparam>
    /// <typeparam name="T3">The type of the method's third argument.</typeparam>
    /// <typeparam name="T4">The type of the method's fourth argument.</typeparam>
    /// <typeparam name="T5">The type of the method's fifth argument.</typeparam>
    /// <typeparam name="T6">The type of the method's sixth argument.</typeparam>
    /// <typeparam name="TResult">The type of the value to be returned.</typeparam>
    /// <param name="mimic">The returns configuration instance.</param>
    /// <param name="valueFunction">A function that takes the method's arguments and computes the return value.</param>
    /// <returns>An <see cref="IReturnsResult"/> that can be used to configure additional return behaviours.</returns>
    /// <remarks>
    /// This method allows the return value to be computed based on the method's arguments while automatically handling the ValueTask wrapping.
    /// The function receives the arguments passed to the mocked method at invocation time.
    /// </remarks>
    public static IReturnsResult Returns<T1, T2, T3, T4, T5, T6, TResult>(this IReturns<ValueTask<TResult>> mimic, Func<T1, T2, T3, T4, T5, T6, TResult> valueFunction)
    {
        return mimic.Returns((T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6) => new ValueTask<TResult>(valueFunction(t1, t2, t3, t4, t5, t6)));
    }

    /// <summary>
    /// Configures the setup to return a value computed by the specified function using the method's seven arguments,
    /// wrapped in a <see cref="ValueTask{TResult}"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the method's first argument.</typeparam>
    /// <typeparam name="T2">The type of the method's second argument.</typeparam>
    /// <typeparam name="T3">The type of the method's third argument.</typeparam>
    /// <typeparam name="T4">The type of the method's fourth argument.</typeparam>
    /// <typeparam name="T5">The type of the method's fifth argument.</typeparam>
    /// <typeparam name="T6">The type of the method's sixth argument.</typeparam>
    /// <typeparam name="T7">The type of the method's seventh argument.</typeparam>
    /// <typeparam name="TResult">The type of the value to be returned.</typeparam>
    /// <param name="mimic">The returns configuration instance.</param>
    /// <param name="valueFunction">A function that takes the method's arguments and computes the return value.</param>
    /// <returns>An <see cref="IReturnsResult"/> that can be used to configure additional return behaviours.</returns>
    /// <remarks>
    /// This method allows the return value to be computed based on the method's arguments while automatically handling the ValueTask wrapping.
    /// The function receives the arguments passed to the mocked method at invocation time.
    /// </remarks>
    public static IReturnsResult Returns<T1, T2, T3, T4, T5, T6, T7, TResult>(this IReturns<ValueTask<TResult>> mimic, Func<T1, T2, T3, T4, T5, T6, T7, TResult> valueFunction)
    {
        return mimic.Returns((T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7) => new ValueTask<TResult>(valueFunction(t1, t2, t3, t4, t5, t6, t7)));
    }

    /// <summary>
    /// Configures the setup to return a value computed by the specified function using the method's eight arguments,
    /// wrapped in a <see cref="ValueTask{TResult}"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the method's first argument.</typeparam>
    /// <typeparam name="T2">The type of the method's second argument.</typeparam>
    /// <typeparam name="T3">The type of the method's third argument.</typeparam>
    /// <typeparam name="T4">The type of the method's fourth argument.</typeparam>
    /// <typeparam name="T5">The type of the method's fifth argument.</typeparam>
    /// <typeparam name="T6">The type of the method's sixth argument.</typeparam>
    /// <typeparam name="T7">The type of the method's seventh argument.</typeparam>
    /// <typeparam name="T8">The type of the method's eighth argument.</typeparam>
    /// <typeparam name="TResult">The type of the value to be returned.</typeparam>
    /// <param name="mimic">The returns configuration instance.</param>
    /// <param name="valueFunction">A function that takes the method's arguments and computes the return value.</param>
    /// <returns>An <see cref="IReturnsResult"/> that can be used to configure additional return behaviours.</returns>
    /// <remarks>
    /// This method allows the return value to be computed based on the method's arguments while automatically handling the ValueTask wrapping.
    /// The function receives the arguments passed to the mocked method at invocation time.
    /// </remarks>
    public static IReturnsResult Returns<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(this IReturns<ValueTask<TResult>> mimic, Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> valueFunction)
    {
        return mimic.Returns((T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8) => new ValueTask<TResult>(valueFunction(t1, t2, t3, t4, t5, t6, t7, t8)));
    }

    /// <summary>
    /// Configures the setup to return a value computed by the specified function using the method's nine arguments,
    /// wrapped in a <see cref="ValueTask{TResult}"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the method's first argument.</typeparam>
    /// <typeparam name="T2">The type of the method's second argument.</typeparam>
    /// <typeparam name="T3">The type of the method's third argument.</typeparam>
    /// <typeparam name="T4">The type of the method's fourth argument.</typeparam>
    /// <typeparam name="T5">The type of the method's fifth argument.</typeparam>
    /// <typeparam name="T6">The type of the method's sixth argument.</typeparam>
    /// <typeparam name="T7">The type of the method's seventh argument.</typeparam>
    /// <typeparam name="T8">The type of the method's eighth argument.</typeparam>
    /// <typeparam name="T9">The type of the method's ninth argument.</typeparam>
    /// <typeparam name="TResult">The type of the value to be returned.</typeparam>
    /// <param name="mimic">The returns configuration instance.</param>
    /// <param name="valueFunction">A function that takes the method's arguments and computes the return value.</param>
    /// <returns>An <see cref="IReturnsResult"/> that can be used to configure additional return behaviours.</returns>
    /// <remarks>
    /// This method allows the return value to be computed based on the method's arguments while automatically handling the ValueTask wrapping.
    /// The function receives the arguments passed to the mocked method at invocation time.
    /// </remarks>
    public static IReturnsResult Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(this IReturns<ValueTask<TResult>> mimic, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> valueFunction)
    {
        return mimic.Returns((T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9) => new ValueTask<TResult>(valueFunction(t1, t2, t3, t4, t5, t6, t7, t8, t9)));
    }

    /// <summary>
    /// Configures the setup to return a value computed by the specified function using the method's ten arguments,
    /// wrapped in a <see cref="ValueTask{TResult}"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the method's first argument.</typeparam>
    /// <typeparam name="T2">The type of the method's second argument.</typeparam>
    /// <typeparam name="T3">The type of the method's third argument.</typeparam>
    /// <typeparam name="T4">The type of the method's fourth argument.</typeparam>
    /// <typeparam name="T5">The type of the method's fifth argument.</typeparam>
    /// <typeparam name="T6">The type of the method's sixth argument.</typeparam>
    /// <typeparam name="T7">The type of the method's seventh argument.</typeparam>
    /// <typeparam name="T8">The type of the method's eighth argument.</typeparam>
    /// <typeparam name="T9">The type of the method's ninth argument.</typeparam>
    /// <typeparam name="T10">The type of the method's tenth argument.</typeparam>
    /// <typeparam name="TResult">The type of the value to be returned.</typeparam>
    /// <param name="mimic">The returns configuration instance.</param>
    /// <param name="valueFunction">A function that takes the method's arguments and computes the return value.</param>
    /// <returns>An <see cref="IReturnsResult"/> that can be used to configure additional return behaviours.</returns>
    /// <remarks>
    /// This method allows the return value to be computed based on the method's arguments while automatically handling the ValueTask wrapping.
    /// The function receives the arguments passed to the mocked method at invocation time.
    /// </remarks>
    public static IReturnsResult Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(this IReturns<ValueTask<TResult>> mimic, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> valueFunction)
    {
        return mimic.Returns((T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10) => new ValueTask<TResult>(valueFunction(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10)));
    }

    /// <summary>
    /// Configures the setup to return a value computed by the specified function using the method's eleven arguments,
    /// wrapped in a <see cref="ValueTask{TResult}"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the method's first argument.</typeparam>
    /// <typeparam name="T2">The type of the method's second argument.</typeparam>
    /// <typeparam name="T3">The type of the method's third argument.</typeparam>
    /// <typeparam name="T4">The type of the method's fourth argument.</typeparam>
    /// <typeparam name="T5">The type of the method's fifth argument.</typeparam>
    /// <typeparam name="T6">The type of the method's sixth argument.</typeparam>
    /// <typeparam name="T7">The type of the method's seventh argument.</typeparam>
    /// <typeparam name="T8">The type of the method's eighth argument.</typeparam>
    /// <typeparam name="T9">The type of the method's ninth argument.</typeparam>
    /// <typeparam name="T10">The type of the method's tenth argument.</typeparam>
    /// <typeparam name="T11">The type of the method's eleventh argument.</typeparam>
    /// <typeparam name="TResult">The type of the value to be returned.</typeparam>
    /// <param name="mimic">The returns configuration instance.</param>
    /// <param name="valueFunction">A function that takes the method's arguments and computes the return value.</param>
    /// <returns>An <see cref="IReturnsResult"/> that can be used to configure additional return behaviours.</returns>
    /// <remarks>
    /// This method allows the return value to be computed based on the method's arguments while automatically handling the ValueTask wrapping.
    /// The function receives the arguments passed to the mocked method at invocation time.
    /// </remarks>
    public static IReturnsResult Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(this IReturns<ValueTask<TResult>> mimic, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> valueFunction)
    {
        return mimic.Returns((T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11) => new ValueTask<TResult>(valueFunction(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11)));
    }

    /// <summary>
    /// Configures the setup to return a value computed by the specified function using the method's twelve arguments,
    /// wrapped in a <see cref="ValueTask{TResult}"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the method's first argument.</typeparam>
    /// <typeparam name="T2">The type of the method's second argument.</typeparam>
    /// <typeparam name="T3">The type of the method's third argument.</typeparam>
    /// <typeparam name="T4">The type of the method's fourth argument.</typeparam>
    /// <typeparam name="T5">The type of the method's fifth argument.</typeparam>
    /// <typeparam name="T6">The type of the method's sixth argument.</typeparam>
    /// <typeparam name="T7">The type of the method's seventh argument.</typeparam>
    /// <typeparam name="T8">The type of the method's eighth argument.</typeparam>
    /// <typeparam name="T9">The type of the method's ninth argument.</typeparam>
    /// <typeparam name="T10">The type of the method's tenth argument.</typeparam>
    /// <typeparam name="T11">The type of the method's eleventh argument.</typeparam>
    /// <typeparam name="T12">The type of the method's twelfth argument.</typeparam>
    /// <typeparam name="TResult">The type of the value to be returned.</typeparam>
    /// <param name="mimic">The returns configuration instance.</param>
    /// <param name="valueFunction">A function that takes the method's arguments and computes the return value.</param>
    /// <returns>An <see cref="IReturnsResult"/> that can be used to configure additional return behaviours.</returns>
    /// <remarks>
    /// This method allows the return value to be computed based on the method's arguments while automatically handling the ValueTask wrapping.
    /// The function receives the arguments passed to the mocked method at invocation time.
    /// </remarks>
    public static IReturnsResult Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(this IReturns<ValueTask<TResult>> mimic, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> valueFunction)
    {
        return mimic.Returns((T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12) => new ValueTask<TResult>(valueFunction(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12)));
    }

    /// <summary>
    /// Configures the setup to return a value computed by the specified function using the method's thirteen arguments,
    /// wrapped in a <see cref="ValueTask{TResult}"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the method's first argument.</typeparam>
    /// <typeparam name="T2">The type of the method's second argument.</typeparam>
    /// <typeparam name="T3">The type of the method's third argument.</typeparam>
    /// <typeparam name="T4">The type of the method's fourth argument.</typeparam>
    /// <typeparam name="T5">The type of the method's fifth argument.</typeparam>
    /// <typeparam name="T6">The type of the method's sixth argument.</typeparam>
    /// <typeparam name="T7">The type of the method's seventh argument.</typeparam>
    /// <typeparam name="T8">The type of the method's eighth argument.</typeparam>
    /// <typeparam name="T9">The type of the method's ninth argument.</typeparam>
    /// <typeparam name="T10">The type of the method's tenth argument.</typeparam>
    /// <typeparam name="T11">The type of the method's eleventh argument.</typeparam>
    /// <typeparam name="T12">The type of the method's twelfth argument.</typeparam>
    /// <typeparam name="T13">The type of the method's thirteenth argument.</typeparam>
    /// <typeparam name="TResult">The type of the value to be returned.</typeparam>
    /// <param name="mimic">The returns configuration instance.</param>
    /// <param name="valueFunction">A function that takes the method's arguments and computes the return value.</param>
    /// <returns>An <see cref="IReturnsResult"/> that can be used to configure additional return behaviours.</returns>
    /// <remarks>
    /// This method allows the return value to be computed based on the method's arguments while automatically handling the ValueTask wrapping.
    /// The function receives the arguments passed to the mocked method at invocation time.
    /// </remarks>
    public static IReturnsResult Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(this IReturns<ValueTask<TResult>> mimic, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> valueFunction)
    {
        return mimic.Returns((T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13) => new ValueTask<TResult>(valueFunction(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13)));
    }

    /// <summary>
    /// Configures the setup to return a value computed by the specified function using the method's fourteen arguments,
    /// wrapped in a <see cref="ValueTask{TResult}"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the method's first argument.</typeparam>
    /// <typeparam name="T2">The type of the method's second argument.</typeparam>
    /// <typeparam name="T3">The type of the method's third argument.</typeparam>
    /// <typeparam name="T4">The type of the method's fourth argument.</typeparam>
    /// <typeparam name="T5">The type of the method's fifth argument.</typeparam>
    /// <typeparam name="T6">The type of the method's sixth argument.</typeparam>
    /// <typeparam name="T7">The type of the method's seventh argument.</typeparam>
    /// <typeparam name="T8">The type of the method's eighth argument.</typeparam>
    /// <typeparam name="T9">The type of the method's ninth argument.</typeparam>
    /// <typeparam name="T10">The type of the method's tenth argument.</typeparam>
    /// <typeparam name="T11">The type of the method's eleventh argument.</typeparam>
    /// <typeparam name="T12">The type of the method's twelfth argument.</typeparam>
    /// <typeparam name="T13">The type of the method's thirteenth argument.</typeparam>
    /// <typeparam name="T14">The type of the method's fourteenth argument.</typeparam>
    /// <typeparam name="TResult">The type of the value to be returned.</typeparam>
    /// <param name="mimic">The returns configuration instance.</param>
    /// <param name="valueFunction">A function that takes the method's arguments and computes the return value.</param>
    /// <returns>An <see cref="IReturnsResult"/> that can be used to configure additional return behaviours.</returns>
    /// <remarks>
    /// This method allows the return value to be computed based on the method's arguments while automatically handling the ValueTask wrapping.
    /// The function receives the arguments passed to the mocked method at invocation time.
    /// </remarks>
    public static IReturnsResult Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(this IReturns<ValueTask<TResult>> mimic, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> valueFunction)
    {
        return mimic.Returns((T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14) => new ValueTask<TResult>(valueFunction(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14)));
    }

    /// <summary>
    /// Configures the setup to return a value computed by the specified function using the method's fifteen arguments,
    /// wrapped in a <see cref="ValueTask{TResult}"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the method's first argument.</typeparam>
    /// <typeparam name="T2">The type of the method's second argument.</typeparam>
    /// <typeparam name="T3">The type of the method's third argument.</typeparam>
    /// <typeparam name="T4">The type of the method's fourth argument.</typeparam>
    /// <typeparam name="T5">The type of the method's fifth argument.</typeparam>
    /// <typeparam name="T6">The type of the method's sixth argument.</typeparam>
    /// <typeparam name="T7">The type of the method's seventh argument.</typeparam>
    /// <typeparam name="T8">The type of the method's eighth argument.</typeparam>
    /// <typeparam name="T9">The type of the method's ninth argument.</typeparam>
    /// <typeparam name="T10">The type of the method's tenth argument.</typeparam>
    /// <typeparam name="T11">The type of the method's eleventh argument.</typeparam>
    /// <typeparam name="T12">The type of the method's twelfth argument.</typeparam>
    /// <typeparam name="T13">The type of the method's thirteenth argument.</typeparam>
    /// <typeparam name="T14">The type of the method's fourteenth argument.</typeparam>
    /// <typeparam name="T15">The type of the method's fifteenth argument.</typeparam>
    /// <typeparam name="TResult">The type of the value to be returned.</typeparam>
    /// <param name="mimic">The returns configuration instance.</param>
    /// <param name="valueFunction">A function that takes the method's arguments and computes the return value.</param>
    /// <returns>An <see cref="IReturnsResult"/> that can be used to configure additional return behaviours.</returns>
    /// <remarks>
    /// This method allows the return value to be computed based on the method's arguments while automatically handling the ValueTask wrapping.
    /// This is the maximum parameter count supported by these extension methods.
    /// </remarks>
    public static IReturnsResult Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(this IReturns<ValueTask<TResult>> mimic, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> valueFunction)
    {
        return mimic.Returns((T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, T15 t15) => new ValueTask<TResult>(valueFunction(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15)));
    }

    #endregion
}
