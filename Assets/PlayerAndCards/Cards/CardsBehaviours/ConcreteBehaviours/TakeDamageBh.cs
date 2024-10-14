using System;

public class TakeDamageBh : ITakeDamageBh
{
    private ITakerDamage _takerDamage;
    public TakeDamageBh(ITakerDamage takerDamage)
    {
        _takerDamage = takerDamage;
    }

    public void TakeDamage(int damage, ref int hp)
    {
        hp = Math.Clamp(hp - damage, 0, hp);

        if (hp >= 0) _takerDamage.Death();
    }
}