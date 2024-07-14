using Mimic.Setup.Fluent;

namespace Mimic;

[PublicAPI]
public sealed partial class Mimic<T> : IMimic
    where T : class
{
    // ReSharper disable once StaticMemberInGenericType
    private static int _instanceCounter;

    private readonly SetupCollection _setups = new();
    private readonly List<Invocation> _invocations = new();

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

    public static Mimic<T> FromObject(T objectInstance)
    {
        if (objectInstance is not IMimicked<T> mimicked)
            throw MimicException.ObjectNotCreatedByMimic();

        return mimicked.Mimic;
    }

    public IConditionalSetup<T> When(Func<bool> condition) => new ConditionalSetup<T>(this, condition);

    private T GetOrInitializeObject()
    {
        return _object ??= (T)ProxyGenerator.Instance.GenerateProxy(
            typeof(T),
            [typeof(IMimicked<T>)],
            _constructorArguments ?? Array.Empty<object>(),
            this);
    }

    public override string ToString() => Name;
}
