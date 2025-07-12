using Xunit;
using FileSystemCommands;
using CommandRunner;

namespace task08tests;

public class FileSystemCommandsTests
{
    [Fact]
    public void DirectorySizeCommand_ShouldCalculateSize()
    {
        var testDir = Path.Combine(Path.GetTempPath(), "TestDir");
        Directory.CreateDirectory(testDir);
        File.WriteAllText(Path.Combine(testDir, "test1.txt"), "Hello");
        File.WriteAllText(Path.Combine(testDir, "test2.txt"), "World");
        
        var output = new StringWriter();
        Console.SetOut(output);

        var command = new DirectorySizeCommand(testDir);
        command.Execute();

        DirectoryInfo di = new DirectoryInfo(testDir);
        long size = di.GetFiles().Select(f => f.Length).Sum();

        Assert.Contains($"Size of '{testDir}' is {size} bytes.", output.ToString());

        Directory.Delete(testDir, true);
    }

    [Fact]
    public void FindFilesCommand_ShouldFindMatchingFiles()
    {
        var testDir = Path.Combine(Path.GetTempPath(), "TestDir");
        Directory.CreateDirectory(testDir);
        File.WriteAllText(Path.Combine(testDir, "file1.txt"), "Text");
        File.WriteAllText(Path.Combine(testDir, "file2.log"), "Log");

        var output = new StringWriter();
        Console.SetOut(output);

        var command = new FindFilesCommand(testDir, "*.txt");
        command.Execute();

        Assert.Contains("file1.txt", output.ToString());
        Assert.DoesNotContain("file2.log", output.ToString());

        Directory.Delete(testDir, true);
    }

    [Fact]
    public void CommandRunner_WorksCorrect()
    {
        var testDir = Path.Combine(Path.GetTempPath(), "TestDir");
        Directory.CreateDirectory(testDir);
        File.WriteAllText(Path.Combine(testDir, "file1.txt"), "Text");
        File.WriteAllText(Path.Combine(testDir, "file2.log"), "Log");

        var output = new StringWriter();
        Console.SetOut(output);

        var args = new string[] {testDir, "*.txt"};
        Program.Main(args);

        DirectoryInfo di = new DirectoryInfo(testDir);
        long size = di.GetFiles().Select(f => f.Length).Sum();

        Assert.Contains($"Size of '{testDir}' is {size} bytes.", output.ToString());
        Assert.Contains("file1.txt", output.ToString());
        Assert.DoesNotContain("file2.log", output.ToString());

        Directory.Delete(testDir, true);
    }
}
