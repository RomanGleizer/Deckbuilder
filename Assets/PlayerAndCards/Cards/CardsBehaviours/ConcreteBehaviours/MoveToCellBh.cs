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

    public void MoveToCell(Cell cell)
    {
        if (cell != null && !IsReached(cell))
        {
            Vector3.MoveTowards(_movableTransform.position, cell.transform.position, _speed * Time.deltaTime);
        }
        else if (IsReached(cell)) RichTargetCell(cell);
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
