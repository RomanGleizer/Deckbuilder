using Game.PlayerAndCards.PlayerScripts;
using Game.PlayerAndCards.PlayerScripts.Interfaces;
using Zenject;

public class StunPlayerAttackBh : ISpecialAttackBh
{
    private int _stunDuration;

    private IStunnable _stunnablePlayer;

    public StunPlayerAttackBh(int stunDuration)
    {
        _stunDuration = stunDuration;
    }

    [Inject]
    private void Construct(Player player)
    {
        _stunnablePlayer = player;
    }

    public void Attack()
    {
        _stunnablePlayer.Stun(_stunDuration);
    }
}