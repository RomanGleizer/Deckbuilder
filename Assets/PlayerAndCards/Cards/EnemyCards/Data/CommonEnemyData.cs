using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New EnemyData", menuName = "New CommonEnemyData")]
public class CommonEnemyData : EnemyData
{
    [SerializeField] private int _attackDistance = 1;
    [SerializeField] private int _damage = 1;

    public int AttackDistance => _attackDistance;
    public int Damage => _damage;
}
