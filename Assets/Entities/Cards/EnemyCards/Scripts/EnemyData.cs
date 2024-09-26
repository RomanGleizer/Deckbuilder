using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyData : ScriptableObject
{
    [SerializeField] private int _hp;
    [SerializeField] private int _shield;
    [SerializeField] private int _damage;

    public int Hp => _hp;
    public int Shield => _shield;
    public int Damage => _damage;

}