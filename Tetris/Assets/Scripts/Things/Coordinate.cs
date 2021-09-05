using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coordinate
{
    public int X { get; private set; }
    public int Y { get; private set; }
    public Transform? Transform { get; private set; }

    private float _originX;
    private float _originY;

    public Coordinate(int x, int y, float originX, float originY)
    {
        X = x;
        Y = y;
        _originX = originX;
        _originY = originY;
        Transform = null;
    }

    public Coordinate(int x, int y, float originX, float originY, Transform transform)
    {
        X = x;
        Y = y;
        _originX = originX;
        _originY = originY;
        Transform = transform;
    }

    public Coordinate XShifted(int xShift)
    {
        return Shifted(xShift, 0);
    }

    public Coordinate YShifted(int yShift)
    {
        return Shifted(0, yShift);
    }

    public Coordinate Shifted(int xShift, int yShift)
    {
        return new Coordinate(X + xShift, Y + yShift, _originX, _originY);
    }

    public void UpdateTransform()
    {
        if (Transform == null) return;
        Transform.position = new Vector3(_originX + X * Transform.localScale.x, _originY - Y * Transform.localScale.y);
    }

    public override string ToString()
    {
        return String.Format("C({0}, {1})", X, Y);
    }

    public override bool Equals(object obj)
    {
        return obj is Coordinate coordinate &&
               X == coordinate.X &&
               Y == coordinate.Y;
    }

    public override int GetHashCode()
    {
        return (X, Y).GetHashCode();
    }
}
