using Mimic.Core;
using Mimic.Exceptions;

namespace Mimic.UnitTests;

public partial class MimicTests
{
    public class Setup
    {
        [Fact]
        public void Setup_WithVoidReturnValue_ShouldCorrectlySetupMethodCallAndNotThrowOnExecution()
        {
            var mimic = new Mimic<ISubject>();
            mimic.Setup(m => m.VoidMethod());

            Should.NotThrow(() => mimic.Object.VoidMethod());
        }

        [Fact]
        public void Setup_WithVoidReturnValue_AndConfiguredWithCallback_ShouldCorrectlySetupMethodCallAndCallCallback()
        {
            var mimic = new Mimic<ISubject>();

            bool callbackCalled = false;
            mimic.Setup(m => m.VoidMethod())
                .Callback(() => callbackCalled = true);

            mimic.Object.VoidMethod();

            callbackCalled.ShouldBeTrue();
        }

        [Fact]
        public void Setup_WithVoidReturnValue_AndConfiguredWithThrows_ShouldCorrectlySetupMethodCallAndThrowOnExecution()
        {
            {
                var mimic = new Mimic<ISubject>();

                mimic.Setup(m => m.VoidMethod())
                    .Throws(new Exception());

                Should.Throw<Exception>(() => mimic.Object.VoidMethod());
            }

            {
                var mimic = new Mimic<ISubject>();

                mimic.Setup(m => m.VoidMethod())
                    .Throws<Exception>();

                Should.Throw<Exception>(() => mimic.Object.VoidMethod());
            }

            {
                var mimic = new Mimic<ISubject>();

                mimic.Setup(m => m.VoidMethod())
                    .Throws((Delegate)(() => new Exception()));

                Should.Throw<Exception>(() => mimic.Object.VoidMethod());
            }

            {
                var mimic = new Mimic<ISubject>();

                mimic.Setup(m => m.VoidMethod())
                    .Throws(() => new Exception());

                Should.Throw<Exception>(() => mimic.Object.VoidMethod());
            }
        }

        [Theory, AutoData]
        public void Setup_WithChainedNestedMimic_AndNestedVoidReturnValue_ShouldCorrectlySetupMethodCallAndNotThrowOnExecution(string value)
        {
            var mimic = new Mimic<ISubject>();
            mimic.Setup(m => m.GetNestedSubject(value).NestedVoidMethod());

            Should.NotThrow(() => mimic.Object.GetNestedSubject(value).NestedVoidMethod());
        }

        [Theory, InlineAutoData(true), InlineAutoData(false)]
        public void Setup_WithChainedNestedMimic_AndNestedVoidReturnValue_ShouldInitializeNestedMimicWithTheSameStrictModeSetting(bool strict, string sValue)
        {
            var mimic = new Mimic<ISubject>(strict);
            mimic.Setup(m => m.GetNestedSubject(sValue).NestedVoidMethod());

            var nestedMimic = Mimic<INestedSubject>.FromObject(mimic.Object.GetNestedSubject(sValue));
            nestedMimic.Strict.ShouldBe(strict);
        }

        [Fact]
        public void Setup_WithChainedTypeThatCannotBeMimicked_ShouldThrowMimicException()
        {
            {
                var mimic = new Mimic<ISubject>();

                var ex = Should.Throw<MimicException>(() => mimic.Setup(m => m.StringMethod().ToString()));
                ex.Message.ShouldBe("Type string cannot be mimicked. It must be an interface or a non-sealed/non-static class.");
            }

            {
                var mimic = new Mimic<ISubject>();

                var ex = Should.Throw<MimicException>(() => mimic.Setup(m => m.IntMethod().ToString()));
                ex.Message.ShouldBe("Type int cannot be mimicked. It must be an interface or a non-sealed/non-static class.");
            }
        }

        [Theory, AutoData]
        public void Setup_WithReturnValue_ShouldCorrectlySetupMethodCallAndReturnCorrectValue(string returnValue)
        {
            var mimic = new Mimic<ISubject>();
            mimic.Setup(m => m.StringMethod()).Returns(returnValue);

            mimic.Object.StringMethod().ShouldBe(returnValue);
        }

        [Theory, AutoData]
        public void Setup_WithReturnValue_AndConfiguredWithCallbacks_ShouldCorrectlySetupMethodCallAndCallCallbacksInCorrectOrder(string returnValue)
        {
            var mimic = new Mimic<ISubject>();

            bool beforeReturnCallbackCalled = false, afterReturnCallbackCalled = false;
            mimic.Setup(m => m.StringMethod())
                .Callback(() =>
                {
                    afterReturnCallbackCalled.ShouldBeFalse();
                    beforeReturnCallbackCalled = true;
                })
                .Returns(returnValue)
                .Callback(() =>
                {
                    beforeReturnCallbackCalled.ShouldBeTrue();
                    afterReturnCallbackCalled = true;
                });

            mimic.Object.StringMethod().ShouldBe(returnValue);

            beforeReturnCallbackCalled.ShouldBeTrue();
            afterReturnCallbackCalled.ShouldBeTrue();
        }

        [Fact]
        public void SetupGet_WithNullExpression_ShouldThrowAssertionException()
        {
            var mimic = new Mimic<ISubject>();

            var ex = Should.Throw<Guard.AssertionException>(() => mimic.SetupGet<string>(null!));
            ex.Expression.ShouldBe("expression");
            ex.Message.ShouldBe("expression must not be null (Expression 'expression')");
        }

