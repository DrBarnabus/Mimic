using Common.Addins.GitVersion;
using Common.Models;
using Common.Utils;

namespace Common;

public abstract class BuildLifetimeBase<T> : FrostingLifetime<T> where T : BuildContextBase
{
    public override void Setup(T context, ISetupContext info)
    {
        var buildSystem = context.BuildSystem();
        context.IsLocalBuild = buildSystem.IsLocalBuild;
        context.IsGitHubActionsBuild = buildSystem.IsRunningOnGitHubActions;

        context.IsOriginalRepo = context.IsOriginalRepo();
        context.IsMainBranch = context.IsMainBranch();
        context.IsPullRequest = buildSystem.IsPullRequest;
        context.IsTagged = context.IsTagged();

        context.IsOnWindows = context.IsRunningOnWindows();
        context.IsOnLinux = context.IsRunningOnLinux();
        context.IsOnMacOs = context.IsRunningOnMacOs();

        if (info.TargetTask.Name == "BuildPrepare")
        {
            context.Information("Running BuildPrepare...");
            return;
        }

        var gitVersion = context.GitVersion(new GitVersionSettings
        {
            OutputTypes = new HashSet<GitVersionOutput> { GitVersionOutput.Json, GitVersionOutput.BuildServer }
        });

        context.Version = BuildVersion.Calculate(gitVersion);
    }

    public override void Teardown(T context, ITeardownContext info)
    {
        context.StartGroup("Build Teardown");

        try
        {
            context.Information("Starting Teardown...");

            LogBuildInformationCore(context);

            context.Information("Finished running tasks.");
        }
        catch (Exception ex)
        {
            context.Error(ex.Dump());
        }

        context.EndGroup();
    }

    protected void LogBuildInformationCore(T context)
    {
        context.StartGroup("Build Setup");


        context.Information($"Build Agent:          {context.GetBuildAgent()}");
        context.Information($"OS:                   {context.GetOs()}");

        if (context.HasArgument(Arguments.Target))
            context.Information($"Target:               {context.Argument<string>(Arguments.Target)}");

        if (context.Version is not null)
            context.Information($"Version:              {context.Version.SemVersion}");

        context.Information($"Repository Name:      {context.GetRepositoryName()}");
        context.Information($"Original Repo:        {context.IsOriginalRepo}");
        context.Information($"Branch Name:          {context.GetBranchName()}");
        context.Information($"Main Branch:          {context.IsMainBranch}");
        context.Information($"Pull Request:         {context.IsPullRequest}");
        context.Information($"Tagged:               {context.IsTagged}");
        context.Information($"Stable Release:       {context.IsStableRelease}");
        context.Information($"Tagged Pre-Release:   {context.IsTaggedPreRelease}");
        context.Information($"Internal Pre-Release: {context.IsInternalPreRelease}");

        LogBuildInformation(context);

        context.EndGroup();
    }

    protected virtual void LogBuildInformation(T context)
    {
    }
}
