using UnityEngine;
using Table.Scripts.Entities;
using System;
using Game.Table.Scripts.Entities;
using Zenject;
using System.Threading.Tasks;

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

    protected CommandFactory _commandFactory;

    protected IInstantiator _instantiator; // for instantiate non-MonoBehaviour objs by Zenject

    private TurnManager _turnManager;

    // Тестовые зависимости. TODO: удалить.
    private SpriteRenderer _spriteRenderer;
    private Color _activeColor;
    private Color _defaultColor;

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

        InitBehaviours();

        var subscribeHandler = _instantiator.Instantiate<SubscribeHandler>();
        subscribeHandler.SetSubscribeActions(Subscribe, Unsubscribe);

        _spriteRenderer = GetComponentInChildren<SpriteRenderer>(); // TODO: удалить
        _activeColor = Color.green; // TODO: удалить
    }

    public void SetStartCell(Cell cell)
    {
        if (_currentCell == null)
        {
            UpdateCells(cell);
        }
        else throw new System.InvalidOperationException("Start cell already exist! Cannot set start cell!");
    }

    protected virtual void InitBehaviours()
    {
        _takeDamageBh = new TakeDamageBh(this);
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
        
    }

    protected virtual void Unsubscribe()
    {
        if (_moveBh != null && _moveBh is IMoveToCellBh moveToCellBh) moveToCellBh.OnCellRiched -= UpdateCells;
        //if (_isInvincibility) _turnManager.OnTurnFinished -= DeactivateInvincibility;
    }

    public void OnMouseDown() // Тестовый метод для проверки игрового цикла. TODO: потом удалить
    {
        if (_turnManager.IsPlayerTurn) Death();
    }

    protected void HiglightActivingEnemy() // Тестовый метод для визуального различия. TODO: потом удалить
    {
        _defaultColor = _spriteRenderer.color;
        _spriteRenderer.color = _activeColor;
    }

    protected void UnhiglightActivingEnemy() // Тестовый метод для визуального различия. TODO: потом удалить
    {
        _spriteRenderer.color = _defaultColor;
    }

    public virtual void Death()
    {
        gameObject.SetActive(false); // Change to ObjectPooling
        _currentCell.ReleaseCellFrom(this);
        _currentCell = null;
    }

    public abstract void CreatePriorityCommand();
}