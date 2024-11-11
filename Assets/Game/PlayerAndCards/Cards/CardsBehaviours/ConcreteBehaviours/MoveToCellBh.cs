using System;
using Table.Scripts.Entities;
using UnityEditor.Build;
using UnityEngine;

public abstract class BaseMoveBh : IMoveBh
{
    protected float _speed;
    protected Transform _movableTransform;

    protected bool _isMoving = false;

    public void Init(Transform movableTransform, float speed)
    {
        _speed = speed;
        _movableTransform = movableTransform;
    }

    public virtual void StartMove()
    {
        _isMoving = true;
    }

    public void UpdateBh()
    {
        if (_isMoving) Move();
    }

    protected abstract void Move();

    protected virtual void RichTarget()
    {
        _isMoving = false;
    }
}

public class MoveToCellBh : BaseMoveBh, IMoveToCellBh
{
    public event Action<Cell> OnCellRiched;

    private Cell _previousCell;
    private Cell _targetCell;

    public void SetParameters(Cell fromCell, Cell toCell)
    {
        _previousCell = fromCell;
        _targetCell = toCell;
    }

    public override void StartMove()
    {
        if (_previousCell != null) _previousCell.ReleaseCell();
        if (_targetCell != null && !IsReached(_targetCell))
        {
            _targetCell.SetCardOnCell(_movableTransform.GetComponent<EnemyCard>());
            _isMoving = true;
        }
    }

    protected override void Move()
    {
        if (_targetCell != null && !IsReached(_targetCell))
        {
            _movableTransform.position = Vector3.MoveTowards(_movableTransform.position, _targetCell.transform.position, _speed * Time.deltaTime);
        }
        else if (IsReached(_targetCell)) RichTarget();
    }

    protected override void RichTarget()
    {
        base.RichTarget();
        OnCellRiched?.Invoke(_targetCell);
        _targetCell = null;
    }

    private bool IsReached(Cell cell)
    {
        return _movableTransform.position == cell.transform.position;
    }
}

public class MoveToDestinationBh : BaseMoveBh, IMoveToDestinationBh
{
    private Vector3 _destination;

    public event Action OnPosRiched;

    public void SetParameters(Vector2 destination)
    {
        _destination = new Vector3(destination.x, destination.y, _movableTransform.position.z);
    }

    protected override void Move()
    {
        _movableTransform.position = Vector3.MoveTowards(_movableTransform.position, _destination, _speed * Time.deltaTime);
        if (IsReached()) RichTarget();
    }

    private bool IsReached()
    {
        return _movableTransform.position == _destination;
    }
}