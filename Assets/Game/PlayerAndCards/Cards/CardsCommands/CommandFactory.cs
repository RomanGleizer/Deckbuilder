using System.Collections;
using System.Collections.Generic;
using Game.Table.Scripts.Entities;
using Table.Scripts.Entities;
using UnityEngine;
using Zenject;

public class CommandFactory
{
    private Field _field;

    private CommandInvoker _commandInvoker;

    private float _commandExecuteDelay;

    public void SetField(Field field)
    {
        _field = field;
    }

    public CommandFactory(float commandExecuteDelay)
    {
        _commandExecuteDelay = commandExecuteDelay;
    }

    [Inject]
    private void Construct(CommandInvoker commandInvoker)
    {
        _commandInvoker = commandInvoker;
    }

    public Command CreateAttackCommand(IAttacker receiver, bool isAddToOrder = true)
    {
        var command = new AttackCommand();
        command.SetReceiver(receiver);
        command.DelayInSec = _commandExecuteDelay;

        if (isAddToOrder) AddComandInQueue(command);
        return command;
    }
    
    public Command CreateSupportCommand(ISupporter receiver, PosInOrderType posInOrder = PosInOrderType.First, bool isAddToOrder = true)
    {
        var command = new SupportCommand(posInOrder);
        command.SetReceiver(receiver);
        command.DelayInSec = _commandExecuteDelay;

        if (isAddToOrder) AddComandInQueue(command);
        return command;
    }

    public Command CreateAsyncSupportCommand(IAsyncSupporter receiver, PosInOrderType posInOrder = PosInOrderType.First, bool isAddToOrder = true)
    {
        var command = new AsyncSupportCommand(posInOrder);

        command.SetReceiver(receiver);

        if (isAddToOrder) AddComandInQueue(command);
        return command;
    }
    
    public Command CreateMoveCommand(IMoverToCell receiver, Cell targetCell, bool isAddToOrder = true)
    {
        var command = new MoveCommand(targetCell);
        command.SetReceiver(receiver);

        if (isAddToOrder) AddComandInQueue(command);
        return command;
    }

    public Command CreateRowMoveForwardCommand(Cell mainCell, bool isAddToOrder = true)
    {
        var row = _field.GetRowByCell(mainCell, true);
        return CreateRowMoveCommandWithoutBusyCells(row, isAddToOrder);
    }

    private Command CreateRowMoveCommandWithoutBusyCells(Cell[] sortedRow, bool isAddToOrder = true)
    {
        var queue = new Queue<MoveCommand>();
        int unbusyCells = 0;

        for (int i = 0; i < sortedRow.Length; ++i)
        {
            var cell = sortedRow[i];

            if (!cell.IsBusy) unbusyCells++;
            else if (unbusyCells > 0)
            {
                var targetCell = sortedRow[i - unbusyCells];

                var mover = cell.GetObjectOnCell<IMoverToCell>();
                var command = CreateMoveCommand(mover, targetCell, false) as MoveCommand;

                queue.Enqueue(command);
            }
        }

        var rowMoveCommand = new RowMoveCommand(queue);
        rowMoveCommand.DelayInSec = _commandExecuteDelay;
        if (isAddToOrder) AddComandInQueue(rowMoveCommand);

        return rowMoveCommand;
    }

    public Command CreateRowMoveBackwardCommand(Cell mainCell, bool isAddToOrder = true)
    {
        var row = _field.GetRowByCell(mainCell, true);

        var command = CreateRowMoveCommand(mainCell, row[row.Length - 1], row, isAddToOrder);
        command.DelayInSec = 0;
        
        return command;
    }

    private Command CreateRowMoveCommand(Cell fromCell, Cell toCell, Cell[] sortedRow, bool isAddToOrder = true)
    {
        bool isStartMove = false;
        var queue = new Queue<MoveCommand>();

        for (int i = 0; i < sortedRow.Length; ++i)
        {
            var cell = sortedRow[i];

            if (!isStartMove && cell == fromCell) isStartMove = true;
            if (!isStartMove) continue;

            if (cell == toCell) break;
            if (!cell.IsBusy) break;

            if (i != sortedRow.Length - 1)
            {
                var targetCell = sortedRow[i + 1];

                var mover = cell.GetObjectOnCell<IMoverToCell>();
                var command = CreateMoveCommand(mover, targetCell, false) as MoveCommand;

                queue.Enqueue(command);
            }
        }

        var rowMoveCommand = new RowMoveCommand(queue);
        rowMoveCommand.DelayInSec = _commandExecuteDelay;
        if (isAddToOrder) AddComandInQueue(rowMoveCommand);

        return rowMoveCommand;
    }

    public Command CreateSwapCommand(Cell currentCell, bool isAddToOrder = true)
    {
        var nextCell = _field.FindCell(currentCell, Vector2.left, 1);
        var commands = new MoveCommand[2];

        commands[0] = CreateMoveCommand(currentCell.GetObjectOnCell<IMoverToCell>(), nextCell, false) as MoveCommand;
        commands[1] = CreateMoveCommand(nextCell.GetObjectOnCell<IMoverToCell>(), currentCell, false) as MoveCommand;

        var swapCommand = new SwapCommand(commands);
        swapCommand.DelayInSec = _commandExecuteDelay;
        if (isAddToOrder) AddComandInQueue(swapCommand);

        return swapCommand;
    }

    private void AddComandInQueue(Command command)
    {
        _commandInvoker.SetCommandInQueue(command);
    }
}