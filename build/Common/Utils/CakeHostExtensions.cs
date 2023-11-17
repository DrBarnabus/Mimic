namespace Common.Utils;

public static class CakeHostExtensions
{
    public static CakeHost UseRootDirectory(this CakeHost host) =>
        host.ConfigureServices(s => s.UseWorkingDirectory(Extensions.GetRootDirectory()));

    public static CakeHost InstallNugetTool(this CakeHost host, string toolName, string toolVersion) =>
        host.ConfigureServices(services => services.UseTool(new Uri($"nuget:?package={toolName}&version={toolVersion}")));

    public static CakeHost InstallDotnetTool(this CakeHost host, string toolName, string toolVersion) =>
        host.ConfigureServices(services => services.UseTool(new Uri($"dotnet:?package={toolName}&version={toolVersion}")));
}
