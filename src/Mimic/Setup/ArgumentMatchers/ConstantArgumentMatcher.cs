using System.Collections;

namespace Mimic.Setup.ArgumentMatchers;

internal sealed class ConstantArgumentMatcher : IArgumentMatcher
{
    private readonly object? _value;

    public ConstantArgumentMatcher(object? value)
    {
        _value = value;
    }

    public bool Matches(object? argument, Type type)
    {
        if (Equals(argument, _value))
            return true;

        if (_value is IEnumerable values && argument is IEnumerable arguments)
            return values.Cast<object>().SequenceEqual(arguments.Cast<object>());

        return false;
    }
}
