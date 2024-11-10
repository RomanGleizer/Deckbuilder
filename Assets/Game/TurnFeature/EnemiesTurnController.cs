using Game.Table.Scripts.Entities;
using Table.Scripts.Entities;
using UnityEngine;
using Zenject;

public class EnemiesTurnController
{
    private Field _field;
    private TurnManager _turnManager;
    private CommandFactory _commandFactory;

    [Inject]
    private void Construct(IInstantiator instantiator, TurnManager turnManager, Field field, CommandFactory commandFactory)
    {
        _turnManager = turnManager;
        _field = field;
        _commandFactory = commandFactory;

        var subscribeHandler = instantiator.Instantiate<SubscribeHandler>();
        subscribeHandler.SetSubscribeActions(Subscribe, Unsubscribe);
    }

    private void ActivateEnemiesTurn()
    {
        //_field.SetCommand(_commandFactory.CreateActionCommand());
        Debug.Log("Start command order");
    }

    private void Subscribe()
    {
        _turnManager.OnEnemiesTurn += ActivateEnemiesTurn;
    }

    private void Unsubscribe()
    {
        _turnManager.OnEnemiesTurn -= ActivateEnemiesTurn;
    }
}
