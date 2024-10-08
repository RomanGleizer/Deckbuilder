﻿public abstract class CommonEnemy : EnemyCard, IAttacker // enemy with common attack
{
    protected CommonEnemyData _baseEnemyData;
    protected IAttackBh _attackBh;

    private int _attackDistance;
    private int _damage;

    protected override void Init()
    {
        _baseEnemyData = TypeChanger.ChangeObjectType<EnemyData, CommonEnemyData>(_enemyData);
        
        _attackDistance = _baseEnemyData.AttackDistance;
        _damage = _baseEnemyData.Damage;

        base.Init();
    }

    protected override void InitBehaviours()
    {
        _attackBh = new DamagePlayerAttackBh();
    }

    public void Attack()
    {
        if (_cell.ColumnId < _attackDistance)
        {
            _attackBh.Attack(_baseEnemyData.Damage);
        }
    }
}