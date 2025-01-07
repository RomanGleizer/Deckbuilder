using System.Collections.Generic;
using System.Linq;
using Game.Table.Scripts.Entities;
using UnityEngine;

namespace Game.PlayerAndCards.Cards.PlayerCards.ConcreteCards
{
    public class SaberCard : PlayerCard
    {
        public override void Use()
        {
            if (!CanSpendEnergy(CardData.EnergyCost))
                return;
            
            var targetCells = GetValidCells();
            if (targetCells.Length == 0) 
                return;

            var targetCell = targetCells[0];
            var enemy = targetCell.GetObjectOnCell<EnemyCard>();

            enemy.TakeDamage(CardData.Damage);
            SpendEnergy(CardData.EnergyCost);
        }

        protected override Cell[] GetValidCells()
        {
            return CurrentCell.IsHidden
                   || CurrentCell?.ColumnId != 0 
                   || CurrentCell.GetObjectOnCell<EnemyCard>() == null
                ? new Cell[] {} 
                : new[] { CurrentCell };
        }
    }
}