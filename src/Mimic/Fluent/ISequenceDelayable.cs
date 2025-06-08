namespace Mimic;

[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface ISequenceDelayable : IFluent
{
    IExpected WithDelay(TimeSpan delay);

    IExpected WithDelay(Func<int, TimeSpan> delayFunction);
}
