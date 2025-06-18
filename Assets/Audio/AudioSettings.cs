using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _volumeSlider;

    private void Awake()
    {
        _volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        
        var volume = SaveService.SaveData.Volume;
        _volumeSlider.value = volume;
    }
    
    private void OnVolumeChanged(float value)
    {
        _audioMixer.SetFloat("Volume", Mathf.Lerp(-50, 20, value));
        SaveService.SaveData.Volume = value;
        SaveService.Save();
    }

    private void OnDestroy()
    {
        _volumeSlider.onValueChanged.RemoveListener(OnVolumeChanged);
    }
}
