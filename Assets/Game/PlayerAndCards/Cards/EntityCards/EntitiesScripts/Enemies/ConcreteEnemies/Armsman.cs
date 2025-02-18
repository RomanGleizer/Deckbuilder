﻿using UnityEngine;

public class Armsman : SupporterEnemy
{
    protected override void InitBehaviours()
    {
        base.InitBehaviours();
        _supportBh = _instantiator.Instantiate<GiveInvincibilitySupportBh>(new object[] {this});
    }

    public override Command CreatePriorityCommand()
    {
        var command = _commandFactory.CreateSupportCommand(this);
        command.SetVisual(HiglightActivingEnemy, UnhiglightActivingEnemy);
        return command;
    }
}