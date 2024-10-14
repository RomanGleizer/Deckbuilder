public class Armsman : SupporterEnemy
{
    protected override void InitBehaviours()
    {
        base.InitBehaviours();
        _supportBh = new GiveInvincibilitySupportBh(this);
    }
}