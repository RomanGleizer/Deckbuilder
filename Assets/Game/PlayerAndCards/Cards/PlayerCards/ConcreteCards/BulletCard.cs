using Game.PlayerAndCards.PlayerScripts.Interfaces;
using Game.Table.Scripts.Entities;
using UnityEngine;

namespace Game.PlayerAndCards.Cards.PlayerCards.ConcreteCards
{
    public class BulletCard : PlayerCard
    {
        public override void Use()
        {
            if (!CanSpendEnergy(CardData.EnergyCost))
                return;

            var targetCells = GetValidCells();
            if (targetCells.Length == 0)
                return;

            var targetCell = targetCells[0];
            
            var haveShieldEnemy = targetCell.GetObjectOnCell<IHaveShield>();
            haveShieldEnemy.BreakShield();
            
            var stunnableEnemy = targetCell.GetObjectOnCell<IStunnable>();
            stunnableEnemy.Stun(1);
            
            SpendEnergy(CardData.EnergyCost);
            HandManager.DeleteCardFromHand(this);
        }

        protected override Cell[] GetValidCells()
        {
            return CurrentCell.IsHidden
                   || (CurrentCell.GetObjectOnCell<IHaveShield>() == null &&
                   CurrentCell.GetObjectOnCell<IStunnable>() == null)
                ? new Cell[] { }
                : new[] { CurrentCell };
        }
    }
}