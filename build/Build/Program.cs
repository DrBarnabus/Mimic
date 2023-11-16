using Build;
using Common;
using Common.Utils;

return new CakeHost()
    .UseContext<BuildContext>()
    .UseLifetime<BuildLifetime>()
    .UseTaskLifetime<BuildTaskLifetime>()
    .UseRootDirectory()
    .InstallDotnetTool("GitVersion.Tool", "5.12.0")
    .InstallNugetTool("CodecovUploader", "0.7.1")
    .Run(args);
