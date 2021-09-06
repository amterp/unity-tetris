using System;
using System.Collections.Generic;
using UnityEngine;

public enum BlockType
{
    Line,
    L,
    J,
    Square,
    S,
    Z,
    T,
}

static class BlockTypeMethods
{
    /** https://tetris.fandom.com/wiki/SRS */
    public static Vector2 PivotOffset(this BlockType blockType)
    {
        switch (blockType)
        {
            case BlockType.Line:
                return new Vector2(1, 2);
            case BlockType.L:
                return new Vector2(1, 1);
            case BlockType.J:
                return new Vector2(0.5f, 1.5f); // todo not correct
            case BlockType.Square:
                return new Vector2(0.5f, 1.5f); // todo not correct
            case BlockType.S:
                return new Vector2(0.5f, 1.5f); // todo not correct
            case BlockType.Z:
                return new Vector2(0.5f, 1.5f); // todo not correct
            case BlockType.T:
                return new Vector2(0.5f, 1.5f); // todo not correct
            default:
                throw new InvalidOperationException("Unknown BlockType: " + blockType);
        }
    }

    /** https://tetris.fandom.com/wiki/SRS */
    public static List<Vector2Int> WallKickTestTranslations(this BlockType blockType, RotationState rotationState, RotationDirection rotationDirection)
    {
        List<Vector2Int> translationsToTest;
        switch (rotationDirection)
        {
            case RotationDirection.Clockwise:
                translationsToTest = blockType.ClockwiseWallKickTestTranslations(rotationState);
                break;
            case RotationDirection.Anticlockwise:
                throw new InvalidOperationException("Anticlockwise rotations are not implemented.");
            default:
                throw new InvalidOperationException("Unknown RotationState: " + rotationDirection);
        }
        translationsToTest.Insert(0, Vector2Int.zero);
        return translationsToTest;
    }

    private static List<Vector2Int> ClockwiseWallKickTestTranslations(this BlockType blockType, RotationState rotationState)
    {
        switch (rotationState)
        {
            case RotationState.Zero:
                return blockType.ZeroToRightRotationTestTranslations();
            case RotationState.Right:
                return blockType.RightToDownRotationTestTranslations();
            case RotationState.Down:
                return blockType.DownToLeftRotationTestTranslations();
            case RotationState.Left:
                return blockType.LeftToZeroRotationTestTranslations();
            default:
                throw new InvalidOperationException("Unknown RotationState: " + rotationState);
        }
    }

    private static List<Vector2Int> ZeroToRightRotationTestTranslations(this BlockType blockType)
    {
        switch (blockType)
        {
            case BlockType.L:
            case BlockType.J:
            case BlockType.S:
            case BlockType.Z:
            case BlockType.T:
                return new List<Vector2Int> { new Vector2Int(-1, 0), new Vector2Int(-1, -1), new Vector2Int(0, 2), new Vector2Int(-1, 2) };
            case BlockType.Line:
                return new List<Vector2Int> { new Vector2Int(-2, 0), new Vector2Int(1, 0), new Vector2Int(-2, 1), new Vector2Int(1, -2) };
            case BlockType.Square:
                return new List<Vector2Int>();
            default:
                throw new InvalidOperationException("Unknown BlockType: " + blockType);
        }
    }

    private static List<Vector2Int> RightToDownRotationTestTranslations(this BlockType blockType)
    {
        switch (blockType)
        {
            case BlockType.L:
            case BlockType.J:
            case BlockType.S:
            case BlockType.Z:
            case BlockType.T:
                return new List<Vector2Int> { new Vector2Int(1, 0), new Vector2Int(1, 1), new Vector2Int(0, -2), new Vector2Int(1, -2) };
            case BlockType.Line:
                return new List<Vector2Int> { new Vector2Int(-1, 0), new Vector2Int(2, 0), new Vector2Int(-1, -2), new Vector2Int(2, 1) };
            case BlockType.Square:
                return new List<Vector2Int>();
            default:
                throw new InvalidOperationException("Unknown BlockType: " + blockType);
        }
    }

    private static List<Vector2Int> DownToLeftRotationTestTranslations(this BlockType blockType)
    {
        switch (blockType)
        {
            case BlockType.L:
            case BlockType.J:
            case BlockType.S:
            case BlockType.Z:
            case BlockType.T:
                return new List<Vector2Int> { new Vector2Int(1, 0), new Vector2Int(1, -1), new Vector2Int(0, 2), new Vector2Int(1, 2) };
            case BlockType.Line:
                return new List<Vector2Int> { new Vector2Int(2, 0), new Vector2Int(-1, 0), new Vector2Int(2, -1), new Vector2Int(-1, 2) };
            case BlockType.Square:
                return new List<Vector2Int>();
            default:
                throw new InvalidOperationException("Unknown BlockType: " + blockType);
        }
    }

    private static List<Vector2Int> LeftToZeroRotationTestTranslations(this BlockType blockType)
    {
        switch (blockType)
        {
            case BlockType.L:
            case BlockType.J:
            case BlockType.S:
            case BlockType.Z:
            case BlockType.T:
                return new List<Vector2Int> { new Vector2Int(-1, 0), new Vector2Int(-1, 1), new Vector2Int(0, -2), new Vector2Int(-1, -2) };
            case BlockType.Line:
                return new List<Vector2Int> { new Vector2Int(1, 0), new Vector2Int(-2, 0), new Vector2Int(1, 2), new Vector2Int(-2, -1) };
            case BlockType.Square:
                return new List<Vector2Int>();
            default:
                throw new InvalidOperationException("Unknown BlockType: " + blockType);
        }
    }
}