using System;
using Table.Scripts.Entities;


/// <summary>
/// Tracker that updates the current cell on which the enemy is standing
/// </summary>
public class CellTrackerByEnemy : ISubscribable
{
    private Cell _currentCell;

    private Action<Cell> UpdateCurrentCell;

    private SubscribeService _subscribeService;
    private EnemyCard _enemyCard;

    public CellTrackerByEnemy(EnemyCard enemyCard)
    {
        UpdateCurrentCell = (cell) => _currentCell = cell;
    }

    private void Construct(SubscribeService subscribeService) // Add Zenject
    {
        _subscribeService = subscribeService;
        _subscribeService.AddSubscribable(this);
    }

    public Cell GetCurrentCell()
    {
        return _currentCell;
    }

    public void Subscribe()
    {
        _enemyCard.OnMovedToCell += UpdateCurrentCell;
    }

    public void Unsubscribe()
    {
        _enemyCard.OnMovedToCell -= UpdateCurrentCell;
        _subscribeService.RemoveSubscribable(this);
    }
}