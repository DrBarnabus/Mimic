using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using Mimic.Core;

namespace Mimic.Expressions;

internal sealed class ExpressionEqualityComparer : IEqualityComparer<Expression>
{
    internal static readonly ExpressionEqualityComparer Default = new();

    [ThreadStatic]
    private static int _quoteDepth;

    public bool Equals(Expression? x, Expression? y)
    {
        if (ReferenceEquals(x, y))
            return true;

        if (x is null || y is null)
            return false;

        if (x is MemberExpression && _quoteDepth == 0)
            x = CaptureEvaluator.Default.Visit(x);

        if (y is MemberExpression && _quoteDepth == 0)
            y = CaptureEvaluator.Default.Visit(y);

        if (x.NodeType != y.NodeType)
            return false;

        if (x.NodeType == ExpressionType.Quote)
        {
            _quoteDepth++;

            try
            {
                return Equals((UnaryExpression)x, (UnaryExpression)y);
            }
            finally
            {
                _quoteDepth--;
            }
        }

        return x.NodeType switch
        {
            ExpressionType.Negate
                or ExpressionType.NegateChecked
                or ExpressionType.Not
                or ExpressionType.Convert
                or ExpressionType.ConvertChecked
                or ExpressionType.ArrayLength
                or ExpressionType.TypeAs
                or ExpressionType.UnaryPlus => Equals((UnaryExpression)x, (UnaryExpression)y),
            ExpressionType.Add
                or ExpressionType.AddChecked
                or ExpressionType.Assign
                or ExpressionType.Subtract
                or ExpressionType.SubtractChecked
                or ExpressionType.Multiply
                or ExpressionType.MultiplyChecked
                or ExpressionType.Divide
                or ExpressionType.Modulo
                or ExpressionType.And
                or ExpressionType.AndAlso
                or ExpressionType.Or
                or ExpressionType.OrElse
                or ExpressionType.LessThan
                or ExpressionType.LessThanOrEqual
                or ExpressionType.GreaterThan
                or ExpressionType.GreaterThanOrEqual
                or ExpressionType.Equal
                or ExpressionType.NotEqual
                or ExpressionType.Coalesce
                or ExpressionType.ArrayIndex
                or ExpressionType.RightShift
                or ExpressionType.LeftShift
                or ExpressionType.ExclusiveOr
                or ExpressionType.Power => Equals((BinaryExpression)x, (BinaryExpression)y),
            ExpressionType.TypeIs => Equals((TypeBinaryExpression)x, (TypeBinaryExpression)y),
            ExpressionType.Conditional => Equals((ConditionalExpression)x, (ConditionalExpression)y),
            ExpressionType.Constant => Equals(((ConstantExpression)x).Value, ((ConstantExpression)y).Value),
            ExpressionType.Parameter => ((ParameterExpression)x).Type == ((ParameterExpression)y).Type,
            ExpressionType.MemberAccess => Equals((MemberExpression)x, (MemberExpression)y),
            ExpressionType.Call => Equals((MethodCallExpression)x, (MethodCallExpression)y),
            ExpressionType.Lambda => Equals((LambdaExpression)x, (LambdaExpression)y),
            ExpressionType.New => Equals((NewExpression)x, (NewExpression)y),
            ExpressionType.NewArrayInit
                or ExpressionType.NewArrayBounds => Equals((NewArrayExpression)x, (NewArrayExpression)y),
            ExpressionType.Index => Equals((IndexExpression)x, (IndexExpression)y),
            ExpressionType.Invoke => Equals((InvocationExpression)x, (InvocationExpression)y),
            ExpressionType.MemberInit => Equals((MemberInitExpression)x, (MemberInitExpression)y),
            ExpressionType.ListInit => Equals((ListInitExpression)x, (ListInitExpression)y),
            _ => false
        };
    }

    public int GetHashCode(Expression obj) => obj.GetHashCode();

    private bool Equals(UnaryExpression x, UnaryExpression y) =>
        x.Method == y.Method && Equals(x.Operand, y.Operand);

    private bool Equals(BinaryExpression x, BinaryExpression y) =>
        x.Method == y.Method && Equals(x.Left, y.Left) && Equals(x.Right, y.Right) && Equals((Expression)x.Conversion!, y.Conversion!);

