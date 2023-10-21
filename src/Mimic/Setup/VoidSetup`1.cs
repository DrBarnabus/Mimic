using Mimic.Setup.Fluent;

namespace Mimic.Setup;

internal sealed class VoidSetup<TMimic> : SetupBase, ISetup<TMimic>
    where TMimic : class
{
    public VoidSetup(MethodCallSetup setup) : base(setup)
    {
    }
}
