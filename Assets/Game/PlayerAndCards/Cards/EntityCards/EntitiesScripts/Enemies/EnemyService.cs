using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyService
{
    private List<EnemyCard> _enemies = new List<EnemyCard>();
    public event Action OnEnemiesFinished;
    
    public void AddEnemy(EnemyCard enemy)
    {
        _enemies.Add(enemy);
    }

    public void RemoveEnemy(EnemyCard enemy)
    {
        _enemies.Remove(enemy);
        Debug.Log($"Enemies count: {_enemies.Count}");
        if (_enemies.Count == 0)
        {
            Debug.Log("Enemies destroyed!");
            OnEnemiesFinished?.Invoke();
        }
    }
}