        [Fact]
        public void SetupGet_WithNonPropertyExpression_ShouldThrowMimicException()
        {
            var mimic = new Mimic<ISubject>();

            var ex = Should.Throw<MimicException>(() => mimic.SetupGet(m => m.StringMethod()));
            ex.Message.ShouldBe("Expression (m => m.StringMethod()) is not a property accessor.");
        }

        [Theory, AutoData]
        public void SetupGet_WithValidProperty_AndReturnValue_ShouldSuccessfullySetupCallAndReturnCorrectValue(int value)
        {
            var mimic = new Mimic<ISubject>();

            mimic.SetupGet(m => m.GetOnlyProperty).Returns(value);

            mimic.Object.GetOnlyProperty.ShouldBe(value);
        }

        [Fact]
        public void SetupSet_WithNullExpression_ShouldThrowAssertionException()
        {
            var mimic = new Mimic<ISubject>();

            var ex = Should.Throw<Guard.AssertionException>(() => mimic.SetupSet(null!));
            ex.Expression.ShouldBe("setterExpression");
            ex.Message.ShouldBe("setterExpression must not be null (Expression 'setterExpression')");
        }

        [Fact]
        public void SetupSet_WithNonPropertyExpression_ShouldThrowMimicException()
        {
            var mimic = new Mimic<ISubject>();

            var ex = Should.Throw<MimicException>(() => mimic.SetupSet(m => m.StringMethod()));
            ex.Message.ShouldBe("Expression (m => m.StringMethod()...) is unsupported, the expression threw an exception while Mimic tried to resolve the method to intercept.");
        }

        [Theory, AutoData]
        public void SetupSet_WithValidProperty_ShouldSuccessfullySetupCallAndCallCallback(double value)
        {
            var mimic = new Mimic<ISubject>();

            bool callbackCalled = false;
            mimic.SetupSet(m => m.SetOnlyProperty = value)
                .Callback(() => callbackCalled = true);

            mimic.Object.SetOnlyProperty = value;

            callbackCalled.ShouldBeTrue();
        }

        [Fact]
        public void SetupSet_WithExplicitPropertyType_AndNullExpression_ShouldThrowAssertionException()
        {
            var mimic = new Mimic<ISubject>();

            var ex = Should.Throw<Guard.AssertionException>(() => mimic.SetupSet<string>(null!));
            ex.Expression.ShouldBe("setterExpression");
            ex.Message.ShouldBe("setterExpression must not be null (Expression 'setterExpression')");
        }

        [Fact]
        public void SetupSet_WithExplicitPropertyType_AndNonPropertyExpression_ShouldThrowMimicException()
        {
            var mimic = new Mimic<ISubject>();

            var ex = Should.Throw<MimicException>(() => mimic.SetupSet<string>(m => m.StringMethod()));
            ex.Message.ShouldBe("Expression (m => m.StringMethod()...) is unsupported, the expression threw an exception while Mimic tried to resolve the method to intercept.");
        }

        [Theory, AutoData]
        public void SetupSet_WithExplicitPropertyType_AndValidProperty_ShouldSuccessfullySetupCallAndCallCallback(string value)
        {
            var mimic = new Mimic<ISubject>();

            bool callbackCalled = false;
            mimic.SetupSet<string>(m => m.Property = value)
                .Callback(v => callbackCalled = v == value);

            mimic.Object.Property = value;

            callbackCalled.ShouldBeTrue();
        }

        [Fact]
        public void SetupProperty_WithNullExpression_ShouldThrowAssertionException()
        {
            var mimic = new Mimic<ISubject>();

            var ex = Should.Throw<Guard.AssertionException>(() => mimic.SetupProperty<string>(null!));
            ex.Expression.ShouldBe("propertyExpression");
            ex.Message.ShouldBe("propertyExpression must not be null (Expression 'propertyExpression')");
        }

        [Fact]
        public void SetupProperty_WithNonPropertyExpression_ShouldThrowMimicException()
        {
            var mimic = new Mimic<ISubject>();

            var ex = Should.Throw<MimicException>(() => mimic.SetupProperty<string>(m => m.StringMethod()));
            ex.Message.ShouldBe("Expression (m => m.StringMethod()) is not a property accessor.");
        }

        [Theory, AutoData]
        public void SetupProperty_WithValidProperty_ShouldSuccessfullySetupPropertyCalls(string initialValue, string newValue)
        {
            var mimic = new Mimic<ISubject>();

            mimic.SetupProperty(m => m.Property, initialValue);

            mimic.Object.Property.ShouldBe(initialValue);

            mimic.Object.Property = newValue;
            mimic.Object.Property.ShouldBe(newValue);
        }

        [Theory, AutoData]
        public void SetupAllProperties_WithValidProperty_ShouldSuccessfullySetupPropertyCalls(string stringValue, float floatValue)
        {
            var mimic = new Mimic<ISubject>();

            mimic.SetupAllProperties();

            mimic.Object.Property.ShouldBeNull();
            mimic.Object.FloatProperty.ShouldBe(default);

            mimic.Object.Property = stringValue;
            mimic.Object.Property.ShouldBe(stringValue);

            mimic.Object.FloatProperty = floatValue;
            mimic.Object.FloatProperty.ShouldBe(floatValue);
        }

        // ReSharper disable once MemberHidesStaticFromOuterClass
        internal interface ISubject
        {
            void VoidMethod();

            int IntMethod();

            string StringMethod();

            INestedSubject GetNestedSubject(string value);

            string Property { get; set; }

            float FloatProperty { get; set; }

            int GetOnlyProperty { get; }

            double SetOnlyProperty { set; }
        }

        internal interface INestedSubject
        {
            void NestedVoidMethod();
        }
    }
}
