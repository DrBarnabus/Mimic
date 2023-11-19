namespace Mimic.Setup.Fluent.Implementations;

internal sealed class SequenceSetup : SequenceSetupBase<ISequenceSetup>, ISequenceSetup
{
    protected override ISequenceSetup This => this;

    public SequenceSetup(MethodCallSetup setup) : base(setup)
    {
    }

    public ISequenceSetup Next()
    {
        Setup.AddNoOpBehaviour();
        return this;
    }
}
