using Game.Table.Scripts.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class EntityCard : MonoBehaviour, IMoverToCell
{
    [SerializeField] protected Cell _currentCell;
    [SerializeField] protected EntityData _entityData;

    public EntityType EntityType => _entityData.Type;

    public Cell CurrentCell => _currentCell;

    public float Speed => _entityData.Speed;

    protected IMoveBh _moveBh;
    public event Action<Cell> OnMovedToCell;

    protected IInstantiator _instantiator; // for instantiate non-MonoBehaviour objs by Zenject
    private TurnManager _turnManager;

    [Inject]
    private void Construct(IInstantiator instantiator, TurnManager turnManager)
    {
        _instantiator = instantiator;
        _turnManager = turnManager;
    }

    public virtual void Init()
    {
        var subscribeHandler = _instantiator.Instantiate<SubscribeHandler>();
        subscribeHandler.SetSubscribeActions(Subscribe, Unsubscribe);
    }

    public void SetStartCell(Cell cell)
    {
        if (_currentCell == null)
        {
            UpdateCells(cell);
        }
        else throw new System.InvalidOperationException("Start cell already exist! Cannot set start cell!");
    }

    public void StartMove(IMoveBh moveBh)
    {
        _moveBh = moveBh;
        _moveBh.Init(transform, Speed);
        _moveBh.StartMove();

        if (_moveBh is IMoveToCellBh moveToCellBh) moveToCellBh.OnCellRiched += UpdateCells;
    }

    private void UpdateCells(Cell cell)
    {
        _currentCell = cell;
        _currentCell.SetCardOnCell(this);

        OnMovedToCell?.Invoke(cell);

        if (_moveBh is IMoveToCellBh moveToCellBh) moveToCellBh.OnCellRiched -= UpdateCells;
    }

    private void Update()
    {
        if (_moveBh != null) _moveBh.UpdateBh();
    }

    protected virtual void Subscribe()
    {

    }

    protected virtual void Unsubscribe()
    {
        if (_moveBh != null && _moveBh is IMoveToCellBh moveToCellBh) moveToCellBh.OnCellRiched -= UpdateCells;
    }

    public virtual void Death()
    {
        gameObject.SetActive(false); // Change to ObjectPooling
        _currentCell.ReleaseCellFrom(this);
        _currentCell = null;
    }

    public virtual void OnMouseDown() // Тестовый метод для проверки игрового цикла. TODO: потом удалить
    {
        if (_turnManager.IsPlayerTurn) Death();
    }
}
