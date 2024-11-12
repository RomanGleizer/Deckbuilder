using System.Collections.Generic;
using Game.Table.Scripts.Entities;
using Table.Scripts.Entities;
using UnityEngine;
using Zenject;

public class GiveInvincibilitySupportBh : ISupportBh
{
    private CommandFactory _commandFactory;

    private EnemyCard _enemyCard;

    private CellTrackerByEnemy _cellTrackerByEnemy;
    private Field _field;

    public GiveInvincibilitySupportBh(EnemyCard enemyCard)
    {
        _enemyCard = enemyCard;
    }

    [Inject]
    private void Construct(IInstantiator instantiator, CommandFactory commandFactory, Field field)
    {
        _commandFactory = commandFactory;
        _field = field;

        _cellTrackerByEnemy = instantiator.Instantiate<CellTrackerByEnemy>(new object[] {_enemyCard});
    }

    public void Support()
    {
        var cells = FindCells();

        foreach (var cell in cells) 
        {
            var invincibilable = cell.GetObjectOnCell<IInvincibilable>();
            if (invincibilable != null) invincibilable.ActivateInvincibility();
        }
    }

    private List<Cell> FindCells()
    {
        List<Cell> cells = new List<Cell>();
        var currentCell = _cellTrackerByEnemy.GetCurrentCell();

        for (int i = -1; i < 2; ++i)
        {
            for (int j = -1; j < 2; ++j)
            {
                if (i * j == 0 && (i != 0 || j != 0))
                {
                    var cell = _field.FindCell(currentCell, new Vector2(i, j), 1);
                    if (cell != null) cells.Add(cell);
                }
            }
        }

        return cells;
    }
}