﻿using Mimic.Expressions;
using Mimic.Setup;
using Mimic.Setup.Fluent;
using SetupBase = Mimic.Setup.SetupBase;

namespace Mimic;

public partial class Mimic<T>
{
    public ISetup<T> Setup(Expression<Action<T>> expression)
    {
        var setup = Setup(this, expression);
        return new Setup<T>(setup);
    }

    public ISetup<T, TResult> Setup<TResult>(Expression<Func<T, TResult>> expression)
    {
        var setup = Setup(this, expression);
        return new Setup<T, TResult>(setup);
    }

    public IGetterSetup<T, TProperty> SetupGet<TProperty>(Expression<Func<T, TProperty>> expression)
    {
        Guard.NotNull(expression);

        if (expression.Body is not (IndexExpression or MethodCallExpression { Method.IsSpecialName: true }))
        {
            if (expression.Body is not MemberExpression { Member: PropertyInfo property })
                throw MimicException.ExpressionNotProperty(expression);

            if (!property.CanReadProperty(out _, out _))
                throw MimicException.ExpressionNotPropertyGetter(property);
        }

        var setup = Setup(this, expression);
        return new Setup<T, TProperty>(setup);
    }

    public ISetup<T> SetupSet(Action<T> setterExpression)
    {
        Guard.NotNull(setterExpression);

        var expression = SetterExpressionConstructor.ConstructFromAction(setterExpression, ConstructorArguments);
        ValidateSetterExpression(expression);

        var setup = Setup(this, expression);
        return new Setup<T>(setup);
    }

    public ISetterSetup<T, TProperty> SetupSet<TProperty>(Action<T> setterExpression)
    {
        Guard.NotNull(setterExpression);

        var expression = SetterExpressionConstructor.ConstructFromAction(setterExpression, ConstructorArguments);
        ValidateSetterExpression(expression);

        var setup = Setup(this, expression);
        return new SetterSetup<T, TProperty>(setup);
    }

    public Mimic<T> SetupProperty<TProperty>(Expression<Func<T, TProperty>> propertyExpression, TProperty? initialValue = default)
    {
        Guard.NotNull(propertyExpression);

        if (propertyExpression.Body is not MemberExpression { Member: PropertyInfo property })
            throw MimicException.ExpressionNotProperty(propertyExpression);

        if (!property.CanReadProperty(out var getter, out _))
            throw MimicException.ExpressionNotPropertyGetter(property);

        if (!property.CanWriteProperty(out var setter, out _))
            throw MimicException.ExpressionNotPropertySetter(property);

        var expectations = ExpressionSplitter.Split(propertyExpression);
        if (expectations.Count != 1)
            throw new UnsupportedExpressionException(propertyExpression);

        _setups.Add(new PropertyStubSetup(this, propertyExpression, getter, setter, initialValue));
        return this;
    }

    public Mimic<T> SetupAllProperties()
    {
        _setups.Add(new AllPropertiesStubSetup(this));
        return this;
    }

    internal static MethodCallSetup Setup(Mimic<T> mimic, LambdaExpression expression, Func<bool>? condition = null)
    {
        Guard.NotNull(expression);

        var expectations = ExpressionSplitter.Split(expression);
        if (expectations.Count != 1)
            throw new UnsupportedExpressionException(expression);

        var setup = new MethodCallSetup(expression, mimic, expectations.Pop(), condition);
        mimic._setups.Add(setup);
        return setup;
    }

    internal static void ValidateSetterExpression(Expression<Action<T>> expression)
    {
        Guard.NotNull(expression);

        if (expression.Body is BinaryExpression { NodeType: ExpressionType.Assign, Left: MemberExpression or IndexExpression })
                return;

        if (expression.Body.NodeType is ExpressionType.Call)
        {
            var call = (MethodCallExpression)expression.Body;
            if (call.Method.IsSetter())
                return;
        }

        throw MimicException.ExpressionNotPropertySetter(expression);
    }

    private sealed class SetupCollection : IReadOnlyList<SetupBase>
    {
        private readonly List<SetupBase> _setups = new();
        private readonly HashSet<IExpectation> _activeSetups = new();

        public void Add(SetupBase setup)
        {
            lock (_setups)
            {
                _setups.Add(setup);

                if (!_activeSetups.Add(setup.Expectation))
                    MarkOverridenSetups();
            }
        }

        public List<SetupBase> FindAll(Predicate<SetupBase> predicate)
        {
            lock (_setups)
                return _setups.Where(setup => !setup.Overridden && predicate(setup)).ToList();
        }

        public SetupBase? FindLast(Predicate<SetupBase> predicate)
        {
            lock (_setups)
            {
                if (_setups.Count == 0)
                    return null;

                for (int i = _setups.Count - 1; i >= 0 ; i--)
                {
                    var setup = _setups[i];
                    if (setup.Overridden)
                        continue;

                    if (predicate(setup))
                        return setup;
                }
            }

            return null;
        }

        public int Count
        {
            get
            {
                lock (_setups)
                    return _setups.Count;
            }
        }

        public SetupBase this[int index]
        {
            get
            {
                lock (_setups)
                {
                    return _setups[index];
                }
            }
        }

        public IEnumerator<SetupBase> GetEnumerator()
        {
            lock (_setups)
            {
                return _setups.GetEnumerator();
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private void MarkOverridenSetups()
        {
            var visitedExpectations = new HashSet<IExpectation>();

            for (int i = _setups.Count - 1; i >= 0 ; i--)
            {
                var setup = _setups[i];
                if (setup.Overridden)
                    continue;

                if (!visitedExpectations.Add(setup.Expectation))
                    setup.Override();
            }
        }
    }
}
