using Common;

namespace Build.Tasks;

[TaskName(nameof(Build))]
[TaskDescription("Builds the solution")]
[IsDependentOn(typeof(Clean))]
public sealed class Build : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        context.Information("Building solution...");

        context.DotNetRestore(Constants.SolutionFile, new DotNetRestoreSettings
        {
            Verbosity = DotNetVerbosity.Minimal,
            Sources = new [] { Constants.NuGetOrgUrl },
            MSBuildSettings = context.MsBuildSettings,
            LockedMode = true
        });

        context.DotNetBuild(Constants.SolutionFile, new DotNetBuildSettings
        {
            Verbosity = DotNetVerbosity.Minimal,
            Configuration = context.MsBuildConfiguration,
            NoRestore = true,
            MSBuildSettings = context.MsBuildSettings
        });
    }
}
