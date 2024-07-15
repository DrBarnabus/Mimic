using Mimic.Setup;

namespace Mimic;

internal interface IMimic
{
    bool Strict { get; }

    object Object { get; }

    SetupCollection Setups { get; }

    IReadOnlyList<Invocation> Invocations { get; }

    void VerifyReceived(Predicate<SetupBase> predicate, HashSet<IMimic> verified);
}
