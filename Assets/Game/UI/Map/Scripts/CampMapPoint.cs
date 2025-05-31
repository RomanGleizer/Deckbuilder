using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CampMapPoint : MapPoint
{
    public override void ActivatePointEvent()
    {
        SceneManager.LoadSceneAsync(3);
    }
}