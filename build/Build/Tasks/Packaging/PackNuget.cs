using Common;

namespace Build.Tasks.Packaging;

[TaskName(nameof(PackNuget))]
[TaskDescription("Packs the NuGet package")]
[IsDependentOn(typeof(Build))]
public sealed class PackNuget : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        context.EnsureDirectoryExists(Paths.Packages);

        var settings = new DotNetPackSettings
        {
            Configuration = context.MsBuildConfiguration,
            MSBuildSettings = context.MsBuildSettings,
            OutputDirectory = Paths.Packages
        };

        context.DotNetPack("./src/Mimic/Mimic.csproj", settings);
    }
}
