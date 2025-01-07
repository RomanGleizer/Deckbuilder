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
            var enemy = targetCell.GetObjectOnCell<EnemyCard>();
            if (enemy.HasShield)
            {
                enemy.BreakShield();
            }
            else
            {
                if (enemy is IStunnable stunnableEnemy)
                {
                    stunnableEnemy.Stun(1);
                }
            }
        }

        protected override Cell[] GetValidCells()
        {
            return CurrentCell.IsHidden
                   || CurrentCell?.GetObjectOnCell<EnemyCard>() == null
                ? new Cell[] { }
                : new [] { CurrentCell };
        }
    }
}