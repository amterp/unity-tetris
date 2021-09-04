using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coordinate
{
    public int X { get; private set; }
    public int Y { get; private set; }
    public Transform Transform { get; private set; }

    private float _originX;
    private float _originY;

    public Coordinate(int x, int y, float originX, float originY, Transform transform)
    {
        X = x;
        Y = y;
        _originX = originX;
        _originY = originY;
        Transform = transform;
    }

    public void UpdateTransform()
    {
        Transform.position = new Vector3(_originX + X * Transform.localScale.x, _originY - Y * Transform.localScale.y);
    }

    public override int GetHashCode()
    {
        return (X, Y, Transform).GetHashCode();
    }
}
