using Mimic.Expressions;
using Mimic.Resources;
using Mimic.Setup;

namespace Mimic.Exceptions;

[PublicAPI]
public class MimicException : Exception
{
    public Reason Reason { get; }

    public MimicException(Reason reason, string? message)
        : base(message)
    {
        Reason = reason;
    }

    public MimicException(Reason reason, string? message, Exception? innerException)
        : base(message, innerException)
    {
        Reason = reason;
    }

    #region UsageError

    internal static MimicException UnmatchableArgumentMatcher(Expression argumentExpression, Type expectedType) =>
        new(Reason.UsageError, Strings.FormatUnmatchableArgumentMatcher(
            argumentExpression,
            TypeNameFormatter.GetFormattedName(argumentExpression.Type),
            TypeNameFormatter.GetFormattedName(expectedType)));

    internal static MimicException WrongCallbackParameterCount(int expectedCount, int actualCount) =>
        new(Reason.UsageError, Strings.FormatWrongCallbackParameterCount(expectedCount, actualCount));

    internal static MimicException WrongCallbackParameterTypes(ParameterInfo[] expectedParameters, ParameterInfo[] actualParameters)
    {
        return new MimicException(Reason.UsageError, Strings.FormatWrongCallbackParameterTypes(
            ToParameterTypeList(expectedParameters),
            ToParameterTypeList(actualParameters)));

        static string ToParameterTypeList(ParameterInfo[] parameters)
        {
            var stringBuilder = new ValueStringBuilder(stackalloc char[256]);

            for (int i = 0; i < parameters.Length; i++)
            {
                if (i != 0)
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

    internal static MimicException WrongCallbackReturnType(Type expectedType, Type? actualType) =>
        new(Reason.UsageError, Strings.FormatWrongCallbackReturnType(
            TypeNameFormatter.GetFormattedName(expectedType),
            actualType is null ? "a void return type" : $"return type '{TypeNameFormatter.GetFormattedName(actualType)}'"));

    internal static MimicException WrongCallbackReturnType() => new(Reason.UsageError, Strings.WrongCallbackReturnTypeNonVoid);

    internal static MimicException ObjectNotCreatedByMimic() => new(Reason.UsageError, Strings.ObjectNotCreatedByMimic);

    #endregion

    #region IncompatibleMimicType

    internal static MimicException TypeCannotBeMimicked(Type type, Exception? innerException = null) =>
        new(Reason.IncompatibleMimicType, Strings.FormatTypeCannotBeMimicked(TypeNameFormatter.GetFormattedName(type)), innerException);

    internal static MimicException NoConstructorWithMatchingArguments(Type type, Exception? innerException = null) =>
        new (Reason.IncompatibleMimicType,
            Strings.FormatNoConstructorWithMatchingArguments(TypeNameFormatter.GetFormattedName(type)),
            innerException);

    internal static MimicException MethodNotAccessibleByProxyGenerator(MethodInfo method, string messageFromProxyGenerator) =>
        new(Reason.IncompatibleMimicType, Strings.FormatMethodNotAccessibleByProxyGenerator(
            method.Name,
            TypeNameFormatter.GetFormattedName(method.DeclaringType!),
            messageFromProxyGenerator));

    #endregion

    #region UnsupportedExpression

    internal static MimicException ExpressionNotProperty(Expression expression) =>
        new(Reason.UnsupportedExpression, Strings.FormatExpressionNotProperty(expression));

    internal static MimicException ExpressionNotPropertyGetter(PropertyInfo property) =>
        new(Reason.UnsupportedExpression, Strings.FormatExpressionNotPropertyGetter(
            TypeNameFormatter.GetFormattedName(property.DeclaringType!),
            property.Name));

    internal static MimicException ExpressionNotPropertySetter(PropertyInfo property) =>
        new(Reason.UnsupportedExpression, Strings.FormatExpressionNotPropertySetter(
            TypeNameFormatter.GetFormattedName(property.DeclaringType!),
            property.Name));

    internal static MimicException ExpressionNotPropertySetter(LambdaExpression expression) =>
        new(Reason.UnsupportedExpression, Strings.FormatExpressionNotPropertySetterLamba(expression));

    internal static MimicException OutExpressionMustBeConstantValue() => new(Reason.UnsupportedExpression, Strings.OutExpressionMustBeConstantValue);

    internal static MimicException ExtensionMethodIsNotOverridable(Expression expression) =>
        new(Reason.UnsupportedExpression, Strings.FormatExtensionMethodIsNotOverridable(expression));

    internal static MimicException StaticMethodIsNotOverridable(Expression expression) =>
        new(Reason.UnsupportedExpression, Strings.FormatStaticMethodIsNotOverridable(expression));

    internal static MimicException MethodIsNotOverridable(Expression expression) =>
        new(Reason.UnsupportedExpression, Strings.FormatMethodIsNotOverridable(expression));

    internal static MimicException NestedMethodCallIsNotAllowed(Expression expression) =>
        new(Reason.UnsupportedExpression, Strings.FormatNestedMethodCallIsNotAllowed(expression));

    internal static MimicException UnsupportedExpressionType(Expression expression) =>
        new(Reason.UnsupportedExpression, Strings.FormatUnsupportedExpressionType(expression));

    internal static MimicException UnsupportedArgumentExpressionType(Expression expression) =>
        new(Reason.UnsupportedExpression, Strings.FormatUnsupportedArgumentExpressionType(expression));

    internal static MimicException UnableToDetermineArgumentMatchers(string expression) =>
        new(Reason.UnsupportedExpression, Strings.FormatUnableToDetermineArgumentMatchers(expression));

    internal static MimicException MemberNotInterceptable(string expression) =>
        new(Reason.UnsupportedExpression, Strings.FormatMemberNotInterceptable(expression));

    internal static MimicException ExpressionThrewAnException(string expression) =>
        new(Reason.UnsupportedExpression, Strings.FormatExpressionThrewAnException(expression));

    #endregion

    #region ExpectationFailed

    internal static MimicException NoMatchingSetup(Invocation invocation) =>
        new(Reason.ExpectationFailed, Strings.FormatNoMatchingSetup(invocation));

    internal static MimicException ExpectedSetupNotMatched(SetupBase setup) =>
        new(Reason.ExpectationFailed, Strings.FormatExpectedSetupNotMatched(setup));

    internal static MimicException ExpectedSequenceSetupNotMatched(SetupBase setup, int remaining) =>
        new(Reason.ExpectationFailed, Strings.FormatExpectedSequenceSetupNotMatched(setup, remaining));

    internal static MimicException ReturnRequired(Invocation invocation) =>
        new(Reason.ExpectationFailed, Strings.FormatReturnRequired(invocation));

    internal static MimicException ExecutionLimitExceeded(MethodCallSetup setup, int limit, int count) =>
        new(Reason.ExpectationFailed, Strings.FormatExecutionLimitExceeded(setup, limit, count));

    internal static Exception NoMatchingInvocations<T>(
        Mimic<T> mimic, LambdaExpression expression, CallCount expectedCallCount, int actualCallCount, string? failureMessage)
        where T : class
    {
        var stringBuilder = new ValueStringBuilder(stackalloc char[512]);

        stringBuilder.Append(failureMessage ?? Strings.NoMatchingInvocationsDefaultFailureMessage);
        stringBuilder.Append(Environment.NewLine.AsSpan());

        stringBuilder.Append(expectedCallCount.GetExceptionMessage(actualCallCount).AsSpan());

        var evaluatedExpression = ExpressionEvaluator.PartiallyEvaluate(expression, true);
        stringBuilder.Append(evaluatedExpression.ToString());
        stringBuilder.Append(Environment.NewLine.AsSpan());

        stringBuilder.Append(Strings.FormatNoMatchingInvocationsActualInvocations(mimic, expression.Parameters[0].Name));
        stringBuilder.Append(Environment.NewLine.AsSpan());

        var invocations = mimic.Invocations.ToList();
        if (invocations.Count > 0)
        {
            foreach (var invocation in invocations)
            {
                stringBuilder.Append($"    {invocation}");
                stringBuilder.Append(Environment.NewLine.AsSpan());
            }
        }
        else
        {
            stringBuilder.Append($"    {Strings.NoMatchingInvocationsThereAreZeroInvocations}");
            stringBuilder.Append(Environment.NewLine.AsSpan());
        }

        return new MimicException(Reason.ExpectationFailed, stringBuilder.ToString());
    }

    internal static Exception UnverifiedInvocations<T>(Mimic<T> mimic, List<Invocation> unverifiedInvocations)
        where T : class
    {
        var stringBuilder = new ValueStringBuilder(stackalloc char[512]);

        stringBuilder.Append(Strings.FormatUnverifiedInvocations(mimic));
        stringBuilder.Append(Environment.NewLine.AsSpan());

        foreach (var unverifiedInvocation in unverifiedInvocations)
        {
            stringBuilder.Append($"    {unverifiedInvocation}");
            stringBuilder.Append(Environment.NewLine.AsSpan());
        }

        return new MimicException(Reason.ExpectationFailed, stringBuilder.ToString());
    }

    #endregion
}
