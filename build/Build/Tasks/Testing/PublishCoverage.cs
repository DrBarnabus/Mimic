using Common;
using Common.Utils;

namespace Build.Tasks.Testing;

[TaskName(nameof(PublishCoverage))]
[TaskDescription("Publishes the test coverage")]
[IsDependentOn(typeof(UnitTest))]
public sealed class PublishCoverage : FrostingTask<BuildContext>
{
    public override bool ShouldRun(BuildContext context)
    {
        bool shouldRun = true;

        shouldRun &= context.ShouldRun(context.IsOnWindows, $"{nameof(PublishCoverage)} only works on Windows agents.");
        shouldRun &= context.ShouldRun(context.IsOriginalRepo, $"{nameof(PublishCoverage)} only works for the original repository.");
        shouldRun &= context.ShouldRun(!string.IsNullOrEmpty(context.Credentials?.CodeCov?.Token), $"{nameof(PublishCoverage)} only works when '{EnvVars.CodeCovToken}' is supplied.");

        return shouldRun;
    }

    public override void Run(BuildContext context)
    {
        string[] coverageFiles = context.GetFiles($"{Paths.TestResults}/*.coverage.xml")
            .Select(f => context.MakeRelative(f).ToString())
            .ToArray();

        string? token = context.Credentials?.CodeCov?.Token;
        if (string.IsNullOrEmpty(token))
            throw new InvalidOperationException("Could not resolve CodeCov token");

        context.Codecov(new CodecovSettings
        {
            Files = coverageFiles,
            Token = token
        });
    }
}
