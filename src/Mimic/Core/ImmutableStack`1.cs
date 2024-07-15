using System.Collections.Immutable;

namespace Mimic.Core;

internal readonly struct ImmutableStack<T>
{
    private readonly ImmutableArray<T> _items;
    private readonly int _index;

    public bool IsEmpty => _index == _items.Length;

    public ImmutableStack(IEnumerable<T> items)
    {
        Guard.NotNull(items);

        _items = items.ToImmutableArray();
        _index = 0;
    }

    private ImmutableStack(ImmutableArray<T> items, int index)
    {
        Guard.NotNull(items);
        Guard.Assert(0 <= _index && _index <= items.Length);

        _items = items;
        _index = index;
    }

    public T Pop(out ImmutableStack<T> remainingItems)
    {
        Guard.Assert(_index < _items.Length);

        remainingItems = new ImmutableStack<T>(_items, _index + 1);
        return _items[_index];
    }
}
