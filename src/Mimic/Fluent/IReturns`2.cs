namespace Mimic;

/// <summary>
/// Provides comprehensive functionality to configure return values for methods in a mimic setup with fluent continuation.
/// This interface supports both direct value returns and computed returns using function delegates with various parameter counts.
/// </summary>
/// <typeparam name="TResult">The type of value that the configured method should return.</typeparam>
/// <typeparam name="TNext">The type of the next interface in the fluent chain, which must implement <see cref="IFluent"/>.</typeparam>
/// <remarks>
/// This interface is marked with <see cref="EditorBrowsableAttribute"/> to hide it from IntelliSense,
/// as it serves as part of the fluent API chain. It provides extensive overloads for return value configuration:
/// <list type="bullet">
/// <item><description>Direct value returns - Returns a specific value</description></item>
/// <item><description>Computed returns - Uses functions to compute return values based on method arguments</description></item>
/// <item><description>Parameterless functions - Returns values computed without method arguments</description></item>
/// <item><description>Parameterized functions - Returns values computed using method arguments (up to 16 parameters)</description></item>
/// <item><description>Proceed functionality - Allows the original method to execute and return its result</description></item>
/// </list>
/// The function-based overloads support up to 16 parameters, covering virtually all practical method signatures.
/// Each method returns <typeparamref name="TNext"/> to continue the fluent API chain.
/// </remarks>
[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IReturns<in TResult, out TNext> : IFluent
    where TNext : IFluent
{
    /// <summary>
    /// Configures the setup to return a specific value.
    /// </summary>
    /// <param name="value">The value to return when the method is called. Can be null.</param>
    /// <returns>The next interface in the fluent chain for further configuration.</returns>
    TNext Returns(TResult? value);

    /// <summary>
    /// Configures the setup to return a value computed by a function.
    /// </summary>
    /// <param name="valueFunction">A function that computes the return value. Can return null.</param>
    /// <returns>The next interface in the fluent chain for further configuration.</returns>
    TNext Returns(Func<TResult?> valueFunction);

    /// <summary>
    /// Configures the setup to return a value computed by a function that takes one method argument.
    /// </summary>
    /// <typeparam name="T">The type of the method argument.</typeparam>
    /// <param name="valueFunction">A function that takes the method argument and computes the return value.</param>
    /// <returns>The next interface in the fluent chain for further configuration.</returns>
    TNext Returns<T>(Func<T, TResult> valueFunction);

    /// <summary>
    /// Configures the setup to return a value computed by a function that takes two method arguments.
    /// </summary>
    /// <typeparam name="T1">The type of the first method argument.</typeparam>
    /// <typeparam name="T2">The type of the second method argument.</typeparam>
    /// <param name="valueFunction">A function that takes the method arguments and computes the return value.</param>
    /// <returns>The next interface in the fluent chain for further configuration.</returns>
    TNext Returns<T1, T2>(Func<T1, T2, TResult> valueFunction);

    /// <summary>
    /// Configures the setup to return a value computed by a function that takes three method arguments.
    /// </summary>
    /// <typeparam name="T1">The type of the first method argument.</typeparam>
    /// <typeparam name="T2">The type of the second method argument.</typeparam>
    /// <typeparam name="T3">The type of the third method argument.</typeparam>
    /// <param name="valueFunction">A function that takes the method arguments and computes the return value.</param>
    /// <returns>The next interface in the fluent chain for further configuration.</returns>
    TNext Returns<T1, T2, T3>(Func<T1, T2, T3, TResult> valueFunction);

    /// <summary>
    /// Configures the setup to return a value computed by a function that takes four method arguments.
    /// </summary>
    /// <typeparam name="T1">The type of the first method argument.</typeparam>
    /// <typeparam name="T2">The type of the second method argument.</typeparam>
    /// <typeparam name="T3">The type of the third method argument.</typeparam>
    /// <typeparam name="T4">The type of the fourth method argument.</typeparam>
    /// <param name="valueFunction">A function that takes the method arguments and computes the return value.</param>
    /// <returns>The next interface in the fluent chain for further configuration.</returns>
    TNext Returns<T1, T2, T3, T4>(Func<T1, T2, T3, T4, TResult> valueFunction);

    /// <summary>
    /// Configures the setup to return a value computed by a function that takes five method arguments.
    /// </summary>
    /// <typeparam name="T1">The type of the first method argument.</typeparam>
    /// <typeparam name="T2">The type of the second method argument.</typeparam>
    /// <typeparam name="T3">The type of the third method argument.</typeparam>
    /// <typeparam name="T4">The type of the fourth method argument.</typeparam>
    /// <typeparam name="T5">The type of the fifth method argument.</typeparam>
    /// <param name="valueFunction">A function that takes the method arguments and computes the return value.</param>
    /// <returns>The next interface in the fluent chain for further configuration.</returns>
    TNext Returns<T1, T2, T3, T4, T5>(Func<T1, T2, T3, T4, T5, TResult> valueFunction);

    /// <summary>
    /// Configures the setup to return a value computed by a function that takes six method arguments.
    /// </summary>
    /// <typeparam name="T1">The type of the first method argument.</typeparam>
    /// <typeparam name="T2">The type of the second method argument.</typeparam>
    /// <typeparam name="T3">The type of the third method argument.</typeparam>
    /// <typeparam name="T4">The type of the fourth method argument.</typeparam>
    /// <typeparam name="T5">The type of the fifth method argument.</typeparam>
    /// <typeparam name="T6">The type of the sixth method argument.</typeparam>
    /// <param name="valueFunction">A function that takes the method arguments and computes the return value.</param>
    /// <returns>The next interface in the fluent chain for further configuration.</returns>
    TNext Returns<T1, T2, T3, T4, T5, T6>(Func<T1, T2, T3, T4, T5, T6, TResult> valueFunction);

    /// <summary>
    /// Configures the setup to return a value computed by a function that takes seven method arguments.
    /// </summary>
    /// <typeparam name="T1">The type of the first method argument.</typeparam>
    /// <typeparam name="T2">The type of the second method argument.</typeparam>
    /// <typeparam name="T3">The type of the third method argument.</typeparam>
    /// <typeparam name="T4">The type of the fourth method argument.</typeparam>
    /// <typeparam name="T5">The type of the fifth method argument.</typeparam>
    /// <typeparam name="T6">The type of the sixth method argument.</typeparam>
    /// <typeparam name="T7">The type of the seventh method argument.</typeparam>
    /// <param name="valueFunction">A function that takes the method arguments and computes the return value.</param>
    /// <returns>The next interface in the fluent chain for further configuration.</returns>
    TNext Returns<T1, T2, T3, T4, T5, T6, T7>(Func<T1, T2, T3, T4, T5, T6, T7, TResult> valueFunction);

    /// <summary>
    /// Configures the setup to return a value computed by a function that takes eight method arguments.
    /// </summary>
    /// <typeparam name="T1">The type of the first method argument.</typeparam>
    /// <typeparam name="T2">The type of the second method argument.</typeparam>
    /// <typeparam name="T3">The type of the third method argument.</typeparam>
    /// <typeparam name="T4">The type of the fourth method argument.</typeparam>
    /// <typeparam name="T5">The type of the fifth method argument.</typeparam>
    /// <typeparam name="T6">The type of the sixth method argument.</typeparam>
    /// <typeparam name="T7">The type of the seventh method argument.</typeparam>
    /// <typeparam name="T8">The type of the eighth method argument.</typeparam>
    /// <param name="valueFunction">A function that takes the method arguments and computes the return value.</param>
    /// <returns>The next interface in the fluent chain for further configuration.</returns>
    TNext Returns<T1, T2, T3, T4, T5, T6, T7, T8>(Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> valueFunction);

    /// <summary>
    /// Configures the setup to return a value computed by a function that takes nine method arguments.
    /// </summary>
    /// <typeparam name="T1">The type of the first method argument.</typeparam>
    /// <typeparam name="T2">The type of the second method argument.</typeparam>
    /// <typeparam name="T3">The type of the third method argument.</typeparam>
    /// <typeparam name="T4">The type of the fourth method argument.</typeparam>
    /// <typeparam name="T5">The type of the fifth method argument.</typeparam>
    /// <typeparam name="T6">The type of the sixth method argument.</typeparam>
    /// <typeparam name="T7">The type of the seventh method argument.</typeparam>
    /// <typeparam name="T8">The type of the eighth method argument.</typeparam>
    /// <typeparam name="T9">The type of the ninth method argument.</typeparam>
    /// <param name="valueFunction">A function that takes the method arguments and computes the return value.</param>
    /// <returns>The next interface in the fluent chain for further configuration.</returns>
    TNext Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> valueFunction);

    /// <summary>
    /// Configures the setup to return a value computed by a function that takes ten method arguments.
    /// </summary>
    /// <typeparam name="T1">The type of the first method argument.</typeparam>
    /// <typeparam name="T2">The type of the second method argument.</typeparam>
    /// <typeparam name="T3">The type of the third method argument.</typeparam>
    /// <typeparam name="T4">The type of the fourth method argument.</typeparam>
    /// <typeparam name="T5">The type of the fifth method argument.</typeparam>
    /// <typeparam name="T6">The type of the sixth method argument.</typeparam>
    /// <typeparam name="T7">The type of the seventh method argument.</typeparam>
    /// <typeparam name="T8">The type of the eighth method argument.</typeparam>
    /// <typeparam name="T9">The type of the ninth method argument.</typeparam>
    /// <typeparam name="T10">The type of the tenth method argument.</typeparam>
    /// <param name="valueFunction">A function that takes the method arguments and computes the return value.</param>
    /// <returns>The next interface in the fluent chain for further configuration.</returns>
    TNext Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> valueFunction);

    /// <summary>
    /// Configures the setup to return a value computed by a function that takes eleven method arguments.
    /// </summary>
    /// <typeparam name="T1">The type of the first method argument.</typeparam>
    /// <typeparam name="T2">The type of the second method argument.</typeparam>
    /// <typeparam name="T3">The type of the third method argument.</typeparam>
    /// <typeparam name="T4">The type of the fourth method argument.</typeparam>
    /// <typeparam name="T5">The type of the fifth method argument.</typeparam>
    /// <typeparam name="T6">The type of the sixth method argument.</typeparam>
    /// <typeparam name="T7">The type of the seventh method argument.</typeparam>
    /// <typeparam name="T8">The type of the eighth method argument.</typeparam>
    /// <typeparam name="T9">The type of the ninth method argument.</typeparam>
    /// <typeparam name="T10">The type of the tenth method argument.</typeparam>
    /// <typeparam name="T11">The type of the eleventh method argument.</typeparam>
    /// <param name="valueFunction">A function that takes the method arguments and computes the return value.</param>
    /// <returns>The next interface in the fluent chain for further configuration.</returns>
    TNext Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> valueFunction);

    /// <summary>
    /// Configures the setup to return a value computed by a function that takes twelve method arguments.
    /// </summary>
    /// <typeparam name="T1">The type of the first method argument.</typeparam>
    /// <typeparam name="T2">The type of the second method argument.</typeparam>
    /// <typeparam name="T3">The type of the third method argument.</typeparam>
    /// <typeparam name="T4">The type of the fourth method argument.</typeparam>
    /// <typeparam name="T5">The type of the fifth method argument.</typeparam>
    /// <typeparam name="T6">The type of the sixth method argument.</typeparam>
    /// <typeparam name="T7">The type of the seventh method argument.</typeparam>
    /// <typeparam name="T8">The type of the eighth method argument.</typeparam>
    /// <typeparam name="T9">The type of the ninth method argument.</typeparam>
    /// <typeparam name="T10">The type of the tenth method argument.</typeparam>
    /// <typeparam name="T11">The type of the eleventh method argument.</typeparam>
    /// <typeparam name="T12">The type of the twelfth method argument.</typeparam>
    /// <param name="valueFunction">A function that takes the method arguments and computes the return value.</param>
    /// <returns>The next interface in the fluent chain for further configuration.</returns>
    TNext Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> valueFunction);

    /// <summary>
    /// Configures the setup to return a value computed by a function that takes thirteen method arguments.
    /// </summary>
    /// <typeparam name="T1">The type of the first method argument.</typeparam>
    /// <typeparam name="T2">The type of the second method argument.</typeparam>
    /// <typeparam name="T3">The type of the third method argument.</typeparam>
    /// <typeparam name="T4">The type of the fourth method argument.</typeparam>
    /// <typeparam name="T5">The type of the fifth method argument.</typeparam>
    /// <typeparam name="T6">The type of the sixth method argument.</typeparam>
    /// <typeparam name="T7">The type of the seventh method argument.</typeparam>
    /// <typeparam name="T8">The type of the eighth method argument.</typeparam>
    /// <typeparam name="T9">The type of the ninth method argument.</typeparam>
    /// <typeparam name="T10">The type of the tenth method argument.</typeparam>
    /// <typeparam name="T11">The type of the eleventh method argument.</typeparam>
    /// <typeparam name="T12">The type of the twelfth method argument.</typeparam>
    /// <typeparam name="T13">The type of the thirteenth method argument.</typeparam>
    /// <param name="valueFunction">A function that takes the method arguments and computes the return value.</param>
    /// <returns>The next interface in the fluent chain for further configuration.</returns>
    TNext Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> valueFunction);

    /// <summary>
    /// Configures the setup to return a value computed by a function that takes fourteen method arguments.
    /// </summary>
    /// <typeparam name="T1">The type of the first method argument.</typeparam>
    /// <typeparam name="T2">The type of the second method argument.</typeparam>
    /// <typeparam name="T3">The type of the third method argument.</typeparam>
    /// <typeparam name="T4">The type of the fourth method argument.</typeparam>
    /// <typeparam name="T5">The type of the fifth method argument.</typeparam>
    /// <typeparam name="T6">The type of the sixth method argument.</typeparam>
    /// <typeparam name="T7">The type of the seventh method argument.</typeparam>
    /// <typeparam name="T8">The type of the eighth method argument.</typeparam>
    /// <typeparam name="T9">The type of the ninth method argument.</typeparam>
    /// <typeparam name="T10">The type of the tenth method argument.</typeparam>
    /// <typeparam name="T11">The type of the eleventh method argument.</typeparam>
    /// <typeparam name="T12">The type of the twelfth method argument.</typeparam>
    /// <typeparam name="T13">The type of the thirteenth method argument.</typeparam>
    /// <typeparam name="T14">The type of the fourteenth method argument.</typeparam>
    /// <param name="valueFunction">A function that takes the method arguments and computes the return value.</param>
    /// <returns>The next interface in the fluent chain for further configuration.</returns>
    TNext Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> valueFunction);

    /// <summary>
    /// Configures the setup to return a value computed by a function that takes fifteen method arguments.
    /// </summary>
    /// <typeparam name="T1">The type of the first method argument.</typeparam>
    /// <typeparam name="T2">The type of the second method argument.</typeparam>
    /// <typeparam name="T3">The type of the third method argument.</typeparam>
    /// <typeparam name="T4">The type of the fourth method argument.</typeparam>
    /// <typeparam name="T5">The type of the fifth method argument.</typeparam>
    /// <typeparam name="T6">The type of the sixth method argument.</typeparam>
    /// <typeparam name="T7">The type of the seventh method argument.</typeparam>
    /// <typeparam name="T8">The type of the eighth method argument.</typeparam>
    /// <typeparam name="T9">The type of the ninth method argument.</typeparam>
    /// <typeparam name="T10">The type of the tenth method argument.</typeparam>
    /// <typeparam name="T11">The type of the eleventh method argument.</typeparam>
    /// <typeparam name="T12">The type of the twelfth method argument.</typeparam>
    /// <typeparam name="T13">The type of the thirteenth method argument.</typeparam>
    /// <typeparam name="T14">The type of the fourteenth method argument.</typeparam>
    /// <typeparam name="T15">The type of the fifteenth method argument.</typeparam>
    /// <param name="valueFunction">A function that takes the method arguments and computes the return value.</param>
    /// <returns>The next interface in the fluent chain for further configuration.</returns>
    TNext Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> valueFunction);

    /// <summary>
    /// Configures the setup to return a value computed by a function that takes sixteen method arguments.
    /// </summary>
    /// <typeparam name="T1">The type of the first method argument.</typeparam>
    /// <typeparam name="T2">The type of the second method argument.</typeparam>
    /// <typeparam name="T3">The type of the third method argument.</typeparam>
    /// <typeparam name="T4">The type of the fourth method argument.</typeparam>
    /// <typeparam name="T5">The type of the fifth method argument.</typeparam>
    /// <typeparam name="T6">The type of the sixth method argument.</typeparam>
    /// <typeparam name="T7">The type of the seventh method argument.</typeparam>
    /// <typeparam name="T8">The type of the eighth method argument.</typeparam>
    /// <typeparam name="T9">The type of the ninth method argument.</typeparam>
    /// <typeparam name="T10">The type of the tenth method argument.</typeparam>
    /// <typeparam name="T11">The type of the eleventh method argument.</typeparam>
    /// <typeparam name="T12">The type of the twelfth method argument.</typeparam>
    /// <typeparam name="T13">The type of the thirteenth method argument.</typeparam>
    /// <typeparam name="T14">The type of the fourteenth method argument.</typeparam>
    /// <typeparam name="T15">The type of the fifteenth method argument.</typeparam>
    /// <typeparam name="T16">The type of the sixteenth method argument.</typeparam>
    /// <param name="valueFunction">A function that takes the method arguments and computes the return value.</param>
    /// <returns>The next interface in the fluent chain for further configuration.</returns>
    TNext Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> valueFunction);

    /// <summary>
    /// Configures the setup to proceed with the original method implementation and return its result.
    /// This allows the real method to execute while still maintaining the mock setup.
    /// </summary>
    /// <returns>The next interface in the fluent chain for further configuration.</returns>
    /// <remarks>
    /// This is useful when you want to combine real method execution with mock behaviors such as
    /// callbacks, delays, or verification. The original method will be called and its return value
    /// will be used as the result.
    /// </remarks>
    TNext Proceed();
}
