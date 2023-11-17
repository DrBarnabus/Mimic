using Build.Tasks.Testing;

namespace Build.Tasks;

[TaskName(nameof(Test))]
[TaskDescription("Run the unit tests and (CI only) publish the test coverage")]
[IsDependentOn(typeof(PublishCoverage))]
public sealed class Test : FrostingTask<BuildContext>
{
}
