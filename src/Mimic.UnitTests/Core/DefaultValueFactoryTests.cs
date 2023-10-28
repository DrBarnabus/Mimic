using System.Collections;
using Mimic.Core;

namespace Mimic.UnitTests.Core;

public class DefaultValueFactoryTests
{
    [Theory]
    [InlineData(typeof(bool), default(bool))]
    [InlineData(typeof(byte), default(byte))]
    [InlineData(typeof(sbyte), default(sbyte))]
    [InlineData(typeof(char), default(char))]
    [InlineData(typeof(double), default(double))]
    [InlineData(typeof(float), default(float))]
    [InlineData(typeof(int), default(int))]
    [InlineData(typeof(uint), default(uint))]
    [InlineData(typeof(long), default(long))]
    [InlineData(typeof(ulong), default(ulong))]
    [InlineData(typeof(short), default(short))]
    [InlineData(typeof(ushort), default(ushort))]
    [InlineData(typeof(object), default(object))]
    [InlineData(typeof(string), default(string))]
    public void GetDefaultValue_WithBuiltInTypes_ShouldReturnCorrectDefaultValue(Type type, object? expectedDefault)
    {
        DefaultValueFactory.GetDefaultValue(type).ShouldBe(expectedDefault);
    }

    [Fact]
    public void GetDefaultValue_WithNonPrimitiveBuiltInTypes_ShouldReturnCorrectDefaultValue()
    {
        DefaultValueFactory.GetDefaultValue(typeof(decimal)).ShouldBe(default(decimal));
        DefaultValueFactory.GetDefaultValue(typeof(nint)).ShouldBe(default(nint));
        DefaultValueFactory.GetDefaultValue(typeof(nuint)).ShouldBe(default(nuint));
    }

    [Fact]
    public void GetDefaultValue_WithArray_ShouldReturnCorrectDefaultValue()
    {
        DefaultValueFactory.GetDefaultValue(typeof(int[])).ShouldBe(Array.Empty<int>());
        DefaultValueFactory.GetDefaultValue(typeof(int[,])).ShouldBe(new int[0, 0]);
    }

    [Fact]
    public void GetDefaultValue_WithUntypedEnumerable_ShouldReturnCorrectDefaultValue()
    {
        DefaultValueFactory.GetDefaultValue(typeof(IEnumerable)).ShouldBe(Array.Empty<object>());
    }

    [Fact]
    public void GetDefaultValue_WithTypedEnumerable_ShouldReturnCorrectDefaultValue()
    {
        DefaultValueFactory.GetDefaultValue(typeof(IEnumerable<object>)).ShouldBe(Array.Empty<object>());
        DefaultValueFactory.GetDefaultValue(typeof(IEnumerable<int>)).ShouldBe(Array.Empty<int>());
    }

    [Fact]
    public void GetDefaultValue_WithValueTuple_ShouldReturnCorrectDefaultValue()
    {
        DefaultValueFactory.GetDefaultValue(typeof(ValueTuple<int>)).ShouldBe(new ValueTuple<int>(default));
        DefaultValueFactory.GetDefaultValue(typeof(ValueTuple<int, int>)).ShouldBe(new ValueTuple<int, int>(default, default));
        DefaultValueFactory.GetDefaultValue(typeof(ValueTuple<int, int, int>)).ShouldBe(new ValueTuple<int, int, int>(default, default, default));
        DefaultValueFactory.GetDefaultValue(typeof(ValueTuple<int, int, int, int>)).ShouldBe(new ValueTuple<int, int, int, int>(default, default, default, default));
        DefaultValueFactory.GetDefaultValue(typeof(ValueTuple<int, int, int, int, int>)).ShouldBe(new ValueTuple<int, int, int, int, int>(default, default, default, default, default));
        DefaultValueFactory.GetDefaultValue(typeof(ValueTuple<int, int, int, int, int, int>)).ShouldBe(new ValueTuple<int, int, int, int, int, int>(default, default, default, default, default, default));
        DefaultValueFactory.GetDefaultValue(typeof(ValueTuple<int, int, int, int, int, int, int>)).ShouldBe(new ValueTuple<int, int, int, int, int, int, int>(default, default, default, default, default, default, default));
    }
}
