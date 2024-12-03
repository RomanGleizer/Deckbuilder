using System.Collections.Generic;
using System.Linq;
using Game.Table.Scripts.Entities;
using UnityEngine;

namespace Game.PlayerAndCards.Cards.PlayerCards.ConcreteCards
{
    public class StormCard : PlayerCard
    {
        [SerializeField] private int _energyCost = 3;

        public override void Use()
        {
            if (!IsCanUse) return;

            var validCells = GetValidCells();
            foreach (var enemy in validCells.Select(
                         c => c.GetObjectOnCell<EnemyCard>()).Where(e => e != null))
            {
                // enemy.RemoveArmor();
                // if (enemy is Shooter shooter)
                // {
                //     shooter.Stun(1);
                // }
            }

            SpendEnergy();
        }
        
        protected override List<Cell> GetValidCells()
        {
            var targetCells = new List<Cell>();
            Field.TraverseCells(cell =>
            {
                if (cell.IsBusy)
                    targetCells.Add(cell);
            }, includeHidden: false);

            return targetCells;
        }
    }
}