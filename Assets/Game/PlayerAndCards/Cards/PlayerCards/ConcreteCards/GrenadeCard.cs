using System.Collections.Generic;
using System.Linq;
using Game.Table.Scripts.Entities;
using UnityEngine;

namespace Game.PlayerAndCards.Cards.PlayerCards.ConcreteCards
{
    public class GrenadeCard : PlayerCard
    {
        public override void Use()
        {
            if (!CanSpendEnergy(CardData.EnergyCost))
                return;
            
            var targetCells = GetValidCells();
            if (targetCells.Length == 0) 
                return;
            
            foreach (var enemy in targetCells.Select(cell => 
                         cell.GetObjectOnCell<EnemyCard>()))
            {
                enemy.TakeDamage(CardData.Damage);
            }
            
            SpendEnergy(CardData.EnergyCost);
            HandManager.DeleteCardFromHand(this);
        }

        protected override Cell[] GetValidCells()
        {
            if (CurrentCell.IsHidden
                || CurrentCell?.GetObjectOnCell<EnemyCard>() == null)
                return new Cell[] {};

            return new List<Vector2>
                {
                    Vector2.zero,
                    Vector2.up,
                    Vector2.down,
                    Vector2.left,
                    Vector2.right
                }
                .Select(direction => Field.FindCell(CurrentCell, direction, 1))
                .Where(targetCell => targetCell != null 
                                     && !targetCell.IsHidden 
                                     && targetCell.GetObjectOnCell<EnemyCard>() != null)
                .ToArray();
        }
    }
}