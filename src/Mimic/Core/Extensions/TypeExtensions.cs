namespace Mimic.Core.Extensions;

internal static class TypeExtensions
{
    internal static object? GetDefaultValue(this Type type) => type.IsValueType ? Activator.CreateInstance(type) : null;
}
