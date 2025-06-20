﻿using Game.Table.Scripts.Entities;
using UnityEngine;
using Zenject;

public class LevelPlacementStackController
{
    private LevelPlacementStack _placementStack;

    private EntitySpawnSystem _entitySpawnSystem;
    private Field _field;

    private EntityCard _destroyingEntity;
    private IMoveToDestinationBh _moveToSpawnBh;

    private float _spawnPointX;

    private IInstantiator _instantiator;
    
    [Inject]
    private void Construct(EntitySpawnSystem entitySpawnSystem, Field field, IInstantiator instantiator)
    {
        _entitySpawnSystem = entitySpawnSystem;
        _field = field;

        _instantiator = instantiator;

        var subscribeHandler = _instantiator.Instantiate<SubscribeHandler>();
        subscribeHandler.SetSubscribeActions(Subscribe, Unsubscribe);
    }

    public LevelPlacementStackController(LevelPlacementStack placementStack, Transform spawnPoint)
    {
        _placementStack = placementStack;
        _spawnPointX = spawnPoint.position.x;

        _moveToSpawnBh = new MoveToDestinationBh(); // TODO: добавить ObjectPooling
    }

    public EntityCard SpawnEntityFromPlacementStack(int rowIndex)
    {
        var entityType = _placementStack.Pop(rowIndex);

        if (entityType == null)
        {
            Debug.Log("Stack elements is finished");
            return null;
        }

        var cell = _field.FindFirstFreeCellFromRow(rowIndex);
        EntityCard entity = null;
        if (cell != null) entity = _entitySpawnSystem.SpawnEntity(entityType.Value);

        if (entity != null)
        {
            MoveEntityToSpawnCell(cell, entity);
        }

        return entity;
    }

    public void SetEntityToPlacementStack(int rowIndex, EntityCard entity)
    {
        var entityType = entity.EntityType;
        _placementStack.Push(rowIndex, entityType);

        MoveEntityToDefaultPoint(entity);
    }

    private void MoveEntityToSpawnCell(Cell cell, EntityCard entity)
    {
        entity.transform.position = new Vector2(_spawnPointX, cell.transform.position.y);
        
        IMoveToCellBh moveBh = new MoveToCellBh(); // TODO: добавить ObjectPooling
        moveBh.SetParameters(entity.CurrentCell, cell);

        entity.StartMove(moveBh);
    }

    private void MoveEntityToDefaultPoint(EntityCard entity)
    {
        _destroyingEntity = entity;
        entity.StartMove(_moveToSpawnBh);
        _moveToSpawnBh.SetParameters(new Vector2(_spawnPointX, entity.CurrentCell.transform.position.y));
    }

    private void DestroyEntity()
    {
        MonoBehaviour.Destroy(_destroyingEntity.gameObject); // TODO: добавить ObjectPooling
    }

    private void Subscribe()
    {
        _moveToSpawnBh.OnPosRiched += DestroyEntity;
    }

    private void Unsubscribe()
    {
        _moveToSpawnBh.OnPosRiched -= DestroyEntity;
    }

    public bool IsPlacementStackEmpty()
    {
        return _placementStack.IsEmpty;
    }
}