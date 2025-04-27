using System;
using UnityEngine;

public class CampMapPoint : MapPoint
{
    public override void ActivatePointEvent()
    {
        Debug.Log("CampMapPointActivated");
    }
}