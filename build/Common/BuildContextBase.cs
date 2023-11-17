using Common.Models;

namespace Common;

public abstract class BuildContextBase : FrostingContext
{
    protected BuildContextBase(ICakeContext context)
        : base(context)
    {
    }

    public BuildVersion? Version { get; set; }

    public bool IsOriginalRepo { get; set; }
    public bool IsMainBranch { get; set; }
    public bool IsPullRequest { get; set; }
    public bool IsTagged { get; set; }

    public bool IsLocalBuild { get; set; }
    public bool IsGitHubActionsBuild { get; set; }

    public bool IsOnWindows { get; set; }
    public bool IsOnLinux { get; set; }
    public bool IsOnMacOs { get; set; }

    public bool IsReleaseBranchOriginalRepo => !IsLocalBuild && IsOriginalRepo && IsMainBranch && !IsPullRequest;
    public bool IsStableRelease => IsReleaseBranchOriginalRepo && IsTagged && Version?.IsPreRelease == false;
    public bool IsTaggedPreRelease => IsReleaseBranchOriginalRepo && IsTagged && Version?.IsPreRelease == true;
    public bool IsInternalPreRelease => IsReleaseBranchOriginalRepo && IsGitHubActionsBuild;
}
