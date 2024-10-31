using Table.Scripts.Entities;
using UnityEngine;

public interface IMover
{
    public void StartMove(IMoveBh moveBh);
}

public interface IMoverToCell : IMover
{
    public Cell CurrentCell { get; }
}
