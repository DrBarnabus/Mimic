using Mimic.Core;

namespace Mimic.UnitTests.Core
{
    public class TypeNameFormatterTests
    {
        [Theory]
        [InlineData(typeof(N1.A), "A")]
        [InlineData(typeof(N1.N2.A), "A")]
        public void GetFormattedName_WithSimpleType_ShouldReturnCorrectlyFormattedName(Type type, string expectedResult)
        {
            TypeNameFormatter.GetFormattedName(type).ShouldBe(expectedResult);
        }

        [Theory]
        [InlineData(typeof(N1.A.B), "A.B")]
        [InlineData(typeof(N1.N2.A.B), "A.B")]
        [InlineData(typeof(N1.A.B.C), "A.B.C")]
        [InlineData(typeof(N1.N2.A.B.C), "A.B.C")]
        public void GetFormattedName_WithSimpleNestedType_ShouldReturnCorrectlyFormattedName(Type type, string expectedResult)
        {
            TypeNameFormatter.GetFormattedName(type).ShouldBe(expectedResult);
        }

        [Theory]
        [InlineData(typeof(N1.A<>), "A<>")]
        [InlineData(typeof(N1.A<,>), "A<,>")]
        [InlineData(typeof(N1.N2.A<>), "A<>")]
        [InlineData(typeof(N1.N2.A<,>), "A<,>")]
        public void GetFormattedName_WithOpenGenericType_ShouldReturnCorrectlyFormattedName(Type type, string expectedResult)
        {
            TypeNameFormatter.GetFormattedName(type).ShouldBe(expectedResult);
        }

        [Theory]
        [InlineData(typeof(N1.A<N1.A>), "A<A>")]
        [InlineData(typeof(N1.A<N1.A,N1.A.B>), "A<A, A.B>")]
        [InlineData(typeof(N1.N2.A<N1.N2.A>), "A<A>")]
        [InlineData(typeof(N1.N2.A<N1.N2.A,N1.N2.A.B>), "A<A, A.B>")]
        public void GetFormattedName_WithClosedGenericType_ShouldReturnCorrectlyFormattedName(Type type, string expectedResult)
        {
            TypeNameFormatter.GetFormattedName(type).ShouldBe(expectedResult);
        }

        [Theory]
        [InlineData(typeof(N1.A<N1.A<N1.A.B>>), "A<A<A.B>>")]
        [InlineData(typeof(N1.N2.A<N1.N2.A<N1.N2.A.B>>), "A<A<A.B>>")]
        public void GetFormattedName_WithRecursiveClosedGenericType_ShouldReturnCorrectlyFormattedName(Type type, string expectedResult)
        {
            TypeNameFormatter.GetFormattedName(type).ShouldBe(expectedResult);
        }

        [Theory]
        [InlineData(typeof(N1.A.B<>), "A.B<>")]
        [InlineData(typeof(N1.A<>.B), "A<>.B")]
        [InlineData(typeof(N1.A<>.B.C), "A<>.B.C")]
        [InlineData(typeof(N1.A<>.B.C<>), "A<>.B.C<>")]
        [InlineData(typeof(N1.A<>.B.C<,>), "A<>.B.C<,>")]
        [InlineData(typeof(N1.A<>.B<>.C), "A<>.B<>.C")]
        [InlineData(typeof(N1.A<>.B<>.C<>), "A<>.B<>.C<>")]
        [InlineData(typeof(N1.A<>.B<>.C<,>), "A<>.B<>.C<,>")]
        [InlineData(typeof(N1.A<>.B<,>.C), "A<>.B<,>.C")]
        [InlineData(typeof(N1.A<>.B<,>.C<>), "A<>.B<,>.C<>")]
        [InlineData(typeof(N1.A<>.B<,>.C<,>), "A<>.B<,>.C<,>")]
        [InlineData(typeof(N1.A<,>.B), "A<,>.B")]
        [InlineData(typeof(N1.A<,>.B.C), "A<,>.B.C")]
        [InlineData(typeof(N1.A<,>.B.C<>), "A<,>.B.C<>")]
        [InlineData(typeof(N1.A<,>.B.C<,>), "A<,>.B.C<,>")]
        [InlineData(typeof(N1.A<,>.B<>.C), "A<,>.B<>.C")]
        [InlineData(typeof(N1.A<,>.B<>.C<>), "A<,>.B<>.C<>")]
        [InlineData(typeof(N1.A<,>.B<>.C<,>), "A<,>.B<>.C<,>")]
        [InlineData(typeof(N1.A<,>.B<,>.C), "A<,>.B<,>.C")]
        [InlineData(typeof(N1.A<,>.B<,>.C<>), "A<,>.B<,>.C<>")]
        [InlineData(typeof(N1.A<,>.B<,>.C<,>), "A<,>.B<,>.C<,>")]
        public void GetFormattedName_WithNestedOpenGenericType_ShouldReturnCorrectlyFormattedName(Type type, string expectedResult)
        {
            TypeNameFormatter.GetFormattedName(type).ShouldBe(expectedResult);
        }

