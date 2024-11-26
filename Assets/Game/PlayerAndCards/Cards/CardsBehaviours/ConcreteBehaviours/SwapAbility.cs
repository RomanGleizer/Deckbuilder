using Game.Table.Scripts.Entities;
using UnityEngine;
using Zenject;

public class SwapAbility : IAbility
{
    private CommandFactory _commandFactory;

    private Field _field;

    private CellTrackerByEnemy _cellTracker;
    private EnemyCard _enemyCard;

    public bool IsCanUse => _cellTracker.GetCurrentCell().ColumnId > 0;

    [Inject]
    private void Construct(IInstantiator instantiator, CommandFactory commandFactory, Field field)
    {
        _commandFactory = commandFactory;
        _field = field;

        _cellTracker = instantiator.Instantiate<CellTrackerByEnemy>(new object[] { _enemyCard });
    }

    public SwapAbility(EnemyCard enemyCard)
    {
        _enemyCard = enemyCard;
    }

    public void Use()
    {
        var cell = _cellTracker.GetCurrentCell();
        _commandFactory.CreateSwapCommand(cell);
    }
}