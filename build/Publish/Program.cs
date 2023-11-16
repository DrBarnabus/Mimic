using Common;
using Common.Utils;
using Publish;

return new CakeHost()
    .UseContext<BuildContext>()
    .UseLifetime<BuildLifetime>()
    .UseTaskLifetime<BuildTaskLifetime>()
    .UseRootDirectory()
    .InstallDotnetTool("GitVersion.Tool", "5.12.0")
    .Run(args);