        [Theory]
        [InlineData(typeof(N1.A.B<bool>), "A.B<bool>")]
        [InlineData(typeof(N1.A<byte>.B), "A<byte>.B")]
        [InlineData(typeof(N1.A<sbyte>.B.C), "A<sbyte>.B.C")]
        [InlineData(typeof(N1.A<short>.B.C<ushort>), "A<short>.B.C<ushort>")]
        [InlineData(typeof(N1.A<int>.B.C<uint,nint>), "A<int>.B.C<uint, nint>")]
        [InlineData(typeof(N1.A<nuint>.B<long>.C), "A<nuint>.B<long>.C")]
        [InlineData(typeof(N1.A<ulong>.B<decimal>.C<double>), "A<ulong>.B<decimal>.C<double>")]
        [InlineData(typeof(N1.A<float>.B<char>.C<string,object>), "A<float>.B<char>.C<string, object>")]
        [InlineData(typeof(N1.A<N1.A>.B<N1.A.B,N1.A.B.C>.C), "A<A>.B<A.B, A.B.C>.C")]
        [InlineData(typeof(N1.A<N1.A<bool>>.B<N1.A<byte>,N1.A.B<sbyte>>.C<N1.A<short>.B<ushort>>), "A<A<bool>>.B<A<byte>, A.B<sbyte>>.C<A<short>.B<ushort>>")]
        public void GetFormattedName_WithNestedClosedGenericType_ShouldReturnCorrectlyFormattedName(Type type, string expectedResult)
        {
            TypeNameFormatter.GetFormattedName(type).ShouldBe(expectedResult);
        }

        [Theory]
        [InlineData(typeof(bool), "bool")]
        [InlineData(typeof(byte), "byte")]
        [InlineData(typeof(sbyte), "sbyte")]
        [InlineData(typeof(short), "short")]
        [InlineData(typeof(ushort), "ushort")]
        [InlineData(typeof(int), "int")]
        [InlineData(typeof(uint), "uint")]
        [InlineData(typeof(nint), "nint")]
        [InlineData(typeof(nuint), "nuint")]
        [InlineData(typeof(long), "long")]
        [InlineData(typeof(ulong), "ulong")]
        [InlineData(typeof(decimal), "decimal")]
        [InlineData(typeof(double), "double")]
        [InlineData(typeof(float), "float")]
        [InlineData(typeof(char), "char")]
        [InlineData(typeof(string), "string")]
        [InlineData(typeof(object), "object")]
        [InlineData(typeof(void), "void")]
        public void GetFormattedName_WithKeywordType_ShouldReturnCorrectlyFormattedName(Type type, string expectedResult)
        {
            TypeNameFormatter.GetFormattedName(type).ShouldBe(expectedResult);
        }

        [Theory]
        [InlineData(typeof(object[]), "object[]")]
        [InlineData(typeof(N1.A[]), "A[]")]
        [InlineData(typeof(N1.A<N1.A[]>), "A<A[]>")]
        [InlineData(typeof(N1.N2.A[]), "A[]")]
        public void GetFormattedName_WithArrayType_ShouldReturnCorrectlyFormattedName(Type type, string expectedResult)
        {
            TypeNameFormatter.GetFormattedName(type).ShouldBe(expectedResult);
        }

        [Theory]
        [InlineData(typeof(object[,]), "object[,]")]
        [InlineData(typeof(object[,,]), "object[,,]")]
        [InlineData(typeof(object[,,,]), "object[,,,]")]
        [InlineData(typeof(object[,][,,][,,,]), "object[,][,,][,,,]")]
        public void GetFormattedName_WithMultidimensionalArrayType_ShouldReturnCorrectlyFormattedName(Type type, string expectedResult)
        {
            TypeNameFormatter.GetFormattedName(type).ShouldBe(expectedResult);
        }

        [Fact]
        public void GetFormattedName_WithRefType_ShouldReturnCorrectlyFormattedName()
        {
            var type = typeof(N1.A).MakeByRefType();

            TypeNameFormatter.GetFormattedName(type).ShouldBe("ref A");
        }

        [Fact]
        public void GetFormattedName_WithPointerType_ShouldReturnCorrectlyFormattedName()
        {
            var type = typeof(void*);

            TypeNameFormatter.GetFormattedName(type).ShouldBe("void*");
        }

