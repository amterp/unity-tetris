using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsSaveManager
{
    private const string MASTER_VOLUME_KEY = "MasterVolume";
    private const string SCREEN_MODE_KEY = "ScreenMode";
    private const string RESOLUTION_WIDTH_KEY = "ResolutionWidth";
    private const string RESOLUTION_HEIGHT_KEY = "ResolutionHeight";
    private const string RESOLUTION_REFRESH_RATE_KEY = "ResolutionRefreshRate";

    public float LoadMasterVolume(float defaultVolume)
    {
        return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY, defaultVolume);
    }

    public FullScreenMode LoadScreenMode(FullScreenMode defaultScreenMode)
    {
        return (FullScreenMode)PlayerPrefs.GetInt(SCREEN_MODE_KEY, ((int)defaultScreenMode));
    }

    public Resolution LoadResolution(Resolution defaultRes)
    {
        Resolution resolution = new Resolution();
        resolution.width = PlayerPrefs.GetInt(RESOLUTION_WIDTH_KEY, defaultRes.width);
        resolution.height = PlayerPrefs.GetInt(RESOLUTION_HEIGHT_KEY, defaultRes.height);
        resolution.refreshRate = PlayerPrefs.GetInt(RESOLUTION_REFRESH_RATE_KEY, defaultRes.refreshRate);
        return resolution;
    }

    public void SaveMasterVolume(float volume)
    {
        PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
    }

    public void SaveScreenMode(FullScreenMode screenMode)
    {
        PlayerPrefs.SetInt(SCREEN_MODE_KEY, ((int)screenMode));
    }

    public void SaveResolution(Resolution res)
    {
        PlayerPrefs.GetInt(RESOLUTION_WIDTH_KEY, res.width);
        PlayerPrefs.GetInt(RESOLUTION_HEIGHT_KEY, res.height);
        PlayerPrefs.GetInt(RESOLUTION_REFRESH_RATE_KEY, res.refreshRate);
    }
}
