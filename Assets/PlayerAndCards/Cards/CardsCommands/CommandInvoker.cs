using Custom.Collections;

public class CommandInvoker
{
    private CommandQueue _commands;

    public void SetCommandInQueue(Command command)
    {
        _commands.Enqueue(command);
    }

    public void SetCommandAndExecute(Command command)
    {
        command.Execute();
    }

    public void ExecuteCommandsQueue()
    {
        foreach (var command in _commands)
        {
            command.Execute();
        }
    }

    public void IncreaseExecuteCount()
    {
        _commands.IncreaseExecuteCount();
    }
}
