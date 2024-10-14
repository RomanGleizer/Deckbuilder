using System;
using Table.Scripts.Entities;
using UnityEditor.Build;
using UnityEngine;

public class MoveToCellBh : IMoveBh
{
    private float _speed;
    private Transform _movableTransform;
    public event Action<Cell> OnCellRiched;

    public MoveToCellBh(Transform movableTransform, float speed)
    {
        _speed = speed;
        _movableTransform = movableTransform;
    }

    public void MoveFromTo(Cell fromCell, Cell toCell)
    {
        fromCell.IsBusy = false;
        if (toCell != null && !IsReached(toCell))
        {
            Vector3.MoveTowards(_movableTransform.position, toCell.transform.position, _speed * Time.deltaTime);
        }
        else if (IsReached(toCell)) RichTargetCell(toCell);
    }

    protected virtual void RichTargetCell(Cell cell)
    {
        OnCellRiched?.Invoke(cell);
    }

    private bool IsReached(Cell cell)
    {
        return _movableTransform.position == cell.transform.position;
    }
}
