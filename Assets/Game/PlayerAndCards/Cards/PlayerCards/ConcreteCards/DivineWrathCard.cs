using System.Collections.Generic;
using System.Linq;
using Game.Table.Scripts.Entities;
using UnityEngine;

namespace Game.PlayerAndCards.Cards.PlayerCards.ConcreteCards
{
    public class DivineWrathCard : PlayerCard
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
            if (CurrentCell.IsHidden || CurrentCell.GetObjectOnCell<ITakerDamage>() == null)
                return new Cell[] {};

            var enemy = CurrentCell.GetObjectOnCell<ITakerDamage>();
            var targetEnemyType = enemy.GetType();
            
            return Field.GetTraversedCells()
                .Where(cell=> cell.GetObjectOnCell<ITakerDamage>() != null 
                              && cell.GetObjectOnCell<ITakerDamage>().GetType() == targetEnemyType)
                .ToArray();
        }
    }
}