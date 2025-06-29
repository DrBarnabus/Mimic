using Mimic.Setup.ArgumentMatchers;

// ReSharper disable ConvertTypeCheckToNullCheck

namespace Mimic;

/// <summary>
/// Provides argument matchers for use in mock setups and verifications.
/// </summary>
[PublicAPI]
public static class Arg
{
    /// <summary>
    /// Matches any value of type <typeparamref name="TValue"/>, including null.
    /// </summary>
    /// <typeparam name="TValue">The type of the argument to match.</typeparam>
    /// <returns>An argument matcher that matches any value of the specified type.</returns>
    public static TValue Any<TValue>() => typeof(TValue).IsOrContainsGenericMatcher()
            ? ArgumentMatcher.Create<TValue>((argument, parameterType) => argument == null || parameterType.IsInstanceOfType(argument))!
            : ArgumentMatcher.Create<TValue>(argument => argument == null || argument is TValue)!;

    /// <summary>
    /// Matches any non-null value of type <typeparamref name="TValue"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the argument to match.</typeparam>
    /// <returns>An argument matcher that matches any non-null value of the specified type.</returns>
    public static TValue AnyNotNull<TValue>() => typeof(TValue).IsOrContainsGenericMatcher()
            ? ArgumentMatcher.Create<TValue>((argument, parameterType) => argument != null && parameterType.IsInstanceOfType(argument))!
            : ArgumentMatcher.Create<TValue>(argument => argument is TValue)!;

    /// <summary>
    /// Matches an argument that is equal to the specified value using the default equality comparer.
    /// </summary>
    /// <typeparam name="TValue">The type of the argument to match.</typeparam>
    /// <param name="value">The value to match against.</param>
    /// <returns>An argument matcher that matches the specified value.</returns>
    /// <remarks>
    /// This overload does not support generic matchers due to the strongly typed value.
    /// For generic matchers, use the overload with <c>Expression&lt;Func&lt;object, Type, bool&gt;&gt; match</c> instead.
    /// </remarks>
    public static TValue Is<TValue>(TValue value)
    {
        Guard.Assert(!typeof(TValue).IsOrContainsGenericMatcher(), "This overload does not support generic matchers due to the strongly typed value. Please use the overload with `Expression<Func<object, Type, bool>> predicate` instead.");

        return ArgumentMatcher.Create<TValue>(argument => Equals(argument, value))!;
    }

    /// <summary>
    /// Matches an argument that is equal to the specified value using the provided equality comparer.
    /// </summary>
    /// <typeparam name="TValue">The type of the argument to match.</typeparam>
    /// <param name="value">The value to match against.</param>
    /// <param name="comparer">The equality comparer to use for comparison.</param>
    /// <returns>An argument matcher that matches the specified value using the provided comparer.</returns>
    /// <remarks>
    /// This overload does not support generic matchers due to the strongly typed value.
    /// For generic matchers, use the overload with <c>Expression&lt;Func&lt;object, Type, bool&gt;&gt; match</c> instead.
    /// </remarks>
    public static TValue Is<TValue>(TValue value, IEqualityComparer<TValue> comparer)
    {
        Guard.Assert(!typeof(TValue).IsOrContainsGenericMatcher(), "This overload does not support generic matchers due to the strongly typed value. Please use the overload with `Expression<Func<object, Type, bool>> predicate` instead.");

        return ArgumentMatcher.Create<TValue>(argument => comparer.Equals(argument, value))!;
    }

    /// <summary>
    /// Matches an argument that satisfies the specified predicate expression.
    /// </summary>
    /// <typeparam name="TValue">The type of the argument to match.</typeparam>
    /// <param name="match">The predicate expression that the argument must satisfy.</param>
    /// <returns>An argument matcher that matches arguments satisfying the predicate.</returns>
    /// <remarks>
    /// This overload does not support generic matchers due to the strongly typed value.
    /// For generic matchers, use the overload with <c>Expression&lt;Func&lt;object, Type, bool&gt;&gt; match</c> instead.
    /// </remarks>
    public static TValue Is<TValue>(Expression<Func<TValue, bool>> match)
    {
        Guard.Assert(!typeof(TValue).IsOrContainsGenericMatcher(), "This overload does not support generic matchers due to the strongly typed value. Please use the overload with `Expression<Func<object, Type, bool>> predicate` instead.");

        return ArgumentMatcher.Create<TValue>(argument => match.Compile().Invoke(argument!))!;
    }

