using System;

public enum RotationDirection
{
    Clockwise,
    Anticlockwise,
}

public static class RotationDirectionMethods
{
    public static RotationState Rotate(this RotationDirection rotationDirection, RotationState rotationState)
    {
        switch (rotationDirection)
        {
            case RotationDirection.Clockwise:
                return rotationState.RotatedClockwise();
            case RotationDirection.Anticlockwise:
                return rotationState.RotatedAnticlockwise();
            default:
                throw new InvalidOperationException("Unknown RotationDirection: " + rotationDirection);
        }
    }
}