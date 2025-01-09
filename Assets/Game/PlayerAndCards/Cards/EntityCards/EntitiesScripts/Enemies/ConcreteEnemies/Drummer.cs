using Zenject;

public class Drummer : SpecialEnemy
{
    private int _attackDelay = 1;

    private DelayAttackBh _delayBh;

    private TurnManager _turnManager;

    [Inject]
    private void Construct(TurnManager turnManager)
    {
        _turnManager = turnManager;
    }

    protected override void InitBehaviours()
    {
        base.InitBehaviours();
        _specialAttackBh = _instantiator.Instantiate<StunPlayerAttackBh>(new object[] {1});
        _delayBh = new DelayAttackBh(_attackDelay, _turnManager, _instantiator.Instantiate<SubscribeHandler>());
    }

    public override void Attack()
    {
        if (_delayBh.IsCanAttack)
        {
            base.Attack();
        }
    }
}