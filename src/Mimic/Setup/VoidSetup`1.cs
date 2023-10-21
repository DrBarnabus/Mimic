using Mimic.Setup.Fluent;

namespace Mimic.Setup;

internal sealed class VoidSetup<T> : SetupBase, ISetup<T>
    where T : class
{
    public VoidSetup(MethodCallSetup setup) : base(setup)
    {
    }
}
