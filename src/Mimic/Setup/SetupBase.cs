using Mimic.Core;

namespace Mimic.Setup;

internal class SetupBase
{
    protected SetupBase(MethodCallSetup setup)
    {
        Guard.NotNull(setup);

        Setup = setup;
    }

    protected MethodCallSetup Setup { get; }

    public override string ToString()
    {
        return Setup.Expression.ToString();
    }
}
