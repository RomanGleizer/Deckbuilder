using UnityEngine;

public class LevelDistributor : MonoBehaviour
{
    [SerializeField] private LevelPlacement[] _levels;
    private int _currentLevel = 0;
    
    public void ChangeCurrentLevel(int level)
    {
        _currentLevel = level - 1;
    }

    public LevelPlacement GetCurrentLevel()
    {
        return _levels[_currentLevel];
    }
}