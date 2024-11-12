using Game.Table.Scripts.Entities;
using System.Threading;
using UnityEngine;
using Zenject;

public class SpawnAttackBh : ISpecialAttackBh // Pioneer special attack
{
    private EntitySpawnSystem _spawnSystem;
    private Field _field;

    private EnemyCard _enemyCard;
    private CellTrackerByEnemy _cellTracker;

    private CommandFactory _commandFactory;
    private CommandInvoker _commandInvoker;

    private CancellationTokenSource _tokenSource;

    public SpawnAttackBh(EnemyCard enemyCard)
    {
        _enemyCard = enemyCard;
    }

    [Inject]
    private void Construct(IInstantiator instantiator, EntitySpawnSystem spawnSystem, Field field, CommandFactory commandFactory, CommandInvoker commandInvoker)
    {
        _spawnSystem = spawnSystem;
        _field = field;

        _cellTracker = instantiator.Instantiate<CellTrackerByEnemy>(new object[] { _enemyCard });

        _commandFactory = commandFactory;
        _commandInvoker = commandInvoker;

        _tokenSource = new CancellationTokenSource();
    }

    public void Attack()
    {
        var currentCell = _cellTracker.GetCurrentCell();
        var randomCell = _field.GetRandomActiveCell(currentCell);

        var command = _commandFactory.CreateRowMoveBackwardCommand(randomCell, false);
        _ = _commandInvoker.SetCommandAndExecute(command, _tokenSource.Token);

        var snare = _spawnSystem.SpawnEntity(EntityType.Snare, randomCell);
    }
}
