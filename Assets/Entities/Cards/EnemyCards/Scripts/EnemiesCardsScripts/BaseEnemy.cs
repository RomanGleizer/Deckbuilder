public abstract class BaseEnemy : EnemyCard, IAttacker, ITakerDamage, IMover // Default enemy with attack, move, take damage behaviours.
{
    protected BaseEnemyData _baseEnemyData;

    #region Behaviours

    protected IAttackBh _attackBh;
    protected ITakeDamageBh _takeDamageBh;
    protected IMoveBh _moveBh;

    #endregion

    public bool IsCanMove => _moveBh.IsCanMove;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _baseEnemyData = TypeChanger.ChangeObjectType<EnemyData, BaseEnemyData>(_enemyData);
    }

    public void Attack()
    {
        _attackBh.Attack(_enemyData.Damage);
    }

    public void Move()
    {
        _moveBh.Move();
    }

    public void TakeDamage(int damage)
    {
        _takeDamageBh.TakeDamage(damage);
    }
}
