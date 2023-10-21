using Mimic.Core;

namespace Mimic.Setup;

internal class SetupBase
{
    public SetupBase(MethodCallSetup setup)
    {
        Guard.NotNull(setup);

        Setup = setup;
    }

    public MethodCallSetup Setup { get; }

    public override string ToString()
    {
        return Setup.Expression.ToString();
    }
}
