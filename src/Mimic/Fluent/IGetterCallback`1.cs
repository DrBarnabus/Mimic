namespace Mimic;

/// <summary>
/// Provides callback functionality for property getter mock setups.
/// This interface allows you to specify actions to execute when a mocked property getter is accessed.
/// </summary>
/// <typeparam name="TProperty">The type of the property being mocked.</typeparam>
/// <remarks>
/// This interface is marked with <see cref="EditorBrowsableAttribute"/> to hide it from IntelliSense,
/// as it's part of the internal fluent API chain. Users typically access this functionality
/// through method chaining on property getter setup expressions.
/// </remarks>
[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IGetterCallback<in TProperty> : IFluent
{
    /// <summary>
    /// Specifies an action callback to execute when the mocked property getter is accessed.
    /// </summary>
    /// <param name="callback">The action to execute when the property getter is called.</param>
    /// <returns>A <see cref="IGetterCallbackResult{TProperty}"/> that allows further configuration of the property getter setup.</returns>
    IGetterCallbackResult<TProperty> Callback(Action callback);
}
