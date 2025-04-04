﻿using System.Collections.Generic;
using System.Linq;
using Game.Table.Scripts.Entities;
using UnityEngine;

namespace Game.PlayerAndCards.Cards.PlayerCards.ConcreteCards
{
    public class PistolCard : PlayerCard
    {
        public override void Use()
        {
            if (!CanSpendEnergy(CardData.EnergyCost))
                return;

            var validCells = GetValidCells();
            if (validCells.Length == 0) 
                return;

            var targetCell = validCells[0];
            var enemy = targetCell.GetObjectOnCell<ITakerDamage>();

            if (enemy == null) return;

            enemy.TakeDamage(CardData.Damage);
            SpendEnergy(CardData.EnergyCost);
            HandManager.DeleteCardFromHand(this);
        }

        protected override Cell[] GetValidCells()
        {
            return CurrentCell.IsHidden || CurrentCell.GetObjectOnCell<ITakerDamage>() == null
                ? new Cell[] {} 
                : new[] { CurrentCell };
        }
    }
}