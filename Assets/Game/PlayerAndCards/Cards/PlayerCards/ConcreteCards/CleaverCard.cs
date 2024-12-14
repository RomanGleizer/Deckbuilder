using System.Collections.Generic;
using System.Linq;
using Game.Table.Scripts.Entities;
using UnityEngine;

namespace Game.PlayerAndCards.Cards.PlayerCards.ConcreteCards
{
    public class CleaverCard : PlayerCard
    {
        [SerializeField] private int _damage = 1;
        [SerializeField] private int _energyCost = 2;

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
                if (enemy.Health > 0)
                {
                    //enemy.Stun(2);
                }
            }

            SpendEnergy(_energyCost);
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