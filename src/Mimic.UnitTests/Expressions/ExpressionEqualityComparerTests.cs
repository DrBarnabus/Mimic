using System.Linq.Expressions;
using Mimic.Expressions;

namespace Mimic.UnitTests.Expressions;

public class ExpressionEqualityComparerTests
{
    private readonly ExpressionEqualityComparer _comparer = ExpressionEqualityComparer.Default;

    [Fact]
    public void Equals_WhenFirstExpressionIsNullAndSecondIsNotNull_ShouldReturnFalse()
    {
        Expression? expr1 = null;
        var expr2 = Expression.Constant(1);

        _comparer.Equals(expr1, expr2).ShouldBeFalse();
    }

    [Fact]
    public void Equals_WhenFirstExpressionIsNotNullAndSecondIsNull_ShouldReturnFalse()
    {
        var expr1 = Expression.Constant(1);
        Expression? expr2 = null;

        _comparer.Equals(expr1, expr2).ShouldBeFalse();
    }

    [Fact]
    public void Equals_WhenBothExpressionsAreNull_ShouldReturnTrue()
    {
        Expression? expr1 = null;
        Expression? expr2 = null;

        _comparer.Equals(expr1, expr2).ShouldBeTrue();
    }

    [Fact]
    public void Equals_WhenExpressionsAreTheSameReference_ShouldReturnTrue()
    {
        var expr = Expression.Constant(1);

        _comparer.Equals(expr, expr).ShouldBeTrue();
    }

    [Fact]
    public void Equals_WhenConstantExpressionsHaveSameValue_ShouldReturnTrue()
    {
        var expr1 = Expression.Constant(42);
        var expr2 = Expression.Constant(42);

        _comparer.Equals(expr1, expr2).ShouldBeTrue();
    }

    [Fact]
    public void Equals_WhenConstantExpressionsHaveDifferentValues_ShouldReturnFalse()
    {
        var expr1 = Expression.Constant(42);
        var expr2 = Expression.Constant(100);

        _comparer.Equals(expr1, expr2).ShouldBeFalse();
    }

    [Fact]
    public void Equals_WhenBinaryExpressionsHaveSameOperandsAndMethod_ShouldReturnTrue()
    {
        var left1 = Expression.Constant(5);
        var right1 = Expression.Constant(3);
        var expr1 = Expression.Add(left1, right1);
        var left2 = Expression.Constant(5);
        var right2 = Expression.Constant(3);
        var expr2 = Expression.Add(left2, right2);

        _comparer.Equals(expr1, expr2).ShouldBeTrue();
    }

    [Fact]
    public void Equals_WhenBinaryExpressionsHaveDifferentMethods_ShouldReturnFalse()
    {
        var left = Expression.Constant(5);
        var right = Expression.Constant(3);
        var expr1 = Expression.Add(left, right);
        var expr2 = Expression.Subtract(left, right);

        _comparer.Equals(expr1, expr2).ShouldBeFalse();
    }

    [Fact]
    public void Equals_WhenUnaryExpressionsHaveSameOperandAndMethod_ShouldReturnTrue()
    {
        var operand1 = Expression.Constant(true);
        var expr1 = Expression.Not(operand1);
        var operand2 = Expression.Constant(true);
        var expr2 = Expression.Not(operand2);

        _comparer.Equals(expr1, expr2).ShouldBeTrue();
    }

    [Fact]
    public void Equals_WhenUnaryExpressionsHaveDifferentMethods_ShouldReturnFalse()
    {
        var operand = Expression.Constant(true);
        var expr1 = Expression.Not(operand);
        var expr2 = Expression.Convert(operand, typeof(object));

        _comparer.Equals(expr1, expr2).ShouldBeFalse();
    }

    [Fact]
    public void Equals_WhenLambdaExpressionsHaveSameBodyAndParameters_ShouldReturnTrue()
    {
        Expression<Func<int, int>> lambda1 = x => x + 1;
        Expression<Func<int, int>> lambda2 = x => x + 1;

        _comparer.Equals(lambda1, lambda2).ShouldBeTrue();
    }

