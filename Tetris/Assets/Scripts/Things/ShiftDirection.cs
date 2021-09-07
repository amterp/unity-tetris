using UnityEngine;
using System;

public enum ShiftDirection
{
    Left,
    Right,
    Down,
    NoShift,
}

public static class ShiftDirectionMethods
{
    private static readonly Vector2Int LEFT = new Vector2Int(-1, 0);
    private static readonly Vector2Int RIGHT = new Vector2Int(1, 0);
    private static readonly Vector2Int DOWN = new Vector2Int(0, 1);
    private static readonly ShiftDirection? INVALID_SHIFT_DIRECTION = null;

    public static ShiftDirection? FromMove(Vector2Int move)
    {
        if (move.Equals(LEFT))
        {
            return ShiftDirection.Left;
        }
        if (move.Equals(RIGHT))
        {
            return ShiftDirection.Right;
        }
        if (move.Equals(DOWN))
        {
            return ShiftDirection.Down;
        }
        return INVALID_SHIFT_DIRECTION;
    }
}