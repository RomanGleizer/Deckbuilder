using UnityEngine;
using Table.Scripts.Entities;

public abstract class EnemyCard : MonoBehaviour, ITakerDamage, IMover
{
    [SerializeField] protected EnemyData _enemyData;
    [SerializeField] protected Cell _cell;

    protected int _hp;
    protected int _shield;

    #region Behaviours

    protected ITakeDamageBh _takeDamageBh;
    protected IMoveBh _moveBh;

    #endregion

    public bool IsActive { get; protected set; }
    public bool IsCanMove => _moveBh.IsCanMove;

    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        _hp = _enemyData.Hp;
        _shield = _enemyData.Shield;

        new CommandHandler(this);

        InitBehaviours();
    }

    protected virtual void InitBehaviours()
    {
        //_moveBh;
        //_takeDamageBh;
    }

    public virtual void Move()
    {
        if (IsCanMove)
        {
            _moveBh.Move();
        }
    }

    public virtual void TakeDamage(int damage)
    {
        _takeDamageBh.TakeDamage(damage, ref _hp);
    }

    public virtual void Death()
    {
        gameObject.SetActive(false); // Change to ObjectPooling
    }
}
