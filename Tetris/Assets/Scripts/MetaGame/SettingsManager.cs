using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour, ISettingsManager
{
    private const string MASTER_VOLUME_PARAM_NAME = "MasterVolume";

    [SerializeField] private AudioMixer mixer;

    public void SetMasterVolume(float volume)
    {
        mixer.SetFloat(MASTER_VOLUME_PARAM_NAME, volume);
    }

    public float GetMasterVolume()
    {
        float volume;
        mixer.GetFloat(MASTER_VOLUME_PARAM_NAME, out volume);
        return volume;
    }
}
