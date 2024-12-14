using System.Collections.Generic;
using System.Linq;
using Game.Table.Scripts.Entities;
using UnityEngine;

namespace Game.PlayerAndCards.Cards.PlayerCards.ConcreteCards
{
    public class ScourgeCard : PlayerCard
    {
        [SerializeField] private int _energyCost = 1;

        public override void Use()
        {
            if (!IsCanUse) return;

            var validCells = GetValidCells();
            if (validCells.Length == 0) return;

            var targetCell = validCells[0];
            var enemy = targetCell.GetObjectOnCell<EnemyCard>();
            if (enemy == null)
                return;

            var targetRow = targetCell.RowId;
            var targetColumn = targetCell.ColumnId;
            
            var firstColumnCell = Field.GetCellAt(targetRow, 0);

            if (firstColumnCell != null)
            {
                var firstColumnEnemy = firstColumnCell.GetObjectOnCell<EnemyCard>();

                if (firstColumnEnemy != null)
                {
                    targetCell.ReleaseCellFrom(enemy);
                    targetCell.SetCardOnCell(firstColumnEnemy);
                    
                    firstColumnCell.ReleaseCellFrom(firstColumnEnemy);
                    firstColumnCell.SetCardOnCell(enemy);
                }
                else
                {
                    targetCell.ReleaseCellFrom(enemy);
                    firstColumnCell.SetCardOnCell(enemy);
                }
            }

            SpendEnergy(_energyCost);
        }

        protected override Cell[] GetValidCells()
        {
            return Field.GetTraversedCells()
                .Where(cell => cell.GetObjectOnCell<EnemyCard>() != null && cell.ColumnId > 0)
                .ToArray();
        }
    }
}
