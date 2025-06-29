namespace Mimic;

/// <summary>
/// Defines a contract for matching generic types in mock setups and argument matching scenarios.
/// Implementing types can create custom generic type matchers that work with the Mimic framework
/// to provide flexible type matching for generic method calls.
/// </summary>
/// <remarks>
/// <para>
/// This interface is used by the Mimic framework to determine whether a generic type argument
/// matches specific criteria during mock setup and verification. Implementations of this interface
/// can be used as type arguments in generic method setups to create flexible matching rules.
/// </para>
/// <para>
/// The built-in <see cref="Generic"/> class provides several common implementations of this interface,
/// including matchers for any type, reference types, value types, and types assignable from a base type.
/// Custom implementations can extend this functionality for specific matching requirements.
/// </para>
/// </remarks>
/// <example>
/// <para>Creating a custom matcher:</para>
/// <code>
/// public class NumericTypeMatcher : IGenericMatcher
/// {
///     public bool Matches(Type genericType)
///     {
///         return genericType == typeof(int) || genericType == typeof(double) ||
///                genericType == typeof(decimal) || genericType == typeof(float);
///     }
/// }
/// </code>
/// <para>Using the custom matcher in mock setups (same as built-in Generic matchers):</para>
/// <code>
/// // Setup a generic method to match only numeric types
/// mimic.Setup(m => m.ProcessValue&lt;NumericTypeMatcher&gt;(It.IsAny&lt;NumericTypeMatcher&gt;()))
///      .Returns(true);
///
/// // This will match calls like:
/// // mock.ProcessValue&lt;int&gt;(42);
/// // mock.ProcessValue&lt;double&gt;(3.14);
/// // But not:
/// // mock.ProcessValue&lt;string&gt;("hello");
/// </code>
/// <para>Combining with the built-in Generic matchers:</para>
/// <code>
/// // Mix custom and built-in matchers
/// mimic.Setup(m => m.Convert&lt;Generic.AnyReferenceType, NumericTypeMatcher&gt;())
///      .Returns("converted");
/// </code>
/// </example>
/// <seealso cref="Generic"/>
/// <seealso cref="Generic.AnyType"/>
/// <seealso cref="Generic.AnyReferenceType"/>
/// <seealso cref="Generic.AnyValueType"/>
/// <seealso cref="Generic.AssignableFromType{T}"/>
[PublicAPI]
public interface IGenericMatcher
{
    /// <summary>
    /// Determines whether the specified generic type matches the criteria defined by this matcher.
    /// </summary>
    /// <param name="genericType">The generic type argument to evaluate for matching.</param>
    /// <returns>
    /// <c>true</c> if the specified <paramref name="genericType"/> satisfies the matching criteria;
    /// otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// This method is called by the Mimic framework during mock setup and verification to determine
    /// if a generic type argument matches the expectations defined by the implementing matcher.
    /// The implementation should be efficient as it may be called frequently during mock operations.
    /// </remarks>
    /// <example>
    /// <code>
    /// public bool Matches(Type genericType)
    /// {
    ///     // Example: Match only types that implement IComparable
    ///     return typeof(IComparable).IsAssignableFrom(genericType);
    /// }
    /// </code>
    /// </example>
    bool Matches(Type genericType);
}
