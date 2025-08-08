namespace Mimic;

/// <summary>
/// Defines the setup interface for configuring expectations on property setters of a mimic object.
/// This interface provides comprehensive configuration options for property setter operations.
/// </summary>
/// <typeparam name="TMimic">The type of the mimic object being configured.</typeparam>
/// <typeparam name="TProperty">The type of the property being set.</typeparam>
/// <remarks>
/// This interface is marked with <see cref="EditorBrowsableAttribute"/> to hide it from IntelliSense,
/// as it serves as part of the fluent API chain for property setter setups. It inherits from multiple interfaces
/// to provide comprehensive configuration options specifically tailored for property setter scenarios:
/// <list type="bullet">
/// <item><description><see cref="ISetterCallback{TProperty}"/> - For defining callback actions when the property setter is invoked</description></item>
/// <item><description><see cref="IDelayable"/> - For introducing delays before the setter executes</description></item>
/// <item><description><see cref="ILimitable"/> - For limiting the number of times the setup can be matched</description></item>
/// <item><description><see cref="IExpected"/> - For defining verification expectations</description></item>
/// </list>
/// This interface is typically reached when setting up expectations for property setters using expressions
/// like <c>SetupSet(() => obj.Property = value)</c>. It provides specialised functionality for property
/// setting scenarios where you need to observe, validate, or control the assignment of property values.
/// Unlike method setups, property setter setups focus on the value being assigned rather than method parameters.
/// </remarks>
[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface ISetterSetup<TMimic, out TProperty> : ISetterCallback<TProperty>, IDelayable, ILimitable, IExpected, IFluent
    where TMimic : class;
