public abstract class CommonEnemy : EnemyCard, IAttacker // enemy with common attack
{
    protected CommonEnemyData _baseEnemyData;
    protected IAttackBh _attackBh;

    private int _attackDistance;
    private int _damage;

    private Command _command;

    public override void Init()
    {
        _baseEnemyData = TypeChanger.ChangeObjectTypeWithException<EntityData, CommonEnemyData>(_entityData);
        
        _attackDistance = _baseEnemyData.AttackDistance;
        _damage = _baseEnemyData.Damage;

        base.Init();
    }

    protected override void InitBehaviours()
    {
        if (_attackBh == null) _attackBh = _instantiator.Instantiate<DamagePlayerAttackBh>(); // default attack
        base.InitBehaviours();
    }

    public virtual void Attack()
    {
        if (_currentCell.ColumnId < _attackDistance)
        {
            _attackBh.Attack(_baseEnemyData.Damage);
        }
    }

    public override Command CreatePriorityCommand()
    {
        _command = null;
        if (_currentCell.ColumnId < _attackDistance)
        {
            _command = _commandFactory.CreateAttackCommand(this);
            _command.SetVisual(HiglightActivingEnemy, UnhiglightActivingEnemy);
        }

        return _command;
    }

    public override void Death()
    {
        base.Death();
        if (_command != null) _command.BlockCommand();
    }
}