using UnityEngine;
using Zenject;

public class LevelEntryPoint : MonoBehaviour
{
    private LevelInitializator _levelInitializator;
    private AudioPlayer _audioPlayer;

    [Inject]
    private void Construct(LevelInitializator levelInitializator, AudioPlayer audioPlayer)
    {
        _levelInitializator = levelInitializator;
        _audioPlayer = audioPlayer;
    }

    private void Awake()
    {
        _audioPlayer.PlayAudio("main");
        SaveService.Load();
        _levelInitializator.InitializeLevel();
    }

    private void Start()
    {
        // start game loop
    }
}