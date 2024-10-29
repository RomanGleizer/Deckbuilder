public class Commander : SupporterEnemy
{
    protected override void InitBehaviours()
    {
        base.InitBehaviours();
        _supportBh = new ActivateSecondAttackSupportBh();
    }
}