using UnityEngine;
using UnityEngine.Audio;
using Zenject;

public class MenuEntryPoint : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    private AudioPlayer _audioPlayer;

    [Inject]
    private void Construct(AudioPlayer audioPlayer)
    {
        _audioPlayer = audioPlayer;
    }
    
    private void Awake()
    {
        SaveService.Load();
    }

    private void Start()
    {
        _audioPlayer.PlayAudio("menu");
        var volume = SaveService.SaveData.Volume;
        _audioMixer.SetFloat("Volume", Mathf.Lerp(-50, 20, volume));
    }
}