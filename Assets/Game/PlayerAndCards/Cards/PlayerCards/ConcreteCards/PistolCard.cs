using System.Collections.Generic;
using System.Linq;
using Game.Table.Scripts.Entities;
using UnityEngine;

namespace Game.PlayerAndCards.Cards.PlayerCards.ConcreteCards
{
    public class PistolCard : PlayerCard
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

            var targetCell = validCells[0];
            var enemy = targetCell.GetObjectOnCell<EnemyCard>();

            if (enemy == null) return;

            enemy.TakeDamage(_damage);
            SpendEnergy(_energyCost);
        }

        protected override Cell[] GetValidCells()
        {
            return CurrentCell.IsHidden || CurrentCell.GetObjectOnCell<EnemyCard>() == null
                ? new Cell[] {} 
                : new[] { CurrentCell };
        }
    }
}