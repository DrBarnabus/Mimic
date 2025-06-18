using System.Linq.Expressions;
using Mimic.Core;
using Mimic.Exceptions;
using Mimic.Expressions;

namespace Mimic.UnitTests.Expressions;

public class ExpressionSplitterTests
{
    [Fact]
    public void Split_WhenCalledWithPropertyGetterExpression_ShouldReturnStackWithSingleMethodExpectation()
    {
        Expression<Func<ISubject, string>> expression = x => x.StringProperty;

        var result = ExpressionSplitter.Split(expression);

        result.Count.ShouldBe(1);
        var expectation = result.Pop();
        expectation.MethodInfo.Name.ShouldBe("get_StringProperty");
    }

    [Fact]
    public void Split_WhenCalledWithMethodCallExpression_ShouldReturnStackWithSingleMethodExpectation()
    {
        Expression<Func<ISubject, string>> expression = x => x.GetString();

        var result = ExpressionSplitter.Split(expression);

        result.Count.ShouldBe(1);
        var expectation = result.Pop();
        expectation.MethodInfo.Name.ShouldBe("GetString");
    }

    [Fact]
    public void Split_WhenCalledWithChainedExpression_ShouldReturnStackWithMultipleMethodExpectations()
    {
        Expression<Func<ISubject, string>> expression = x => x.GetSubject().StringProperty;

        var result = ExpressionSplitter.Split(expression);

        result.Count.ShouldBe(2);
        var firstExpectation = result.Pop();
        var secondExpectation = result.Pop();

        firstExpectation.MethodInfo.Name.ShouldBe("GetSubject");
        secondExpectation.MethodInfo.Name.ShouldBe("get_StringProperty");
    }

    [Fact]
    public void Split_WhenCalledWithIndexerExpression_ShouldReturnStackWithSingleMethodExpectation()
    {
        Expression<Func<ISubject, string>> expression = x => x["key"];

        var result = ExpressionSplitter.Split(expression);

        result.Count.ShouldBe(1);
        var expectation = result.Pop();
        expectation.MethodInfo.Name.ShouldBe("get_Item");
    }

    [Fact]
    public void Split_WhenCalledWithAssignmentExpression_ShouldReturnStackWithSingleMethodExpectation()
    {
        var parameter = Expression.Parameter(typeof(ISubject), "x");
        var property = Expression.Property(parameter, nameof(ISubject.StringProperty));
        var value = Expression.Constant("test");
        var assignment = Expression.Assign(property, value);
        var expression = Expression.Lambda<Action<ISubject>>(assignment, parameter);

        var result = ExpressionSplitter.Split(expression);

        result.Count.ShouldBe(1);
        var expectation = result.Pop();
        expectation.MethodInfo.Name.ShouldBe("set_StringProperty");
    }

    [Fact]
    public void Split_WhenCalledWithIndexerGetterExpression_ShouldReturnStackWithSingleMethodExpectation()
    {
        Expression<Func<ISubject, string>> expression = x => x["key"];

        var result = ExpressionSplitter.Split(expression);

        result.Count.ShouldBe(1);
        var expectation = result.Pop();
        expectation.MethodInfo.Name.ShouldBe("get_Item");
    }

    [Fact]
    public void Split_WhenCalledWithIndexerSetterExpression_ShouldReturnStackWithSingleMethodExpectation()
    {
        var parameter = Expression.Parameter(typeof(ISubject), "x");
        var indexer = Expression.MakeIndex(parameter, typeof(ISubject).GetProperty("Item"), [Expression.Constant("key")]);
        var assignment = Expression.Assign(indexer, Expression.Constant("value"));
        var expression = Expression.Lambda<Action<ISubject>>(assignment, parameter);

        var result = ExpressionSplitter.Split(expression);

        result.Count.ShouldBe(1);
        var expectation = result.Pop();
        expectation.MethodInfo.Name.ShouldBe("set_Item");
    }

    [Fact]
    public void Split_WhenCalledWithExtensionMethodExpression_ShouldThrowExtensionMethodIsNotOverridableException()
    {
        Expression<Func<ISubject, bool>> expression = x => x.CustomExtension("test");

        var exception = Should.Throw<MimicException>(() => ExpressionSplitter.Split(expression));

        exception.Message.ShouldContain("extension method is not overridable");
    }


    [Fact]
    public void Split_WhenCalledWithUnsupportedExpression_ShouldThrowMimicException()
    {
        Expression<Func<ISubject, int>> expression = x => 42;

        Should.Throw<MimicException>(() => ExpressionSplitter.Split(expression));
    }

    [Fact]
    public void Split_WhenCalledWithNullExpression_ShouldThrowAssertionException()
    {
        Should.Throw<Guard.AssertionException>(() => ExpressionSplitter.Split(null!));
    }
}

file interface ISubject
{
    string StringProperty { get; set; }

    // ReSharper disable UnusedMember.Global
    string this[string key] { get; set; }

    string GetString();

    ISubject GetSubject();
}

file static class ISubjectExtensions
{
    // ReSharper disable UnusedParameter.Local
    public static bool CustomExtension(this ISubject subject, string parameter) => true;
}
