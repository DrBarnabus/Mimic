using Mimic.Core;

namespace Mimic.Setup.ArgumentMatchers;

internal sealed class ParamArrayArgumentMatcher : IArgumentMatcher
{
    private readonly IArgumentMatcher[] _argumentMatchers;

    public ParamArrayArgumentMatcher(IArgumentMatcher[] argumentMatchers)
    {
        Guard.NotNull(argumentMatchers);

        _argumentMatchers = argumentMatchers;
    }

    public bool Matches(object? argument, Type type)
    {
        if (argument is not Array argumentValues || _argumentMatchers.Length != argumentValues.Length)
            return false;

        var elementType = type.GetElementType();
        Guard.NotNull(elementType);

        for (int i = 0; i < argumentValues.Length; i++)
        {
            if (!_argumentMatchers[i].Matches(argumentValues.GetValue(i), elementType))
                return false;
        }

        return true;
    }
}
