public class Snare : CommonEnemy
{
    protected override void InitBehaviours()
    {
        _attackBh = new DamageAndDestroyAttackBh(this);
        base.InitBehaviours();
    }
}
