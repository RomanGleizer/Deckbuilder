using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioData[] _audio;
    private AudioSource _audioSource;
    
    private string _currentAudio;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudio(string audioName)
    {
        if (_currentAudio == audioName) return;
        
        var clip = FindClip(audioName);
        
        _currentAudio = audioName;
        _audioSource.clip = clip;
        _audioSource.Play();
    }

    private AudioClip FindClip(string name)
    {
        return _audio.FirstOrDefault(audioData => audioData.AudioName == name)?.AudioClip;
    }
}

[Serializable]
public class AudioData
{
    [SerializeField] private string audioName;
    [SerializeField] private AudioClip audioClip;
    
    public string AudioName => audioName;
    public AudioClip AudioClip => audioClip;
}
