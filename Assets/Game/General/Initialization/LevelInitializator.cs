using System.Collections;
using Game.Table.Scripts.Entities;
using Table.Scripts.Entities;
using UnityEngine;
using Zenject;

public class LevelInitializator
{
    private Field _field;

    private LevelPlacementStackController _stackController;

    private float _spawnPointX;

    [Inject]
    private void Construct(Field field, LevelPlacementStackController placementStackController)
    {
        _field = field;
        _stackController = placementStackController;
    }

    public void InitializeLevel()
    {
        for (int i = 0; i < _field.RowsCount; ++i)
        {
            for (int j = 0; j < _field.ColumnsCount; ++j)
            {
                _stackController.SpawnEntityFromPlacementStack(i);
            }
        }
    }
}
