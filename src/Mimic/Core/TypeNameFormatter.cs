using System.Reflection;
using System.Runtime.CompilerServices;

namespace Mimic.Core;

internal static class TypeNameFormatter
{
    private static readonly Dictionary<Type, string> BuiltinTypeKeywords = new()
    {
        [typeof(bool)] = "bool",
        [typeof(byte)] = "byte",
        [typeof(sbyte)] = "sbyte",
        [typeof(short)] = "short",
        [typeof(ushort)] = "ushort",
        [typeof(int)] = "int",
        [typeof(uint)] = "uint",
        [typeof(nint)] = "nint",
        [typeof(nuint)] = "nuint",
        [typeof(long)] = "long",
        [typeof(ulong)] = "ulong",
        [typeof(decimal)] = "decimal",
        [typeof(double)] = "double",
        [typeof(float)] = "float",
        [typeof(char)] = "char",
        [typeof(string)] = "string",
        [typeof(object)] = "object",
        [typeof(void)] = "void",
    };

    internal static string GetFormattedName(Type type)
    {
        Guard.NotNull(type);

        var stringBuilder = new ValueStringBuilder(stackalloc char[256]);
        AppendFormattedTypeName(ref stringBuilder, type);
        return stringBuilder.ToString();
    }

    internal static void AppendFormattedTypeName(ref ValueStringBuilder stringBuilder, Type type)
    {
        Guard.NotNull(type);

        AppendFormattedTypeName(ref stringBuilder, type, type);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void AppendFormattedTypeName(
        ref ValueStringBuilder stringBuilder, Type type, Type typeWithGenericArguments)
    {
        if (BuiltinTypeKeywords.TryGetValue(type, out string? keyword))
        {
            stringBuilder.Append(keyword.AsSpan());
            return;
        }

        if (type.HasElementType)
        {
            AppendElementType(ref stringBuilder, type);
            return;
        }

        bool isConstructedGeneric = type is { IsGenericType: true, IsGenericTypeDefinition: false };
        if (isConstructedGeneric && TryAppendConstructedGeneric(ref stringBuilder, type))
            return;

        var typeName = type.Name.AsSpan();
        if (typeName.StartsWith("<>f", StringComparison.Ordinal))
        {
            AppendAnonymousType(ref stringBuilder, type);
            return;
        }

        if (type is { IsGenericParameter: false, IsNested: true })
        {
            AppendFormattedTypeName(ref stringBuilder, type.DeclaringType!, typeWithGenericArguments);
            stringBuilder.Append('.');
        }

        if (isConstructedGeneric || type.IsGenericType)
            AppendGenericType(ref stringBuilder, type, typeWithGenericArguments);
        else
            stringBuilder.Append(typeName);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void AppendElementType(ref ValueStringBuilder stringBuilder, Type type)
    {
        var elementType = type.GetElementType()!;

        if (type.IsArray)
        {
            AppendArrayElementType(ref stringBuilder, elementType, type);
        }
        else if (type.IsByRef)
        {
            stringBuilder.Append("ref ".AsSpan());
            AppendFormattedTypeName(ref stringBuilder, elementType, elementType);
        }
        else if (type.IsPointer)
        {
            AppendFormattedTypeName(ref stringBuilder, elementType, elementType);
            stringBuilder.Append('*');
        }
        else
        {
            Guard.Assert(false, $"{type.Name} has an element type but is not an array, by-ref or pointer.");
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void AppendArrayElementType(ref ValueStringBuilder stringBuilder, Type elementType, Type arrayType)
    {
        var arrayRanks = new Queue<int>();
        arrayRanks.Enqueue(arrayType.GetArrayRank());

        while (true)
        {
            if (elementType.IsArray)
            {
                arrayRanks.Enqueue(elementType.GetArrayRank());

                elementType = elementType.GetElementType()!;
                continue;
            }

            AppendFormattedTypeName(ref stringBuilder, elementType, elementType);
            while (arrayRanks.Count > 0)
            {
                stringBuilder.Append('[');
                stringBuilder.Append(',', arrayRanks.Dequeue() - 1);
                stringBuilder.Append(']');
            }

            break;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool TryAppendConstructedGeneric(ref ValueStringBuilder stringBuilder, Type type)
    {
        var nullableUnderlyingType = Nullable.GetUnderlyingType(type);
        if (nullableUnderlyingType is not null)
        {
            AppendFormattedTypeName(ref stringBuilder, nullableUnderlyingType, nullableUnderlyingType);
            stringBuilder.Append('?');

            return true;
        }

        if (!type.Name.AsSpan().StartsWith("ValueTuple`", StringComparison.Ordinal) || type.Namespace is not "System")
            return false;

        var genericArguments = type.GetGenericArguments();

        stringBuilder.Append('(');

        for (int i = 0; i < genericArguments.Length; i++)
        {
            if (i > 0)
                stringBuilder.Append(", ".AsSpan());

            AppendFormattedTypeName(ref stringBuilder, genericArguments[i], genericArguments[i]);
        }

        stringBuilder.Append(')');

        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void AppendAnonymousType(ref ValueStringBuilder stringBuilder, Type type)
    {
        stringBuilder.Append('{');

        var declaredProperties = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance |
                                                    BindingFlags.DeclaredOnly);
        for (int i = 0; i < declaredProperties.Length; i++)
        {
            if (i > 0)
                stringBuilder.Append(", ".AsSpan());

            var property = declaredProperties[i];

            AppendFormattedTypeName(ref stringBuilder, property.PropertyType, property.PropertyType);
            stringBuilder.Append(' ');
            stringBuilder.Append(property.Name);
        }

        stringBuilder.Append('}');
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void AppendGenericType(ref ValueStringBuilder stringBuilder, Type type, Type typeWithGenericArguments)
    {
        var typeName = type.Name.AsSpan();

        int indexOfBacktick = typeName.LastIndexOf('`');
        stringBuilder.Append(indexOfBacktick >= 0 ? type.Name.AsSpan(0, indexOfBacktick) : typeName);

        int ownGenericArguments = type.GetGenericArguments().Length;

        int startIndex = 0;
        if (type.IsNested)
        {
            int declaringTypeGenericArguments = type.DeclaringType!.GetGenericArguments().Length;
            if (ownGenericArguments >= declaringTypeGenericArguments)
                startIndex = declaringTypeGenericArguments;
        }

        if (startIndex >= ownGenericArguments)
            return;

        stringBuilder.Append('<');

        if (typeWithGenericArguments is { IsGenericType: true, IsGenericTypeDefinition: false })
        {
            var genericArguments = typeWithGenericArguments.GetGenericArguments();

            for (int i = startIndex; i < ownGenericArguments; i++)
            {
                if (i > startIndex)
                    stringBuilder.Append(", ".AsSpan());

                AppendFormattedTypeName(ref stringBuilder, genericArguments[i], genericArguments[i]);
            }
        }
        else
        {
            stringBuilder.Append(',', ownGenericArguments - startIndex - 1);
        }

        stringBuilder.Append('>');
    }
}
