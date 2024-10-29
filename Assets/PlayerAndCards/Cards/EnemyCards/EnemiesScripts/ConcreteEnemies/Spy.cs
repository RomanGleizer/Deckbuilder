public class Spy : SpecialEnemy
{
    protected override void InitBehaviours()
    {
        _specialAttackBh = new DestroyingCardsAttackBh();
        base.InitBehaviours();
    }
}
