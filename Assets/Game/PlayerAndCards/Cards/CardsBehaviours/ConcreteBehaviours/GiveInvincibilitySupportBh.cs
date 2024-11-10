using System.Collections.Generic;
using Game.Table.Scripts.Entities;
using Table.Scripts.Entities;
using UnityEngine;
using Zenject;

public class GiveInvincibilitySupportBh : ISupportBh
{
    private CommandFactory _commandFactory;

    private CellTrackerByEnemy _cellTrackerByEnemy;
    private Field _field;

    public GiveInvincibilitySupportBh(EnemyCard enemyCard)
    {
        _cellTrackerByEnemy = new CellTrackerByEnemy(enemyCard);
    }

    [Inject]
    private void Construct(CommandFactory commandFactory, Field field)
    {
        _commandFactory = commandFactory;
        _field = field;
    }

    public void Support()
    {
        var cells = FindCells();

        foreach (var cell in cells) 
        {
            var command = _commandFactory.CreateInvincibilityCommand();
            cell.SetCommand(command);
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