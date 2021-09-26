using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IJsonSerializable
{
    string ToJson();

    void LoadFromJson(string jsonString);
}
