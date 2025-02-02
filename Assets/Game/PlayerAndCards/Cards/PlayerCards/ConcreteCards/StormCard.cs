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
            }

            foreach (var enemy in validCells.Select(cell => cell.GetObjectOnCell<EnemyCard>()))
            {
                if (enemy is Shooter shooter and IStunnable stunnable)
                {
                    stunnable.Stun(1);
                }
            }

            SpendEnergy(CardData.EnergyCost);
            HandManager.DeleteCardFromHand(this);
        }

        protected override Cell[] GetValidCells()
        {
            return Field.GetTraversedCells()
                .Where(cell =>
                    cell.GetObjectOnCell<IStunnable>() != null || cell.GetObjectOnCell<IHaveShield>() != null)
                .ToArray();
        }
    }
}