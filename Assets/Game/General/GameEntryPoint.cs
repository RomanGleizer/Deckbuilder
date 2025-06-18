using UnityEngine;
using Zenject;

public class GameEntryPoint : MonoBehaviour
{
    private AudioPlayer _audioPlayer;

    [Inject]
    private void Construct(AudioPlayer audioPlayer)
    {
        _audioPlayer = audioPlayer;
    }
    
    private void Awake()
    {
        _audioPlayer.PlayAudio("main");
        SaveService.Load();
    }
}