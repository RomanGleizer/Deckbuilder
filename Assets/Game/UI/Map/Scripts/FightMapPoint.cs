using System;
using UnityEngine;

public class FightMapPoint : MapPoint
{
    public override void ActivatePointEvent()
    {
        Debug.Log("FightMapPointActivated");
    }
}