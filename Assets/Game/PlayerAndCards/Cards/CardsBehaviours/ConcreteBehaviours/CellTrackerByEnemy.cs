using System;
using Game.Table.Scripts.Entities;
using UnityEngine;
using Zenject;


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

        _enemyCard = enemyCard;
        _currentCell = enemyCard.CurrentCell;
    }

    [Inject]
    private void Construct(IInstantiator instantiator)
    {
        var subscribeHandler = instantiator.Instantiate<SubscribeHandler>();
        subscribeHandler.SetSubscribeActions(Subscribe, Unsubscribe);
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