using UnityEngine;

public class Armsman : SupporterEnemy
{
    protected override void InitBehaviours()
    {
        base.InitBehaviours();
        _supportBh = _instantiator.Instantiate<GiveInvincibilitySupportBh>(new object[] {this});
    }

    public override void CreatePriorityCommand()
    {
        var command = _commandFactory.CreateSupportCommand(PosInOrderType.First);
        _commandHandler.HandleCommand(command);
    }
}