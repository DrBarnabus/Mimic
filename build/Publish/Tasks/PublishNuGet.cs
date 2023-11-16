using Cake.Common.Tools.DotNet.NuGet.Push;
using Common;
using Common.Utils;

namespace Publish.Tasks;

[TaskName(nameof(PublishNuGet))]
[TaskDescription("Publish NuGet package")]
[IsDependentOn(typeof(PublishNuGetInternal))]
public sealed class PublishNuGet : FrostingTask<BuildContext>
{
}

[TaskName(nameof(PublishNuGetInternal))]
[TaskDescription("Publish NuGet to GitHub Packages & NuGet.org")]
public sealed class PublishNuGetInternal : FrostingTask<BuildContext>
{
    public override bool ShouldRun(BuildContext context)
    {
        bool shouldRun = true;

        shouldRun &= context.ShouldRun(context.IsGitHubActionsBuild, $"{nameof(PublishNuGetInternal)} only works on GitHub Actions.");
        shouldRun &= context.ShouldRun(context.IsStableRelease || context.IsTaggedPreRelease || context.IsInternalPreRelease, $"{nameof(PublishNuGetInternal)} only works for releases.");

        return shouldRun;
    }

    public override void Run(BuildContext context)
    {
        // Publish to GitHub Packages
        if (context.IsInternalPreRelease)
        {
            context.StartGroup("Publish to GitHub Packages");

            string? token = context.Credentials?.GitHub.Token;
            if (token is null or "")
                throw new InvalidOperationException("Could not resolve GitHub Packages token.");

            NuGetPush(context, token, Constants.GithubPackagesUrl);

            context.EndGroup();
        }

        // Publish to nuget.org
        if (context.IsStableRelease | context.IsTaggedPreRelease)
        {
            context.StartGroup("Publish to nuget.org");

            string? apiKey = context.Credentials?.NuGet.ApiKey;
            if (apiKey is null or "")
                throw new InvalidOperationException("Could not resolve NuGet.org API Key.");

            NuGetPush(context, apiKey, Constants.NuGetOrgUrl);

            context.EndGroup();
        }
    }

    private static void NuGetPush(BuildContext context, string apiKey, string apiUrl)
    {
        string nugetVersion = context.Version!.SemVersion!;

        foreach ((string packageName, var filePath) in context.Packages)
        {
            context.Information($"Pushing package {packageName}, version {nugetVersion} to {apiUrl}.");

            context.DotNetNuGetPush(filePath.FullPath, new DotNetNuGetPushSettings
            {
                ApiKey = apiKey,
                Source = apiUrl,
                SkipDuplicate = true
            });
        }
    }
}
