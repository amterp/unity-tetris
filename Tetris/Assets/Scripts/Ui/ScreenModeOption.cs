using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScreenModeOption : MonoBehaviour
{

    [SerializeField] private TMP_Dropdown _dropdown;

    private SettingsManager _settingsManager;

    void Awake()
    {
        _settingsManager = GoUtil.FindSettingsManager();
        _dropdown.onValueChanged.AddListener(OnScreenModeChanged);
    }

    void Start()
    {
        _dropdown.ClearOptions();
        _dropdown.AddOptions(new List<string> { "Borderless", "Fullscreen", "Windowed" });
        _dropdown.SetValueWithoutNotify(_settingsManager.GetScreenMode().ToDropdownInt());
    }

    private void OnScreenModeChanged(int chosenIndex)
    {
        _settingsManager.SetScreenMode(ScreenModeMethods.FromInt(chosenIndex).ToUnityVersion());
    }
}

public enum ScreenMode
{
    Borderless = 0,
    Fullscreen = 1,
    Windowed = 2,
}

public static class ScreenModeMethods
{
    public static ScreenMode FromInt(int index)
    {
        switch (index)
        {
            case 0: return ScreenMode.Borderless;
            case 1: return ScreenMode.Fullscreen;
            case 2: return ScreenMode.Windowed;
            default: throw new InvalidOperationException("Invalid index: " + index);
        }
    }

    public static FullScreenMode ToUnityVersion(this ScreenMode screenMode)
    {
        switch (screenMode)
        {
            case ScreenMode.Borderless: return FullScreenMode.FullScreenWindow;
            case ScreenMode.Fullscreen: return FullScreenMode.ExclusiveFullScreen;
            case ScreenMode.Windowed: return FullScreenMode.Windowed;
            default: throw new InvalidOperationException("Unknown screen mode: " + screenMode);
        }
    }

    public static int ToDropdownInt(this FullScreenMode fullScreenMode)
    {
        switch (fullScreenMode)
        {
            case FullScreenMode.FullScreenWindow: return 0;
            case FullScreenMode.ExclusiveFullScreen: return 1;
            case FullScreenMode.Windowed: return 2;
            default: throw new InvalidOperationException("Unknown screen mode: " + fullScreenMode);
        }
    }
}