    [Fact]
    public void Equals_WhenLambdaExpressionsHaveDifferentBodies_ShouldReturnFalse()
    {
        Expression<Func<int, int>> lambda1 = x => x + 1;
        Expression<Func<int, int>> lambda2 = x => x * 2;

        _comparer.Equals(lambda1, lambda2).ShouldBeFalse();
    }

    [Fact]
    public void Equals_WhenMethodCallExpressionsHaveSameMethodAndArguments_ShouldReturnTrue()
    {
        string str = "test";
        var expr1 = Expression.Call(Expression.Constant(str), typeof(string).GetMethod("ToUpper", Type.EmptyTypes)!);
        var expr2 = Expression.Call(Expression.Constant(str), typeof(string).GetMethod("ToUpper", Type.EmptyTypes)!);

        _comparer.Equals(expr1, expr2).ShouldBeTrue();
    }

    [Fact]
    public void Equals_WhenMethodCallExpressionsHaveDifferentMethods_ShouldReturnFalse()
    {
        string str = "test";
        var expr1 = Expression.Call(Expression.Constant(str), typeof(string).GetMethod("ToUpper", Type.EmptyTypes)!);
        var expr2 = Expression.Call(Expression.Constant(str), typeof(string).GetMethod("ToLower", Type.EmptyTypes)!);

        _comparer.Equals(expr1, expr2).ShouldBeFalse();
    }

    [Fact]
    public void Equals_WhenNewExpressionsHaveSameConstructorAndArguments_ShouldReturnTrue()
    {
        var expr1 = Expression.New(
            typeof(DateTime).GetConstructor([typeof(int), typeof(int), typeof(int)])!,
            Expression.Constant(2025), Expression.Constant(6), Expression.Constant(17));
        var expr2 = Expression.New(
            typeof(DateTime).GetConstructor([typeof(int), typeof(int), typeof(int)])!,
            Expression.Constant(2025), Expression.Constant(6), Expression.Constant(17));

        _comparer.Equals(expr1, expr2).ShouldBeTrue();
    }

    [Fact]
    public void Equals_WhenNewExpressionsHaveDifferentArguments_ShouldReturnFalse()
    {
        var expr1 = Expression.New(
            typeof(DateTime).GetConstructor([typeof(int), typeof(int), typeof(int)])!,
            Expression.Constant(2025), Expression.Constant(6), Expression.Constant(17));
        var expr2 = Expression.New(
            typeof(DateTime).GetConstructor([typeof(int), typeof(int), typeof(int)])!,
            Expression.Constant(2025), Expression.Constant(6), Expression.Constant(18));

        _comparer.Equals(expr1, expr2).ShouldBeFalse();
    }

    [Fact]
    public void Equals_WhenConditionalExpressionsHaveSameTestAndBranches_ShouldReturnTrue()
    {
        var expr1 = Expression.Condition(
            Expression.Constant(true),
            Expression.Constant("yes"),
            Expression.Constant("no"));
        var expr2 = Expression.Condition(
            Expression.Constant(true),
            Expression.Constant("yes"),
            Expression.Constant("no"));

        _comparer.Equals(expr1, expr2).ShouldBeTrue();
    }

    [Fact]
    public void Equals_WhenConditionalExpressionsHaveDifferentBranches_ShouldReturnFalse()
    {
        var expr1 = Expression.Condition(
            Expression.Constant(true),
            Expression.Constant("yes"),
            Expression.Constant("no"));
        var expr2 = Expression.Condition(
            Expression.Constant(true),
            Expression.Constant("yes"),
            Expression.Constant("different"));

        _comparer.Equals(expr1, expr2).ShouldBeFalse();
    }

    [Fact]
    public void Equals_WhenTypeBinaryExpressionsHaveSameTypeOperandAndExpression_ShouldReturnTrue()
    {
        var expr = Expression.Parameter(typeof(object), "x");
        var expr1 = Expression.TypeIs(expr, typeof(string));
        var expr2 = Expression.TypeIs(expr, typeof(string));

        _comparer.Equals(expr1, expr2).ShouldBeTrue();
    }

    [Fact]
    public void Equals_WhenTypeBinaryExpressionsHaveDifferentTypes_ShouldReturnFalse()
    {
        var expr = Expression.Parameter(typeof(object), "x");
        var expr1 = Expression.TypeIs(expr, typeof(string));
        var expr2 = Expression.TypeIs(expr, typeof(int));

        _comparer.Equals(expr1, expr2).ShouldBeFalse();
    }

