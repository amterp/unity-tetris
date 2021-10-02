using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManagerProxy : MonoBehaviour, ISettingsManager
{

    private SettingsManager _delegate;

    void Awake()
    {
        _delegate = GoUtil.FindSettingsManager();
    }

    public void SetMasterVolume(float volume)
    {
        _delegate.SetMasterVolume(volume);
    }

    public float GetMasterVolume()
    {
        return _delegate.GetMasterVolume();
    }

    public void SetMusicVolume(float volume)
    {
        _delegate.SetMusicVolume(volume);
    }

    public float GetMusicVolume()
    {
        return _delegate.GetMusicVolume();
    }

    public void SetEffectsVolume(float volume)
    {
        _delegate.SetEffectsVolume(volume);
    }

    public float GetEffectsVolume()
    {
        return _delegate.GetEffectsVolume();
    }
}
