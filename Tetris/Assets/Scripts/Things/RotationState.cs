using System;

public enum RotationState
{
    Zero = 0,
    Right = 1,
    Down = 2,
    Left = 3,
}

static class RotationStateMethods
{
    public static RotationState RotatedClockwise(this RotationState rotationState)
    {
        return (RotationState)(((int)rotationState + 1) % 4);
    }

    public static RotationState RotatedAnticlockwise(this RotationState rotationState)
    {
        return (RotationState)(((int)rotationState + 3) % 4);
    }
}