
using Mimic.Core;

namespace Mimic.UnitTests.Core;

public class ImmutableStackTests
{
    [Fact]
    public void Constructor_ShouldSuccessfullyConstruct()
    {
        var immutableStack = new ImmutableStack<int>([]);
        immutableStack.IsEmpty.ShouldBeTrue();
    }

    [Fact]
    public void Constructor_WhenItemsIsNull_ShouldThrowAssertionException()
    {
        var ex = Should.Throw<Guard.AssertionException>(() => new ImmutableStack<int>(null!));
        ex.Message.ShouldBe("items must not be null (Expression 'items')");
    }

    [Fact]
    public void Pop_ShouldReturnTopItemAndRemainingItems()
    {
        var immutableStack = new ImmutableStack<int>([1, 2, 3]);

        immutableStack.Pop(out var remainingItems).ShouldBe(1);
        remainingItems.IsEmpty.ShouldBeFalse();
    }
}
