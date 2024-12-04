using Zenject;

public class Sniper : CommonEnemy
{
    private int _attackDelay = 2;

    private TurnManager _turnManager;

    private DelayAttackBh _delayBh;

    [Inject]
    private void Construct(TurnManager turnManager)
    {
        _turnManager = turnManager;
    }

    protected override void InitBehaviours()
    {
        base.InitBehaviours();
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