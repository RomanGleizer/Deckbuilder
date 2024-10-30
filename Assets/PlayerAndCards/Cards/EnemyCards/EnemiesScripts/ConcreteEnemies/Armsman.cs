using UnityEngine;

public class Armsman : SupporterEnemy
{
    protected override void InitBehaviours()
    {
        base.InitBehaviours();
        _supportBh = _instantiator.Instantiate<GiveInvincibilitySupportBh>(new object[] {this});
    }
}