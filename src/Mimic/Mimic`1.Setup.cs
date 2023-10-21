using System.Collections;
using System.Linq.Expressions;
using Mimic.Core;
using Mimic.Expressions;
using Mimic.Setup;

namespace Mimic;

public partial class Mimic<T>
{
    public ISetup<T> Setup(Expression<Action<T>> expression)
    {
        var setup = Setup(this, expression);
        return new VoidSetup<T>(setup);
    }

    public ISetup<T, TResult> Setup<TResult>(Expression<Func<T, TResult>> expression)
    {
        var setup = Setup(this, expression);
        return new NonVoidSetup<T, TResult>(setup);
    }

    private static MethodCallSetup Setup(Mimic<T> mimic, LambdaExpression expression)
    {
        Guard.NotNull(expression);

        var expectations = ExpressionSplitter.Split(expression);
        if (expectations.Count != 1)
            throw new NotSupportedException($"Unsupported expression: {expression}");

        var setup = new MethodCallSetup(expression, mimic, expectations.Pop());
        mimic._setups.Add(setup);
        return setup;
    }

    private sealed class SetupCollection : IReadOnlyList<MethodCallSetup>
    {
        private readonly List<MethodCallSetup> _setups = new();
        private readonly HashSet<MethodExpectation> _activeSetups = new();

        public void Add(MethodCallSetup setup)
        {
            lock (_setups)
            {
                _setups.Add(setup);

                if (!_activeSetups.Add(setup.Expectation))
                    MarkOverridenSetups();
            }
        }

        public MethodCallSetup? FindLast(Predicate<MethodCallSetup> predicate)
        {
            lock (_setups)
            {
                if (_setups.Count == 0)
                    return null;

                for (int i = _setups.Count - 1; i >= 0 ; i--)
                {
                    var setup = _setups[i];
                    if (setup.Overriden)
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

        public MethodCallSetup this[int index]
        {
            get
            {
                lock (_setups)
                {
                    return _setups[index];
                }
            }
        }

        public IEnumerator<MethodCallSetup> GetEnumerator()
        {
            lock (_setups)
            {
                return _setups.GetEnumerator();
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private void MarkOverridenSetups()
        {
            var visitedExpectations = new HashSet<MethodExpectation>();

            for (int i = _setups.Count - 1; i >= 0 ; i--)
            {
                var setup = _setups[i];
                if (setup.Overriden)
                    continue;

                if (!visitedExpectations.Add(setup.Expectation))
                    setup.Override();
            }
        }
    }
}
