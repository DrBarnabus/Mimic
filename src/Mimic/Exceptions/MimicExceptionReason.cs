namespace Mimic.Exceptions;

public enum Reason
{
    /// <summary>
    /// The exception was the result of an unspecified/unknown error in the library.
    /// </summary>
    Unspecified,

    /// <summary>
    /// The exception is an indicator that something was used wrong with the library. It should be accompanied by a
    /// message describing the error and how to fix it.
    /// </summary>
    UsageError,

    /// <summary>
    /// Mimic was unable to validate and/or generate the required proxy to mimicked type.
    /// </summary>
    IncompatibleMimicType,

    /// <summary>
    /// Mimic was unable to process an expression used in a Setup/VerifyReceived call.
    /// </summary>
    UnsupportedExpression,

    /// <summary>
    /// An expectation from a Setup or VerifyReceived has not been met, an exception with this reason is thrown to fail
    /// the test.
    /// </summary>
    ExpectationFailed,
}
