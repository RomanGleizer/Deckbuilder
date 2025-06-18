using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class FightMapPoint : MapPoint
{
    [SerializeField] private int _level;
    private LevelDistributor _levelDistributor;
    
    [Inject]
    private void Construct(LevelDistributor levelDistributor)
    {
        _levelDistributor = levelDistributor;
    }
    
    public override void ActivatePointEvent()
    {
        _levelDistributor.ChangeCurrentLevel(_level);    
        SceneManager.LoadSceneAsync(2);
    }
}