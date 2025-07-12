using System.Reflection;
using CommandLib;

namespace CommandRunner;

public class Program
{
    public static void Main(string[] args)
    {
        string dir = args[0];
        string pattern = args[1];
        string dllPath = Path.Combine(AppContext.BaseDirectory, "FileSystemCommands.dll");
        Console.WriteLine(dllPath);
        
        Assembly assem = Assembly.LoadFrom(dllPath);
        Type[] types = assem.GetTypes();

        foreach (var type in types)
        {
            ICommand cmd = null;
            switch (type.Name)
            {
                case "DirectorySizeCommand":
                    cmd = (ICommand)Activator.CreateInstance(type, dir);
                    break;
                case "FindFilesCommand":
                    cmd = (ICommand)Activator.CreateInstance(type, dir, pattern);
                    break;
            };
            cmd?.Execute();
        }
    }
}
