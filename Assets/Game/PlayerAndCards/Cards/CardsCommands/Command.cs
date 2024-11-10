using System;
using System.Collections.Generic;
using UnityEngine;
using Table.Scripts.Entities;

public enum PosInOrderType { First, NoMatter, Last }

public abstract class Command : IPriorityObj, IComparable
{
    public CommandType CommandType { get; protected set; }

    public int Priority => (int)PosInOrder;
    public PosInOrderType PosInOrder { get; protected set; }

    protected bool _isAddToOrder = true;
    public bool IsAddToOrder => _isAddToOrder;

    public Command()
    {
        PosInOrder = PosInOrderType.NoMatter;
        _isAddToOrder = true;
    }

    public Command(PosInOrderType orderPosType)
    {
        PosInOrder = orderPosType;
        _isAddToOrder = true;
    }

    public Command(bool isAddToOrder)
    {
        _isAddToOrder = isAddToOrder;
    }

    public abstract void Execute();

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
    
    public override void Execute()
    {
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

    public override void Execute()
    {
        _supporter.Support();
    }
}

#endregion

#region Move Commands

public class MoveCommand : Command
{
    private IMoverToCell _mover;
    private Cell _cell;

    public MoveCommand(Cell targetCell, bool isAddToOrder)
    {
        CommandType = CommandType.Move;
        _isAddToOrder = isAddToOrder;
        _cell = targetCell;
    }

    public void SetReceiver(IMoverToCell mover)
    {
        _mover = mover;
    }

    public override void Execute()
    {
        Debug.Log("Execute move command");
        
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

    public override void Execute()
    {
        Debug.Log("Execute row move command");
        while (_moveCommands.Count > 0)
        {
            _moveCommands.Dequeue().Execute();
        }
    }
}

public class SpawnFromQueueCommand : Command
{
    public override void Execute()
    {
        throw new NotImplementedException();
    }
}

public class ReleaseToQueueCommand : Command
{
    public override void Execute()
    {
        throw new NotImplementedException();
    }
}

public class SwapCommand : Command
{
    private MoveCommand[] _moveCommands;

    public SwapCommand(MoveCommand[] moveCommands)
    {
        _moveCommands = moveCommands;
    }

    public override void Execute()
    {
        Debug.Log("Execute swap command");

        foreach (MoveCommand moveCommand in _moveCommands)
        {
            moveCommand.Execute();
        }
    }
}

#endregion

#region Take Damage Commands

public class TakeDamageCommand : Command
{
    private int _damage;

    private ITakerDamage _takerDamage;

    public TakeDamageCommand(int damage)
    {
        CommandType = CommandType.TakeDamage;
        _damage = damage;
    }

    public void SetReceiver(ITakerDamage takerDamage)
    {
        _takerDamage = takerDamage;
    }

    public override void Execute()
    {
        _takerDamage.TakeDamage(_damage);
    }
}

#endregion

public class InvincibilityCommand : Command
{
    private IInvincibilable _invincibilable;

    public void SetReceiver(IInvincibilable invincibilable)
    {
        _invincibilable = invincibilable;   
    }

    public override void Execute()
    {
        _invincibilable.ActivateInvincibility();
    }
}