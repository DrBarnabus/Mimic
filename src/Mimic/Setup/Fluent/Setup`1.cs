namespace Mimic.Setup.Fluent;

internal class Setup<TMimic> : SetupBase, ISetup<TMimic>
    where TMimic : class
{
    public Setup(MethodCallSetup setup) : base(setup)
    {
    }

    public ISequenceSetup AsSequence() => new SequenceSetup(Setup);
}
