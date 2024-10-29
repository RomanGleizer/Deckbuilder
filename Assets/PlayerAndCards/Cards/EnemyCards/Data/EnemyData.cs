using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyData : ScriptableObject
{
    [SerializeField] private int _hp;
    [SerializeField] private int _shield;
    [SerializeField] private int _speed = 10;

    public int Hp => _hp;
    public int Shield => _shield;
    public int Speed => _speed;
}