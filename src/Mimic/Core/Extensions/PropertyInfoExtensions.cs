using System.Reflection;

namespace Mimic.Core.Extensions;

internal static class PropertyInfoExtensions
{
    internal static bool CanReadProperty(this PropertyInfo property, out MethodInfo? getter, out PropertyInfo? getterProperty)
    {
        while (true)
        {
            if (property.CanRead)
            {
                getter = property.GetGetMethod(true)!;
                getterProperty = property;

                Guard.NotNull(getter);
                return true;
            }

            var setter = property.GetSetMethod(true);
            Guard.NotNull(setter);

            var baseSetter = setter.GetBaseDefinition();
            if (baseSetter != setter)
            {
                var baseProperty = baseSetter.DeclaringType!.GetMember(property.Name, MemberTypes.Property, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                    .Cast<PropertyInfo>()
                    .First(p => p.GetSetMethod(true) == baseSetter);

                property = baseProperty;
                continue;
            }

            getter = null;
            getterProperty = null;
            return false;
        }
    }

    internal static bool CanWriteProperty(this PropertyInfo property, out MethodInfo? setter, out PropertyInfo? setterProperty)
    {
        while (true)
        {
            if (property.CanWrite)
            {
                setter = property.GetSetMethod(true)!;
                setterProperty = property;

                Guard.NotNull(setter);
                return true;
            }

            var getter = property.GetGetMethod(true);
            Guard.NotNull(getter);

            var baseSetter = getter.GetBaseDefinition();
            if (baseSetter != getter)
            {
                var baseProperty = baseSetter.DeclaringType!.GetMember(property.Name, MemberTypes.Property, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                    .Cast<PropertyInfo>()
                    .First(p => p.GetSetMethod(true) == baseSetter);

                property = baseProperty;
                continue;
            }

            setter = null;
            setterProperty = null;
            return false;
        }
    }
}
