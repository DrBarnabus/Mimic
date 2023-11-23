using Mimic.Core.Extensions;

namespace Mimic.UnitTests.Core.Extensions;

public class TypeExtensionsTests
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
    [InlineData(typeof(TypeExtensionsTests), default(TypeExtensionsTests))]
    public void GetDefaultValue_ReturnsExpectedDefaultValueForType(Type type, object? expectedValue)
    {
        type.GetDefaultValue().ShouldBe(expectedValue);
    }

    // ReSharper disable once InconsistentNaming
    public static List<object[]> CompareWith_WithNoGenericMatchers_TestData =>
    [
        new object[] { new[] { typeof(bool), typeof(string) }, new[] { typeof(bool), typeof(string) }, true },
        new object[] { new[] { typeof(long), typeof(object) }, new[] { typeof(long), typeof(object) }, true },
        new object[] { new[] { typeof(nuint), typeof(char) }, new[] { typeof(nuint), typeof(char) }, true },

        // wrong number of types
        new object[] { new[] { typeof(bool), typeof(string) }, new[] { typeof(bool) }, false },
        new object[] { new[] { typeof(bool) }, new[] { typeof(bool), typeof(string) }, false },

        // 2nd type mismatched
        new object[] { new[] { typeof(bool), typeof(int) }, new[] { typeof(bool), typeof(string) }, false },

        // 1st type mismatched (array)
        new object[] { new[] { typeof(bool), typeof(string) }, new[] { typeof(bool[]), typeof(string) }, false },
        new object[] { new[] { typeof(bool[]), typeof(string) }, new[] { typeof(int[]), typeof(string) }, false },

        // 1st type mismatched (pointer)
        new object[] { new[] { typeof(int), typeof(string) }, new[] { typeof(int*), typeof(string) }, false }
    ];

    [Theory]
    [MemberData(nameof(CompareWith_WithNoGenericMatchers_TestData))]
    public void CompareWith_WithNoGenericMatchers_ShouldReturnCorrectResult(Type[] types, Type[] otherTypes, bool expectedResult)
    {
        types.CompareWith(otherTypes).ShouldBe(expectedResult);
    }

    // ReSharper disable once InconsistentNaming
    public static List<object[]> CompareWith_WithGenericMatchers_TestData =>
    [
        new object[]
        {
            new[] { typeof(Generic.AnyType), typeof(Generic.AnyReferenceType) },
            new[] { typeof(bool), typeof(string) },
            true
        },

        new object[]
        {
            new[] { typeof(Generic.AnyValueType), typeof(Generic.AssignableFromType<A>) },
            new[] { typeof(bool), typeof(B) },
            true
        },

        new object[]
        {
            new[]
            {
                typeof(Generic.AnyValueType[]), typeof(Generic.AnyReferenceType[]), typeof(Generic.AnyType[]),
                typeof(Generic.AssignableFromType<A>[])
            },
            new[] { typeof(bool[]), typeof(string[]), typeof(C[]), typeof(B[]) },
            true
        },

        new object[]
        {
            new[]
            {
                typeof(List<Generic.AnyValueType>), typeof(List<Generic.AnyReferenceType>),
                typeof(List<Generic.AnyType>), typeof(List<Generic.AssignableFromType<A>>)
            },
            new[] { typeof(List<bool>), typeof(List<string>), typeof(List<C>), typeof(List<B>) },
            true
        },

        new object[]
        {
            new[] { typeof(Generic.AnyValueType).MakeByRefType(), typeof(Generic.AnyValueType).MakePointerType() },
            new[] { typeof(int).MakeByRefType(), typeof(char).MakePointerType() },
            true
        },

        // any reference type compared with value type

        new object[]
        {
            new[] { typeof(Generic.AnyReferenceType) },
            new[] { typeof(int) },
            false
        },

        // any value type compared with reference type

        new object[]
        {
            new[] { typeof(Generic.AnyValueType) },
            new[] { typeof(string) },
            false
        },

        // assignable from type compared with non-assignable type

        new object[]
        {
            new[] { typeof(Generic.AssignableFromType<A>) },
            new[] { typeof(string) },
            false
        },

        new object[]
        {
            new[] { typeof(Generic.AssignableFromType<A>) },
            new[] { typeof(C) },
            false
        },

        // generic with any type compared with different generic type definition

        new object[]
        {
            new[] { typeof(List<Generic.AnyType>) },
            new[] { typeof(HashSet<C>) },
            false
        }
    ];

    [Theory]
    [MemberData(nameof(CompareWith_WithGenericMatchers_TestData))]
    public void CompareWith_WithGenericMatchers_ShouldReturnCorrectResult(Type[] types, Type[] otherTypes, bool expectedResult)
    {
        types.CompareWith(otherTypes).ShouldBe(expectedResult);
    }

    private class A;
    private class B : A;
    private class C;
}
