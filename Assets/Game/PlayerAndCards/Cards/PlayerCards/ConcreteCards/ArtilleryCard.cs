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
            if (!IsCanUse || CurrentCell == null) return;

            var validCells = GetValidCells();
            foreach (var enemy in validCells
                         .Select(c => c.GetObjectOnCell<EnemyCard>())
                         .Where(e => e != null))
            {
                enemy.TakeDamage(_damage);
            }

            SpendEnergy(_energyCost);
        }

        protected override List<Cell> GetValidCells()
        {
            return CurrentCell == null 
                ? new List<Cell>() 
                : Field.GetRowByCell(CurrentCell, includeHidden: false).ToList();
        }
    }
}