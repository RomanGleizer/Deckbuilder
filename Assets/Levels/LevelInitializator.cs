using System.Collections;
using Table.Scripts.Entities;
using UnityEngine;
using Zenject;

public class LevelInitializator
{
    private Field _field;

    private EntitySpawnSystem _spawnSystem;

    private float _spawnPointX;

    [Inject]
    private void Construct(Field field, EntitySpawnSystem spawnSystem)
    {
        _field = field;
        _spawnSystem = spawnSystem;
    }

    public void InitializeLevel(LevelPlacement levelPlacement, float spawnPointX)
    {
        _spawnPointX = spawnPointX;

        for (int i = 0; i < levelPlacement.MarkerTable.Length; ++i)
        {
            var row = _field.GetRowByIndex(i, true);
            for (int j = 0; j < row.Length; ++j)
            {
                InitializeEntityOnCell(row[j], levelPlacement.MarkerTable[i].RowMarkers[j]);
            }
        }
    }

    public void InitializeEntityOnCell(Cell cell, EntityType entityType)
    {
        var enemy = _spawnSystem.SpawnEntity(entityType);

        if (enemy != null)
        {
            enemy.transform.position = new Vector2(_spawnPointX + cell.transform.position.x, cell.transform.position.y);
            enemy.MoveToCell(cell);
        }
    }
}
