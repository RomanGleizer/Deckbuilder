using System.Collections.Generic;
using System.Linq;
using Game.Table.Scripts.Entities;
using UnityEngine;

namespace Game.PlayerAndCards.Cards.PlayerCards.ConcreteCards
{
    public class GrenadeCard : PlayerCard
    {
        [SerializeField] private int _damage = 1;
        [SerializeField] private int _energyCost = 4;

        public override void Use()
        {
            if (!IsCanUse) return;

            var validCells = GetValidCells();
            if (validCells.Count == 0) return;

            foreach (var enemy in validCells.Select(cell => cell
                         .GetObjectOnCell<EnemyCard>())
                         .Where(enemy => enemy != null))
            {
                enemy.TakeDamage(_damage);
            }

            SpendEnergy(_energyCost);
        }

        protected override List<Cell> GetValidCells()
        {
            if (CurrentCell == null) return new List<Cell>();

            var crossCells = new List<Cell> { CurrentCell };
            crossCells.AddRange(Field.GetAdjacentCells(CurrentCell, includeDiagonals: false));
            return crossCells;
        }
    }

}