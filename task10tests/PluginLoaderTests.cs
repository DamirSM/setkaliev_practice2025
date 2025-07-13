using task10;

namespace task10tests;

public class PluginLoaderTests
{
    [Fact]
    public void PluginLoader_LoadsPluginsCorrectly()
    {
        string folder = Path.Combine(AppContext.BaseDirectory, "../../../../Plugins");
        var output = new StringWriter();
        Console.SetOut(output);
        PluginLoader.PluginExecute(folder);
        var outputList = output.ToString().Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

        Assert.Equal("Plugin1 executed", outputList[0]);
        Assert.Equal("Plugin4 executed after Plugin1", outputList[1]);
        Assert.Equal("Plugin2 executed after Plugin1 and Plugin4", outputList[2]);
        Assert.Equal("Plugin3 executed after Plugin4", outputList[3]);
    }
}
