using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class CommandQueueController
{
    private List<Command> _commands = new List<Command>();
    private MonoBehaviour _monoBhForCoroutine;

    private bool _isExecuting;

    public async Task ExecuteCommandsQueue(CancellationToken token) // TODO: заменить на UniTask
    {
        _isExecuting = true;

        foreach (var command in _commands)
        {
            await command.Execute(token);
            if (token.IsCancellationRequested) return;
        }

        _isExecuting = false;
        _commands.Clear();
    }

    public async Task RepeatAttackCommands(CancellationToken token) // TODO: заменить на UniTask
    {
        if (!_isExecuting) throw new InvalidOperationException("Сommands are not running yet. It is impossible to repeat the commands!");
 
        foreach (var command in _commands)
        {
            if (command.CommandType == CommandType.Attack)
            {
                await command.Execute(token);
                if (token.IsCancellationRequested) return;
            }
        }
    }

    public void InsertByPriority(Command command)
    {
        var lastIndex = _commands.FindLastIndex(cm => cm.Priority <= command.Priority);

        if (lastIndex == -1) _commands.Insert(0, command);
        else if (lastIndex + 1 == _commands.Count) _commands.Add(command);
        else _commands.Insert(lastIndex + 1, command);
    }
}