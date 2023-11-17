namespace Common.Utils;

public static class Extensions
{
    public static IEnumerable<Type> FindAllDerivedTypes(this Assembly assembly, Type baseType) =>
        assembly.GetExportedTypes().Where(t => baseType.IsAssignableFrom(t) && t.GetTypeInfo() is { IsClass: true, IsAbstract: false });

    public static string GetTaskDescription(this Type task)
    {
        if (task is null)
            throw new ArgumentNullException(nameof(task));

        return task.GetCustomAttribute<TaskDescriptionAttribute>()?.Description ?? string.Empty;
    }

    public static string GetTaskName(this Type task)
    {
        if (task is null)
            throw new ArgumentNullException(nameof(task));

        return task.GetCustomAttribute<TaskNameAttribute>()?.Name ?? task.Name;
    }

    public static string GetTaskArguments(this Type task)
    {
        if (task is null)
        {
            throw new ArgumentNullException(nameof(task));
        }

        var attributes = task.GetCustomAttributes<TaskArgumentAttribute>().ToArray();
        return !attributes.Any()
            ? string.Empty
            : string.Join(" ", attributes.Select(attribute => $"[--{attribute.Name} ({string.Join(" | ", attribute.PossibleValues)})]"));
    }

    public static DirectoryPath GetRootDirectory()
    {
        var currentPath = DirectoryPath.FromString(Directory.GetCurrentDirectory());
        while (!Directory.Exists(currentPath.Combine(".git").FullPath))
            currentPath = currentPath.GetParent();

        return currentPath;
    }
}
