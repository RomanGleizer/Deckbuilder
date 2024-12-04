using Game.PlayerAndCards.PlayerScripts;
using Zenject;

public class PlayerTurnController
{
    private TurnManager _turnManager;

    private Player _player;

    [Inject]
    private void Construct(IInstantiator instantiator, TurnManager turnManager, Player player)
    {
        _turnManager = turnManager;

        var subscribeHandler = instantiator.Instantiate<SubscribeHandler>();
        subscribeHandler.SetSubscribeActions(Subscribe, Unsubscribe);

        _player = player;
    }

    private void ActivatePlayerTurn()
    {
        if (_player.IsStunned) _turnManager.ChangeTurn();
        else
        {
            // Player turn logic
        }
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