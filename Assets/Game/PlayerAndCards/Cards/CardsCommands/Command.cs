using System;
using System.Collections.Generic;
using UnityEngine;
using Table.Scripts.Entities;
using UnityEditor.Experimental.GraphView;
using System.Threading.Tasks;
using Zenject;
using System.Threading;

public enum PosInOrderType { First, NoMatter, Last }

public class CommandVisual // Тестовый класс. TODO: потом удалить
{
    private Action ActivateAction;
    private Action DeactivateAction;


    public CommandVisual(Action ActivateAction, Action DeactivateAction)
    {
        this.ActivateAction = ActivateAction;
        this.DeactivateAction = DeactivateAction;
    }

    public void ActivateVisual()
    {
        ActivateAction?.Invoke();
    }

    public void DeactivateVisual() 
    {
        DeactivateAction?.Invoke();
    }
}

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

    protected CommandVisual _visual;

    public Command()
    {
        PosInOrder = PosInOrderType.NoMatter;
    }

    public Command(PosInOrderType orderPosType)
    {
        PosInOrder = orderPosType;
    }

    public void SetVisual(Action activateVisual, Action deactivateVisual)
    {
        _visual = new CommandVisual(activateVisual, deactivateVisual);
    }

    public async virtual Task Execute(CancellationToken token)
    {
        if (_isBlocked) return;
        if (_visual != null) _visual.ActivateVisual();
        await Task.Delay(_delayInMillisec, cancellationToken : token);
        if (_visual != null && !token.IsCancellationRequested) _visual.DeactivateVisual();
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
    
    public async override Task Execute(CancellationToken token)
    {
        if (_isBlocked) return;
        await base.Execute(token);
        if (!token.IsCancellationRequested) _attacker.Attack();
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

    public async override Task Execute(CancellationToken token)
    {
        await base.Execute(token);
        if (!token.IsCancellationRequested) _supporter.Support();
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

    public async override Task Execute(CancellationToken token)
    {
        _visual.ActivateVisual();
        await _supporter.Support(token);
        if (!token.IsCancellationRequested) _visual.DeactivateVisual();
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

    public async override Task Execute(CancellationToken token)
    {
        await base.Execute(token);
        if (token.IsCancellationRequested) return;

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
    private Queue<Command> _commands;

    public RowMoveCommand(Queue<Command> commands)
    {
        CommandType = CommandType.Move;
        PosInOrder = PosInOrderType.Last;
        _commands = commands;
    }

    public async override Task Execute(CancellationToken token)
    {
        await base.Execute(token);
        if (token.IsCancellationRequested) return;
        while (_commands.Count > 0)
        {
            await _commands.Dequeue().Execute(token);
            if (token.IsCancellationRequested) return;
        }
    }
}

public class SpawnFromQueueCommand : Command
{
    private LevelPlacementStackController _stackController;
    private int _rowIndex;

    public SpawnFromQueueCommand(int rowIndex)
    {
        _rowIndex = rowIndex;
    }

    [Inject]
    private void Construct(LevelPlacementStackController stackController)
    {
        _stackController = stackController;
    }

    public async override Task Execute(CancellationToken token)
    {
        await base.Execute(token);
        if (token.IsCancellationRequested) return;
        _stackController.SpawnEntityFromPlacementStack(_rowIndex);
    }
}

public class ReleaseToQueueCommand : Command
{
    private LevelPlacementStackController _stackController;
    private int _rowIndex;
    private EnemyCard _entity;

    public ReleaseToQueueCommand(int rowIndex, EnemyCard entity)
    {
        _rowIndex = rowIndex;
        _entity = entity;
    }

    [Inject]
    private void Construct(LevelPlacementStackController stackController)
    {
        _stackController = stackController;
    }

    public async override Task Execute(CancellationToken token)
    {
        await base.Execute(token);
        if (token.IsCancellationRequested) return;
        _stackController.SetEntityToPlacementStack(_rowIndex, _entity);
    }
}

public class SwapCommand : Command
{
    private MoveCommand[] _moveCommands;

    public SwapCommand(MoveCommand[] moveCommands)
    {
        _moveCommands = moveCommands;
        CommandType = CommandType.Ability;
        PosInOrder = PosInOrderType.NoMatter;
    }

    public async override Task Execute(CancellationToken token)
    {
        await base.Execute(token);
        if (token.IsCancellationRequested) return;

        foreach (MoveCommand moveCommand in _moveCommands)
        {
            await moveCommand.Execute(token);
            if (token.IsCancellationRequested) return;
        }
    }
}

#endregion