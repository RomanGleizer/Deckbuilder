using System.Collections.Generic;
using System.Linq;
using Game.Table.Scripts.Entities;
using UnityEngine;

namespace Game.PlayerAndCards.Cards.PlayerCards.ConcreteCards
{
    public class SaberCard : PlayerCard
    {
        [SerializeField] private int _damage = 1;
        [SerializeField] private int _attackRange = 1;
        
        public override void Use()
        {
            if (!IsCanUse) return;

            var validCells = GetValidCells();
            if (validCells.Count == 0) return;

            var targetCell = validCells[0];
            var enemy = targetCell.GetObjectOnCell<EnemyCard>();

            if (enemy == null) return;
            enemy.TakeDamage(_damage);
            SpendEnergy();
        }

        protected override List<Cell> GetValidCells()
        {
            var playerCell = Player.CurrentCell;
            if (playerCell == null) return new List<Cell>();
            
            var column = Field.GetColumnByCell(playerCell, includeHidden: false);
            var targetCell = column.FirstOrDefault(
                cell => cell.RowId == playerCell.RowId + _attackRange && cell.IsBusy);
            
            return targetCell != null ? new List<Cell> { targetCell } : new List<Cell>();
        }
    }
}