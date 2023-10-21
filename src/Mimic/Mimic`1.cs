using Mimic.Core;
using Mimic.Proxy;

namespace Mimic;

public sealed partial class Mimic<T> : IMimic
    where T : class
{
    // ReSharper disable once StaticMemberInGenericType
    private static int _instanceCounter;

    private readonly SetupCollection _setups = new();
    private T? _object;

    public string Name { get; init; }

    public T Object => GetOrInitializeObject();

    public Mimic()
    {
        if (!typeof(T).IsInterface)
            throw new NotSupportedException($"Type {typeof(T)} must be an interface.");

        int instanceNumber = Interlocked.Increment(ref _instanceCounter);
        Name = $"Mimic<{TypeNameFormatter.GetFormattedName(typeof(T))}>:{instanceNumber}";
    }

    private T GetOrInitializeObject()
    {
        return _object ??= (T)ProxyGenerator.Instance.GenerateProxy(
            typeof(T),
            new[] { typeof(IMimicked<T>) },
            this);
    }

    public override string ToString() => Name;
}
