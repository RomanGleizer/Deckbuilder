using Game.Table.Scripts.Entities;
using Table.Scripts.Entities;
using UnityEngine;
using Zenject;

public class SpawnAttackBh : ISpecialAttackBh // Pioneer special attack
{
    private EntitySpawnSystem _spawnSystem;
    private Field _field;

    private CellTrackerByEnemy _cellTracker;

    private CommandFactory _commandFactory;
    private CommandInvoker _commandInvoker;

    public SpawnAttackBh(EnemyCard enemyCard)
    {
        _cellTracker = new CellTrackerByEnemy(enemyCard);
    }

    [Inject]
    private void Construct(EntitySpawnSystem spawnSystem, Field field, CommandFactory commandFactory, CommandInvoker commandInvoker)
    {
        _spawnSystem = spawnSystem;
        _field = field;

        _commandFactory = commandFactory;
        _commandInvoker = commandInvoker;
    }

    public void Attack()
    {
        var currentCell = _cellTracker.GetCurrentCell();
        var randomCell = _field.GetRandomActiveCell(currentCell);

        var command = _commandFactory.CreateRowMoveBackwardCommand(randomCell);
        _commandInvoker.SetCommandAndExecute(command);

        var snare = _spawnSystem.SpawnEntity(EntityType.Snare, randomCell);
    }
}