    /// <summary>
    /// Matches an argument that satisfies the specified predicate expression with type information.
    /// This overload supports generic matchers.
    /// </summary>
    /// <typeparam name="TValue">The type of the argument to match.</typeparam>
    /// <param name="match">The predicate expression that takes the argument and its parameter type.</param>
    /// <returns>An argument matcher that matches arguments satisfying the predicate.</returns>
    public static TValue Is<TValue>(Expression<Func<object, Type, bool>> match) =>
        ArgumentMatcher.Create<TValue>((argument, parameterType) => match.Compile().Invoke(argument!, parameterType))!;

    /// <summary>
    /// Matches an argument contained in the specified collection of values.
    /// </summary>
    /// <typeparam name="TValue">The type of the argument to match.</typeparam>
    /// <param name="values">The collection of values to check against.</param>
    /// <returns>An argument matcher that matches values contained in the collection.</returns>
    public static TValue In<TValue>(IEnumerable<TValue> values) =>
        ArgumentMatcher.Create<TValue>(argument => values.Contains(argument))!;

    /// <summary>
    /// Matches an argument contained in the specified collection of values using the provided equality comparer.
    /// </summary>
    /// <typeparam name="TValue">The type of the argument to match.</typeparam>
    /// <param name="values">The collection of values to check against.</param>
    /// <param name="comparer">The equality comparer to use for comparison.</param>
    /// <returns>An argument matcher that matches values contained in the collection using the comparer.</returns>
    public static TValue In<TValue>(IEnumerable<TValue> values, IEqualityComparer<TValue> comparer) =>
        ArgumentMatcher.Create<TValue>(argument => values.Contains(argument, comparer!))!;

    /// <summary>
    /// Matches an argument contained in the specified array of values.
    /// </summary>
    /// <typeparam name="TValue">The type of the argument to match.</typeparam>
    /// <param name="values">The array of values to check against.</param>
    /// <returns>An argument matcher that matches values contained in the array.</returns>
    public static TValue In<TValue>(params TValue[] values) =>
        ArgumentMatcher.Create<TValue>(argument => values.Contains(argument))!;

    /// <summary>
    /// Matches an argument not contained in the specified collection of values.
    /// </summary>
    /// <typeparam name="TValue">The type of the argument to match.</typeparam>
    /// <param name="values">The collection of values to check against.</param>
    /// <returns>An argument matcher that matches values not contained in the collection.</returns>
    public static TValue NotIn<TValue>(IEnumerable<TValue> values) =>
        ArgumentMatcher.Create<TValue>(argument => !values.Contains(argument))!;

    /// <summary>
    /// Matches an argument not contained in the specified collection of values using the provided equality comparer.
    /// </summary>
    /// <typeparam name="TValue">The type of the argument to match.</typeparam>
    /// <param name="values">The collection of values to check against.</param>
    /// <param name="comparer">The equality comparer to use for comparison.</param>
    /// <returns>An argument matcher that matches values not contained in the collection using the comparer.</returns>
    public static TValue NotIn<TValue>(IEnumerable<TValue> values, IEqualityComparer<TValue> comparer) =>
        ArgumentMatcher.Create<TValue>(argument => !values.Contains(argument, comparer!))!;

    /// <summary>
    /// Matches an argument not contained in the specified array of values.
    /// </summary>
    /// <typeparam name="TValue">The type of the argument to match.</typeparam>
    /// <param name="values">The array of values to check against.</param>
    /// <returns>An argument matcher that matches values not contained in the array.</returns>
    public static TValue NotIn<TValue>(params TValue[] values) =>
        ArgumentMatcher.Create<TValue>(argument => !values.Contains(argument))!;

    /// <summary>
    /// Provides reference argument matchers for ref parameters.
    /// </summary>
    /// <typeparam name="TValue">The type of the reference argument.</typeparam>
    public static class Ref<TValue>
    {
        /// <summary>
        /// Matches any reference argument of type <typeparamref name="TValue"/>.
        /// </summary>
        #pragma warning disable CA2211
        public static TValue Any = default!;
        #pragma warning restore CA2211
    }
}
