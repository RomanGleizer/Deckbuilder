using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyData : ScriptableObject
{
    [SerializeField] private EntityType _type;

    [SerializeField] private int _hp;
    [SerializeField] private int _shield;
    [SerializeField] private int _speed = 10;

    public EntityType Type;

    public int Hp => _hp;
    public int Shield => _shield;
    public int Speed => _speed;
}