using UnityEngine;
using Table.Scripts.Entities;
using Zenject;

public class SwapAbility : IAbility
{
    private CommandFactory _commandFactory;
    private CommandInvoker _commandInvoker;

    private Field _field;

    private CellTrackerByEnemy _cellTracker;

    public bool IsCanUse => _cellTracker.GetCurrentCell().ColumnId > 0;

    [Inject]
    private void Construct(CommandFactory commandFactory, Field field, CommandInvoker commandInvoker)
    {
        _commandFactory = commandFactory;
        _commandInvoker = commandInvoker;
        _field = field;
    }

    public SwapAbility(EnemyCard enemyCard)
    {
        _cellTracker = new CellTrackerByEnemy(enemyCard);
    }

    public void Use()
    {
        var cell = _cellTracker.GetCurrentCell();
        var command = _commandFactory.CreateSwapCommand(cell);
        _commandInvoker.SetCommandInQueue(command);
    }
}