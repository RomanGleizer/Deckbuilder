using System.Collections.Generic;
using System.Threading;
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

    public async Task SetCommandAndExecute(Command command, CancellationToken token)
    {
        _ = command.Execute(token);
    }

    public async Task ExecuteCommandsQueue(CancellationToken token)
    {
        await _commands.ExecuteCommandsQueue(token);
    }

    public async Task RepeatAttackCommands(CancellationToken token)
    {
        await _commands.RepeatAttackCommands(token);
    }
}
