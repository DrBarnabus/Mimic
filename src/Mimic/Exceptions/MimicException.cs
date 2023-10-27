using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using JetBrains.Annotations;
using Mimic.Core;
using Mimic.Proxy;

namespace Mimic.Exceptions;

[PublicAPI]
public class MimicException : Exception
{
    public virtual string Identifier => "mimic_generic";

    public MimicException(string? message)
        : base(message)
    {
    }

    public MimicException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(Identifier), Identifier);
    }

    internal static MimicException UnmatchableArgumentMatcher(Expression argumentExpression, Type expectedType)
    {
        string formattedFromType = TypeNameFormatter.GetFormattedName(argumentExpression.Type);
        string formattedToType = TypeNameFormatter.GetFormattedName(expectedType);

        return new MimicException($"ArgumentMatcher for argument '{argumentExpression}' is unmatchable. An implicit conversion of the argument from type '{formattedFromType}' to type '{formattedToType}' which is an incompatible assignment");
    }

    internal static MimicException ReturnRequired(IInvocation invocation)
    {
        return new MimicException($"Invocation of '{invocation}' failed. Invocation needs to return a non-void value but there is no corresponding setup that provides one");
    }

    internal static MimicException ExpressionNotProperty(Expression expression)
    {
        return new MimicException($"Expression ({expression}) is not a property accessor");
    }

    internal static MimicException ExpressionNotPropertyGetter(PropertyInfo property)
    {
        string formattedDeclaringTypeName = TypeNameFormatter.GetFormattedName(property.DeclaringType!);
        return new MimicException($"Property ({formattedDeclaringTypeName}.{property.Name}) does not have a getter");
    }

    internal static MimicException ExpressionNotPropertySetter(PropertyInfo property)
    {
        string formattedDeclaringTypeName = TypeNameFormatter.GetFormattedName(property.DeclaringType!);
        return new MimicException($"Property ({formattedDeclaringTypeName}.{property.Name}) does not have a setter");
    }

    internal static MimicException ExpressionNotPropertySetter(LambdaExpression expression)
    {
        return new MimicException($"Expression ({expression}) is not a property setter");
    }

    internal static MimicException WrongCallbackArgumentCount(int expectedCount, int actualCount)
    {
        return new MimicException($"Setup on method with {expectedCount} expected argument(s) cannot invoke a callback method with {actualCount} argument(s)");
    }

    internal static MimicException WrongCallbackArgumentTypes(ParameterInfo[] expectedArgumentTypes, ParameterInfo[] actualArgumentTypes)
    {
        string expectedArgumentTypeList = GetArgumentTypeList(expectedArgumentTypes);
        string actualArgumentTypesList = GetArgumentTypeList(actualArgumentTypes);

        return new MimicException($"Setup on method with arguments ({expectedArgumentTypeList}) cannot invoke a callback method with the wrong argument types ({actualArgumentTypesList})");

        static string GetArgumentTypeList(ParameterInfo[] parameters)
        {
            var stringBuilder = new ValueStringBuilder(stackalloc char[256]);

            for (int i = 0; i < parameters.Length; i++)
            {
                if (i == 0)
                    stringBuilder.Append(", ".AsSpan());

                var parameter = parameters[i];
                var parameterType = parameter.ParameterType;

                if (parameterType.IsByRef)
                {
                    stringBuilder.Append((parameter.Attributes & (ParameterAttributes.In | ParameterAttributes.Out)) switch
                    {
                        ParameterAttributes.In => "in ".AsSpan(),
                        ParameterAttributes.Out => "out ".AsSpan(),
                        _ => "ref ".AsSpan()
                    });

                    parameterType = parameterType.GetElementType()!;
                }

                if (parameterType.IsArray && parameter.IsDefined(typeof(ParamArrayAttribute), true))
                    stringBuilder.Append("params ".AsSpan());

                TypeNameFormatter.AppendFormattedTypeName(ref stringBuilder, parameterType);
            }

            return stringBuilder.ToString();
        }
    }

    internal static MimicException WrongReturnCallbackReturnType(Type expectedType, Type? actualType)
    {
        string formattedExpectedType = TypeNameFormatter.GetFormattedName(expectedType);

        string error = actualType is null ? "a void return type" : $"return type '{TypeNameFormatter.GetFormattedName(actualType)}'";
        return new MimicException($"Setup on method with return type '{formattedExpectedType}' cannot invoke a callback method with {error}");
    }

    internal static MimicException WrongCallbackReturnType()
    {
        return new MimicException("Setup on method cannot invoke a callback method with a non-void return type");
    }
}
