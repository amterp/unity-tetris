using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VolumeSliderTextController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Slider _volumeSlider;

    void Awake()
    {
        _volumeSlider.onValueChanged.AddListener(UpdateText);
    }

    void Start()
    {
        UpdateText(_volumeSlider.value);
    }

    public void UpdateText(float volume)
    {
        float min = _volumeSlider.minValue;
        float max = _volumeSlider.maxValue;
        float normalized = (volume - min) / (max - min);
        _text.text = normalized.ToString("0.##");
    }
}
