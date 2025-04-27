using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FightMapPoint : MapPoint
{
    public override void ActivatePointEvent()
    {
        SceneManager.LoadSceneAsync(2);
    }
}