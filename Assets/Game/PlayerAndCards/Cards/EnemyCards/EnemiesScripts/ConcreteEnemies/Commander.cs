public class Commander : SupporterEnemy
{
    protected override void InitBehaviours()
    {
        base.InitBehaviours();
        _supportBh = new ActivateSecondAttackSupportBh();
    }

    public override void CreatePriorityCommand()
    {
        var command = _commandFactory.CreateSupportCommand(PosInOrderType.Last);
        _commandHandler.HandleCommand(command);
    }
}