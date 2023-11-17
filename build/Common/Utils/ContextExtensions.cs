namespace Common.Utils;

public static class ContextExtensions
{
    public static bool IsEnabled(this ICakeContext context, string variable, bool nullOrEmptyAsEnabled = true)
    {
        string? value = context.EnvironmentVariable(variable);
        return string.IsNullOrWhiteSpace(value)
            ? nullOrEmptyAsEnabled
            : bool.Parse(value);
    }

    public static bool ShouldRun(this ICakeContext context, bool criteria, string skipMessage)
    {
        if (criteria) return true;

        context.Information(skipMessage);
        return false;
    }

    public static bool IsOriginalRepo(this ICakeContext context)
    {
        string repositoryName = context.GetRepositoryName();
        return !string.IsNullOrWhiteSpace(repositoryName) && StringComparer.OrdinalIgnoreCase.Equals($"{Constants.RepoOwner}/{Constants.Repository}", repositoryName);
    }

    public static bool IsMainBranch(this ICakeContext context)
    {
        string repositoryBranch = context.GetBranchName();
        return !string.IsNullOrWhiteSpace(repositoryBranch) && StringComparer.OrdinalIgnoreCase.Equals(Constants.DefaultBranch, repositoryBranch);
    }

    public static bool IsTagged(this ICakeContext context)
    {
        string sha = context.ExecGitCmd("rev-parse --verify HEAD").Single();
        bool isTagged = context.ExecGitCmd("tag --points-at " + sha).Any();

        return isTagged;
    }

    public static string GetOs(this ICakeContext context)
    {
        if (context.IsRunningOnWindows()) return "Windows";
        if (context.IsRunningOnLinux()) return "Linux";
        if (context.IsRunningOnMacOs()) return "macOs";
        return string.Empty;
    }

    public static string GetBuildAgent(this ICakeContext context)
    {
        var buildSystem = context.BuildSystem();
        return buildSystem.Provider switch
        {
            BuildProvider.Local => "Local",
            BuildProvider.GitHubActions => "GitHubActions",
            _ => string.Empty
        };
    }

    public static string GetRepositoryName(this ICakeContext context)
    {
        var buildSystem = context.BuildSystem();
        return buildSystem.IsRunningOnGitHubActions
            ? buildSystem.GitHubActions.Environment.Workflow.Repository
            : string.Empty;
    }

    public static string GetBranchName(this ICakeContext context)
    {
        var buildSystem = context.BuildSystem();
        return buildSystem.IsRunningOnGitHubActions
            ? buildSystem.GitHubActions.Environment.Workflow.Ref.Replace("refs/heads/", string.Empty)
            : context.ExecGitCmd("rev-parse --abbrev-ref HEAD").Single();
    }

    public static void StartGroup(this ICakeContext context, string title)
    {
        var buildSystem = context.BuildSystem();
        if (buildSystem.IsRunningOnGitHubActions)
            context.GitHubActions().Commands.StartGroup(title);
    }

    public static void EndGroup(this ICakeContext context)
    {
        var buildSystem = context.BuildSystem();
        if (buildSystem.IsRunningOnGitHubActions)
            context.GitHubActions().Commands.EndGroup();
    }

    public static IEnumerable<string> ExecuteCommand(this ICakeContext context, FilePath exe, string? args, DirectoryPath? workDir = null)
    {
        var processSettings = new ProcessSettings { Arguments = args, RedirectStandardOutput = true };
        if (workDir is not null)
            processSettings.WorkingDirectory = workDir;

        context.StartProcess(exe, processSettings, out var redirectedOutput);
        return redirectedOutput.ToList();
    }

    private static IEnumerable<string> ExecGitCmd(this ICakeContext context, string? cmd, DirectoryPath? workDir = null)
    {
        var gitExe = context.Tools.Resolve(context.IsRunningOnWindows() ? "git.exe" : "git");
        return context.ExecuteCommand(gitExe, cmd, workDir);
    }
}
