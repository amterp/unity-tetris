using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coordinate
{
    public int X { get; private set; }
    public int Y { get; private set; }
    public float OriginX { get; private set; }
    public float OriginY { get; private set; }
    public float Scale { get; private set; }
    public Transform Transform { get; private set; }

    public Coordinate(int x, int y, float originX, float originY, float scale, Transform transform)
    {
        X = x;
        Y = y;
        OriginX = originX;
        OriginY = originY;
        Scale = scale;
        Transform = transform;
    }

    public void UpdateTransform()
    {
        Transform.position = new Vector3(OriginX + X * Scale, OriginY - Y * Scale);
    }

    public override int GetHashCode()
    {
        return (X, Y, Transform).GetHashCode();
    }
}
