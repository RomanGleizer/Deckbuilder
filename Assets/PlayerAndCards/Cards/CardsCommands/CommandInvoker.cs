using System.Collections.Generic;

public class CommandInvoker
{
    private Queue<Command> _commands = new Queue<Command>();

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
        while (_commands.Count > 0) // need to add delay in future
        {
            _commands.Dequeue().Execute();
        }
    }
}