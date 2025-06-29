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

    /// <summary>
    /// Gets the unique name identifier for this mimic instance.
    /// </summary>
    /// <value>A string representing the mimic's name in the format "Mimic&lt;TypeName&gt;:InstanceNumber".</value>
    public string Name { get; init; }

    /// <summary>
    /// Gets a value indicating whether this mimic operates in strict mode.
    /// </summary>
    /// <value><c>true</c> if the mimic is in strict mode; otherwise, <c>false</c>. The default value is <c>true</c>.</value>
    /// <remarks>
    /// In strict mode, the mimic will enforce that all method calls have corresponding setups.
    /// </remarks>
    public bool Strict { get; init; } = true;

    /// <summary>
    /// Gets or sets the constructor arguments to be used when creating the mimicked object.
    /// </summary>
    /// <value>An array of objects to be passed as constructor arguments, or <c>null</c> if no arguments are needed.</value>
    /// <exception cref="ArgumentException">Thrown when attempting to set constructor arguments for an interface type.</exception>
    /// <remarks>
    /// Constructor arguments should only be provided when mimicking concrete classes, not interfaces.
    /// </remarks>
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

    /// <summary>
    /// Gets the mimicked object instance.
    /// </summary>
    /// <value>The proxy object that implements the behaviour defined by the mimic setups.</value>
    /// <remarks>
    /// The object is lazily initialised upon first access. Subsequent calls return the same instance.
    /// </remarks>
    public T Object => GetOrInitializeObject();

    internal IReadOnlyList<Invocation> Invocations
    {
        get { lock (_invocations) return _invocations.ToArray(); }
    }

    /// <summary>
    /// Initialises a new instance of the <see cref="Mimic{T}"/> class with strict mode enabled.
    /// </summary>
    /// <exception cref="MimicException">Thrown when the type <typeparamref name="T"/> cannot be mimicked.</exception>
    /// <remarks>
    /// The mimic will operate in strict mode by default, requiring all method calls to have corresponding setups.
    /// </remarks>
    public Mimic()
    {
        if (!typeof(T).CanBeMimicked())
            throw MimicException.TypeCannotBeMimicked(typeof(T));

        int instanceNumber = Interlocked.Increment(ref _instanceCounter);
        Name = $"Mimic<{TypeNameFormatter.GetFormattedName(typeof(T))}>:{instanceNumber}";
    }

    /// <summary>
    /// Initialises a new instance of the <see cref="Mimic{T}"/> class with the specified strict mode setting.
    /// </summary>
    /// <param name="strict"><c>true</c> to enable strict mode; <c>false</c> to disable it.</param>
    /// <exception cref="MimicException">Thrown when the type <typeparamref name="T"/> cannot be mimicked.</exception>
    /// <remarks>
    /// In strict mode, all method calls must have corresponding setups. In non-strict mode,
    /// method calls without setups will return default values or perform no action.
    /// </remarks>
    public Mimic(bool strict) : this() => Strict = strict;

    /// <summary>
    /// Retrieves the mimic instance from an object that was created by a mimic.
    /// </summary>
    /// <param name="objectInstance">The object instance that was created by a mimic.</param>
    /// <returns>The <see cref="Mimic{T}"/> instance that created the specified object.</returns>
    /// <exception cref="MimicException">Thrown when the provided object was not created by a mimic.</exception>
    /// <remarks>
    /// This method allows you to get the original mimic instance from a mimicked object,
    /// which is useful for verification or additional setup operations.
    /// </remarks>
    public static Mimic<T> FromObject(T objectInstance)
    {
        if (objectInstance is not IMimicked<T> mimicked)
            throw MimicException.ObjectNotCreatedByMimic();

        return mimicked.Mimic;
    }

    /// <summary>
    /// Creates a conditional setup that applies only when the specified condition is met.
    /// </summary>
    /// <param name="condition">A function that returns <c>true</c> when the conditional setup should be active.</param>
    /// <returns>An <see cref="IConditionalSetup{T}"/> that can be used to define behaviour for the conditional setup.</returns>
    /// <remarks>
    /// Conditional setups allow you to define different behaviours based on runtime conditions.
    /// The condition is evaluated each time a matching method is called.
    /// </remarks>
    public IConditionalSetup<T> When(Func<bool> condition) => new ConditionalSetup<T>(this, condition);

    /// <summary>
    /// Returns a string representation of this mimic instance.
    /// </summary>
    /// <returns>The name of this mimic instance.</returns>
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
