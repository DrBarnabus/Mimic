using Mimic.Setup;
using Mimic.Setup.Fluent;
using SetupBase = Mimic.Setup.SetupBase;

namespace Mimic;

[PublicAPI]
public sealed partial class Mimic<T> : IMimic
    where T : class
{
    // ReSharper disable once StaticMemberInGenericType
    private static int _instanceCounter;

    private readonly SetupCollection _setups = [];
    private readonly List<Invocation> _invocations = [];

    private object[]? _constructorArguments;
    private T? _object;

    public string Name { get; init; }

    public bool Strict { get; init; } = true;

    public object[]? ConstructorArguments
    {
        get => _constructorArguments;
        init
        {
            if (typeof(T).IsInterface && value is { Length: >0 })
                throw new ArgumentException($"{nameof(ConstructorArguments)} should not be set when mimicking an interface.");

            _constructorArguments = value;
        }
    }

    public T Object => GetOrInitializeObject();

    internal IReadOnlyList<Invocation> Invocations
    {
        get { lock (_invocations) return _invocations.ToArray(); }
    }

    public Mimic()
    {
        if (!typeof(T).CanBeMimicked())
            throw MimicException.TypeCannotBeMimicked(typeof(T));

        int instanceNumber = Interlocked.Increment(ref _instanceCounter);
        Name = $"Mimic<{TypeNameFormatter.GetFormattedName(typeof(T))}>:{instanceNumber}";
    }

    public Mimic(bool strict) : this() => Strict = strict;

    public static Mimic<T> FromObject(T objectInstance)
    {
        if (objectInstance is not IMimicked<T> mimicked)
            throw MimicException.ObjectNotCreatedByMimic();

        return mimicked.Mimic;
    }

    public IConditionalSetup<T> When(Func<bool> condition) => new ConditionalSetup<T>(this, condition);

    public override string ToString() => Name;

    private T GetOrInitializeObject()
    {
        return _object ??= (T)ProxyGenerator.Instance.GenerateProxy(
            typeof(T),
            [typeof(IMimicked), typeof(IMimicked<T>)],
            _constructorArguments ?? [],
            this);
    }

    #region IMimic

    object IMimic.Object => Object;

    SetupCollection IMimic.Setups => _setups;

    IReadOnlyList<Invocation> IMimic.Invocations => Invocations;

    void IMimic.VerifyReceived(Predicate<SetupBase> predicate, HashSet<IMimic> verified) => VerifyReceived(predicate, verified);

    #endregion
}
