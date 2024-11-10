using System;
using Table.Scripts.Entities;


/// <summary>
/// Tracker that updates the current cell on which the enemy is standing
/// </summary>
public class CellTrackerByEnemy
{
    private Cell _currentCell;

    private Action<Cell> UpdateCurrentCell;

    private SubscribeService _subscribeService;
    private EnemyCard _enemyCard;

    public CellTrackerByEnemy(EnemyCard enemyCard)
    {
        UpdateCurrentCell = (cell) => _currentCell = cell;

        _currentCell = enemyCard.CurrentCell;
        new SubscribeHandler(Subscribe, Unsubscribe);
    }

    public Cell GetCurrentCell()
    {
        return _currentCell;
    }

    private void Subscribe()
    {
        _enemyCard.OnMovedToCell += UpdateCurrentCell;
    }

    private void Unsubscribe()
    {
        _enemyCard.OnMovedToCell -= UpdateCurrentCell;
    }
}