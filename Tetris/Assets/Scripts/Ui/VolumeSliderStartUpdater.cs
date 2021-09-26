using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderStartUpdater : MonoBehaviour
{

    [SerializeField] private Slider _volumeSlider;

    void Start()
    {
        _volumeSlider.value = GoUtil.FindSettingsManager().GetMasterVolume();
    }
}
