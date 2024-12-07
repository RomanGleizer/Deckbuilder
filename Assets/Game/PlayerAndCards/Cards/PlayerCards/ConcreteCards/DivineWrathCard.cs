using System.Collections.Generic;
using System.Linq;
using Game.Table.Scripts.Entities;
using UnityEngine;

namespace Game.PlayerAndCards.Cards.PlayerCards.ConcreteCards
{
    public class DivineWrathCard : PlayerCard
    {
        [SerializeField] private int _damage = 1;
        [SerializeField] private int _energyCost = 3;

        public override void Use()
        {
            if (!IsCanUse) return;

            var validCells = GetValidCells();
            if (validCells.Count == 0) return;

            var targetEnemy = validCells[0].GetObjectOnCell<EnemyCard>();
            if (targetEnemy == null) return;
            
            var targetEnemyType = targetEnemy.GetType();
            
            var enemies = Field.GetTraversedCells()
                .Select(cell => cell.GetObjectOnCell<EnemyCard>())
                .Where(enemy => enemy != null && enemy.GetType() == targetEnemyType)
                .ToList();
            
            foreach (var enemy in enemies)
            {
                enemy.TakeDamage(_damage);
            }
            
            SpendEnergy(_energyCost);
        }

        protected override List<Cell> GetValidCells()
        {
            return Field.GetTraversedCells()
                .Where(cell => cell.GetObjectOnCell<EnemyCard>() != null)
                .ToList();
        }
    }
}