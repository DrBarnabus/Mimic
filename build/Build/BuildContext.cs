using Build.Models;
using Common;

namespace Build;

public sealed class BuildContext : BuildContextBase
{
    public BuildContext(ICakeContext context)
        : base(context)
    {
    }

    public string MsBuildConfiguration { get; set; } = Constants.DefaultConfiguration;

    public bool EnableUnitTests { get; set; }

    public Credentials? Credentials { get; set; }

    public DotNetMSBuildSettings MsBuildSettings { get; } = new();
}
