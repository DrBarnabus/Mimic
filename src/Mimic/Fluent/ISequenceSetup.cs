namespace Mimic;

[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface ISequenceSetup : IThrows<ISequenceSetup>, IExpected, IFluent
{
    ISequenceSetup Proceed();

    ISequenceSetup Next();
}
