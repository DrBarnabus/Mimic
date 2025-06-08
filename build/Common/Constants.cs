namespace Common;

public static class Constants
{
    public const string RepoOwner = "DrBarnabus";
    public const string Repository = "Mimic";

    public const string Version80 = "8.0";
    public const string Version90 = "9.0";
    public const string VersionLatest = Version90;

    public const string NetVersion80 = $"net{Version80}";
    public const string NetVersion90 = $"net{Version90}";
    public const string NetVersionLatest = $"net{VersionLatest}";

    public static readonly string[] TargetFrameworks = { NetVersion80, NetVersion90 };

    public const string DefaultBranch = "main";
    public const string DefaultConfiguration = "Release";

    public const string SolutionFile = "./Mimic.sln";

    public const string NuGetOrgUrl = "https://api.nuget.org/v3/index.json";
    public const string GithubPackagesUrl = "https://nuget.pkg.github.com/DrBarnabus/index.json";
}
