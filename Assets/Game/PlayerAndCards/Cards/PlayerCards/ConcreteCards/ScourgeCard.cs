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
            if (validCells.Count == 0) return;

            var targetCell = validCells[0];
            var enemy = targetCell.GetObjectOnCell<EnemyCard>();
            if (enemy == null) 
                return;

            Field.MoveEnemyToColumn(enemy, 0);
            SpendEnergy(_energyCost);
        }

        protected override List<Cell> GetValidCells()
        {
            return Field.GetTraversedCells()
                .Where(cell => cell.GetObjectOnCell<EnemyCard>() != null)
                .ToList();
        }
    }
}