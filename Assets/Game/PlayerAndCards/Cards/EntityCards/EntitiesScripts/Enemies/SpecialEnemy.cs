public abstract class SpecialEnemy : EnemyCard, IAttacker // enemy with special attack
{
    protected SpecialEnemyData _specialEnemyData;
    protected ISpecialAttackBh _specialAttackBh;

    protected int _attackDistance;

    public override void Init()
    {
        _specialEnemyData = TypeChanger.ChangeObjectTypeWithException<EntityData, SpecialEnemyData>(_entityData);

        _attackDistance = _specialEnemyData.AttackDistance;

        base.Init();
    }

    protected override void InitBehaviours()
    {
        base.InitBehaviours();
    }

    public virtual void Attack()
    {
        if (_currentCell.ColumnId < _attackDistance)
        {
            _specialAttackBh.Attack();
        }
    }

    public override Command CreatePriorityCommand()
    {
        Command command = null;
        if (_currentCell.ColumnId < _attackDistance)
        {
            command = _commandFactory.CreateAttackCommand(this);
            command.SetVisual(HiglightActivingEnemy, UnhiglightActivingEnemy);
        }

        return command; 
    }
}