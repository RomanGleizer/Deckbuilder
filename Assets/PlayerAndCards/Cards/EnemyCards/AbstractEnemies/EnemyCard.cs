using UnityEngine;
using Table.Scripts.Entities;
using System;

public abstract class EnemyCard : MonoBehaviour, ITakerDamage, IMover
{
    [SerializeField] protected EnemyData _enemyData;
    [SerializeField] protected Cell _currentCell;

    private Field _field;

    protected int _hp;
    protected int _shield;

    public bool IsActive { get; protected set; }

    #region Behaviours

    protected ITakeDamageBh _takeDamageBh;
    protected IMoveBh _moveBh;

    #endregion


    public event Action<Cell> OnMovedToCell; 

    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        _hp = _enemyData.Hp;
        _shield = _enemyData.Shield;

        new CommandHandler(this);

        InitBehaviours();
    }

    protected virtual void InitBehaviours()
    {
        _takeDamageBh = new TakeDamageBh(this);
        _moveBh = new MoveToCellBh(transform, 10);

        _moveBh.OnCellRiched += UpdateCells;
    }

    private void UpdateCells(Cell cell)
    {
        _currentCell = cell;
        cell.IsBusy = true;
        OnMovedToCell?.Invoke(cell);
    }

    public virtual void MoveToCell(Cell cell)
    {
        _moveBh.MoveFromTo(_currentCell, cell);
    }

    public virtual void TakeDamage(int damage)
    {
        _takeDamageBh.TakeDamage(damage, ref _hp);
    }

    public virtual void Death()
    {
        gameObject.SetActive(false); // Change to ObjectPooling
    }

    private void OnDestroy()
    {
        _moveBh.OnCellRiched -= UpdateCells;
    }
}
