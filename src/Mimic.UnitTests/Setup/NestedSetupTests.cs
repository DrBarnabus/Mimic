using System.Linq.Expressions;
using Mimic.Setup;

namespace Mimic.UnitTests.Setup;

public class NestedSetupTests
{
    [Fact]
    public void Constructor_ShouldSuccessfullyConstruct()
    {
        var mimic = new Mimic<ISubject>();
        Expression<Action<ISubject>> expression = m => m.GetNested();
        var methodCallExpression = (MethodCallExpression)expression.Body;
        var methodExpectation = new MethodExpectation(expression, methodCallExpression.Method, methodCallExpression.Arguments);

        var nestedMimic = new Mimic<INestedSubject>();
        var setup = new NestedSetup(methodCallExpression, mimic, methodExpectation, nestedMimic.Object);

        setup.OriginalExpression.ShouldBeSameAs(methodCallExpression);
        setup.Mimic.ShouldBeSameAs(mimic);
        setup.Expectation.ShouldBeSameAs(methodExpectation);
        setup.Expression.ShouldBeSameAs(methodExpectation.Expression);
        setup.Matched.ShouldBeFalse();
        setup.Overridden.ShouldBeFalse();
        setup.Expected.ShouldBeTrue();
    }

    [Fact]
    public void Execute_ShouldCorrectlySetReturnValueOfInvocation()
    {
        var mimic = new Mimic<ISubject>();
        Expression<Action<ISubject>> expression = m => m.GetNested();
        var methodCallExpression = (MethodCallExpression)expression.Body;
        var methodExpectation = new MethodExpectation(expression, methodCallExpression.Method, methodCallExpression.Arguments);

        var nestedMimic = new Mimic<INestedSubject>();
        var setup = new NestedSetup(methodCallExpression, mimic, methodExpectation, nestedMimic.Object);

        var invocation = InvocationFixture.ForMethod<ISubject>(nameof(ISubject.GetNested));
        setup.Execute(invocation);

        invocation.ReturnValue.ShouldBeSameAs(nestedMimic.Object);
    }

    [Fact]
    public void GetNested_WhenTheReturnValueIsMimicked_ShouldReturnTheMimicObject()
    {
        var mimic = new Mimic<ISubject>();
        Expression<Action<ISubject>> expression = m => m.GetNested();
        var methodCallExpression = (MethodCallExpression)expression.Body;
        var methodExpectation = new MethodExpectation(expression, methodCallExpression.Method, methodCallExpression.Arguments);

        var nestedMimic = new Mimic<INestedSubject>();
        var setup = new NestedSetup(methodCallExpression, mimic, methodExpectation, nestedMimic.Object);

        var nestedMimics = setup.GetNested();

        nestedMimics.ShouldNotBeNull();
        nestedMimics.ShouldNotBeEmpty();
        nestedMimics[0].ShouldBeSameAs(nestedMimic);
    }

    // ReSharper disable once MemberCanBePrivate.Global
    internal interface ISubject
    {
        public INestedSubject GetNested();
    }

    // ReSharper disable once MemberCanBePrivate.Global
    internal interface INestedSubject
    {
        public void NestedMethod();
    }
}
