using UnityEngine;
using Table.Scripts.Entities;

public abstract class EnemyCard : MonoBehaviour, ITakerDamage, IMover
{
    [SerializeField] protected EnemyData _enemyData;
    [SerializeField] protected Cell _currentCell;

    protected int _hp;
    protected int _shield;

    #region Behaviours

    protected ITakeDamageBh _takeDamageBh;
    protected IMoveBh _moveBh;

    #endregion

    public bool IsActive { get; protected set; }
    private Field _field;

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
        _moveBh = new MoveToCellBh(transform, 10);

        _moveBh.OnCellRiched += UpdateCells;
        //_takeDamageBh;
    }

    private void UpdateCells(Cell cell)
    {
        _currentCell = cell;
    }

    public virtual void MoveToCell(Cell cell)
    {
        _moveBh.MoveToCell(cell);
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
