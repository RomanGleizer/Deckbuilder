using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyData : EntityData
{
    [SerializeField] private int _hp;
    [SerializeField] private int _shield;

    [SerializeField] private int _coins = 2;

    public int Hp => _hp;
    public int Shield => _shield;
    public int Coins => _coins;
}
