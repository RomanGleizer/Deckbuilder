using Table.Scripts.Entities;
using UnityEngine;
using Zenject;

public class MoveTestScript : MonoBehaviour
{
    [SerializeField] private Cell _mainCell;

    private CommandFactory _commandFactory;
    private CommandInvoker _commandInvoker;

    [Inject]
    private void Construct(CommandFactory commandFactory, CommandInvoker commandInvoker)
    {
        _commandFactory = commandFactory;
        _commandInvoker = commandInvoker;
    }

    private void Start()
    {
        var command = _commandFactory.CreateRowMoveForwardCommand(_mainCell, false);
        _commandInvoker.SetCommandAndExecute(command);
    }
}