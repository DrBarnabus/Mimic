namespace Mimic;

/// <summary>
/// Provides callback functionality for mock setups that do not return values.
/// This interface allows you to specify actions to execute when a mocked method is called.
/// </summary>
/// <remarks>
/// This interface is marked with <see cref="EditorBrowsableAttribute"/> to hide it from IntelliSense,
/// as it's part of the internal fluent API chain. Users typically access callback functionality
/// through method chaining on setup expressions.
/// </remarks>
[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface ICallback : IFluent
{
    /// <summary>
    /// Specifies a delegate callback to execute when the mocked method is called.
    /// </summary>
    /// <param name="callback">The delegate to execute. The delegate signature should match the mocked method.</param>
    /// <returns>A <see cref="ICallbackResult"/> that allows further configuration of the mock setup.</returns>
    ICallbackResult Callback(Delegate callback);

    /// <summary>
    /// Specifies an action callback to execute when the mocked method is called.
    /// </summary>
    /// <param name="callback">The action to execute with no parameters.</param>
    /// <returns>A <see cref="ICallbackResult"/> that allows further configuration of the mock setup.</returns>
    ICallbackResult Callback(Action callback);

    /// <summary>
    /// Specifies an action callback to execute when the mocked method is called.
    /// </summary>
    /// <typeparam name="T">The type of the first parameter.</typeparam>
    /// <param name="callback">The action to execute with one parameter.</param>
    /// <returns>A <see cref="ICallbackResult"/> that allows further configuration of the mock setup.</returns>
    ICallbackResult Callback<T>(Action<T> callback);

    /// <summary>
    /// Specifies an action callback to execute when the mocked method is called.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter.</typeparam>
    /// <typeparam name="T2">The type of the second parameter.</typeparam>
    /// <param name="callback">The action to execute with two parameters.</param>
    /// <returns>A <see cref="ICallbackResult"/> that allows further configuration of the mock setup.</returns>
    ICallbackResult Callback<T1, T2>(Action<T1, T2> callback);

    /// <summary>
    /// Specifies an action callback to execute when the mocked method is called.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter.</typeparam>
    /// <typeparam name="T2">The type of the second parameter.</typeparam>
    /// <typeparam name="T3">The type of the third parameter.</typeparam>
    /// <param name="callback">The action to execute with three parameters.</param>
    /// <returns>A <see cref="ICallbackResult"/> that allows further configuration of the mock setup.</returns>
    ICallbackResult Callback<T1, T2, T3>(Action<T1, T2, T3> callback);

    /// <summary>
    /// Specifies an action callback to execute when the mocked method is called.
    /// Additional overloads are provided to support methods with varying parameter counts (4-16 parameters).
    /// </summary>
    /// <remarks>
    /// The remaining callback overloads follow the same pattern, supporting methods with 4 to 16 parameters.
    /// Each overload provides strongly-typed parameter access to the callback action.
    /// </remarks>
    ICallbackResult Callback<T1, T2, T3, T4>(Action<T1, T2, T3, T4> callback);

    /// <summary>
    /// Specifies an action callback to execute when the mocked method is called.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter.</typeparam>
    /// <typeparam name="T2">The type of the second parameter.</typeparam>
    /// <typeparam name="T3">The type of the third parameter.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter.</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter.</typeparam>
    /// <param name="callback">The action to execute with five parameters.</param>
    /// <returns>A <see cref="ICallbackResult"/> that allows further configuration of the mock setup.</returns>
    ICallbackResult Callback<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> callback);

    /// <summary>
    /// Specifies an action callback to execute when the mocked method is called.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter.</typeparam>
    /// <typeparam name="T2">The type of the second parameter.</typeparam>
    /// <typeparam name="T3">The type of the third parameter.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter.</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter.</typeparam>
    /// <typeparam name="T6">The type of the sixth parameter.</typeparam>
    /// <param name="callback">The action to execute with six parameters.</param>
    /// <returns>A <see cref="ICallbackResult"/> that allows further configuration of the mock setup.</returns>
    ICallbackResult Callback<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> callback);

    /// <summary>
    /// Specifies an action callback to execute when the mocked method is called.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter.</typeparam>
    /// <typeparam name="T2">The type of the second parameter.</typeparam>
    /// <typeparam name="T3">The type of the third parameter.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter.</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter.</typeparam>
    /// <typeparam name="T6">The type of the sixth parameter.</typeparam>
    /// <typeparam name="T7">The type of the seventh parameter.</typeparam>
    /// <param name="callback">The action to execute with seven parameters.</param>
    /// <returns>A <see cref="ICallbackResult"/> that allows further configuration of the mock setup.</returns>
    ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> callback);

    /// <summary>
    /// Specifies an action callback to execute when the mocked method is called.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter.</typeparam>
    /// <typeparam name="T2">The type of the second parameter.</typeparam>
    /// <typeparam name="T3">The type of the third parameter.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter.</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter.</typeparam>
    /// <typeparam name="T6">The type of the sixth parameter.</typeparam>
    /// <typeparam name="T7">The type of the seventh parameter.</typeparam>
    /// <typeparam name="T8">The type of the eighth parameter.</typeparam>
    /// <param name="callback">The action to execute with eight parameters.</param>
    /// <returns>A <see cref="ICallbackResult"/> that allows further configuration of the mock setup.</returns>
    ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8> callback);

    /// <summary>
    /// Specifies an action callback to execute when the mocked method is called.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter.</typeparam>
    /// <typeparam name="T2">The type of the second parameter.</typeparam>
    /// <typeparam name="T3">The type of the third parameter.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter.</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter.</typeparam>
    /// <typeparam name="T6">The type of the sixth parameter.</typeparam>
    /// <typeparam name="T7">The type of the seventh parameter.</typeparam>
    /// <typeparam name="T8">The type of the eighth parameter.</typeparam>
    /// <typeparam name="T9">The type of the ninth parameter.</typeparam>
    /// <param name="callback">The action to execute with nine parameters.</param>
    /// <returns>A <see cref="ICallbackResult"/> that allows further configuration of the mock setup.</returns>
    ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> callback);

    /// <summary>
    /// Specifies an action callback to execute when the mocked method is called.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter.</typeparam>
    /// <typeparam name="T2">The type of the second parameter.</typeparam>
    /// <typeparam name="T3">The type of the third parameter.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter.</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter.</typeparam>
    /// <typeparam name="T6">The type of the sixth parameter.</typeparam>
    /// <typeparam name="T7">The type of the seventh parameter.</typeparam>
    /// <typeparam name="T8">The type of the eighth parameter.</typeparam>
    /// <typeparam name="T9">The type of the ninth parameter.</typeparam>
    /// <typeparam name="T10">The type of the tenth parameter.</typeparam>
    /// <param name="callback">The action to execute with ten parameters.</param>
    /// <returns>A <see cref="ICallbackResult"/> that allows further configuration of the mock setup.</returns>
    ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> callback);

    /// <summary>
    /// Specifies an action callback to execute when the mocked method is called.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter.</typeparam>
    /// <typeparam name="T2">The type of the second parameter.</typeparam>
    /// <typeparam name="T3">The type of the third parameter.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter.</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter.</typeparam>
    /// <typeparam name="T6">The type of the sixth parameter.</typeparam>
    /// <typeparam name="T7">The type of the seventh parameter.</typeparam>
    /// <typeparam name="T8">The type of the eighth parameter.</typeparam>
    /// <typeparam name="T9">The type of the ninth parameter.</typeparam>
    /// <typeparam name="T10">The type of the tenth parameter.</typeparam>
    /// <typeparam name="T11">The type of the eleventh parameter.</typeparam>
    /// <param name="callback">The action to execute with eleven parameters.</param>
    /// <returns>A <see cref="ICallbackResult"/> that allows further configuration of the mock setup.</returns>
    ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> callback);

    /// <summary>
    /// Specifies an action callback to execute when the mocked method is called.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter.</typeparam>
    /// <typeparam name="T2">The type of the second parameter.</typeparam>
    /// <typeparam name="T3">The type of the third parameter.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter.</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter.</typeparam>
    /// <typeparam name="T6">The type of the sixth parameter.</typeparam>
    /// <typeparam name="T7">The type of the seventh parameter.</typeparam>
    /// <typeparam name="T8">The type of the eighth parameter.</typeparam>
    /// <typeparam name="T9">The type of the ninth parameter.</typeparam>
    /// <typeparam name="T10">The type of the tenth parameter.</typeparam>
    /// <typeparam name="T11">The type of the eleventh parameter.</typeparam>
    /// <typeparam name="T12">The type of the twelfth parameter.</typeparam>
    /// <param name="callback">The action to execute with twelve parameters.</param>
    /// <returns>A <see cref="ICallbackResult"/> that allows further configuration of the mock setup.</returns>
    ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> callback);

    /// <summary>
    /// Specifies an action callback to execute when the mocked method is called.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter.</typeparam>
    /// <typeparam name="T2">The type of the second parameter.</typeparam>
    /// <typeparam name="T3">The type of the third parameter.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter.</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter.</typeparam>
    /// <typeparam name="T6">The type of the sixth parameter.</typeparam>
    /// <typeparam name="T7">The type of the seventh parameter.</typeparam>
    /// <typeparam name="T8">The type of the eighth parameter.</typeparam>
    /// <typeparam name="T9">The type of the ninth parameter.</typeparam>
    /// <typeparam name="T10">The type of the tenth parameter.</typeparam>
    /// <typeparam name="T11">The type of the eleventh parameter.</typeparam>
    /// <typeparam name="T12">The type of the twelfth parameter.</typeparam>
    /// <typeparam name="T13">The type of the thirteenth parameter.</typeparam>
    /// <param name="callback">The action to execute with thirteen parameters.</param>
    /// <returns>A <see cref="ICallbackResult"/> that allows further configuration of the mock setup.</returns>
    ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> callback);

    /// <summary>
    /// Specifies an action callback to execute when the mocked method is called.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter.</typeparam>
    /// <typeparam name="T2">The type of the second parameter.</typeparam>
    /// <typeparam name="T3">The type of the third parameter.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter.</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter.</typeparam>
    /// <typeparam name="T6">The type of the sixth parameter.</typeparam>
    /// <typeparam name="T7">The type of the seventh parameter.</typeparam>
    /// <typeparam name="T8">The type of the eighth parameter.</typeparam>
    /// <typeparam name="T9">The type of the ninth parameter.</typeparam>
    /// <typeparam name="T10">The type of the tenth parameter.</typeparam>
    /// <typeparam name="T11">The type of the eleventh parameter.</typeparam>
    /// <typeparam name="T12">The type of the twelfth parameter.</typeparam>
    /// <typeparam name="T13">The type of the thirteenth parameter.</typeparam>
    /// <typeparam name="T14">The type of the fourteenth parameter.</typeparam>
    /// <param name="callback">The action to execute with fourteen parameters.</param>
    /// <returns>A <see cref="ICallbackResult"/> that allows further configuration of the mock setup.</returns>
    ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> callback);

    /// <summary>
    /// Specifies an action callback to execute when the mocked method is called.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter.</typeparam>
    /// <typeparam name="T2">The type of the second parameter.</typeparam>
    /// <typeparam name="T3">The type of the third parameter.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter.</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter.</typeparam>
    /// <typeparam name="T6">The type of the sixth parameter.</typeparam>
    /// <typeparam name="T7">The type of the seventh parameter.</typeparam>
    /// <typeparam name="T8">The type of the eighth parameter.</typeparam>
    /// <typeparam name="T9">The type of the ninth parameter.</typeparam>
    /// <typeparam name="T10">The type of the tenth parameter.</typeparam>
    /// <typeparam name="T11">The type of the eleventh parameter.</typeparam>
    /// <typeparam name="T12">The type of the twelfth parameter.</typeparam>
    /// <typeparam name="T13">The type of the thirteenth parameter.</typeparam>
    /// <typeparam name="T14">The type of the fourteenth parameter.</typeparam>
    /// <typeparam name="T15">The type of the fifteenth parameter.</typeparam>
    /// <param name="callback">The action to execute with fifteen parameters.</param>
    /// <returns>A <see cref="ICallbackResult"/> that allows further configuration of the mock setup.</returns>
    ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> callback);

    /// <summary>
    /// Specifies an action callback to execute when the mocked method is called.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter.</typeparam>
    /// <typeparam name="T2">The type of the second parameter.</typeparam>
    /// <typeparam name="T3">The type of the third parameter.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter.</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter.</typeparam>
    /// <typeparam name="T6">The type of the sixth parameter.</typeparam>
    /// <typeparam name="T7">The type of the seventh parameter.</typeparam>
    /// <typeparam name="T8">The type of the eighth parameter.</typeparam>
    /// <typeparam name="T9">The type of the ninth parameter.</typeparam>
    /// <typeparam name="T10">The type of the tenth parameter.</typeparam>
    /// <typeparam name="T11">The type of the eleventh parameter.</typeparam>
    /// <typeparam name="T12">The type of the twelfth parameter.</typeparam>
    /// <typeparam name="T13">The type of the thirteenth parameter.</typeparam>
    /// <typeparam name="T14">The type of the fourteenth parameter.</typeparam>
    /// <typeparam name="T15">The type of the fifteenth parameter.</typeparam>
    /// <typeparam name="T16">The type of the sixteenth parameter.</typeparam>
    /// <param name="callback">The action to execute with sixteen parameters.</param>
    /// <returns>A <see cref="ICallbackResult"/> that allows further configuration of the mock setup.</returns>
    ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> callback);
}
