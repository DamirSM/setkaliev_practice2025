namespace PluginLib;

public interface IPlugin
{
    void Execute();
}

[AttributeUsage(AttributeTargets.Class)]
public class PluginLoadAttribute : Attribute
{
    public string DependsOn { get; }

    public PluginLoadAttribute()
    {
        this.DependsOn = String.Empty;
    }

    public PluginLoadAttribute(string DependsOn)
    {
        this.DependsOn = DependsOn;
    }
}
