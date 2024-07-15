namespace Mimic.Setup;

internal sealed class SetupCollection : IReadOnlyList<SetupBase>
{
    private readonly List<SetupBase> _setups = [];
    private readonly HashSet<IExpectation> _activeSetups = [];

    public void Add(SetupBase setup)
    {
        lock (_setups)
        {
            _setups.Add(setup);

            if (!_activeSetups.Add(setup.Expectation))
                MarkOverridenSetups();
        }
    }

    public List<SetupBase> FindAll(Predicate<SetupBase> predicate)
    {
        lock (_setups)
            return _setups.Where(setup => !setup.Overridden && predicate(setup)).ToList();
    }

    public SetupBase? FindLast(Predicate<SetupBase> predicate)
    {
        lock (_setups)
        {
            if (_setups.Count == 0)
                return null;

            for (int i = _setups.Count - 1; i >= 0 ; i--)
            {
                var setup = _setups[i];
                if (setup.Overridden)
                    continue;

                if (predicate(setup))
                    return setup;
            }
        }

        return null;
    }

    public int Count
    {
        get
        {
            lock (_setups)
                return _setups.Count;
        }
    }

    public SetupBase this[int index]
    {
        get
        {
            lock (_setups)
            {
                return _setups[index];
            }
        }
    }

    public IEnumerator<SetupBase> GetEnumerator()
    {
        lock (_setups)
        {
            return _setups.ToList().GetEnumerator();
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private void MarkOverridenSetups()
    {
        var visitedExpectations = new HashSet<IExpectation>();

        for (int i = _setups.Count - 1; i >= 0 ; i--)
        {
            var setup = _setups[i];
            if (setup.Overridden)
                continue;

            if (!visitedExpectations.Add(setup.Expectation))
                setup.Override();
        }
    }
}