        [Theory]
        [InlineData(typeof(bool?), "bool?")]
        [InlineData(typeof(byte?), "byte?")]
        [InlineData(typeof(sbyte?), "sbyte?")]
        [InlineData(typeof(short?), "short?")]
        [InlineData(typeof(ushort?), "ushort?")]
        [InlineData(typeof(int?), "int?")]
        [InlineData(typeof(uint?), "uint?")]
        [InlineData(typeof(nint?), "nint?")]
        [InlineData(typeof(nuint?), "nuint?")]
        [InlineData(typeof(long?), "long?")]
        [InlineData(typeof(ulong?), "ulong?")]
        [InlineData(typeof(decimal?), "decimal?")]
        [InlineData(typeof(double?), "double?")]
        [InlineData(typeof(float?), "float?")]
        [InlineData(typeof(char?), "char?")]
        [InlineData(typeof(N1.S?), "S?")]
        public void GetFormattedName_WithNullableType_ShouldReturnCorrectlyFormattedName(Type type, string expectedResult)
        {
            TypeNameFormatter.GetFormattedName(type).ShouldBe(expectedResult);
        }

        [Theory]
        [InlineData(typeof(ValueTuple<,>), "ValueTuple<,>")]
        [InlineData(typeof(ValueTuple<bool>), "(bool)")]
        [InlineData(typeof(ValueTuple<byte, sbyte>), "(byte, sbyte)")]
        [InlineData(typeof((short, ushort?)), "(short, ushort?)")]
        [InlineData(typeof((int, uint, nint)?), "(int, uint, nint)?")]
        [InlineData(typeof((long, ulong)[]), "(long, ulong)[]")]
        public void GetFormattedName_WithTupleType_ShouldReturnCorrectlyFormattedName(Type type, string expectedResult)
        {
            TypeNameFormatter.GetFormattedName(type).ShouldBe(expectedResult);
        }

        [Fact]
        public void GetFormattedName_WithEmptyAnonymousType_ShouldReturnCorrectlyFormattedName()
        {
            var anonymousObject = new { };

            TypeNameFormatter.GetFormattedName(anonymousObject.GetType()).ShouldBe("{}");
        }

        [Fact]
        public void GetFormattedName_WithSimpleAnonymousType_ShouldReturnCorrectlyFormattedName()
        {
            var anonymousObject = new
            {
                Year = 2023,
                Month = "October"
            };

            TypeNameFormatter.GetFormattedName(anonymousObject.GetType()).ShouldBe("{int Year, string Month}");
        }

        [Fact]
        public void GetFormattedName_WithNestedAnonymousType_ShouldReturnCorrectlyFormattedName()
        {
            var anonymousObject = new
            {
                Year = 2023,
                Month = "October"
            };

            TypeNameFormatter.GetFormattedName(anonymousObject.GetType()).ShouldBe("{int Year, string Month}");
        }

        [Fact]
        public void GetFormattedName_WithNestedComplexAnonymousType_ShouldReturnCorrectlyFormattedName()
        {
            var anonymousObject = new
            {
                Year = 2023,
                Month = "October",
                Other = new
                {
                    Nullable = (Guid?)null,
                    Class = new N1.A()
                }
            };

            TypeNameFormatter.GetFormattedName(anonymousObject.GetType()).ShouldBe("{int Year, string Month, {Guid? Nullable, A Class} Other}");
        }
    }
}

#region Types

namespace N1
{
    internal class A
    {
        internal class B
        {
            internal class C
            {
            }
        }

        internal class B<T>
        {
        }
    }

    internal class A<T1>
    {
        internal class B
        {
            internal class C
            {
            }

            internal class C<T2>
            {
            }

            internal class C<T2, T3>
            {
            }
        }

        internal class B<T2>
        {
            internal class C
            {
            }

            internal class C<T3>
            {
            }

            internal class C<T3, T4>
            {
            }
        }

        internal class B<T2, T3>
        {
            internal class C
            {
            }

            internal class C<T4>
            {
            }

            internal class C<T4, T5>
            {
            }
        }
    }

    internal class A<T1, T2>
    {
        internal class B
        {
            internal class C
            {
            }

            internal class C<T3>
            {
            }

            internal class C<T4, T5>
            {
            }
        }

        internal class B<T3>
        {
            internal class C
            {
            }

            internal class C<T4>
            {
            }

            internal class C<T4, T5>
            {
            }
        }

        internal class B<T3, T4>
        {
            internal class C
            {
            }

            internal class C<T5>
            {
            }

            internal class C<T5, T6>
            {
            }
        }
    }

    internal struct S
    {
    }

    namespace N2
    {
        internal class A
        {
            internal class B
            {
                internal class C
                {
                }
            }
        }

        internal class A<T>
        {
        }

        internal class A<T1, T2>
        {
        }
    }
}

#endregion
