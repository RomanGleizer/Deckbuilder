using Game.PlayerAndCards.PlayerScripts;
using Game.PlayerAndCards.PlayerScripts.Interfaces;

namespace Game.PlayerAndCards.Cards.CardsBehaviours.ConcreteBehaviours
{
    public class StunPlayerAttackBh : ISpecialAttackBh
    {
        private Player _player;
        private IStunnable _stunnablePlayer;

        public void Attack()
        {
            _player.Stun(1f);
        }
    }
}
