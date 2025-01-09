public class Snare : CommonEnemy
{
    protected override void InitBehaviours()
    {
        _attackBh = _instantiator.Instantiate<DamageAndDestroyAttackBh>(new object[] { this });
        base.InitBehaviours();
    }
}
