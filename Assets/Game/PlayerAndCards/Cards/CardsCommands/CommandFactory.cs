using System.Collections;
using System.Collections.Generic;
using Table.Scripts.Entities;
using UnityEngine;

public class CommandFactory
{
    private Field _field;

    public void SetField(Field field)
    {
        _field = field;
    }

    public Command CreateAttackCommand()
    {
        return new AttackCommand();
    }
    
    public Command CreateSupportCommand(PosInOrderType posInOrder)
    {
        return new SupportCommand(posInOrder);
    }
    
    public Command CreateMoveCommand(Cell targetCell)
    {
        return new MoveCommand(targetCell, true);
    }

    public Command CreateRowMoveForwardCommand(Cell mainCell)
    {
        var row = _field.GetRowByCell(mainCell, true);
        return CreateRowMoveCommandWithoutBusyCells(row);
    }

    private Command CreateRowMoveCommandWithoutBusyCells(Cell[] sortedRow)
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

                var command = new MoveCommand(targetCell, false);
                cell.SetCommand(command);
                queue.Enqueue(command);
            }
        }

        return new RowMoveCommand(queue);
    }

    public Command CreateRowMoveBackwardCommand(Cell mainCell)
    {
        var row = _field.GetRowByCell(mainCell, true);

        return CreateRowMoveCommand(mainCell, row[row.Length - 1], row);
    }

    private Command CreateRowMoveCommand(Cell fromCell, Cell toCell, Cell[] sortedRow)
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
                var command = new MoveCommand(targetCell, false);
                cell.SetCommand(command);
                queue.Enqueue(command);
            }
        }

        return new RowMoveCommand(queue);
    }

    public Command CreateSwapCommand(Cell currentCell)
    {
        var nextCell = _field.FindCell(currentCell, Vector2.left, 1);
        var commands = new MoveCommand[2] { new MoveCommand(nextCell, false), new MoveCommand(currentCell, false) };

        currentCell.SetCommand(commands[0]);
        nextCell.SetCommand(commands[1]);

        return new SwapCommand(commands);
    }

    public Command CreateTakeDamageCommand(int damage)
    {
        return new TakeDamageCommand(damage);
    }

    public Command CreateInvincibilityCommand()
    {
        return new InvincibilityCommand();
    }

    public Command CreateActionCommand()
    {
        return new ActionCommand();
    }
}