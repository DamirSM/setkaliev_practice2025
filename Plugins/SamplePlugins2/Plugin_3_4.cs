using PluginLib;

namespace SamplePlugins2;

[PluginLoad("Plugin4")]
public class Plugin3: IPlugin
{
    public void Execute()
    {
        Console.WriteLine("Plugin3 executed after Plugin4");
    }
}

[PluginLoad("Plugin1")]
public class Plugin4 : IPlugin
{
    public void Execute()
    {
        Console.WriteLine("Plugin4 executed after Plugin1");
    }
}
