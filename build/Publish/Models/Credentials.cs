using Common;
using Common.Models;

namespace Publish.Models;

public sealed record Credentials(GitHubCredentials GitHub, NuGetCredentials NuGet)
{
    public static Credentials GetCredentials(ICakeContext context) => new(
        new GitHubCredentials(context.EnvironmentVariable(EnvVars.GitHubToken)),
        new NuGetCredentials(context.EnvironmentVariable(EnvVars.NuGetApiKey)));
}
