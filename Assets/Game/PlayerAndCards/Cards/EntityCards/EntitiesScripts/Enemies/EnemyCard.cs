using UnityEngine;
using System;
using Game.Table.Scripts.Entities;
using Zenject;
using System.Threading.Tasks;

public abstract class EnemyCard : EntityCard, ITakerDamage, IMoverToCell, IInvincibilable, IHavePriorityCommand, 
    IHaveShield
{
    private EnemyData _enemyData;

    private Field _field;

    protected int _hp;
    protected int _shield;

    public bool IsActive { get; protected set; }

    public int Health => _hp;
    
    private bool _isInvincibility;

    protected ITakeDamageBh _takeDamageBh;

    protected CommandFactory _commandFactory;
    
    public bool HasShield => _shield > 0;
    
    // Тестовые зависимости. TODO: удалить.
    private SpriteRenderer _spriteRenderer;
    private Color _activeColor;
    private Color _defaultColor;

    [Inject]
    private void Construct(CommandFactory commandFactory)
    {
        _commandFactory = commandFactory;
    }

    public override void Init()
    {
        _enemyData = TypeChanger.ChangeObjectTypeWithException<EntityData, EnemyData>(_entityData);

        _hp = _enemyData.Hp;
        _shield = _enemyData.Shield;

        InitBehaviours();

        base.Init();

        _spriteRenderer = GetComponentInChildren<SpriteRenderer>(); // TODO: удалить
        _activeColor = Color.green; // TODO: удалить
    }

    protected virtual void InitBehaviours()
    {
        _takeDamageBh = new TakeDamageBh(this);
    }

    public virtual void TakeDamage(int damage)
    {
        if (!_isInvincibility)
        {
            var remainDamage = damage - _shield;
            _shield = Mathf.Clamp(_shield - damage, 0, _shield);
            if (remainDamage > 0) _takeDamageBh.TakeDamage(remainDamage, ref _hp);
        }
    }
    
    public void AddShield(int value)
    {
        _shield += value;
    }

    public void BreakShield()
    {
        _shield = 0;
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

    protected override void Subscribe()
    {
        base.Subscribe();
    }

    protected override void Unsubscribe()
    {
        base.Unsubscribe();
        //if (_isInvincibility) _turnManager.OnTurnFinished -= DeactivateInvincibility;
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

    public abstract Command CreatePriorityCommand();
}