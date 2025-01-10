using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipMoveButton : CustomButton
{
    [SerializeField] HandManager _manager;
    protected override void OnClick()
    {
        //_manager.ClearHand();
    }
}
