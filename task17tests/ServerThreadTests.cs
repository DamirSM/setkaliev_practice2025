using task17;

namespace task17tests;

public class ServerThreadTests
{
    [Fact]
    public void HardStopCommmand_Stops_Immediately()
    {
        var serverThread = new ServerThread();
        var executed1 = false;
        var executed2 = false;

        serverThread.Post(new ActionCommand(() => executed1 = true));
        serverThread.Post(new HardStopCommand(serverThread));
        serverThread.Post(new ActionCommand(() => executed2 = true));

        serverThread.Join();

        Assert.True(executed1);
        Assert.False(executed2);
    }

    [Fact]
    public void SoftStopCommand_Processes_All_Commands()
    {
        var serverThread = new ServerThread();
        var executed1 = false;
        var executed2 = false;

        serverThread.Post(new ActionCommand(() => executed1 = true));
        serverThread.Post(new SoftStopCommand(serverThread));
        serverThread.Post(new ActionCommand(() => executed2 = true));

        serverThread.Join();

        Assert.True(executed1);
        Assert.True(executed2);
    }

    [Fact]
    public void StopCommands_Throw_If_Called_From_Wrong_Thread()
    {
        var serverThread = new ServerThread();
        var hardStop = new HardStopCommand(serverThread);
        var softStop = new SoftStopCommand(serverThread);

        Assert.Throws<InvalidOperationException>(() => hardStop.Execute());
        Assert.Throws<InvalidOperationException>(() => softStop.Execute());
    }
}
