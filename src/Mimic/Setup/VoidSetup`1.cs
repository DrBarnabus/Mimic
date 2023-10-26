using Mimic.Setup.Fluent;

namespace Mimic.Setup;

internal class VoidSetup<TMimic> : SetupBase, ISetup<TMimic>
    where TMimic : class
{
    public VoidSetup(MethodCallSetup setup) : base(setup)
    {
    }
}
