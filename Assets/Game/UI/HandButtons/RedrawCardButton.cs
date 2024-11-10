using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedrawCardButton : CustomButton
{
    [SerializeField] HandManager _manager;
    protected override void OnClick()
    {
        _manager.DrawCardsInHand();
    }
}
