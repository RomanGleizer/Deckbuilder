using System;
using System.Collections.Generic;
using UnityEngine;
using Table.Scripts.Entities;
using UnityEditor.Experimental.GraphView;
using System.Threading.Tasks;

public enum PosInOrderType { First, NoMatter, Last }

public abstract class Command : IPriorityObj, IComparable
{
    public CommandType CommandType { get; protected set; }

    public int Priority => (int)PosInOrder;
    public PosInOrderType PosInOrder { get; protected set; }

    private int _delayInMillisec = 0;
    public float DelayInSec
    { 
        set 
        {
            if (value < 0) _delayInMillisec = 0;
            else _delayInMillisec = (int)(value * 1000);
        } 
    }

    protected bool _isBlocked;

    public Command()
    {
        PosInOrder = PosInOrderType.NoMatter;
    }

    public Command(PosInOrderType orderPosType)
    {
        PosInOrder = orderPosType;
    }

    public async virtual Task Execute()
    {
        if (_isBlocked) return;
        await Task.Delay(_delayInMillisec);
    }

    public void BlockCommand()
    {
        _isBlocked = true;
    }

    public int CompareTo(object obj)
    {
        if (this == obj) return 0;
        else return -1;
    }
}

#region Attack And Support Commands

public class AttackCommand : Command
{
    private IAttacker _attacker;

    public AttackCommand()
    {
        CommandType = CommandType.Attack;
    }

    public void SetReceiver(IAttacker attacker)
    {
        _attacker = attacker;
    }
    
    public async override Task Execute()
    {
        if (_isBlocked) return;
        await base.Execute();
        Debug.Log(_attacker + " execute AttackCommand");
        _attacker.Attack();
    }
}

public class SupportCommand : Command
{
    private ISupporter _supporter;

    public SupportCommand(PosInOrderType posInOrder) : base(posInOrder)
    {
        CommandType = CommandType.Support;
    }

    public void SetReceiver(ISupporter supporter)
    {
        _supporter = supporter;
    }

    public async override Task Execute()
    {
        await base.Execute();
        Debug.Log(_supporter + " execute SupportCommand");
        _supporter.Support();
    }
}

public class AsyncSupportCommand : Command
{
    private IAsyncSupporter _supporter;

    public AsyncSupportCommand(PosInOrderType posInOrder) : base(posInOrder)
    {
        CommandType = CommandType.Support;
    }

    public void SetReceiver(IAsyncSupporter supporter)
    {
        _supporter = supporter;
    }

    public async override Task Execute()
    {
        await _supporter.Support();
    }
}

#endregion

#region Move Commands

public class MoveCommand : Command
{
    private IMoverToCell _mover;
    private Cell _cell;

    public MoveCommand(Cell targetCell)
    {
        CommandType = CommandType.Move;
        _cell = targetCell;
    }

    public void SetReceiver(IMoverToCell mover)
    {
        _mover = mover;
    }

    public async override Task Execute()
    {
        await base.Execute();
        IMoveToCellBh moveBh = new MoveToCellBh(); // TODO: добавить ObjectPooling 
        moveBh.SetParameters(_mover.CurrentCell, _cell);

        _mover.StartMove(moveBh);
    }
}

/// <summary>
/// Command which moves choosed cells in the row
/// </summary>
public class RowMoveCommand : Command
{
    private Queue<MoveCommand> _moveCommands;

    public RowMoveCommand(Queue<MoveCommand> moveCommands)
    {
        CommandType = CommandType.Move;
        PosInOrder = PosInOrderType.Last;
        _moveCommands = moveCommands;
    }

    public async override Task Execute()
    {
        await base.Execute();
        Debug.Log("Execute row move command");
        while (_moveCommands.Count > 0)
        {
            await _moveCommands.Dequeue().Execute();
        }
    }
}

public class SpawnFromQueueCommand : Command
{
    public async override Task Execute()
    {
        await base.Execute();
    }
}

public class ReleaseToQueueCommand : Command
{
    public async override Task Execute()
    {
        await base.Execute();
    }
}

public class SwapCommand : Command
{
    private MoveCommand[] _moveCommands;

    public SwapCommand(MoveCommand[] moveCommands)
    {
        _moveCommands = moveCommands;
        CommandType = CommandType.Ability;
    }

    public async override Task Execute()
    {
        await base.Execute();
        Debug.Log("Execute swap command");

        foreach (MoveCommand moveCommand in _moveCommands)
        {
            await moveCommand.Execute();
        }
    }
}

#endregion