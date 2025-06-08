namespace Mimic;

[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface ISequenceSetup : IThrows<ISequenceSetup>, ISequenceDelayable, IExpected, IFluent
{
    ISequenceSetup Proceed();

    ISequenceSetup Next();
}
