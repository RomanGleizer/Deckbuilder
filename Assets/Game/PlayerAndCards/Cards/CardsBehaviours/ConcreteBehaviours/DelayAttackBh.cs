public class DelayAttackBh
{
    private int _attackDelay;
    private int _currentDelay;

    public bool IsCanAttack { get; private set; }

    private TurnManager _turnManager;

    public DelayAttackBh(int delay, TurnManager turnManager, SubscribeHandler subscribeHandler)
    {
        _attackDelay = delay;
        _currentDelay = delay;

        _turnManager = turnManager;
        subscribeHandler.SetSubscribeActions(Subscribe, Unsubscribe);
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
        IsCanAttack = true;
    }

    private void DeactivateAttackBh()
    {
        IsCanAttack = false;
    }

    private void Subscribe()
    {
        _turnManager.OnEnemiesTurn += UpdateDelayParameters;
    }

    private void Unsubscribe()
    {
        _turnManager.OnEnemiesTurn -= UpdateDelayParameters;
    }
}