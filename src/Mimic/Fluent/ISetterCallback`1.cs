namespace Mimic;

/// <summary>
/// Provides callback functionality specifically for property setter configurations in a mimic setup.
/// This interface allows defining actions that will be executed when a property setter is invoked.
/// </summary>
/// <typeparam name="TProperty">The type of the property being set.</typeparam>
/// <remarks>
/// This interface is marked with <see cref="EditorBrowsableAttribute"/> to hide it from IntelliSense,
/// as it serves as part of the fluent API chain for property setter setups. It inherits from multiple interfaces
/// to provide comprehensive configuration options for property setter scenarios:
/// <list type="bullet">
/// <item><description><see cref="IDelayable"/> - For introducing delays before the setter executes</description></item>
/// <item><description><see cref="ILimitable"/> - For limiting the number of times the setup can be matched</description></item>
/// <item><description><see cref="IExpected"/> - For defining verification expectations</description></item>
/// </list>
/// This interface is specifically designed for property setter operations and provides access to
/// the value being assigned to the property through callback mechanisms. It's typically used when
/// you need to observe or react to property value changes in a controlled testing environment.
/// </remarks>
[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface ISetterCallback<out TProperty> : IDelayable, ILimitable, IExpected, IFluent
{
    /// <summary>
    /// Configures a callback action that will be executed when the property setter is invoked.
    /// The callback receives the value being assigned to the property.
    /// </summary>
    /// <param name="callback">
    /// An action that will be called when the property setter is invoked.
    /// The action receives the value being assigned to the property as its parameter.
    /// </param>
    /// <returns>
    /// A <see cref="ICallbackResult"/> instance that allows for further configuration
    /// after the callback has been defined.
    /// </returns>
    /// <remarks>
    /// This method allows you to define custom behavior that executes when a property is set.
    /// The callback has access to the value being assigned, enabling scenarios such as:
    /// <list type="bullet">
    /// <item><description>Validation of property values during testing</description></item>
    /// <item><description>Logging or auditing property changes</description></item>
    /// <item><description>Triggering side effects based on property values</description></item>
    /// <item><description>Capturing property values for later assertion</description></item>
    /// </list>
    /// The callback is executed synchronously as part of the property setter operation.
    /// </remarks>
    ICallbackResult Callback(Action<TProperty> callback);
}
