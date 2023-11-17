using Build.Tasks.Packaging;

namespace Build.Tasks;

[TaskName(nameof(Package))]
[TaskDescription("Packages the project")]
[IsDependentOn(typeof(PackNuget))]
public sealed class Package : FrostingTask<BuildContext>
{
}
