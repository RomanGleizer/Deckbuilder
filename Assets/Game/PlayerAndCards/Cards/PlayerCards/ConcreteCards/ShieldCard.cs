using System.Collections.Generic;
using Game.Table.Scripts.Entities;
using UnityEngine;

namespace Game.PlayerAndCards.Cards.PlayerCards.ConcreteCards
{
    public class ShieldCard : PlayerCard
    {
        [SerializeField] private int _shieldDuration = 2;
        [SerializeField] private int _energyCost = 2;
        
        protected override List<Cell> GetValidCells()
        {
            return new List<Cell>();
        }
        
        public override void Use()
        {
            if (!IsCanUse) return;

            Player.AddShield(_shieldDuration);
            SpendEnergy();
        }
    }
}