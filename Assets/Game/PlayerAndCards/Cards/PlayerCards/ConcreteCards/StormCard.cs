using System.Collections.Generic;
using System.Linq;
using Game.PlayerAndCards.PlayerScripts.Interfaces;
using Game.Table.Scripts.Entities;
using UnityEngine;

namespace Game.PlayerAndCards.Cards.PlayerCards.ConcreteCards
{
    public class StormCard : PlayerCard
    {
        public override void Use()
        {
            if (!CanSpendEnergy(CardData.EnergyCost)) 
                return;
            
            var validCells = GetValidCells();
            if (validCells.Length == 0) 
                return;
            
            foreach (var enemy in validCells.Select(cell => cell.GetObjectOnCell<EnemyCard>()))
            {
                enemy.BreakShield();
                if (enemy is Shooter shooter and IStunnable stunnable)
                {
                    stunnable.Stun(1); 
                }
            }
            
            SpendEnergy(CardData.EnergyCost);
        }

        protected override Cell[] GetValidCells()
        {
            return Field.GetTraversedCells()
                .Where(cell => cell.GetObjectOnCell<EnemyCard>() != null)
                .ToArray();
        }
    }
}