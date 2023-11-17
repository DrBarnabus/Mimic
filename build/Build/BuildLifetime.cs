using Build.Models;
using Common;
using Common.Utils;

namespace Build;

public sealed class BuildLifetime : BuildLifetimeBase<BuildContext>
{
    public override void Setup(BuildContext context, ISetupContext info)
    {
        base.Setup(context, info);

        context.MsBuildConfiguration = context.Argument(Arguments.Configuration, Constants.DefaultConfiguration);
        context.EnableUnitTests = context.IsEnabled(EnvVars.EnableUnitTests);
        context.Credentials = Credentials.GetCredentials(context);

        SetMsBuildSettings(context);
    }

    protected override void LogBuildInformation(BuildContext context)
    {
        context.Information($"Configuration:        {context.MsBuildConfiguration}");
    }

    private static void SetMsBuildSettings(BuildContext context)
    {
        var msBuildSettings = context.MsBuildSettings;
        var version = context.Version!;

        msBuildSettings.SetAssemblyVersion(version.Version);
        msBuildSettings.SetFileVersion(version.Version);
        msBuildSettings.SetVersion(version.SemVersion);
        msBuildSettings.SetPackageVersion(version.SemVersion);
        msBuildSettings.SetInformationalVersion(version.GitVersion.InformationalVersion);
        msBuildSettings.SetContinuousIntegrationBuild(!context.IsLocalBuild);
        msBuildSettings.WithProperty("RepositoryBranch", version.GitVersion.BranchName);
        msBuildSettings.WithProperty("RepositoryCommit", version.GitVersion.Sha);
        msBuildSettings.WithProperty("NoPackageAnalysis", "true");
    }
}
