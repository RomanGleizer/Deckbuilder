using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackBh
{
    public void Attack(int damage);
}

public interface ISpecialAttackBh
{
    public void Attack();
}