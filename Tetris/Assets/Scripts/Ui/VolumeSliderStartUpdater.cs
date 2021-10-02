using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderStartUpdater : MonoBehaviour
{

    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private SoundMixer _soundMixer;

    void Start()
    {
        GoUtil.FindSettingsManager().VolumesLoaded.AddListener(() => _volumeSlider.value = _soundMixer.GetVolume());
    }
}

enum SoundMixer
{
    Master,
    Music,
    Effects,
}

static class SoundMixerMethods
{
    public static float GetVolume(this SoundMixer soundMixer)
    {
        SettingsManager settingsManager = GoUtil.FindSettingsManager();
        switch (soundMixer)
        {
            case SoundMixer.Master: return settingsManager.GetMasterVolume();
            case SoundMixer.Music: return settingsManager.GetMusicVolume();
            case SoundMixer.Effects: return settingsManager.GetEffectsVolume();
            default: throw new InvalidOperationException("Unknown sound mixer enum value: " + soundMixer);
        }
    }
}