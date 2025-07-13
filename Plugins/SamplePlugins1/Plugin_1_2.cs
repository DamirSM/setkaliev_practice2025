using PluginLib;

namespace SamplePlugins1;

[PluginLoad]
public class Plugin1: IPlugin
{
    public void Execute()
    {
        Console.WriteLine("Plugin1 executed");
    }
}

[PluginLoad("Plugin1, Plugin4")]
public class Plugin2 : IPlugin
{
    public void Execute()
    {
        Console.WriteLine("Plugin2 executed after Plugin1 and Plugin4");
    }
}
