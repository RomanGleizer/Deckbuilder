using System.Collections.Generic;
using System.Linq;
using Game.PlayerAndCards.PlayerScripts.Interfaces;
using Game.Table.Scripts.Entities;
using UnityEngine;

namespace Game.PlayerAndCards.Cards.PlayerCards.ConcreteCards
{
    public class CleaverCard : PlayerCard
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
                if (enemy.Health > 0 && enemy is IStunnable stunnable)
                {
                    stunnable.Stun(1); 
                }
            }

            SpendEnergy(CardData.EnergyCost);
        }

        protected override Cell[] GetValidCells()
        {
            return CurrentCell == null || CurrentCell.ColumnId != 0 
                ? new Cell[] {}
                : Field.GetColumnById(0, includeHidden: false).
                    Where(cell => cell.GetObjectOnCell<EnemyCard>() != null)
                    .ToArray();
        }
    }
}