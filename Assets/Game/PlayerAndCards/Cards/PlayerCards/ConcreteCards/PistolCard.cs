using System.Collections.Generic;
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
            if (!IsCanUse) return;

            var validCells = GetValidCells();
            if (validCells.Count == 0) return;
            
            // Выбираем первую подходящую цель
            var targetCell = validCells[0];
            var enemy = targetCell.GetObjectOnCell<EnemyCard>();

            if (enemy == null) return;

            enemy.TakeDamage(_damage);
            SpendEnergy();
        }

        protected override List<Cell> GetValidCells()
        {
            var targetCells = new List<Cell>();
            Field.TraverseCells(cell =>
            {
                if (cell.IsBusy)
                    targetCells.Add(cell);
            }, includeHidden: false);

            return targetCells;
        }
    }
}