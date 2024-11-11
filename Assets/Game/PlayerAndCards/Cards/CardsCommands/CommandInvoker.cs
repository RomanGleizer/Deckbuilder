using System.Threading.Tasks;
using UnityEngine;

public class CommandInvoker
{
    private CommandQueueController _commands;

    public CommandInvoker()
    {
        _commands = new CommandQueueController();
    }

    public void SetCommandInQueue(Command command)
    {
        _commands.InsertByPriority(command);
    }

    public void SetCommandAndExecute(Command command)
    {
        _ = command.Execute();
    }

    public async Task ExecuteCommandsQueue()
    {
        await _commands.ExecuteCommandsQueue();
    }

    public async Task RepeatAttackCommands()
    {
        await _commands.RepeatAttackCommands();
    }
}
