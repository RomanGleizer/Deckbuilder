using UnityEngine;
using Table.Scripts.Entities;
using System;
using Zenject;

public abstract class EnemyCard : MonoBehaviour, ITakerDamage, IMoverToCell, IInvincibilable, IHavePriorityCommand
{
    [SerializeField] protected EnemyData _enemyData;
    [SerializeField] protected Cell _currentCell;

    public EntityType EntityType => _enemyData.Type;
    public Cell CurrentCell => _currentCell;

    private Field _field;

    protected int _hp;
    protected int _shield;

    public float Speed => _enemyData.Speed;

    public bool IsActive { get; protected set; }
    private bool _isInvincibility;

    #region Behaviours

    protected ITakeDamageBh _takeDamageBh;
    protected IMoveBh _moveBh;

    #endregion

    public event Action<Cell> OnMovedToCell;

    protected CommandHandler _commandHandler;
    protected CommandFactory _commandFactory;

    protected IInstantiator _instantiator; // for instantiate non-MonoBehaviour objs by Zenject

    private TurnManager _turnManager;

    [Inject]
    private void Construct(IInstantiator instantiator, CommandFactory commandFactory, TurnManager turnManager)
    {
        _instantiator = instantiator;

        _commandFactory = commandFactory;

        _turnManager = turnManager;
    }

    public virtual void Init()
    {
        _hp = _enemyData.Hp;
        _shield = _enemyData.Shield;

        _commandHandler = _instantiator.Instantiate<CommandHandler>(new object[] { this });

        InitBehaviours();

        var subscribeHandler = _instantiator.Instantiate<SubscribeHandler>();
        subscribeHandler.SetSubscribeActions(Subscribe, Unsubscribe);
    }

    public void SetStartCell(Cell cell)
    {
        UpdateCells(cell);
        //if (!_currentCell)
        //{
        //    _currentCell = cell;
        //    UpdateCells(cell);
        //}
        //else throw new System.InvalidOperationException("Start cell already exist! Cannot set start cell!");
    }

    protected virtual void InitBehaviours()
    {
        _takeDamageBh = new TakeDamageBh(this);
    }

    private void UpdateCells(Cell cell)
    {
        if (_currentCell) _currentCell.OnCommandSet -= SetCommand;
        _currentCell = cell;
        _currentCell.OnCommandSet += SetCommand;

        OnMovedToCell?.Invoke(cell);

        if (_moveBh is IMoveToCellBh moveToCellBh) moveToCellBh.OnCellRiched -= UpdateCells;
    }

    private void Update()
    {
        if (_moveBh != null) _moveBh.UpdateBh();
    }

    public void StartMove(IMoveBh moveBh)
    {
        _moveBh = moveBh;
        _moveBh.Init(transform, Speed);
        _moveBh.StartMove();

        if (_moveBh is IMoveToCellBh moveToCellBh) moveToCellBh.OnCellRiched += UpdateCells;
    }

    public virtual void TakeDamage(int damage)
    {
        if (!_isInvincibility)
        {
            _takeDamageBh.TakeDamage(damage, ref _hp);
        }
    }

    private void SetCommand(Command command)
    {
        _commandHandler.HandleCommand(command);
    }

    public void ActivateInvincibility()
    {
        _isInvincibility = true;
        Debug.Log("Activate invincibility on " + gameObject.name);
        //_turnManager.OnTurnFinished += DeactivateInvincibility;
    }

    public void DeactivateInvincibility()
    {
        _isInvincibility = false;
    }

    protected virtual void Subscribe()
    {
        if (_currentCell) _currentCell.OnCommandSet += SetCommand;
    }

    protected virtual void Unsubscribe()
    {
        if (_moveBh != null && _moveBh is IMoveToCellBh moveToCellBh) moveToCellBh.OnCellRiched -= UpdateCells;
        if (_currentCell) _currentCell.OnCommandSet -= SetCommand;
        //if (_isInvincibility) _turnManager.OnTurnFinished -= DeactivateInvincibility;
    }

    public void OnMouseDown() // Тестовый метод для проверки игрового цикла. TODO: потом удалить
    {
        if (_turnManager.IsPlayerTurn) Death();
    }

    public virtual void Death()
    {
        gameObject.SetActive(false); // Change to ObjectPooling
        _currentCell.IsBusy = false;
        _currentCell = null;
    }

    public abstract void CreatePriorityCommand();
}