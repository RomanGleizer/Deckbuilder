using System.Collections.Generic;
using System.Linq;
using Game.Table.Scripts.Entities;
using UnityEngine;

namespace Game.PlayerAndCards.Cards.PlayerCards.ConcreteCards
{
    public class ArtilleryCard : PlayerCard
    {
        public override void Use()
        {
            if (!CanSpendEnergy(CardData.EnergyCost)) 
                return;

            var validCells = GetValidCells();
            if (validCells.Length == 0) 
                return;
            
            foreach (var enemy in validCells.Select(cell => 
                         cell.GetObjectOnCell<ITakerDamage>()))
            {
                enemy.TakeDamage(CardData.Damage);
            }

            SpendEnergy(CardData.EnergyCost);
            HandManager.DeleteCardFromHand(this);
        }

        protected override Cell[] GetValidCells()
        {
            return CurrentCell == null
                ? new Cell[] {}
                : Field.GetRowByCell(CurrentCell, includeHidden: false)
                    .Where(cell => cell.GetObjectOnCell<ITakerDamage>() != null)
                    .ToArray();
        }
    }
}