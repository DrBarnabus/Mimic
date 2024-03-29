﻿namespace Mimic.Core.Extensions;

internal static class PropertyInfoExtensions
{
    internal static bool CanReadProperty(
        this PropertyInfo property,
        [NotNullWhen(true)] out MethodInfo? getter,
        [NotNullWhen(true)] out PropertyInfo? getterProperty)
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

    internal static bool CanWriteProperty(
        this PropertyInfo property,
        [NotNullWhen(true)] out MethodInfo? setter,
        [NotNullWhen(true)] out PropertyInfo? setterProperty)
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

            var baseGetter = getter.GetBaseDefinition();
            if (baseGetter != getter)
            {
                var baseProperty = baseGetter.DeclaringType!.GetMember(property.Name, MemberTypes.Property, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                    .Cast<PropertyInfo>()
                    .First(p => p.GetGetMethod(true) == baseGetter);

                property = baseProperty;
                continue;
            }

            setter = null;
            setterProperty = null;
            return false;
        }
    }
}
