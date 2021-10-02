using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour, ISettingsManager
{
    private const string MASTER_VOLUME_PARAM_NAME = "MasterVolume";
    private const string MUSIC_VOLUME_PARAM_NAME = "MusicVolume";
    private const string EFFECTS_VOLUME_PARAM_NAME = "EffectsVolume";

    public LatentAction VolumesLoaded;

    [SerializeField] private AudioMixerGroup _masterMixer;
    [SerializeField] private AudioMixerGroup _musicMixer;
    [SerializeField] private AudioMixerGroup _effectsMixer;

    private FullScreenMode _screenMode;
    private Resolution _resolution;
    private SettingsSaveManager _settingsSaveManager;

    void Awake()
    {
        _screenMode = FullScreenMode.FullScreenWindow;
        _resolution = Screen.currentResolution;
        _settingsSaveManager = new SettingsSaveManager();
        VolumesLoaded = new LatentAction();
    }

    void Start()
    {
        SetMasterVolume(_settingsSaveManager.LoadMasterVolume(GetMasterVolume()));
        SetMusicVolume(_settingsSaveManager.LoadMusicVolume(GetMusicVolume()));
        SetEffectsVolume(_settingsSaveManager.LoadEffectsVolume(GetEffectsVolume()));
        SetScreenMode(_settingsSaveManager.LoadScreenMode(GetScreenMode()));
        SetResolution(_settingsSaveManager.LoadResolution(GetResolution()));
        VolumesLoaded.Invoke();
    }

    public void SetMasterVolume(float volume)
    {
        _masterMixer.audioMixer.SetFloat(MASTER_VOLUME_PARAM_NAME, volume);
        _settingsSaveManager.SaveMasterVolume(volume);
    }

    public float GetMasterVolume()
    {
        float volume;
        _masterMixer.audioMixer.GetFloat(MASTER_VOLUME_PARAM_NAME, out volume);
        return volume;
    }

    public void SetMusicVolume(float volume)
    {
        _musicMixer.audioMixer.SetFloat(MUSIC_VOLUME_PARAM_NAME, volume);
        _settingsSaveManager.SaveMusicVolume(volume);
    }

    public float GetMusicVolume()
    {
        float volume;
        _musicMixer.audioMixer.GetFloat(MUSIC_VOLUME_PARAM_NAME, out volume);
        return volume;
    }

    public void SetEffectsVolume(float volume)
    {
        _effectsMixer.audioMixer.SetFloat(EFFECTS_VOLUME_PARAM_NAME, volume);
        _settingsSaveManager.SaveEffectsVolume(volume);
    }

    public float GetEffectsVolume()
    {
        float volume;
        _effectsMixer.audioMixer.GetFloat(EFFECTS_VOLUME_PARAM_NAME, out volume);
        return volume;
    }

    public void SetScreenMode(FullScreenMode screenMode)
    {
        _screenMode = screenMode;
        Screen.fullScreenMode = screenMode;
        _settingsSaveManager.SaveScreenMode(screenMode);
    }

    public FullScreenMode GetScreenMode()
    {
        return _screenMode;
    }

    public void SetResolution(Resolution resolution)
    {
        _resolution = resolution;
        Screen.SetResolution(_resolution.width, _resolution.height, _screenMode);
        _settingsSaveManager.SaveResolution(resolution);
    }

    public Resolution GetResolution()
    {
        return _resolution;
    }
}
