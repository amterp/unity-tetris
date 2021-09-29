using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerProxy : MonoBehaviour
{

    private AudioManager _audioManager;

    void Awake()
    {
        _audioManager = GoUtil.FindAudioManager();
    }

    public void PlayUiBeep()
    {
        _audioManager.Play(SoundEnum.UiBeep);
    }
}
