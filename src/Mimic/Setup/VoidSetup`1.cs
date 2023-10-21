namespace Mimic.Setup;

internal class VoidSetup<T> : SetupBase, ISetup<T>
    where T : class
{
    public VoidSetup(MethodCallSetup setup) : base(setup)
    {
    }
}
