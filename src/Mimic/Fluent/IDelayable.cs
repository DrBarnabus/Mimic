namespace Mimic;

[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IDelayable : IFluent
{
    IDelayableResult WithDelay(TimeSpan delay);

    IDelayableResult WithDelay(Func<int, TimeSpan> delayFunction);
}
