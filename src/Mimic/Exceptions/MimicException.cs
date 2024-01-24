using System.Runtime.Serialization;
using Mimic.Expressions;
using Mimic.Setup;

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

        return new MimicException($"ArgumentMatcher for argument '{argumentExpression}' is unmatchable. Due to an implicit conversion of the argument from type '{formattedFromType}' to type '{formattedToType}' which is an incompatible assignment");
    }

    internal static MimicException NoMatchingSetup(Invocation invocation)
    {
        return new MimicException($"Invocation of '{invocation}' failed. Mimic is configured in Strict mode so all invocations must match a setup or this error will be thrown");
    }

    internal static MimicException SetupNotMatched(SetupBase setup)
    {
        return new MimicException($"Setup '{setup}' which was marked as verifiable has not been matched");
    }

    internal static MimicException SequenceSetupNotMatched(SetupBase setup, int remaining)
    {
        return new MimicException($"Setup '{setup}' with sequence which was marked as verifiable has not been matched ({remaining} {(remaining == 1 ? "seqeuence" : "seqeuences")} result has not been used).");
    }

    internal static MimicException ReturnRequired(Invocation invocation)
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

    internal static MimicException ExecutionLimitExceeded(MethodCallSetup setup, int limit, int count)
    {
        return new MimicException($"Setup '{setup}' has been limited to {limit} {(limit == 1 ? "execution" : "executions")} but was actually executed {count} times");
    }

    internal static MimicException OutExpressionMustBeConstantValue() => new("Out expression must evaluate to a constant value");

    internal static Exception NoMatchingInvocations<T>(
        Mimic<T> mimic,
        LambdaExpression expression,
        CallCount expectedCallCount,
        int actualCallCount,
        string? failureMessage)
        where T : class
    {
        var stringBuilder = new ValueStringBuilder(stackalloc char[512]);

        stringBuilder.Append(failureMessage ?? "Verification failed with incorrect matching invocations");
        stringBuilder.Append(Environment.NewLine.AsSpan());

        stringBuilder.Append(expectedCallCount.GetExceptionMessage(actualCallCount).AsSpan());

        var evaluatedExpression = ExpressionEvaluator.PartiallyEvaluate(expression, true);
        stringBuilder.Append(evaluatedExpression.ToString());
        stringBuilder.Append(Environment.NewLine.AsSpan());

        stringBuilder.Append($"Actual invocations on {mimic} ({expression.Parameters[0].Name}):".AsSpan());
        stringBuilder.Append(Environment.NewLine.AsSpan());

        var invocations = mimic.Invocations.ToList();
        if (invocations.Any())
        {
            foreach (var invocation in invocations)
            {
                stringBuilder.Append($"    {invocation}");
                stringBuilder.Append(Environment.NewLine.AsSpan());
            }
        }
        else
        {
            stringBuilder.Append($"    There are zero invocations..");
            stringBuilder.Append(Environment.NewLine.AsSpan());
        }

        return new MimicException(stringBuilder.ToString());
    }

    internal static Exception UnverifiedInvocations<T>(Mimic<T> mimic, List<Invocation> unverifiedInvocations)
        where T : class
    {
        var stringBuilder = new ValueStringBuilder(stackalloc char[512]);

        stringBuilder.Append($"{mimic}: Verification failed due to the following unverified invocations:");
        stringBuilder.Append(Environment.NewLine.AsSpan());

        foreach (var unverifiedInvocation in unverifiedInvocations)
        {
            stringBuilder.Append($"    {unverifiedInvocation}");
            stringBuilder.Append(Environment.NewLine.AsSpan());
        }

        return new MimicException(stringBuilder.ToString());
    }
}
