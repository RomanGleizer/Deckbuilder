using System.Collections.Generic;
using System.Linq;
using Game.Table.Scripts.Entities;
using UnityEngine;

namespace Game.PlayerAndCards.Cards.PlayerCards.ConcreteCards
{
    public class StormCard : PlayerCard
    {
        [SerializeField] private int _energyCost = 1;

        public override void Use()
        {
            if (!CanSpendEnergy(_energyCost)) 
                return;
            
            var validCells = GetValidCells();
            if (validCells.Length == 0) 
                return;
            
            foreach (var enemy in validCells.Select(cell => cell.GetObjectOnCell<EnemyCard>()))
            {
                //enemy.RemoveArmor();
                if (enemy is Shooter shooter)
                {
                    //shooter.Stun(1); 
                }
            }
        }

        protected override Cell[] GetValidCells()
        {
            return Field.GetTraversedCells()
                .Where(cell => cell.GetObjectOnCell<EnemyCard>() != null)
                .ToArray();
        }
    }
}