using System.Collections.Concurrent;
using CommandLib;

namespace task17;

public class HardStopException : Exception { }

public class ServerThread
{
    private readonly BlockingCollection<ICommand> _queue = new(new ConcurrentQueue<ICommand>());
    private readonly Thread _thread;
    private readonly int _threadId;

    private bool _isHardStopped = false;

    public ServerThread()
    {
        _thread = new Thread(WorkLoop) { IsBackground = true };
        _thread.Start();
        _threadId = _thread.ManagedThreadId;
    }

    public int ThreadId => _threadId;

    public void Post(ICommand command)
    {
        if (_queue.IsAddingCompleted)
            throw new InvalidOperationException("Поток остановлен");

        _queue.Add(command);
    }

    public void Join()
    {
        _thread.Join();
    }

    private void WorkLoop()
    {
        foreach (var command in _queue.GetConsumingEnumerable())
        {
            if (_isHardStopped)
                break;
            try
            {
                command.Execute();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка в команде: {ex.Message}");
            }
        }

        _queue.CompleteAdding();
    }

    public void PerformHardStop()
    {
        _isHardStopped = true;
        _queue.CompleteAdding();
    }

    public void RequestSoftStop()
    {
        _queue.CompleteAdding();
    }
}

public class SoftStopCommand : ICommand
{
    private readonly ServerThread _serverThread;

    public SoftStopCommand(ServerThread serverThread)
    {
        _serverThread = serverThread;
    }

    public void Execute()
    {
        if (Thread.CurrentThread.ManagedThreadId != _serverThread.ThreadId)
            throw new InvalidOperationException("SoftStop может быть выполнен только в целевом потоке");

        _serverThread.RequestSoftStop();
    }
}

public class HardStopCommand : ICommand
{
    private readonly ServerThread _serverThread;

    public HardStopCommand(ServerThread serverThread)
    {
        _serverThread = serverThread;
    }

    public void Execute()
    {
        if (Thread.CurrentThread.ManagedThreadId != _serverThread.ThreadId)
            throw new InvalidOperationException("HardStop может быть выполнен только в целевом потоке");

        _serverThread.PerformHardStop();
    }
}

public class ActionCommand : ICommand
{
    private readonly Action _action;
    public ActionCommand(Action action) => _action = action;
    public void Execute() => _action();
}
