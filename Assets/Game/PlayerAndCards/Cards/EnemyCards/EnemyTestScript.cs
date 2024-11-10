using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTestScript : MonoBehaviour // Script to test enemy interactions
{
    [SerializeField] private EnemyCard _enemyCard;

    [SerializeField] private EnemyCard[] _initedEnemyCards;

    void Awake()
    {
        //var concreteEnemy = _enemyCard as Pioneer;
        InitEnemies();
        //concreteEnemy.Attack();
    }

    private void InitEnemies()
    {
        foreach (var enemy in _initedEnemyCards)
        {
            enemy.Init();
            enemy.SetStartCell(enemy.CurrentCell);
        }
    }
}
