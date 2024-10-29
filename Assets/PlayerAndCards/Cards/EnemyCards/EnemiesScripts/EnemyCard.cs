using UnityEngine;
using Table.Scripts.Entities;
using System;

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


    public event Action<Cell> OnMovedToCell; 

    public virtual void Init()
    {
        _hp = _enemyData.Hp;
        _shield = _enemyData.Shield;

        new CommandHandler(this);
        new SubscribeHandler(Subscribe, Unsubscribe);

        InitBehaviours();
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
        _moveBh.Update();
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

    public void ActivateInvincibility()
    {
        _isInvincibility = true;
        //_turnManager.OnTurnFinished += DeactivateInvincibility;
    }

    public void DeactivateInvincibility()
    {
        _isInvincibility = false;
    }

    protected virtual void Subscribe()
    {
        _moveBh.OnCellRiched += UpdateCells;
    }

    protected virtual void Unsubscribe()
    {
        _moveBh.OnCellRiched -= UpdateCells;
        //if (_isInvincibility) _turnManager.OnTurnFinished -= DeactivateInvincibility;
    }

    public virtual void Death()
    {
        gameObject.SetActive(false); // Change to ObjectPooling
    }
}