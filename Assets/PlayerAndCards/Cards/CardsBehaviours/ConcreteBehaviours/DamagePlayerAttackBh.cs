using PlayerAndCards.Player;
using Unity.VisualScripting;
using Zenject;

public class DamagePlayerAttackBh : IAttackBh
{
    //protected ITakerDamage _player;

    //[Inject]
    //private void Construct(Player player)
    //{
    //    _player = player;
    //}

    public virtual void Attack(int damage)
    {
        // _player.TakeDamage(damage);
    }
}
