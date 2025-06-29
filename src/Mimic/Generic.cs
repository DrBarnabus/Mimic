namespace Mimic;

/// <summary>
/// Provides generic type matchers for use with mock setups and argument matching.
/// These matchers work in conjunction with the <see cref="Arg"/> class to enable
/// flexible type matching in generic method scenarios.
/// </summary>
[PublicAPI]
public sealed class Generic
{
    /// <summary>
    /// A generic type matcher that matches any type without restrictions.
    /// This matcher accepts all types, including value types, reference types,
    /// interfaces, and abstract classes.
    /// </summary>
    /// <remarks>
    /// Use this matcher when you want to accept any generic type argument in your mock setup.
    /// This is equivalent to an unconstrained generic type parameter.
    /// </remarks>
    /// <example>
    /// <code>
    /// // Matches any generic method call regardless of the type argument
    /// mimic.Setup(m => m.GenericMethod&lt;Generic.AnyType&gt;()).Returns(true);
    /// </code>
    /// </example>
    public sealed class AnyType : IGenericMatcher
    {
        /// <summary>
        /// Determines whether the specified generic type matches this matcher.
        /// </summary>
        /// <param name="genericType">The generic type to evaluate.</param>
        /// <returns>Always returns <c>true</c> since this matcher accepts any type.</returns>
        public bool Matches(Type genericType) => true;
    }

    /// <summary>
    /// A generic type matcher that matches only reference types.
    /// This includes classes, interfaces, delegates, and arrays, but excludes value types like structs and enums.
    /// </summary>
    /// <remarks>
    /// Use this matcher when you want to restrict generic type arguments to reference types only.
    /// This is equivalent to a generic type parameter with a <c>class</c> constraint.
    /// </remarks>
    /// <example>
    /// <code>
    /// // Matches generic method calls only when the type argument is a reference type
    /// mimic.Setup(m => m.GenericMethod&lt;Generic.AnyReferenceType&gt;()).Returns("reference type");
    /// </code>
    /// </example>
    public sealed class AnyReferenceType : IGenericMatcher
    {
        /// <summary>
        /// Determines whether the specified generic type matches this matcher.
        /// </summary>
        /// <param name="genericType">The generic type to evaluate.</param>
        /// <returns><c>true</c> if the type is a reference type; otherwise, <c>false</c>.</returns>
        public bool Matches(Type genericType) => !genericType.IsValueType;
    }

    /// <summary>
    /// A generic type matcher that matches types that are assignable from a specified type <typeparamref name="T"/>.
    /// This includes the type <typeparamref name="T"/> itself and any types that inherit from or implement <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The base type or interface that the generic type argument must be assignable from.</typeparam>
    /// <remarks>
    /// Use this matcher when you want to restrict generic type arguments to types that have a specific inheritance relationship.
    /// This is useful for matching generic methods that should only work with certain type hierarchies.
    /// </remarks>
    /// <example>
    /// <code>
    /// // Matches generic method calls only when the type argument implements IDisposable
    /// mimic.Setup(m => m.GenericMethod&lt;Generic.AssignableFromType&lt;IDisposable&gt;&gt;()).Returns(true);
    ///
    /// // Matches generic method calls only when the type argument inherits from Exception
    /// mimic.Setup(m => m.HandleException&lt;Generic.AssignableFromType&lt;Exception&gt;&gt;()).Returns("handled");
    /// </code>
    /// </example>
    public sealed class AssignableFromType<T> : IGenericMatcher
    {
        /// <summary>
        /// Determines whether the specified generic type matches this matcher.
        /// </summary>
        /// <param name="genericType">The generic type to evaluate.</param>
        /// <returns><c>true</c> if <typeparamref name="T"/> is assignable from the generic type; otherwise, <c>false</c>.</returns>
        public bool Matches(Type genericType) => typeof(T).IsAssignableFrom(genericType);
    }

    /// <summary>
    /// A generic type matcher that matches only value types.
    /// This includes structs, enums, and primitive types, but excludes reference types like classes and interfaces.
    /// </summary>
    /// <remarks>
    /// Use this matcher when you want to restrict generic type arguments to value types only.
    /// This is equivalent to a generic type parameter with a <c>struct</c> constraint.
    /// </remarks>
    /// <example>
    /// <code>
    /// // Matches generic method calls only when the type argument is a value type
    /// mimic.Setup(m => m.GenericMethod&lt;Generic.AnyValueType&gt;()).Returns(42);
    /// </code>
    /// </example>
    public readonly struct AnyValueType : IGenericMatcher
    {
        /// <summary>
        /// Determines whether the specified generic type matches this matcher.
        /// </summary>
        /// <param name="genericType">The generic type to evaluate.</param>
        /// <returns><c>true</c> if the type is a value type; otherwise, <c>false</c>.</returns>
        public bool Matches(Type genericType) => genericType.IsValueType;
    }
}
