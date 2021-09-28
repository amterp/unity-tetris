using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class ResolutionOption : MonoBehaviour
{

    [SerializeField] private TMP_Dropdown _dropdown;

    private SettingsManager _settingsManager;
    private Resolution[] _resolutions;

    void Awake()
    {
        _settingsManager = GoUtil.FindSettingsManager();
        _resolutions = Screen.resolutions;
        SetupDropdown(_dropdown);
        _dropdown.onValueChanged.AddListener(OnResolutionChange);
    }

    private void SetupDropdown(TMP_Dropdown dropdown)
    {
        dropdown.ClearOptions();

        Array.Sort(_resolutions, (r1, r2) => -r1.width.CompareTo(r2.width));
        _resolutions.Where(resolution => resolution.refreshRate == Screen.currentResolution.refreshRate);

        List<string> options = new List<string>();
        int currentResolutionIndex = -1;
        for (int i = 0; i < _resolutions.Length; i++)
        {
            Resolution resolution = _resolutions[i];

            if (resolution.DimensionsEqual(Screen.currentResolution)) currentResolutionIndex = i;

            options.Add($"{resolution.width} x {resolution.height}");
        }

        dropdown.AddOptions(options);

        dropdown.SetValueWithoutNotify(currentResolutionIndex);
    }

    private void OnResolutionChange(int index)
    {
        _settingsManager.SetResolution(_resolutions[index]);
    }
}

public static class ResolutionMethods
{
    public static bool DimensionsEqual(this Resolution r1, Resolution r2)
    {
        return r1.width == r2.width && r1.height == r2.height;
    }
}
