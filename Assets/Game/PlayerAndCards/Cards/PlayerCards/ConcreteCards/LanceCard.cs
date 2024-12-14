using Game.Table.Scripts.Entities;
using UnityEngine;

namespace Game.PlayerAndCards.Cards.PlayerCards.ConcreteCards
{
    public class LanceCard : PlayerCard
    {
        [SerializeField] private int _damage = 1;
        [SerializeField] private int _energyCost = 1;

        public override void Use()
        {
            if (!CanSpendEnergy(_energyCost))
                return;
            
            var targetCells = GetValidCells();
            if (targetCells.Length == 0) 
                return;

            var targetCell = targetCells[0];
            var enemy = targetCell.GetObjectOnCell<EnemyCard>();

            enemy.TakeDamage(_damage);
            SpendEnergy(_energyCost);
        }

        protected override Cell[] GetValidCells()
        {
            return CurrentCell.IsHidden
                   || CurrentCell?.ColumnId != 1 
                   || CurrentCell?.GetObjectOnCell<EnemyCard>() == null
                ? new Cell[] { }
                : new [] { CurrentCell };
        }
    }
}