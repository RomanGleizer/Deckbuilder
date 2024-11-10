using System;
using UnityEngine;
using Table.Scripts.Entities;

public interface IMoveBh
{
    public void Init(Transform movableTransform, float speed);
    public void UpdateBh();
    public void StartMove();
}

public interface IMoveToCellBh : IMoveBh
{
    public void SetParameters(Cell fromCell, Cell toCell);
    public event Action<Cell> OnCellRiched;
}

public interface IMoveToDestinationBh : IMoveBh
{
    public void SetParameters(Vector2 destination);
    public event Action OnPosRiched;
}