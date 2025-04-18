using Game.Table.Scripts.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public abstract class EntityCard : MonoBehaviour, IMoverToCell
{
    [SerializeField] protected Cell _currentCell;
    [SerializeField] protected EntityData _entityData;

    [SerializeField] private TextMeshPro _hpIndicator;
    [SerializeField] private TextMeshPro _shieldIndicator;

    protected EntityCardsIndicators _indicators;
    
    public EntityType EntityType => _entityData.Type;

    public Cell CurrentCell => _currentCell;

    public float Speed => _entityData.Speed;

    protected IMoveBh _moveBh;
    public event Action<Cell> OnMovedToCell;

    protected IInstantiator _instantiator; // for instantiate non-MonoBehaviour objs by Zenject
    private TurnManager _turnManager;
    protected WindowActivator _windowActivator;

    [Inject]
    private void Construct(IInstantiator instantiator, TurnManager turnManager, WindowActivator windowActivator)
    {
        _instantiator = instantiator;
        _turnManager = turnManager;
        _windowActivator = windowActivator;
    }

    public virtual void Init()
    {
        var subscribeHandler = _instantiator.Instantiate<SubscribeHandler>();
        subscribeHandler.SetSubscribeActions(Subscribe, Unsubscribe);
        
        _indicators = new EntityCardsIndicators(_hpIndicator, _shieldIndicator, 0, 0);
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

    public virtual void OnMouseDown() 
    {
        GameObject pauseControllerGO = GameObject.Find("PauseController");
        PauseController pauseCtrl = pauseControllerGO.GetComponent<PauseController>();
        if (pauseCtrl.isInGame)
        {
            if (_entityData.name != "ChestData")
            {
                _windowActivator.ActivateWindow(WindowType.EnemyInfo);
                EnemyInfoCard enemyInfoCard = FindObjectOfType<EnemyInfoCard>(true);
                enemyInfoCard.Name.text = _entityData.TypeOnRussian;
                enemyInfoCard.Description.text = _entityData.Description;
            }                
        }
    }
}
