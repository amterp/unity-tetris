using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCell : MonoBehaviour
{

    public Coordinate Coordinate { get; private set; }

    public void InitializeCoordinate(int x, int y, float originX, float originY)
    {
        Coordinate = new Coordinate(x, y, originX, originY, transform);
        Coordinate.UpdateTransform();
    }
}
