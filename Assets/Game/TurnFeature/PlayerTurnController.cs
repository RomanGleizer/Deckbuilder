using Game.PlayerAndCards.PlayerScripts;
using Zenject;

public class PlayerTurnController
{
    private TurnManager _turnManager;
    private HandManager _handManager;

    private Player _player;

    [Inject]
    private void Construct(IInstantiator instantiator, TurnManager turnManager, Player player, HandManager handManager)
    {
        _turnManager = turnManager;

        var subscribeHandler = instantiator.Instantiate<SubscribeHandler>();
        subscribeHandler.SetSubscribeActions(Subscribe, Unsubscribe);

        _player = player;
        
        _handManager = handManager;
    }

    private void ActivatePlayerTurn()
    {
        if (_player.IsStunned) _turnManager.ChangeTurn();
        else
        {
            _player.RegenerateEnergy();
            _handManager.FillHandAfter();
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