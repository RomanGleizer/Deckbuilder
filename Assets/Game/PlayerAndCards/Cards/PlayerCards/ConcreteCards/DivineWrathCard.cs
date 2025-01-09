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
                         cell.GetObjectOnCell<EnemyCard>()))
            {
                enemy.TakeDamage(CardData.Damage);
            }
            
            SpendEnergy(CardData.EnergyCost);
        }

        protected override Cell[] GetValidCells()
        {
            if (CurrentCell.IsHidden || CurrentCell.GetObjectOnCell<EnemyCard>() == null)
                return new Cell[] {};

            var enemy = CurrentCell.GetObjectOnCell<EnemyCard>();
            var targetEnemyType = enemy.GetType();
            
            return Field.GetTraversedCells()
                .Where(cell=> cell.GetObjectOnCell<EnemyCard>() != null 
                              && cell.GetObjectOnCell<EnemyCard>().GetType() == targetEnemyType)
                .ToArray();
        }
    }
}