    private bool Equals(TypeBinaryExpression x, TypeBinaryExpression y) =>
        x.TypeOperand == y.TypeOperand && Equals(x.Expression, y.Expression);

    private bool Equals(ConditionalExpression x, ConditionalExpression y) =>
        Equals(x.Test, y.Test) && Equals(x.IfTrue, y.IfTrue) && Equals(x.IfFalse, y.IfFalse);

    private bool Equals(MemberExpression x, MemberExpression y) =>
        x.Member == y.Member && Equals(x.Expression, y.Expression);

    private bool Equals(MethodCallExpression x, MethodCallExpression y) =>
        x.Method == y.Method && Equals(x.Object, y.Object) && SequenceEqual(x.Arguments, y.Arguments, Equals);

    private bool Equals(LambdaExpression x, LambdaExpression y) =>
        x.GetType() == y.GetType() && Equals(x.Body, y.Body) && SequenceEqual(x.Parameters, y.Parameters, (x2, y2) => x2.Type == y2.Type);

    private bool Equals(NewExpression x, NewExpression y) =>
        x.Constructor == y.Constructor && SequenceEqual(x.Arguments, y.Arguments, Equals);

    private bool Equals(NewArrayExpression x, NewArrayExpression y) =>
        x.Type == y.Type && SequenceEqual(x.Expressions, y.Expressions, Equals);

    private bool Equals(IndexExpression x, IndexExpression y) =>
        Equals(x.Object, x.Object) && Equals(x.Indexer, y.Indexer) && SequenceEqual(x.Arguments, x.Arguments, Equals);

    private bool Equals(InvocationExpression x, InvocationExpression y) =>
        Equals(x.Expression, y.Expression) && SequenceEqual(x.Arguments, y.Arguments, Equals);

    private bool Equals(MemberInitExpression x, MemberInitExpression y) =>
        Equals(x.NewExpression, y.NewExpression) && SequenceEqual(x.Bindings, y.Bindings, Equals);

    private bool Equals(ListInitExpression x, ListInitExpression y) =>
        Equals(x.NewExpression, y.NewExpression) && SequenceEqual(x.Initializers, y.Initializers, Equals);


    private bool Equals(MemberBinding x, MemberBinding y)
    {
        if (x.BindingType != y.BindingType || x.Member != y.Member)
            return false;

        return x.BindingType switch
        {
            MemberBindingType.Assignment => Equals(((MemberAssignment)x).Expression, ((MemberAssignment)y).Expression),
            MemberBindingType.MemberBinding => SequenceEqual(((MemberMemberBinding)x).Bindings, ((MemberMemberBinding)y).Bindings, Equals),
            MemberBindingType.ListBinding => SequenceEqual(((MemberListBinding)x).Initializers, ((MemberListBinding)y).Initializers, Equals),
            _ => throw new ArgumentOutOfRangeException(nameof(x.BindingType))
        };
    }

    private bool Equals(ElementInit x, ElementInit y) =>
        x.AddMethod == y.AddMethod && SequenceEqual(x.Arguments, y.Arguments, Equals);

    private static bool SequenceEqual<TSource>(IReadOnlyList<TSource> x, IReadOnlyList<TSource> y, Func<TSource, TSource, bool> comparer)
    {
        Guard.NotNull(x);
        Guard.NotNull(y);

        if (x.Count != y.Count)
            return false;

        for (int i = 0; i < x.Count; i++)
        {
            if (!comparer(x[i], y[i]))
                return false;
        }

        return true;
    }

    private sealed class CaptureEvaluator : ExpressionVisitor
    {
        internal static readonly ExpressionVisitor Default = new CaptureEvaluator();

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node is { Member: FieldInfo fieldInfo, Expression: ConstantExpression constantExpression }
                && fieldInfo.DeclaringType!.IsDefined(typeof(CompilerGeneratedAttribute)))
                return Expression.Constant(fieldInfo.GetValue(constantExpression.Value), node.Type);

            return base.VisitMember(node);
        }

        protected override Expression VisitUnary(UnaryExpression node) =>
            node.NodeType == ExpressionType.Quote ? node : base.VisitUnary(node);
    }
}