    [Fact]
    public void Equals_WhenMemberExpressionsHaveSameMemberAndExpression_ShouldReturnTrue()
    {
        var paramType = typeof(DateTime);
        var param = Expression.Parameter(paramType, "d");
        var yearProp = paramType.GetProperty("Year")!;
        var expr1 = Expression.Property(param, yearProp);
        var expr2 = Expression.Property(param, yearProp);

        _comparer.Equals(expr1, expr2).ShouldBeTrue();
    }

    [Fact]
    public void Equals_WhenMemberExpressionsHaveDifferentMembers_ShouldReturnFalse()
    {
        var paramType = typeof(DateTime);
        var param = Expression.Parameter(paramType, "d");
        var yearProp = paramType.GetProperty("Year")!;
        var monthProp = paramType.GetProperty("Month")!;
        var expr1 = Expression.Property(param, yearProp);
        var expr2 = Expression.Property(param, monthProp);

        _comparer.Equals(expr1, expr2).ShouldBeFalse();
    }

    [Fact]
    public void Equals_WhenNewArrayExpressionsHaveSameTypeAndExpressions_ShouldReturnTrue()
    {
        var expr1 = Expression.NewArrayInit(typeof(int), Expression.Constant(1), Expression.Constant(2));
        var expr2 = Expression.NewArrayInit(typeof(int), Expression.Constant(1), Expression.Constant(2));

        _comparer.Equals(expr1, expr2).ShouldBeTrue();
    }

    [Fact]
    public void Equals_WhenNewArrayExpressionsHaveDifferentExpressions_ShouldReturnFalse()
    {
        var expr1 = Expression.NewArrayInit(typeof(int), Expression.Constant(1), Expression.Constant(2));
        var expr2 = Expression.NewArrayInit(typeof(int), Expression.Constant(1), Expression.Constant(3));

        _comparer.Equals(expr1, expr2).ShouldBeFalse();
    }

    [Fact]
    public void Equals_WhenIndexExpressionsHaveSameObjectIndexerAndArguments_ShouldReturnTrue()
    {
        var array = Expression.Parameter(typeof(int[]), "array");
        var expr1 = Expression.ArrayAccess(array, Expression.Constant(0));
        var expr2 = Expression.ArrayAccess(array, Expression.Constant(0));

        _comparer.Equals(expr1, expr2).ShouldBeTrue();
    }

    [Fact]
    public void Equals_WhenIndexExpressionsHaveDifferentArguments_ShouldReturnFalse()
    {
        var array = Expression.Parameter(typeof(int[]), "array");
        var expr1 = Expression.ArrayAccess(array, Expression.Constant(0));
        var expr2 = Expression.ArrayAccess(array, Expression.Constant(1));

        _comparer.Equals(expr1, expr2).ShouldBeFalse();
    }

    [Fact]
    public void Equals_WhenInvocationExpressionsHaveSameExpressionAndArguments_ShouldReturnTrue()
    {
        var param = Expression.Parameter(typeof(int), "x");
        var lambda = Expression.Lambda<Func<int, int>>(
            Expression.Add(param, Expression.Constant(1)),
            param);
        var expr1 = Expression.Invoke(lambda, Expression.Constant(1));
        var expr2 = Expression.Invoke(lambda, Expression.Constant(1));

        _comparer.Equals(expr1, expr2).ShouldBeTrue();
    }

    [Fact]
    public void Equals_WhenInvocationExpressionsHaveDifferentArguments_ShouldReturnFalse()
    {
        var param = Expression.Parameter(typeof(int), "x");
        var lambda = Expression.Lambda<Func<int, int>>(
            Expression.Add(param, Expression.Constant(1)),
            param);
        var expr1 = Expression.Invoke(lambda, Expression.Constant(1));
        var expr2 = Expression.Invoke(lambda, Expression.Constant(2));

        _comparer.Equals(expr1, expr2).ShouldBeFalse();
    }

