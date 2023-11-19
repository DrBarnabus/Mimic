namespace Mimic.Setup.Fluent;

[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface ISequenceSetup : IThrows<ISequenceSetup>, IVerifiable, IFluent
{
    ISequenceSetup Next();
}
