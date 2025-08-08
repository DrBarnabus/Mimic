namespace Mimic;

/// <summary>
/// Provides conditional setup functionality for configuring mock behaviour based on specific conditions.
/// This interface allows you to set up method calls, property getters, and property setters when certain conditions are met.
/// </summary>
/// <typeparam name="TMimic">The type being mocked. Must be a reference type.</typeparam>
/// <remarks>
/// This interface is marked with <see cref="EditorBrowsableAttribute"/> to hide it from IntelliSense,
/// as it's part of the internal fluent API chain. Users typically access conditional setup functionality
/// through method chaining after specifying conditions on the mock.
/// </remarks>
[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IConditionalSetup<TMimic>
    where TMimic : class
{
    /// <summary>
    /// Sets up a mock method or property setter that does not return a value.
    /// </summary>
    /// <param name="expression">An expression that specifies the method or property setter to set up.</param>
    /// <returns>A <see cref="ISetup{TMimic}"/> that allows further configuration of the mock behavior.</returns>
    ISetup<TMimic> Setup(Expression<Action<TMimic>> expression);

    /// <summary>
    /// Sets up a mock method or property getter that returns a value of type <typeparamref name="TResult"/>.
    /// </summary>
    /// <typeparam name="TResult">The type of value returned by the method or property.</typeparam>
    /// <param name="expression">An expression that specifies the method or property getter to set up.</param>
    /// <returns>A <see cref="ISetup{TMimic, TResult}"/> that allows further configuration of the mock behaviour including return values.</returns>
    ISetup<TMimic, TResult> Setup<TResult>(Expression<Func<TMimic, TResult>> expression);

    /// <summary>
    /// Sets up a mock property getter specifically.
    /// </summary>
    /// <typeparam name="TProperty">The type of the property.</typeparam>
    /// <param name="expression">An expression that specifies the property getter to set up.</param>
    /// <returns>A <see cref="IGetterSetup{TMimic, TProperty}"/> that allows configuration specific to property getters.</returns>
    IGetterSetup<TMimic, TProperty> SetupGet<TProperty>(Expression<Func<TMimic, TProperty>> expression);

    /// <summary>
    /// Sets up a mock property setter without specifying the property type.
    /// </summary>
    /// <param name="setterExpression">An action that specifies the property setter to set up.</param>
    /// <returns>A <see cref="ISetup{TMimic}"/> that allows further configuration of the mock behaviour.</returns>
    ISetup<TMimic> SetupSet(Action<TMimic> setterExpression);

    /// <summary>
    /// Sets up a mock property setter with a specific property type.
    /// </summary>
    /// <typeparam name="TProperty">The type of the property being set.</typeparam>
    /// <param name="setterExpression">An action that specifies the property setter to set up.</param>
    /// <returns>A <see cref="ISetterSetup{TMimic, TProperty}"/> that allows configuration specific to property setters.</returns>
    ISetterSetup<TMimic, TProperty> SetupSet<TProperty>(Action<TMimic> setterExpression);
}
