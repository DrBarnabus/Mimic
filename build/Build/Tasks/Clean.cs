using Common;

namespace Build.Tasks;

[TaskName(nameof(Clean))]
[TaskDescription("Clean build artifacts")]
public sealed class Clean : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        context.Information("Cleaning directories...");

        context.CleanDirectories(Paths.Src + "/**/bin/" + context.MsBuildConfiguration);
        context.CleanDirectories(Paths.Src + "/**/obj");
        context.CleanDirectory(Paths.TestResults);
        context.CleanDirectory(Paths.Packages);
        context.CleanDirectory(Paths.Artifacts);
    }
}
