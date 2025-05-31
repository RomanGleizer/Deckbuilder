using System;
using UnityEngine;
using Zenject;

public class ShopMapPoint : MapPoint
{
    private WindowActivator _windowActivator;
    
    [Inject]
    private void Construct(WindowActivator windowActivator)
    {
        _windowActivator = windowActivator;
    }
    
    public override void ActivatePointEvent()
    {
        _windowActivator.ActivateWindow(WindowType.Shop);
    }
}