using System.Collections.Generic;
using System.Linq;
using Game.Table.Scripts.Entities;
using UnityEngine;

namespace Game.PlayerAndCards.Cards.PlayerCards.ConcreteCards
{
    public class ArtilleryCard : PlayerCard
    {
        [SerializeField] private int _damage = 1;
        [SerializeField] private int _energyCost = 3;

        public override void Use()
        {
            if (!CanSpendEnergy(_energyCost)) 
                return;

            var validCells = GetValidCells();
            if (validCells.Length == 0) 
                return;
            
            foreach (var enemy in validCells.Select(cell => 
                         cell.GetObjectOnCell<EnemyCard>()))
            {
                enemy.TakeDamage(_damage);
            }

            SpendEnergy(_energyCost);
        }

        protected override Cell[] GetValidCells()
        {
            return CurrentCell == null
                ? new Cell[] {}
                : Field.GetRowByCell(CurrentCell, includeHidden: false)
                    .Where(cell => cell.GetObjectOnCell<EnemyCard>() != null)
                    .ToArray();
        }
    }
}