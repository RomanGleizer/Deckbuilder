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
            foreach (var enemy in validCells
                         .Select(c => c.GetObjectOnCell<EnemyCard>()).Where(e => e != null))
            {
                enemy.TakeDamage(_damage);
            }

            SpendEnergy();
        }
        
        protected override List<Cell> GetValidCells()
        {
            var playerCell = Player.CurrentCell;
            if (playerCell == null) return new List<Cell>();

            var targetCells = new List<Cell>
            {
                playerCell,
                Field.FindCell(playerCell, Vector2.up, 1),
                Field.FindCell(playerCell, Vector2.down, 1),
                Field.FindCell(playerCell, Vector2.left, 1),
                Field.FindCell(playerCell, Vector2.right, 1)
            };

            return targetCells.FindAll(cell => cell != null && cell.IsBusy);
        }
    }
}