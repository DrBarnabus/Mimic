using Common;
using Common.Utils;

namespace Build.Tasks.Testing;

[TaskName(nameof(UnitTest))]
[TaskDescription("Run the unit tests")]
[TaskArgument(Arguments.TargetFramework, Constants.NetVersion60, Constants.NetVersion70, Constants.NetVersion80)]
[IsDependentOn(typeof(Build))]
public sealed class UnitTest : FrostingTask<BuildContext>
{
    public override bool ShouldRun(BuildContext context) => context.EnableUnitTests;

    public override void Run(BuildContext context)
    {
        string? targetFrameworkArgument = context.Argument(Arguments.TargetFramework, string.Empty);

        string[] targetFrameworks = Constants.TargetFrameworks;
        if (!string.IsNullOrEmpty(targetFrameworkArgument))
        {
            if (!targetFrameworks.Contains(targetFrameworkArgument))
                throw new Exception($"TargetFramework of {targetFrameworkArgument} is not supported at this time");

            targetFrameworks = new[] { targetFrameworkArgument };
        }

        foreach (string targetFramework in targetFrameworks)
        {
            var projects = context.GetFiles($"{Paths.Src}/**/*.UnitTests.csproj");
            foreach (var project in projects)
                TestProjectForTarget(context, project, targetFramework);
        }
    }

    public override void OnError(Exception exception, BuildContext context)
    {
        var error = (exception as AggregateException)?.InnerExceptions[0];
        context.Error(error.Dump());

        throw exception;
    }

    private static void TestProjectForTarget(BuildContext context, FilePath project, string targetFramework)
    {
        string projectName = $"{project.GetFilenameWithoutExtension()}.{targetFramework}";
        var testResultsPath = context.MakeAbsolute(Paths.TestResults.CombineWithFilePath(
            $"{projectName}.results.xml"));

        var settings = new DotNetTestSettings
        {
            Framework = targetFramework,
            NoBuild = true,
            NoRestore = true,
            Configuration = context.MsBuildConfiguration,
            TestAdapterPath = new DirectoryPath("."),
            Loggers = { $"junit;LogFileName={testResultsPath}" }
        };

        // TODO: Investigate doing this with coverlet.collector instead
        var coverletSettings = new CoverletSettings
        {
            CollectCoverage = true,
            CoverletOutputFormat = CoverletOutputFormat.cobertura,
            CoverletOutputDirectory = Paths.TestResults,
            CoverletOutputName = $"{projectName}.coverage.xml",
            Exclude = { "[Mimic.UnitTests]*" }
        };

        context.DotNetTest(project.FullPath, settings, coverletSettings);
    }
}
