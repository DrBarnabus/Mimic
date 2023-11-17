namespace Common.Utils;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
public sealed class TaskArgumentAttribute : Attribute
{
    public string Name { get; }

    public string[] PossibleValues { get; }

    public TaskArgumentAttribute(string name, params string[] possibleValues)
    {
        Name = name;
        PossibleValues = possibleValues;
    }
}
