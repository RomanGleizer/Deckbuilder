using Game.PlayerAndCards.PlayerScripts;
using Zenject;

public class DamageAndDestroyAttackBh : DamagePlayerAttackBh, IAttackBh // Snare's attack
{
    public ITakerDamage _destroyingAttacker;

    public DamageAndDestroyAttackBh(ITakerDamage takerDamage)
    {
        _destroyingAttacker = takerDamage;
    }

    public override void Attack(int damage)
    {
        base.Attack(damage);
        _destroyingAttacker.Death();
    }
}