using UnityEngine;

public abstract class EnemyCard : MonoBehaviour
{
    [SerializeField] private EnemyData _enemyData;
    //[SerializeField] private Cell _cell

    public bool IsActive { get; private set; }

}
