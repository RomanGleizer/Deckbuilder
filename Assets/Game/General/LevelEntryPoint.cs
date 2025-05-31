using UnityEngine;
using Zenject;

public class LevelEntryPoint : MonoBehaviour
{
    private LevelInitializator _levelInitializator;

    [Inject]
    private void Construct(LevelInitializator levelInitializator)
    {
        _levelInitializator = levelInitializator;
    }

    private void Awake()
    {
        SaveService.Load();
        _levelInitializator.InitializeLevel();
    }

    private void Start()
    {
        // start game loop
    }
}