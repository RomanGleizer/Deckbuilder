using UnityEngine;
using Zenject;

public class LevelEntryPoint : MonoBehaviour
{
    [SerializeField] private LevelPlacement _levelPlacement;
    [SerializeField] private Transform _spawnPoint;

    private LevelInitializator _levelInitializator;

    [Inject]
    private void Construct(LevelInitializator levelInitializator)
    {
        _levelInitializator = levelInitializator;
    }

    private void Awake()
    {
        _levelInitializator.InitializeLevel(_levelPlacement, _spawnPoint.position.x);
    }

    private void Start()
    {
        // start game loop
    }
}