using Common.Utils;

namespace Common.Tasks;

[TaskName(nameof(Default))]
[TaskDescription("Shows this output")]
public abstract class Default : FrostingTask
{
    public override void Run(ICakeContext context)
    {
        var entryAssembly = Assembly.GetEntryAssembly();
        var tasks = entryAssembly?.FindAllDerivedTypes(typeof(IFrostingTask)).Where(x => !x.Name.Contains("Internal")).ToList();
        if (tasks == null) return;

        var defaultTask = tasks.Find(x => x.Name.Contains(nameof(Default)));
        if (defaultTask != null && tasks.Remove(defaultTask))
            tasks.Insert(0, defaultTask);

        context.Information($"Available targets:{Environment.NewLine}");
        foreach (var task in tasks)
        {
            context.Information($"# {task.GetTaskDescription()}");

            string taskName = task.GetTaskName();
            string target = taskName != nameof(Default) ? $"--target {taskName}" : string.Empty;
            string arguments = task.GetTaskArguments();
            context.Information($"  dotnet run/{entryAssembly?.GetName().Name}.dll {target} {arguments}\n");
        }
    }
}
