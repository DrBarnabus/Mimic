namespace Common;

public static class Paths
{
    public static readonly DirectoryPath Root = "./";

    public static readonly DirectoryPath Src = Root.Combine("src");
    public static readonly DirectoryPath Build = Root.Combine("build");
    public static readonly DirectoryPath Tools = Root.Combine("tools");

    public static readonly DirectoryPath Artifacts = Root.Combine("artifacts");
    public static readonly DirectoryPath TestResults = Artifacts.Combine("test-results");
    public static readonly DirectoryPath Packages = Artifacts.Combine("packages");
}
