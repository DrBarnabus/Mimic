namespace Mimic;

/// <summary>
/// Provides comprehensive functionality to configure exception throwing behavior for methods in a mimic setup with fluent continuation.
/// This interface supports various ways to throw exceptions including direct exception instances and factory functions with different parameter counts.
/// </summary>
/// <typeparam name="TNext">The type of the next interface in the fluent chain, which must implement <see cref="IFluent"/>.</typeparam>
/// <remarks>
/// This interface is marked with <see cref="EditorBrowsableAttribute"/> to hide it from IntelliSense,
/// as it serves as part of the fluent API chain. It provides extensive overloads for exception throwing configuration:
/// <list type="bullet">
/// <item><description>Direct exception throwing - Throws a specific exception instance</description></item>
/// <item><description>Generic exception creation - Creates exceptions using parameterless constructors</description></item>
/// <item><description>Generic delegate factories - Creates exceptions using delegate factories</description></item>
/// <item><description>Parameterized factories - Creates exceptions using method arguments (up to 16 parameters)</description></item>
/// </list>
/// The function-based overloads support up to 16 parameters, covering virtually all practical method signatures.
/// Each method returns <typeparamref name="TNext"/> to continue the fluent API chain.
/// All exception factory methods are constrained to ensure only valid <see cref="Exception"/> types are used.
/// </remarks>
[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IThrows<out TNext> : IFluent
    where TNext : IFluent
{
    /// <summary>
    /// Configures the setup to throw a specific exception instance.
    /// </summary>
    /// <param name="exception">The exception instance to throw when the method is called.</param>
    /// <returns>The next interface in the fluent chain for further configuration.</returns>
    TNext Throws(Exception exception);

    /// <summary>
    /// Configures the setup to throw a new instance of the specified exception type using its parameterless constructor.
    /// </summary>
    /// <typeparam name="TException">The type of exception to throw. Must inherit from <see cref="Exception"/> and have a parameterless constructor.</typeparam>
    /// <returns>The next interface in the fluent chain for further configuration.</returns>
    TNext Throws<TException>() where TException : Exception, new();

    /// <summary>
    /// Configures the setup to throw an exception created by a delegate factory.
    /// </summary>
    /// <param name="exceptionFactory">A delegate that creates the exception to throw.</param>
    /// <returns>The next interface in the fluent chain for further configuration.</returns>
    TNext Throws(Delegate exceptionFactory);

    /// <summary>
    /// Configures the setup to throw an exception created by a parameterless function.
    /// </summary>
    /// <typeparam name="TException">The type of exception to throw. Must inherit from <see cref="Exception"/>.</typeparam>
    /// <param name="exceptionFactory">A function that creates the exception to throw. Can return null.</param>
    /// <returns>The next interface in the fluent chain for further configuration.</returns>
    TNext Throws<TException>(Func<TException?> exceptionFactory)
        where TException : Exception;

    /// <summary>
    /// Configures the setup to throw an exception created by a function that takes one method argument.
    /// </summary>
    /// <typeparam name="T">The type of the method argument.</typeparam>
    /// <typeparam name="TException">The type of exception to throw. Must inherit from <see cref="Exception"/>.</typeparam>
    /// <param name="exceptionFactory">A function that takes the method argument and creates the exception to throw.</param>
    /// <returns>The next interface in the fluent chain for further configuration.</returns>
    TNext Throws<T, TException>(Func<T, TException> exceptionFactory)
        where TException : Exception;

    /// <summary>
    /// Configures the setup to throw an exception created by a function that takes two method arguments.
    /// </summary>
    /// <typeparam name="T1">The type of the first method argument.</typeparam>
    /// <typeparam name="T2">The type of the second method argument.</typeparam>
    /// <typeparam name="TException">The type of exception to throw. Must inherit from <see cref="Exception"/>.</typeparam>
    /// <param name="exceptionFactory">A function that takes the method arguments and creates the exception to throw.</param>
    /// <returns>The next interface in the fluent chain for further configuration.</returns>
    TNext Throws<T1, T2, TException>(Func<T1, T2, TException> exceptionFactory)
        where TException : Exception;

    /// <summary>
    /// Configures the setup to throw an exception created by a function that takes three method arguments.
    /// </summary>
    /// <typeparam name="T1">The type of the first method argument.</typeparam>
    /// <typeparam name="T2">The type of the second method argument.</typeparam>
    /// <typeparam name="T3">The type of the third method argument.</typeparam>
    /// <typeparam name="TException">The type of exception to throw. Must inherit from <see cref="Exception"/>.</typeparam>
    /// <param name="exceptionFactory">A function that takes the method arguments and creates the exception to throw.</param>
    /// <returns>The next interface in the fluent chain for further configuration.</returns>
    TNext Throws<T1, T2, T3, TException>(Func<T1, T2, T3, TException> exceptionFactory)
        where TException : Exception;

    /// <summary>
    /// Configures the setup to throw an exception created by a function that takes four method arguments.
    /// </summary>
    /// <typeparam name="T1">The type of the first method argument.</typeparam>
    /// <typeparam name="T2">The type of the second method argument.</typeparam>
    /// <typeparam name="T3">The type of the third method argument.</typeparam>
    /// <typeparam name="T4">The type of the fourth method argument.</typeparam>
    /// <typeparam name="TException">The type of exception to throw. Must inherit from <see cref="Exception"/>.</typeparam>
    /// <param name="exceptionFactory">A function that takes the method arguments and creates the exception to throw.</param>
    /// <returns>The next interface in the fluent chain for further configuration.</returns>
    TNext Throws<T1, T2, T3, T4, TException>(Func<T1, T2, T3, T4, TException> exceptionFactory)
        where TException : Exception;

    /// <summary>
    /// Configures the setup to throw an exception created by a function that takes five method arguments.
    /// </summary>
    /// <typeparam name="T1">The type of the first method argument.</typeparam>
    /// <typeparam name="T2">The type of the second method argument.</typeparam>
    /// <typeparam name="T3">The type of the third method argument.</typeparam>
    /// <typeparam name="T4">The type of the fourth method argument.</typeparam>
    /// <typeparam name="T5">The type of the fifth method argument.</typeparam>
    /// <typeparam name="TException">The type of exception to throw. Must inherit from <see cref="Exception"/>.</typeparam>
    /// <param name="exceptionFactory">A function that takes the method arguments and creates the exception to throw.</param>
    /// <returns>The next interface in the fluent chain for further configuration.</returns>
    TNext Throws<T1, T2, T3, T4, T5, TException>(Func<T1, T2, T3, T4, T5, TException> exceptionFactory)
        where TException : Exception;

    /// <summary>
    /// Configures the setup to throw an exception created by a function that takes six method arguments.
    /// </summary>
    /// <typeparam name="T1">The type of the first method argument.</typeparam>
    /// <typeparam name="T2">The type of the second method argument.</typeparam>
    /// <typeparam name="T3">The type of the third method argument.</typeparam>
    /// <typeparam name="T4">The type of the fourth method argument.</typeparam>
    /// <typeparam name="T5">The type of the fifth method argument.</typeparam>
    /// <typeparam name="T6">The type of the sixth method argument.</typeparam>
    /// <typeparam name="TException">The type of exception to throw. Must inherit from <see cref="Exception"/>.</typeparam>
    /// <param name="exceptionFactory">A function that takes the method arguments and creates the exception to throw.</param>
    /// <returns>The next interface in the fluent chain for further configuration.</returns>
    TNext Throws<T1, T2, T3, T4, T5, T6, TException>(Func<T1, T2, T3, T4, T5, T6, TException> exceptionFactory)
        where TException : Exception;

    /// <summary>
    /// Configures the setup to throw an exception created by a function that takes seven method arguments.
    /// </summary>
    /// <typeparam name="T1">The type of the first method argument.</typeparam>
    /// <typeparam name="T2">The type of the second method argument.</typeparam>
    /// <typeparam name="T3">The type of the third method argument.</typeparam>
    /// <typeparam name="T4">The type of the fourth method argument.</typeparam>
    /// <typeparam name="T5">The type of the fifth method argument.</typeparam>
    /// <typeparam name="T6">The type of the sixth method argument.</typeparam>
    /// <typeparam name="T7">The type of the seventh method argument.</typeparam>
    /// <typeparam name="TException">The type of exception to throw. Must inherit from <see cref="Exception"/>.</typeparam>
    /// <param name="exceptionFactory">A function that takes the method arguments and creates the exception to throw.</param>
    /// <returns>The next interface in the fluent chain for further configuration.</returns>
    TNext Throws<T1, T2, T3, T4, T5, T6, T7, TException>(Func<T1, T2, T3, T4, T5, T6, T7, TException> exceptionFactory)
        where TException : Exception;

    /// <summary>
    /// Configures the setup to throw an exception created by a function that takes eight method arguments.
    /// </summary>
    /// <typeparam name="T1">The type of the first method argument.</typeparam>
    /// <typeparam name="T2">The type of the second method argument.</typeparam>
    /// <typeparam name="T3">The type of the third method argument.</typeparam>
    /// <typeparam name="T4">The type of the fourth method argument.</typeparam>
    /// <typeparam name="T5">The type of the fifth method argument.</typeparam>
    /// <typeparam name="T6">The type of the sixth method argument.</typeparam>
    /// <typeparam name="T7">The type of the seventh method argument.</typeparam>
    /// <typeparam name="T8">The type of the eighth method argument.</typeparam>
    /// <typeparam name="TException">The type of exception to throw. Must inherit from <see cref="Exception"/>.</typeparam>
    /// <param name="exceptionFactory">A function that takes the method arguments and creates the exception to throw.</param>
    /// <returns>The next interface in the fluent chain for further configuration.</returns>
    TNext Throws<T1, T2, T3, T4, T5, T6, T7, T8, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, TException> exceptionFactory)
        where TException : Exception;

    /// <summary>
    /// Configures the setup to throw an exception created by a function that takes nine method arguments.
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
    /// <typeparam name="TException">The type of exception to throw. Must inherit from <see cref="Exception"/>.</typeparam>
    /// <param name="exceptionFactory">A function that takes the method arguments and creates the exception to throw.</param>
    /// <returns>The next interface in the fluent chain for further configuration.</returns>
    TNext Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TException> exceptionFactory)
        where TException : Exception;

    /// <summary>
    /// Configures the setup to throw an exception created by a function that takes ten method arguments.
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
    /// <typeparam name="TException">The type of exception to throw. Must inherit from <see cref="Exception"/>.</typeparam>
    /// <param name="exceptionFactory">A function that takes the method arguments and creates the exception to throw.</param>
    /// <returns>The next interface in the fluent chain for further configuration.</returns>
    TNext Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TException> exceptionFactory)
        where TException : Exception;

    /// <summary>
    /// Configures the setup to throw an exception created by a function that takes eleven method arguments.
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
    /// <typeparam name="TException">The type of exception to throw. Must inherit from <see cref="Exception"/>.</typeparam>
    /// <param name="exceptionFactory">A function that takes the method arguments and creates the exception to throw.</param>
    /// <returns>The next interface in the fluent chain for further configuration.</returns>
    TNext Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TException> exceptionFactory)
        where TException : Exception;

    /// <summary>
    /// Configures the setup to throw an exception created by a function that takes twelve method arguments.
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
    /// <typeparam name="TException">The type of exception to throw. Must inherit from <see cref="Exception"/>.</typeparam>
    /// <param name="exceptionFactory">A function that takes the method arguments and creates the exception to throw.</param>
    /// <returns>The next interface in the fluent chain for further configuration.</returns>
    TNext Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TException> exceptionFactory)
        where TException : Exception;

    /// <summary>
    /// Configures the setup to throw an exception created by a function that takes thirteen method arguments.
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
    /// <typeparam name="TException">The type of exception to throw. Must inherit from <see cref="Exception"/>.</typeparam>
    /// <param name="exceptionFactory">A function that takes the method arguments and creates the exception to throw.</param>
    /// <returns>The next interface in the fluent chain for further configuration.</returns>
    TNext Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TException> exceptionFactory)
        where TException : Exception;

    /// <summary>
    /// Configures the setup to throw an exception created by a function that takes fourteen method arguments.
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
    /// <typeparam name="TException">The type of exception to throw. Must inherit from <see cref="Exception"/>.</typeparam>
    /// <param name="exceptionFactory">A function that takes the method arguments and creates the exception to throw.</param>
    /// <returns>The next interface in the fluent chain for further configuration.</returns>
    TNext Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TException> exceptionFactory)
        where TException : Exception;

    /// <summary>
    /// Configures the setup to throw an exception created by a function that takes fifteen method arguments.
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
    /// <typeparam name="TException">The type of exception to throw. Must inherit from <see cref="Exception"/>.</typeparam>
    /// <param name="exceptionFactory">A function that takes the method arguments and creates the exception to throw.</param>
    /// <returns>The next interface in the fluent chain for further configuration.</returns>
    TNext Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TException> exceptionFactory)
        where TException : Exception;

    /// <summary>
    /// Configures the setup to throw an exception created by a function that takes sixteen method arguments.
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
    /// <typeparam name="TException">The type of exception to throw. Must inherit from <see cref="Exception"/>.</typeparam>
    /// <param name="exceptionFactory">A function that takes the method arguments and creates the exception to throw.</param>
    /// <returns>The next interface in the fluent chain for further configuration.</returns>
    TNext Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TException> exceptionFactory)
        where TException : Exception;
}