    [Fact]
    public void Equals_WhenMemberInitExpressionsHaveSameNewExpressionAndBindings_ShouldReturnTrue()
    {
        var type = typeof(TestClass);
        var ctor = type.GetConstructor(Type.EmptyTypes)!;
        var prop = type.GetProperty(nameof(TestClass.Value))!;
        var expr1 = Expression.MemberInit(
            Expression.New(ctor),
            Expression.Bind(prop, Expression.Constant(42)));
        var expr2 = Expression.MemberInit(
            Expression.New(ctor),
            Expression.Bind(prop, Expression.Constant(42)));

        _comparer.Equals(expr1, expr2).ShouldBeTrue();
    }

    [Fact]
    public void Equals_WhenMemberInitExpressionsHaveDifferentBindings_ShouldReturnFalse()
    {
        var type = typeof(TestClass);
        var ctor = type.GetConstructor(Type.EmptyTypes)!;
        var prop = type.GetProperty(nameof(TestClass.Value))!;
        var expr1 = Expression.MemberInit(
            Expression.New(ctor),
            Expression.Bind(prop, Expression.Constant(42)));
        var expr2 = Expression.MemberInit(
            Expression.New(ctor),
            Expression.Bind(prop, Expression.Constant(100)));

        _comparer.Equals(expr1, expr2).ShouldBeFalse();
    }

    [Fact]
    public void Equals_WhenListInitExpressionsHaveSameNewExpressionAndInitializers_ShouldReturnTrue()
    {
        var list = Expression.New(typeof(List<int>));
        var addMethod = typeof(List<int>).GetMethod("Add")!;
        var expr1 = Expression.ListInit(list,
            Expression.ElementInit(addMethod, Expression.Constant(1)),
            Expression.ElementInit(addMethod, Expression.Constant(2)));
        var expr2 = Expression.ListInit(list,
            Expression.ElementInit(addMethod, Expression.Constant(1)),
            Expression.ElementInit(addMethod, Expression.Constant(2)));

        _comparer.Equals(expr1, expr2).ShouldBeTrue();
    }

    [Fact]
    public void Equals_WhenListInitExpressionsHaveDifferentInitializers_ShouldReturnFalse()
    {
        var list = Expression.New(typeof(List<int>));
        var addMethod = typeof(List<int>).GetMethod("Add")!;
        var expr1 = Expression.ListInit(list,
            Expression.ElementInit(addMethod, Expression.Constant(1)),
            Expression.ElementInit(addMethod, Expression.Constant(2)));
        var expr2 = Expression.ListInit(list,
            Expression.ElementInit(addMethod, Expression.Constant(1)),
            Expression.ElementInit(addMethod, Expression.Constant(3)));

        _comparer.Equals(expr1, expr2).ShouldBeFalse();
    }

    [Fact]
    public void Equals_WhenMemberAssignmentBindingsAreSame_ShouldReturnTrue()
    {
        var type = typeof(TestComplexClass);
        var ctor = type.GetConstructor(Type.EmptyTypes)!;
        var valueProp = type.GetProperty(nameof(TestComplexClass.Value))!;
        var assign1 = Expression.MemberInit(
            Expression.New(ctor),
            Expression.Bind(valueProp, Expression.Constant(42)));
        var assign2 = Expression.MemberInit(
            Expression.New(ctor),
            Expression.Bind(valueProp, Expression.Constant(42)));

        _comparer.Equals(assign1, assign2).ShouldBeTrue();
    }

    [Fact]
    public void Equals_WhenMemberMemberBindingsAreSame_ShouldReturnTrue()
    {
        var type = typeof(TestComplexClass);
        var ctor = type.GetConstructor(Type.EmptyTypes)!;
        var nestedProp = type.GetProperty(nameof(TestComplexClass.Nested))!;
        var nestedValueProp = typeof(TestClass).GetProperty(nameof(TestClass.Value))!;
        var memberBinding1 = Expression.MemberInit(
            Expression.New(ctor),
            Expression.MemberBind(nestedProp,
                Expression.Bind(nestedValueProp, Expression.Constant(42))));
        var memberBinding2 = Expression.MemberInit(
            Expression.New(ctor),
            Expression.MemberBind(nestedProp,
                Expression.Bind(nestedValueProp, Expression.Constant(42))));

        _comparer.Equals(memberBinding1, memberBinding2).ShouldBeTrue();
    }

