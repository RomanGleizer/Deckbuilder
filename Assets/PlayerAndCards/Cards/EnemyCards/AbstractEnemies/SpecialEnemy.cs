public abstract class SpecialEnemy : EnemyCard, IAttacker // enemy with special attack
{
    protected SpecialEnemyData _specialEnemyData;
    protected ISpecialAttackBh _specialAttackBh;

    protected int _attackDistance;

    protected override void Init()
    {
        _specialEnemyData = TypeChanger.ChangeObjectType<EnemyData, SpecialEnemyData>(_enemyData);

        _attackDistance = _specialEnemyData.AttackDistance;

        base.Init();
    }

    public void Attack()
    {
        if (_cell.ColumnId < _attackDistance)
        {
            _specialAttackBh.Attack();
        }
    }
}