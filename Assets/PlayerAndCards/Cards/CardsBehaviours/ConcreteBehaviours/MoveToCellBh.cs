using System;
using Table.Scripts.Entities;
using UnityEditor.Build;
using UnityEngine;

public class MoveToCellBh : IMoveBh
{
    private float _speed;
    private Transform _movableTransform;
    public event Action<Cell> OnCellRiched;

    private Cell _targetCell;

    private bool _isMoving = false;

    public MoveToCellBh(Transform movableTransform, float speed)
    {
        _speed = speed;
        _movableTransform = movableTransform;
    }

    public void StartMoveFromTo(Cell fromCell, Cell toCell)
    {
        if (fromCell != null) fromCell.IsBusy = false;
        if (toCell != null && !IsReached(toCell))
        {
            _isMoving = true;
            _targetCell = toCell;
        }
    }

    public void Update()
    {
        if (_isMoving) Move();
    }

    private void Move()
    {
        if (_targetCell != null && !IsReached(_targetCell))
        {
            _movableTransform.position = Vector3.MoveTowards(_movableTransform.position, _targetCell.transform.position, _speed * Time.deltaTime);
        }
        else if (IsReached(_targetCell)) RichTargetCell(_targetCell);
    }

    protected virtual void RichTargetCell(Cell cell)
    {
        _isMoving = false;
        _targetCell = null;
        OnCellRiched?.Invoke(cell);
    }

    private bool IsReached(Cell cell)
    {
        return _movableTransform.position == cell.transform.position;
    }
}
