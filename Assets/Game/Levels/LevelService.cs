using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class LevelService : MonoBehaviour
{
    private LevelPlacementStackController _stackController;
    private EnemyService _enemyService;

    [Inject]
    private void Construct(LevelPlacementStackController stackController, EnemyService enemyService)
    {
        _stackController = stackController;
        _enemyService = enemyService;
        Subscribe();
    }
    
    private void CheckFinishConditions()
    {
        Debug.Log("Check finish conditions");
        if (_stackController.IsPlacementStackEmpty()) FinishLevel();
    }
    
    private void FinishLevel()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void Subscribe()
    {
        _enemyService.OnEnemiesFinished += CheckFinishConditions;
    }

    public void Unsubscribe()
    {
        _enemyService.OnEnemiesFinished -= CheckFinishConditions;
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }
}