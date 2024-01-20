﻿using Mimic.Setup.Fluent;

namespace Mimic;

[PublicAPI]
public sealed partial class Mimic<T> : IMimic
    where T : class
{
    // ReSharper disable once StaticMemberInGenericType
    private static int _instanceCounter;

    private readonly SetupCollection _setups = new();
    private readonly List<IInvocation> _invocations = new();
    private T? _object;

    public string Name { get; init; }

    public bool Strict { get; init; } = true;

    public T Object => GetOrInitializeObject();

    internal IReadOnlyList<IInvocation> Invocations
    {
        get { lock (_invocations) return _invocations.ToArray(); }
    }

    public Mimic()
    {
        if (!typeof(T).IsInterface)
            throw new NotSupportedException($"Type to mimic {TypeNameFormatter.GetFormattedName(typeof(T))} must be an interface");

        int instanceNumber = Interlocked.Increment(ref _instanceCounter);
        Name = $"Mimic<{TypeNameFormatter.GetFormattedName(typeof(T))}>:{instanceNumber}";
    }

    public static Mimic<T> FromObject(T objectInstance)
    {
        if (objectInstance is not IMimicked<T> mimicked)
            throw new ArgumentException($"Object was not created by Mimic, it does not implement {nameof(IMimicked<T>)}", nameof(objectInstance));

        return mimicked.Mimic;
    }

    public IConditionalSetup<T> When(Func<bool> condition) => new ConditionalSetup<T>(this, condition);

    private T GetOrInitializeObject()
    {
        return _object ??= (T)ProxyGenerator.Instance.GenerateProxy(
            typeof(T),
            new[] { typeof(IMimicked<T>) },
            this);
    }

    public override string ToString() => Name;
}
