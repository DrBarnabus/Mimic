using Common;
using Publish.Models;

namespace Publish;

public sealed class BuildContext : BuildContextBase
{
    public Credentials? Credentials { get; set; }

    public List<NuGetPackage> Packages { get; set; } = new();

    public BuildContext(ICakeContext context) : base(context)
    {
    }
}
