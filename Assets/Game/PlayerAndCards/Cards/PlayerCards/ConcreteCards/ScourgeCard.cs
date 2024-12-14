using System.Collections.Generic;
using System.Linq;
using Game.Table.Scripts.Entities;
using UnityEngine;

namespace Game.PlayerAndCards.Cards.PlayerCards.ConcreteCards
{
    public class ScourgeCard : PlayerCard
    {
        [SerializeField] private int _energyCost = 1;

        public override void Use()
        {
        }

        protected override Cell[] GetValidCells()
        {
            return new Cell[] { };
        }
    }
}