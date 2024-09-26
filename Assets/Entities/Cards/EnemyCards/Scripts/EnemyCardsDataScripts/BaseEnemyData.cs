using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Base Enemy Data", menuName = "New Enemy Data")]
public class BaseEnemyData : EnemyData
{
    [SerializeField] private int _attackDistance = 1;

    public int AttackDistance => _attackDistance;
}
