using Game.Table.Scripts.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class EnemiesTurnController
{
    private Field _field;
    private TurnManager _turnManager;

    private CommandFactory _commandFactory;
    private CommandInvoker _commandInvoker;

    private List<int> _movedRows = new List<int>();

    private CancellationTokenSource _cancellationTokenSource;

    [Inject]
    private void Construct(IInstantiator instantiator, TurnManager turnManager, Field field, CommandFactory commandFactory, CommandInvoker commandInvoker)
    {
        _turnManager = turnManager;
        _field = field;

        _commandFactory = commandFactory;
        _commandInvoker = commandInvoker;

        var subscribeHandler = instantiator.Instantiate<SubscribeHandler>();
        subscribeHandler.SetSubscribeActions(Subscribe, Unsubscribe);

        _cancellationTokenSource = new CancellationTokenSource();
    }

    private async void ActivateEnemiesTurn()
    {
        var cells = _field.GetTraversedCells();

        foreach (var cell in cells)
        {
            var objsWithCommands = cell.GetObjectOnCell<IHavePriorityCommand>();
            
            if (objsWithCommands != null) objsWithCommands.CreatePriorityCommand();
        }

        await _commandInvoker.ExecuteCommandsQueue(_cancellationTokenSource.Token);

        foreach (var cell in cells)
        {
            var objsWithCommands = cell.GetObjectOnCell<IHavePriorityCommand>();
            if (objsWithCommands == null && !_movedRows.Contains(cell.RowId))
            {
                _movedRows.Add(cell.RowId);
                _commandFactory.CreateRowMoveForwardCommand(cell);
            }
        }
        await _commandInvoker.ExecuteCommandsQueue(_cancellationTokenSource.Token);

        _movedRows.Clear();
        _turnManager.ChangeTurn();
    }

    private void Subscribe()
    {
        _turnManager.OnEnemiesTurn += ActivateEnemiesTurn;
    }

    private void Unsubscribe()
    {
        _turnManager.OnEnemiesTurn -= ActivateEnemiesTurn;
        _cancellationTokenSource.Cancel();
    }
}
