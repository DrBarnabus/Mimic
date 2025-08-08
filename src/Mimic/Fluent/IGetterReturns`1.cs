namespace Mimic;

/// <summary>
/// Provides return value configuration functionality for property getter mock setups.
/// This interface allows you to specify what value should be returned when a mocked property getter is accessed.
/// </summary>
/// <typeparam name="TProperty">The type of the property being mocked.</typeparam>
/// <remarks>
/// This interface is marked with <see cref="EditorBrowsableAttribute"/> to hide it from IntelliSense,
/// as it's part of the internal fluent API chain. Users typically access this functionality
/// through method chaining on property getter setup expressions.
/// </remarks>
[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IGetterReturns<in TProperty> : IFluent
{
    /// <summary>
    /// Specifies the value to return when the mocked property getter is accessed.
    /// </summary>
    /// <param name="value">The value to return when the property getter is called.</param>
    /// <returns>A <see cref="IReturnsResult"/> that allows further configuration of the mock setup.</returns>
    IReturnsResult Returns(TProperty? value);

    /// <summary>
    /// Specifies a function to compute the value to return when the mocked property getter is accessed.
    /// </summary>
    /// <param name="valueFunction">A function that computes the value to return when the property getter is called.</param>
    /// <returns>A <see cref="IReturnsResult"/> that allows further configuration of the mock setup.</returns>
    /// <remarks>
    /// The function is executed each time the property getter is accessed, allowing for dynamic value computation.
    /// </remarks>
    IReturnsResult Returns(Func<TProperty?> valueFunction);
}
