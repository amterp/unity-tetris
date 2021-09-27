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

    void Start()
    {
        SetScreenMode(FullScreenMode.FullScreenWindow);
    }

    public void SetMasterVolume(float volume)
    {
        _mixer.SetFloat(MASTER_VOLUME_PARAM_NAME, volume);
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
    }

    public FullScreenMode GetScreenMode()
    {
        return _screenMode;
    }

    public Resolution GetResolution()
    {
        return _resolution;
    }

    public void SetResolution(Resolution resolution)
    {
        _resolution = resolution;
        Screen.SetResolution(_resolution.width, _resolution.height, Screen.fullScreenMode);
    }
}
