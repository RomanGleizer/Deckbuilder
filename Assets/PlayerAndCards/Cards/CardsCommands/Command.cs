using System.Collections.Generic;

public abstract class Command
{
    public CommandType CommandType { get; protected set; }

    public abstract void Execute();
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

    public SupportCommand()
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
    private IMover _mover;

    public MoveCommand()
    {
        CommandType = CommandType.Move;
    }

    public void SetReceiver(IMover mover)
    {
        _mover = mover;
    }

    public override void Execute()
    {
        _mover.Move();
    }
}

public class RowMoveCommand : Command // Command which moves whole row
{
    private Queue<MoveCommand> _moveCommands;

    public RowMoveCommand(Queue<MoveCommand> moveCommands)
    {
        CommandType = CommandType.Move;
        _moveCommands = moveCommands;
    }

    public override void Execute()
    {
        while (_moveCommands.Count > 0)
        {
            _moveCommands.Dequeue().Execute();
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