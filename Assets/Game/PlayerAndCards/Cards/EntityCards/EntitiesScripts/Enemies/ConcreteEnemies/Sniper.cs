using Zenject;

public class Sniper : CommonEnemy
{
    private int _attackDelay = 2;
    private int _currentDelay = 2;

    private bool _isCanAttack;

    private TurnManager _turnManager;

    [Inject]
    private void Construct(TurnManager turnManager)
    {
        _turnManager = turnManager;
    }

    public override void Attack()
    {
        if (_isCanAttack)
        {
            base.Attack();
        }
    }

    public void UpdateDelayParameters()
    {
        if (_currentDelay >= _attackDelay)
        {
            ActivateAttackBh();
            _currentDelay = 0;
        }
        else
        {
            _currentDelay++;
            DeactivateAttackBh();
        }
    }

    private void ActivateAttackBh()
    {
        _isCanAttack = true;
    }

    private void DeactivateAttackBh() 
    {
        _isCanAttack = false;
    }

    protected override void Subscribe()
    {
        base.Subscribe();
        _turnManager.OnEnemiesTurn += UpdateDelayParameters;
    }

    protected override void Unsubscribe()
    {
        base.Unsubscribe();
        _turnManager.OnEnemiesTurn -= UpdateDelayParameters;
    }
}