using System;
using UnityEngine;

public class ShopMapPoint : MapPoint
{
    public override void ActivatePointEvent()
    {
        Debug.Log("ShopMapPointActivated");
    }
}