    [Fact]
    public void Equals_WhenMemberListBindingsAreSame_ShouldReturnTrue()
    {
        var type = typeof(TestComplexClass);
        var ctor = type.GetConstructor(Type.EmptyTypes)!;
        var listProp = type.GetProperty(nameof(TestComplexClass.List))!;
        var addMethod = typeof(List<int>).GetMethod("Add")!;
        var listBinding1 = Expression.MemberInit(
            Expression.New(ctor),
            Expression.ListBind(listProp,
                Expression.ElementInit(addMethod, Expression.Constant(1))));
        var listBinding2 = Expression.MemberInit(
            Expression.New(ctor),
            Expression.ListBind(listProp,
                Expression.ElementInit(addMethod, Expression.Constant(1))));

        _comparer.Equals(listBinding1, listBinding2).ShouldBeTrue();
    }

    [Fact]
    public void Equals_WhenQuotedExpressionsHaveSameOperands_ShouldReturnTrue()
    {
        var param = Expression.Parameter(typeof(int), "x");
        var lambda1 = Expression.Lambda<Func<int, int>>(
            Expression.Add(param, Expression.Constant(1)),
            param);
        var lambda2 = Expression.Lambda<Func<int, int>>(
            Expression.Add(param, Expression.Constant(1)),
            param);
        var quote1 = Expression.Quote(lambda1);
        var quote2 = Expression.Quote(lambda2);

        _comparer.Equals(quote1, quote2).ShouldBeTrue();
    }

    [Fact]
    public void Equals_WhenQuotedExpressionsHaveDifferentOperands_ShouldReturnFalse()
    {
        var param = Expression.Parameter(typeof(int), "x");
        var lambda1 = Expression.Lambda<Func<int, int>>(
            Expression.Add(param, Expression.Constant(1)),
            param);
        var lambda2 = Expression.Lambda<Func<int, int>>(
            Expression.Add(param, Expression.Constant(2)),
            param);
        var quote1 = Expression.Quote(lambda1);
        var quote2 = Expression.Quote(lambda2);

        _comparer.Equals(quote1, quote2).ShouldBeFalse();
    }

    [Fact]
    public void Equals_WhenCaptureEvaluatorHandlesMemberExpressionsWithSameValues_ShouldReturnTrue()
    {
        int captured = 42;
        Expression<Func<int>> expr1 = () => captured;
        Expression<Func<int>> expr2 = () => captured;

        _comparer.Equals(expr1, expr2).ShouldBeTrue();
    }

    [Fact]
    public void Equals_WhenCaptureEvaluatorHandlesMemberExpressionsWithDifferentValues_ShouldReturnFalse()
    {
        int captured = 42;
        Expression<Func<int>> expr1 = () => captured;
        Expression<Func<int>> expr2 = () => 100;

        _comparer.Equals(expr1, expr2).ShouldBeFalse();
    }

    [Fact]
    public void Equals_WhenCaptureEvaluatorHandlesUnaryOperationsWithSameValues_ShouldReturnTrue()
    {
        int captured = 42;
        Expression<Func<int>> expr1 = () => -captured;
        Expression<Func<int>> expr2 = () => -captured;

        _comparer.Equals(expr1, expr2).ShouldBeTrue();
    }

    [Fact]
    public void Equals_WhenCaptureEvaluatorHandlesUnaryOperationsWithDifferentValues_ShouldReturnFalse()
    {
        int captured = 42;
        Expression<Func<int>> expr1 = () => -captured;
        Expression<Func<int>> expr2 = () => -100;

        _comparer.Equals(expr1, expr2).ShouldBeFalse();
    }

    [Fact]
    public void GetHashCode_WhenCalledWithExpression_ShouldReturnUnderlyingHashCode()
    {
        var expr = Expression.Constant(42);
        var expected = expr.GetHashCode();

        _comparer.GetHashCode(expr).ShouldBe(expected);
    }

    private class TestClass
    {
        public int Value { get; set; }
    }

    private class TestComplexClass
    {
        public int Value { get; set; }
        public List<int> List { get; set; } = new();
        public TestClass Nested { get; set; } = new();
    }
}
