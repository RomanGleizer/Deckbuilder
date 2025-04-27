using System;
using UnityEngine;

public class ChooseMapPoint : MapPoint
{
    public override void ActivatePointEvent()
    {
        Debug.Log("ChooseMapPointActivated");
    }
}