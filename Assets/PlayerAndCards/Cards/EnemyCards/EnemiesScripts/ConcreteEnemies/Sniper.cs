public class Sniper : CommonEnemy
{
    private int _attackDelay = 2;
    private int _currentDelay = 2;

    private bool _isCanAttack;

    public override void Attack()
    {
        if (_isCanAttack)
        {
            base.Attack();
        }
    }

    public void UpdateDelayParameters()
    {
        _currentDelay = 0;

        if (_currentDelay >= _attackDelay) 
        {
            ActivateAttackBh();
            _currentDelay++;
        }
        else DeactivateAttackBh();
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
        //_turnManager.OnTurnFinished += UpdateDelayParameters;
    }

    protected override void Unsubscribe()
    {
        base.Unsubscribe();
        //_turnManager.OnTurnFinished -= UpdateDelayParameters;
    }
}