using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour, ISettingsManager
{
    private const string MASTER_VOLUME_PARAM_NAME = "MasterVolume";

    [SerializeField] private AudioMixer _mixer;

    private FullScreenMode _screenMode;
    private Resolution _resolution;
    private SettingsSaveManager _settingsSaveManager;

    void Awake()
    {
        _screenMode = FullScreenMode.FullScreenWindow;
        _resolution = Screen.currentResolution;
        _settingsSaveManager = new SettingsSaveManager();
    }

    void Start()
    {
        SetMasterVolume(_settingsSaveManager.LoadMasterVolume(GetMasterVolume()));
        SetScreenMode(_settingsSaveManager.LoadScreenMode(GetScreenMode()));
        SetResolution(_settingsSaveManager.LoadResolution(GetResolution()));
    }

    public void SetMasterVolume(float volume)
    {
        _mixer.SetFloat(MASTER_VOLUME_PARAM_NAME, volume);
        _settingsSaveManager.SaveMasterVolume(volume);
    }

    public float GetMasterVolume()
    {
        float volume;
        _mixer.GetFloat(MASTER_VOLUME_PARAM_NAME, out volume);
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
