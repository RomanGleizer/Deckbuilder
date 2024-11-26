using Game.Table.Scripts.Entities;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class EnemiesTurnController
{
    private Cell[] _cells;
    private TurnManager _turnManager;

    private CommandFactory _commandFactory;
    private CommandInvoker _commandInvoker;

    private CancellationTokenSource _cancellationTokenSource;
    private ValueCancelTokenSource _valueCancelTokenSource;

    private bool[] _rowsMoveState;

    [Inject]
    private void Construct(IInstantiator instantiator, TurnManager turnManager, Field field, CommandFactory commandFactory, CommandInvoker commandInvoker)
    {
        _turnManager = turnManager;
        _cells = field.GetTraversedCells();

        _commandFactory = commandFactory;
        _commandInvoker = commandInvoker;

        var subscribeHandler = instantiator.Instantiate<SubscribeHandler>();
        subscribeHandler.SetSubscribeActions(Subscribe, Unsubscribe);

        _cancellationTokenSource = new CancellationTokenSource();
        _valueCancelTokenSource = new ValueCancelTokenSource();

        _rowsMoveState = new bool[field.RowsCount];
    }

    private async void ActivateEnemiesTurn()
    {
        foreach (var cell in _cells)
        {
            var objsWithCommands = cell.GetObjectOnCell<IHavePriorityCommand>();

            if (objsWithCommands != null)
            {
                var command = objsWithCommands.CreatePriorityCommand();
                if (command != null) command.SetValueCancellationToken(_valueCancelTokenSource.GetToken(cell.RowId));
            }
        }

        await _commandInvoker.ExecuteCommandsQueue(_cancellationTokenSource.Token);

        for (int i = 0; i < _rowsMoveState.Length; ++i)
        {
            _commandFactory.CreateRowMoveForwardCommand(i);
        }

        await _commandInvoker.ExecuteCommandsQueue(_cancellationTokenSource.Token);

        ReleaseAfterTurn();
        _turnManager.ChangeTurn();
    }

    private void ReleaseAfterTurn()
    {
        for (int i = 0; i < _rowsMoveState.Length; ++i)
        {
            _rowsMoveState[i] = false;
        }

        _valueCancelTokenSource.Release();
    }

    private void Subscribe()
    {
        _turnManager.OnEnemiesTurn += ActivateEnemiesTurn;

        foreach (var cell in _cells)
        {
            cell.OnObjDestroyed += AddRowMoveState;
        }
    }

    private void Unsubscribe()
    {
        _turnManager.OnEnemiesTurn -= ActivateEnemiesTurn;

        foreach (var cell in _cells)
        {
            cell.OnObjDestroyed -= AddRowMoveState;
        }

        _cancellationTokenSource.Cancel();
    }

    private void AddRowMoveState(Cell cell)
    {
        if (_rowsMoveState[cell.RowId] == false)
        {
            _valueCancelTokenSource.Cancel(cell.RowId);
            _rowsMoveState[cell.RowId] = true;
        }
    }
}
