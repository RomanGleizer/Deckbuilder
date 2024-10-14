using System;
using UnityEngine;
using Table.Scripts.Entities;

public interface IMoveBh
{
    public void MoveFromTo(Cell fromCell, Cell toCell);

    public event Action<Cell> OnCellRiched;
}
