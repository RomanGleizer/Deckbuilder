using UnityEngine;

[CreateAssetMenu(fileName = "New EnemyData", menuName = "New SpecialEnemyData")]
public class SpecialEnemyData : EnemyData
{
    [SerializeField] private int _attackDistance = 1;

    public int AttackDistance => _attackDistance;
}
