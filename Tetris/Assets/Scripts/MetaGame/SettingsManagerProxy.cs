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
}
