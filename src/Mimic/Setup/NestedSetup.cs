namespace Mimic.Setup;

internal sealed class NestedSetup : SetupBase
{
    private readonly object _returnValue;

    public NestedSetup(Expression? originalExpression, IMimic mimic, IExpectation expectation, object returnValue)
        : base(originalExpression, mimic, expectation)
    {
        Guard.Assert(returnValue is IMimicked);
        _returnValue = returnValue;

        FlagAsExpected();
    }

    protected override void ExecuteCore(Invocation invocation) => invocation.SetReturnValue(_returnValue);

    internal override IReadOnlyList<IMimic> GetNested() => _returnValue is IMimicked mimicked ? [mimicked.Mimic] : [];
}
