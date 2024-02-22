using System.Linq.Expressions;
using Mimic.Core;
using Mimic.Expressions;
using Mimic.Setup;
using Mimic.Setup.Fluent;

namespace Mimic.UnitTests.Setup.Fluent;

public class SetterSetupTests
{
    [Fact]
    public void Constructor_ShouldSuccessfullyConstruct()
    {
        var methodCallSetup = ToMethodCallSetup(m => m.PropertyWithSetter = Arg.Any<string>());

        var setup = new SetterSetup<ISubject, string>(methodCallSetup);

        setup.ToString().ShouldBe(methodCallSetup.Expression.ToString());
    }

    [Fact]
    public void Constructor_WhenSetupIsNull_ShouldThrowAssertionException()
    {
        var ex = Should.Throw<Guard.AssertionException>(() => new Setup<ISubject>(null!));
        ex.ShouldNotBeNull();
        ex.Message.ShouldContain("setup must not be null");
    }

    [Fact]
    public void Callback_WithAction_ShouldCorrectlySetCallbackBehaviour()
    {
        var methodCallSetup = ToMethodCallSetup(m => m.PropertyWithSetter = Arg.Any<string>());
        var setup = new SetterSetup<ISubject, string>(methodCallSetup);

        setup.Callback(_ => {}).ShouldBeSameAs(setup);

        methodCallSetup.ConfiguredBehaviours.PreReturnCallback.ShouldNotBeNull();
    }

    private static MethodCallSetup ToMethodCallSetup(Action<ISubject> setterExpression)
    {
        var expression = SetterExpressionConstructor.ConstructFromAction(setterExpression, null);

        var mimic = new Mimic<ISubject>();
        var methodCallExpression = (MethodCallExpression)expression.Body;
        var methodExpectation = new MethodExpectation(expression, methodCallExpression.Method, methodCallExpression.Arguments);

        return new MethodCallSetup(methodCallExpression, mimic, methodExpectation, null);
    }

    // ReSharper disable once MemberCanBePrivate.Global
    internal interface ISubject
    {
        public string PropertyWithSetter { get; set; }
    }
}
