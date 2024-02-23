namespace Mimic.Setup.Fluent;

internal class Setup<TMimic> : SetupBase, ISetup<TMimic>, IProceedResult
    where TMimic : class
{
    public Setup(MethodCallSetup setup) : base(setup)
    {
    }

    public IProceedResult Proceed()
    {
        Setup.SetProceedBehaviour();
        return this;
    }

    public ISequenceSetup AsSequence() => new SequenceSetup(Setup);
}
