namespace Mimic.Setup.Fluent;

internal class VoidSetup<TMimic> : SetupBase, ISetup<TMimic>
    where TMimic : class
{
    public VoidSetup(MethodCallSetup setup) : base(setup)
    {
    }

    public ISequenceSetup AsSequence() => new SequenceSetup(Setup);
}
