public class Pioneer : SpecialEnemy
{
    protected override void InitBehaviours()
    {
        _specialAttackBh = _instantiator.Instantiate<SpawnAttackBh>(new object[] {this});
        base.InitBehaviours();
    }
}