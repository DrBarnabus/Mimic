using System.Collections;
using Mimic.Core.Extensions;

namespace Mimic.Core;

internal static class DefaultValueFactory
{
    private static readonly Dictionary<Type, Func<Type, object?>> Factories = new()
    {
        { typeof(Array), GetDefaultArray },
        { typeof(IEnumerable), GetDefaultEnumerable },
        { typeof(IEnumerable<>), GetDefaultEnumerableOf },
        { typeof(ValueTuple<>), GetDefaultValueTupleOf },
        { typeof(ValueTuple<,>), GetDefaultValueTupleOf },
        { typeof(ValueTuple<,,>), GetDefaultValueTupleOf },
        { typeof(ValueTuple<,,,>), GetDefaultValueTupleOf },
        { typeof(ValueTuple<,,,,>), GetDefaultValueTupleOf },
        { typeof(ValueTuple<,,,,,>), GetDefaultValueTupleOf },
        { typeof(ValueTuple<,,,,,,>), GetDefaultValueTupleOf },
    };

    internal static object? GetDefaultValue(Type type)
    {
        Guard.NotNull(type);
        Guard.Assert(type != typeof(void));

        var typeKey = type.IsGenericType
            ? type.GetGenericTypeDefinition()
            : type.IsArray
                ? typeof(Array)
                : type;

        return Factories.TryGetValue(typeKey, out var factory) ? factory.Invoke(type) : type.GetDefaultValue();
    }

    private static object GetDefaultArray(Type type)
    {
        var elementType = type.GetElementType()!;
        int[] lengths = new int[type.GetArrayRank()];
        return Array.CreateInstance(elementType, lengths);
    }

    private static object GetDefaultEnumerable(Type type) => Array.Empty<object>();

    private static object GetDefaultEnumerableOf(Type type)
    {
        var elementType = type.GetGenericArguments()[0];
        return Array.CreateInstance(elementType, 0);
    }

    private static object? GetDefaultValueTupleOf(Type type)
    {
        var itemTypes = type.GetGenericArguments();

        object?[] defaultItemValues = new object[itemTypes.Length];
        for (int i = 0; i < itemTypes.Length; i++)
            defaultItemValues[i] = GetDefaultValue(itemTypes[i]);

        return Activator.CreateInstance(type, defaultItemValues);
    }
}
