using UnityEngine;

public abstract class EnemyCard : MonoBehaviour
{
    [SerializeField] protected EnemyData _enemyData;
    //[SerializeField] protected Cell _cell

    public bool IsActive { get; protected set; }

}
