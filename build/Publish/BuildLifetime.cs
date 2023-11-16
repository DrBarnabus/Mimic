using Common;
using Publish.Models;

namespace Publish;

public sealed class BuildLifetime : BuildLifetimeBase<BuildContext>
{
    public override void Setup(BuildContext context, ISetupContext info)
    {
        base.Setup(context, info);

        context.Credentials = Credentials.GetCredentials(context);

        if (context.Version?.SemVersion == null)
            return;

        string version = context.Version.SemVersion;

        var packageFiles = context.GetFiles(Paths.Packages + "/*.nupkg");
        foreach (var packageFile in packageFiles)
            context.Packages.Add(new NuGetPackage(GetPackageName(packageFile, version), packageFile));
    }

    private static string GetPackageName(FilePath? packageFile, string version)
    {
        string? packageName = packageFile?.GetFilenameWithoutExtension().ToString();
        if (packageName is null or "")
            throw new InvalidOperationException("Unable to parse package name from package file path");

        return packageName[..^(version.Length + 1)].ToLowerInvariant();
    }
}
