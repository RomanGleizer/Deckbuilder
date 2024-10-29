using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Table.Scripts.Entities;
using UnityEngine;
using Zenject;

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

    public Command CreateRowMoveCommand(Cell mainCell, Vector2 moveDirection)
    {
        var row = _field.GetRowByCell(mainCell, true);
        var queue = new Queue<MoveCommand>();

        int unbusyCells = 0;

        if (moveDirection.x > 0) row = row.Reverse().ToArray();

        foreach (var cell in row)
        {
            if (!cell.IsBusy) unbusyCells++;
            else
            {
                var targetCell = _field.FindCell(cell, moveDirection, unbusyCells);

                var command = new MoveCommand(targetCell, false);
                cell.SetCommand(command);
                queue.Enqueue(command);

                unbusyCells = 0;
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
}