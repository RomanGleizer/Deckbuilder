using System;
using UnityEngine;
using Table.Scripts.Entities;

public interface IMoveBh
{
    public void MoveToCell(Cell cell);

    public event Action<Cell> OnCellRiched;
}
