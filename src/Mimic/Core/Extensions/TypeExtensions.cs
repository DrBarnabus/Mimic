namespace Mimic.Core.Extensions;

internal static class TypeExtensions
{
    internal static object? GetDefaultValue(this Type type) => type.IsValueType ? Activator.CreateInstance(type) : null;

    internal static bool CompareWith(this Type[] types, Type[] otherTypes)
    {
        if (types.Length != otherTypes.Length)
            return false;

        for (int i = 0; i < otherTypes.Length; i++)
        {
            var type = types[i];
            var otherType = otherTypes[i];

            if (type.IsOrContainsGenericMatcher())
                type = type.SubstituteGenericMatchers(otherType);

            if (!type.IsAssignableFrom(otherType))
                return false;
        }

        return true;
    }

    private static bool IsGenericMatcher(this Type type) => typeof(IGenericMatcher).IsAssignableFrom(type);

    private static bool IsGenericMatcher(this Type type, [NotNullWhen(true)] out Type? genericMatcherType)
    {
        if (!type.IsGenericMatcher())
        {
            genericMatcherType = null;
            return false;
        }

        genericMatcherType = type;
        return type.IsValueType || type.GetConstructor(Type.EmptyTypes) != null;
    }

    private static bool IsOrContainsGenericMatcher(this Type type)
    {
        if (type.IsGenericMatcher())
            return true;

        if (type.HasElementType)
            return IsOrContainsGenericMatcher(type.GetElementType()!);

        return type.IsGenericType && type.GetGenericArguments().Any(IsOrContainsGenericMatcher);
    }

    private static Type SubstituteGenericMatchers(this Type type, Type otherType)
    {
        if (type.IsGenericMatcher(out var genericMatcherType))
        {
            var genericMatcher = (IGenericMatcher?)Activator.CreateInstance(genericMatcherType);
            if (genericMatcher != null && genericMatcher.Matches(otherType))
                return otherType;
        }
        else if (type.HasElementType && otherType.HasElementType)
        {
            return SubstituteGenericMatcherInElementType(type, otherType);
        }
        else if (type.IsGenericType && otherType.IsGenericType)
        {
            return SubstituteGenericMatchersInGenericType(type, otherType);
        }

        return type;
    }

    private static Type SubstituteGenericMatcherInElementType(Type type, Type otherType)
    {
        Guard.Assert(type.HasElementType);
        Guard.Assert(otherType.HasElementType);

        var typeElementType = type.GetElementType()!;
        var otherTypeElementType = otherType.GetElementType()!;

        if (type.IsArray && otherType.IsArray)
        {
            int typeArrayRank = type.GetArrayRank();
            if (typeArrayRank == otherType.GetArrayRank())
            {
                var substitutedElementType = typeElementType.SubstituteGenericMatchers(otherTypeElementType);
                if (substitutedElementType == typeElementType)
                    return type;

                return typeArrayRank == 1
                    ? substitutedElementType.MakeArrayType()
                    : substitutedElementType.MakeArrayType(typeArrayRank);
            }
        }
        else if (type.IsByRef && otherType.IsByRef)
        {
            var substitutedElementType = typeElementType.SubstituteGenericMatchers(otherTypeElementType);
            return substitutedElementType == typeElementType ? type : substitutedElementType.MakeByRefType();
        }
        else if (type.IsPointer && otherType.IsPointer)
        {
            var substitutedElementType = typeElementType.SubstituteGenericMatchers(otherTypeElementType);
            return substitutedElementType == typeElementType ? type : substitutedElementType.MakePointerType();
        }

        return type;
    }

    private static Type SubstituteGenericMatchersInGenericType(Type type, Type otherType)
    {
        var typeDefinition = type.GetGenericTypeDefinition();
        var otherTypeDefinition = otherType.GetGenericTypeDefinition();

        if (typeDefinition != otherTypeDefinition)
            return type;

        var typeGenericArguments = type.GetGenericArguments();
        var otherTypeGenericArguments = otherType.GetGenericArguments();

        Guard.Assert(typeGenericArguments.Length == otherTypeGenericArguments.Length);

        bool changed = false;
        for (int i = 0; i < typeGenericArguments.Length; i++)
        {
            var substitutedTypeGenericArgument =
                typeGenericArguments[i].SubstituteGenericMatchers(otherTypeGenericArguments[i]);
            if (substitutedTypeGenericArgument == typeGenericArguments[i])
                continue;

            changed = true;
            typeGenericArguments[i] = substitutedTypeGenericArgument;
        }

        return changed ? typeDefinition.MakeGenericType(typeGenericArguments) : type;
    }
}
