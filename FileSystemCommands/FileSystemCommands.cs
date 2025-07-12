using CommandLib;

namespace FileSystemCommands;

public class DirectorySizeCommand : ICommand
{
    private string _path;
    
    public DirectorySizeCommand(string path)
    {
        _path = path;
    }

    public void Execute()
    {
        long size = GetDirectorySize(_path);
        Console.WriteLine($"Size of '{_path}' is {size} bytes.");
    }

    private long GetDirectorySize(string path)
    {
        DirectoryInfo di = new DirectoryInfo(path);
        long size = di.GetFiles().Select(f => f.Length).Sum();
        return size;
    }
}

public class FindFilesCommand : ICommand
{
    private readonly string _path;
    private readonly string _pattern;

    public FindFilesCommand(string path, string searchPattern)
    {
        _path = path;
        _pattern = searchPattern;
    }

    public void Execute()
    {
        string[] files = Directory.GetFiles(_path, _pattern);
        Console.WriteLine($"Files in '{_path}' matching '{_pattern}':");
        foreach (var file in files)
        {
            Console.WriteLine(file);
        }
    }
}
