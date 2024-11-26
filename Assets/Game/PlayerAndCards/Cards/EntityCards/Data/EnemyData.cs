using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyData : EntityData
{
    [SerializeField] private int _hp;
    [SerializeField] private int _shield;

    public int Hp => _hp;
    public int Shield => _shield;

}
