namespace Mimic.Setup.ArgumentMatchers;

internal sealed class ParamArrayArgumentMatcher : IArgumentMatcher
{
    private readonly IArgumentMatcher[] _argumentMatchers;

    public ParamArrayArgumentMatcher(IArgumentMatcher[] argumentMatchers)
    {
        Guard.NotNull(argumentMatchers);

        _argumentMatchers = argumentMatchers;
    }

    public bool Matches(object? argument)
    {
        if (argument is not Array argumentValues || _argumentMatchers.Length != argumentValues.Length)
            return false;

        for (int i = 0; i < argumentValues.Length; i++)
        {
            if (!_argumentMatchers[i].Matches(argumentValues.GetValue(i)))
                return false;
        }

        return true;
    }
}
