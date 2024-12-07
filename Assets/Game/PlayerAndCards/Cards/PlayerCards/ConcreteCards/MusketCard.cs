using System.Collections.Generic;
using System.Linq;
using Game.Table.Scripts.Entities;
using UnityEngine;

namespace Game.PlayerAndCards.Cards.PlayerCards.ConcreteCards
{
    public class MusketCard : PlayerCard
    {
        [SerializeField] private int _damage = 1;
        [SerializeField] private int _energyCost = 3;

        public override void Use()
        {
            if (!IsCanUse) return;

            var validCells = GetValidCells();
            foreach (var enemy in validCells.Select(cell => 
                         cell.GetObjectOnCell<EnemyCard>())
                         .Where(enemy => enemy != null))
            {
                enemy.TakeDamage(_damage);
            }

            SpendEnergy(_energyCost);
        }

        protected override List<Cell> GetValidCells()
        {
            return Field.GetColumnById(0, includeHidden: false)
                .Where(cell => cell.GetObjectOnCell<EnemyCard>() != null)
                .ToList();
        }
    }
}