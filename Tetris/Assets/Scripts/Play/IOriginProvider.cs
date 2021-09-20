using System;
using System.Collections.Generic;
using UnityEngine;

public interface IOriginProvider
{
    float GetX();
    float GetY();
    bool IsOriginChanging();
    Action OriginChangeEvent { get; set; }
}

public class ZeroOriginProvider : IOriginProvider
{
    public Action OriginChangeEvent { get; set; }

    public float GetX()
    {
        return 0;
    }

    public float GetY()
    {
        return 0;
    }

    public bool IsOriginChanging()
    {
        return false;
    }
}
