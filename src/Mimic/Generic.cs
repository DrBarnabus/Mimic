using JetBrains.Annotations;

namespace Mimic;

[PublicAPI]
public sealed class Generic
{
    public sealed class AnyType : IGenericMatcher
    {
        public bool Matches(Type genericType) => true;
    }

    public sealed class AnyReferenceType : IGenericMatcher
    {
        public bool Matches(Type genericType) => !genericType.IsValueType;
    }

    public sealed class AssignableFromType<T> : IGenericMatcher
    {
        public bool Matches(Type genericType) => typeof(T).IsAssignableFrom(genericType);
    }

    public readonly struct AnyValueType : IGenericMatcher
    {
        public bool Matches(Type genericType) => genericType.IsValueType;
    }
}
