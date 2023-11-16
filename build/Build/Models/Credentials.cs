using Common;
using Common.Models;

namespace Build.Models;

public sealed record Credentials(CodeCovCredentials CodeCov)
{
    public static Credentials GetCredentials(ICakeContext context) => new(
        new CodeCovCredentials(context.EnvironmentVariable(EnvVars.CodeCovToken)));
}
