using Zenject;

public class PlayerTurnController
{
    private TurnManager _turnManager;

    [Inject]
    private void Construct(IInstantiator instantiator, TurnManager turnManager)
    {
        _turnManager = turnManager;

        var subscribeHandler = instantiator.Instantiate<SubscribeHandler>();
        subscribeHandler.SetSubscribeActions(Subscribe, Unsubscribe);
    }

    private void ActivatePlayerTurn()
    {
        // Player turn logic
    }

    private void Subscribe()
    {
        _turnManager.OnPlayerTurn += ActivatePlayerTurn;
    }

    private void Unsubscribe()
    {
        _turnManager.OnPlayerTurn -= ActivatePlayerTurn;
    }
}