using System;
using UnityEngine;
using Table.Scripts.Entities;

public interface IMoveBh
{
    public void StartMoveFromTo(Cell fromCell, Cell toCell);
    public void Update();

    public event Action<Cell> OnCellRiched;
}
