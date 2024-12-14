using System.Collections.Generic;
using Game.Table.Scripts.Entities;
using UnityEngine;

namespace Game.PlayerAndCards.Cards.PlayerCards.ConcreteCards
{
    public class ShieldCard : PlayerCard
    {
        [SerializeField] private int _duration = 2;
        [SerializeField] private int _energyCost = 2;

        public override void Use()
        {
            if (!CanSpendEnergy(_energyCost)) 
                return;

            Player.AddShield(_duration);
            SpendEnergy(_energyCost);
        }

        protected override Cell[] GetValidCells()
        {
            return new Cell[] {};
        }
    }

}