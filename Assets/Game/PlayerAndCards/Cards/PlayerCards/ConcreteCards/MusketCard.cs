﻿using System.Collections.Generic;
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
            if (validCells.Count == 0) return;

            foreach (var enemy in validCells.Select(c => 
                             c.GetObjectOnCell<EnemyCard>()).Where(e => e != null))
            {
                enemy.TakeDamage(_damage);
            }

            SpendEnergy();
        }

        protected override List<Cell> GetValidCells()
        {
            var column = Field.GetColumnById(0, includeHidden: false);
            return column.Where(cell => cell.IsBusy).ToList();
        }
    }
}