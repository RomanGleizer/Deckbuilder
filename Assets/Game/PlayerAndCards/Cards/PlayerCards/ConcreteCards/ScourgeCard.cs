using System.Collections.Generic;
using System.Linq;
using Game.Table.Scripts.Entities;
using UnityEngine;

namespace Game.PlayerAndCards.Cards.PlayerCards.ConcreteCards
{
    public class ScourgeCard : PlayerCard
    {
        public override void Use()
        {
            // if (!CanSpendEnergy(CardData.EnergyCost)) 
            //     return;
            //
            // var validCells = GetValidCells();
            // if (validCells.Length == 0) return;
            //
            // var targetCell = validCells[0];
            // var enemy = targetCell.GetObjectOnCell<EnemyCard>();
            // if (enemy == null)
            //     return;
            //
            // var targetRow = targetCell.RowId;
            // var targetColumn = targetCell.ColumnId;
            //
            // var firstColumnCell = Field.GetCellAt(targetRow, 0);
            //
            // if (firstColumnCell != null)
            // {
            //     var firstColumnEnemy = firstColumnCell.GetObjectOnCell<EnemyCard>();
            //
            //     if (firstColumnEnemy != null)
            //     {
            //         targetCell.ReleaseCellFrom(enemy);
            //         targetCell.SetCardOnCell(firstColumnEnemy);
            //         
            //         firstColumnCell.ReleaseCellFrom(firstColumnEnemy);
            //         firstColumnCell.SetCardOnCell(enemy);
            //         
            //         enemy.MoveToCellWithDoTween(firstColumnCell, 0.5f);
            //         firstColumnEnemy.MoveToCellWithDoTween(targetCell, 0.5f);
            //     }
            //     else
            //     {
            //         targetCell.ReleaseCellFrom(enemy);
            //         firstColumnCell.SetCardOnCell(enemy);
            //         
            //         enemy.MoveToCellWithDoTween(firstColumnCell, 0.5f);
            //     }
            // }
            //
            // SpendEnergy(CardData.EnergyCost);
        }
        
        // public void MoveToCellWithDoTween(Cell targetCell, float duration)
        // {
        //     if (targetCell == null) return;
        //
        //     transform.DOMove(targetCell.transform.position, duration)
        //         .OnComplete(() => UpdateCells(targetCell));
        // }
        
        protected override Cell[] GetValidCells()
        {
            return CurrentCell != null && (CurrentCell.IsHidden
                                           || CurrentCell?.ColumnId == 0 
                                           || CurrentCell?.GetObjectOnCell<EnemyCard>() == null)
                ? new Cell[] {} 
                : new[] { CurrentCell };
        }
    }
}