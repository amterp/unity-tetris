using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCell : MonoBehaviour
{

    public Coordinate Coordinate { get; private set; }

    public void InitCoordinate(Coordinate coordinate)
    {
        if (Coordinate != null) throw new InvalidOperationException("Can only add a coordinate once!");
        Coordinate = coordinate;
    }
}
