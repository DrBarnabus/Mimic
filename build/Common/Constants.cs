namespace Common;

public static class Constants
{
    public const string RepoOwner = "DrBarnabus";
    public const string Repository = "Mimic";

    public const string Version60 = "6.0";
    public const string Version70 = "7.0";
    public const string Version80 = "8.0";
    public const string VersionLatest = Version80;

    public const string NetVersion60 = $"net{Version60}";
    public const string NetVersion70 = $"net{Version70}";
    public const string NetVersion80 = $"net{Version80}";
    public const string NetVersionLatest = $"net{VersionLatest}";

    public static readonly string[] TargetFrameworks = { NetVersion60, NetVersion70, NetVersion80 };

    public const string DefaultBranch = "main";
    public const string DefaultConfiguration = "Release";

    public const string SolutionFile = "./Mimic.sln";

    public const string NuGetOrgUrl = "https://api.nuget.org/v3/index.json";
    public const string GithubPackagesUrl = "https://nuget.pkg.github.com/DrBarnabus/index.json";
}
