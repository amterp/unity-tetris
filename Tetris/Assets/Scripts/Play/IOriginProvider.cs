using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IOriginProvider
{
    float GetX();
    float GetY();
}

public class ZeroOriginProvider : IOriginProvider
{
    public float GetX()
    {
        return 0;
    }

    public float GetY()
    {
        return 0;
    }
}
