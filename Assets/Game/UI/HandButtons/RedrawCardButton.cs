using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedrawCardButton : CustomButton
{
    [SerializeField] private HandManager _manager;
    protected override void OnClick()
    {
        _manager.RedrawCardsInHand();
    }
}
