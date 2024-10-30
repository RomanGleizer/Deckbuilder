using UnityEngine;
using Table.Scripts.Entities;
using System;
using Zenject;

public abstract class EnemyCard : MonoBehaviour, ITakerDamage, IMover, IInvincibilable
{
    [SerializeField] protected EnemyData _enemyData;
    [SerializeField] protected Cell _currentCell;

    private Field _field;

    protected int _hp;
    protected int _shield;

    public bool IsActive { get; protected set; }
    private bool _isInvincibility;

    #region Behaviours

    protected ITakeDamageBh _takeDamageBh;
    protected IMoveBh _moveBh;

    #endregion

    public Cell CurrentCell => _currentCell;
    public event Action<Cell> OnMovedToCell;

    private CommandHandler _commandHandler;

    protected IInstantiator _instantiator; // for instantiate non-MonoBehaviour objs by Zenject

    [Inject]
    private void Construct(IInstantiator instantiator)
    {
        _instantiator = instantiator;
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

    protected virtual void InitBehaviours()
    {
        _takeDamageBh = new TakeDamageBh(this);
        _moveBh = new MoveToCellBh(transform, _enemyData.Speed);
    }

    private void UpdateCells(Cell cell)
    {
        _currentCell = cell;
        cell.IsBusy = true;
        OnMovedToCell?.Invoke(cell);
    }

    private void Update()
    {
        if (_moveBh != null) _moveBh.Update();
    }

    public virtual void MoveToCell(Cell cell)
    {
        _moveBh.StartMoveFromTo(_currentCell, cell);
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
        _moveBh.OnCellRiched += UpdateCells;
        _currentCell.OnCommandSet += SetCommand;
    }

    protected virtual void Unsubscribe()
    {
        _moveBh.OnCellRiched -= UpdateCells;
        _currentCell.OnCommandSet -= SetCommand;
        //if (_isInvincibility) _turnManager.OnTurnFinished -= DeactivateInvincibility;
    }

    public virtual void Death()
    {
        gameObject.SetActive(false); // Change to ObjectPooling
    }
}