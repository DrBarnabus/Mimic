namespace Mimic;

/// <summary>
/// Represents the result interface after configuring a return value in a mimic setup.
/// This interface provides additional configuration options that can be applied after defining what a method should return.
/// </summary>
/// <remarks>
/// This interface is marked with <see cref="EditorBrowsableAttribute"/> to hide it from IntelliSense,
/// as it serves as part of the fluent API chain. It inherits from multiple interfaces to provide
/// additional configuration capabilities after a return value has been specified:
/// <list type="bullet">
/// <item><description><see cref="ICallback"/> - For defining callback actions when the method is invoked</description></item>
/// <item><description><see cref="IDelayable"/> - For introducing delays before method execution</description></item>
/// <item><description><see cref="ILimitable"/> - For limiting the number of times the setup can be matched</description></item>
/// <item><description><see cref="IExpected"/> - For defining verification expectations</description></item>
/// </list>
/// This interface is typically reached after calling a <c>Returns</c> method on a setup, allowing you to
/// further customize the behavior with callbacks, delays, occurrence limits, or verification requirements.
/// It serves as the continuation point in the fluent API chain for return-value-configured setups.
/// </remarks>
[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IReturnsResult : ICallback, IDelayable, ILimitable, IExpected, IFluent;
