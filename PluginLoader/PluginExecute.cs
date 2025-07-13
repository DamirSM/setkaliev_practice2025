using System.Reflection;
using PluginLib;

namespace task10;

public class PluginLoader
{
    public static void PluginExecute(string folder)
    {
        var dlls = Directory.EnumerateFiles(folder, "*.dll", SearchOption.AllDirectories).Where(p =>!p.Contains("obj")).ToList();
        var assemblies = dlls.Select(Assembly.LoadFrom).ToList();
        var pluginTypes = assemblies.SelectMany(a => a.GetTypes()).Where(t => t.GetCustomAttribute<PluginLoadAttribute>() != null).ToList();
        var graph = new Dictionary<string, List<string>>();
        foreach (var type in pluginTypes)
        {
            var attr = type.GetCustomAttribute<PluginLoadAttribute>()!;
            graph[type.Name] = attr.DependsOn.Split(", ").ToList();
        }

        var sorted = Sort(graph);
        
        foreach (var name in sorted)
        {
            var type = pluginTypes.Single(t => t.Name == name);
            IPlugin plugin = (IPlugin)Activator.CreateInstance(type)!;
            plugin?.Execute();
        }
    }

    public static List<string> Sort(Dictionary<string, List<string>> graph)
    {
        Dictionary<string, List<string>> cleanGraph = graph
        .Where(kv => !string.IsNullOrWhiteSpace(kv.Key)).ToDictionary(kv => kv.Key, kv => kv.Value.Where(dep => !string.IsNullOrWhiteSpace(dep)).ToList());

        var visited = new Dictionary<string, bool>();
        var result  = new List<string>();

        void DFS(string node)
        {
            if (visited.TryGetValue(node, out var done))
            {
                if (!done)
                    throw new InvalidOperationException($"Цикл зависимостей обнаружен на узле «{node}»");
                return;
            }

            visited[node] = false;
            if (cleanGraph.TryGetValue(node, out var deps))
            {
                foreach (var dep in deps)
                    DFS(dep);
            }

            visited[node] = true;
            result.Add(node);
        }

        foreach (var node in cleanGraph.Keys)
        {
            if (!visited.ContainsKey(node))
                DFS(node);
        }

        return result;
    }
}
