using System.Collections.Generic;
using System.Linq;
using Game.Table.Scripts.Entities;
using UnityEngine;

namespace Game.PlayerAndCards.Cards.PlayerCards.ConcreteCards
{
    public class DirkCard : PlayerCard
    {
        [SerializeField] private int _damage = 100;
        [SerializeField] private int _attackRange = 1;
        [SerializeField] private int _energyCost = 4;
        
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
            
            var column = Field.GetColumnById(playerCell.ColumnId + _attackRange, false);
            if (column == null) return new List<Cell>();

            var targetCell = column.FirstOrDefault(cell => cell.IsBusy);
        
            return targetCell != null ? new List<Cell> { targetCell } : new List<Cell>();
        }
    }
}