using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISettingsManager
{
    void SetMasterVolume(float volume);

    float GetMasterVolume();
}
