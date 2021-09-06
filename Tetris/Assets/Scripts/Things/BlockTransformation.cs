using System.Collections.Generic;
using UnityEngine;

public class BlockTransformation
{
    public static readonly BlockTransformation INVALID_TRANSFORMATION =
        new BlockTransformation(null, new Dictionary<Coordinate, Coordinate>(), Vector2Int.zero, RotationState.Zero);

    public Block Block { get; private set; }
    public Dictionary<Coordinate, Coordinate> OldToNewCoordinates { get; private set; }
    public Vector2Int LinearTranslation { get; private set; }
    public RotationState NewRotationState { get; private set; }

    public BlockTransformation(Block block,
        Dictionary<Coordinate, Coordinate> oldToNewCoordinates,
        Vector2Int kickTranslation,
        RotationState newRotationState)
    {
        Block = block;
        OldToNewCoordinates = oldToNewCoordinates;
        LinearTranslation = kickTranslation;
        NewRotationState = newRotationState;
    }

    public bool IsValid()
    {
        return OldToNewCoordinates.Count > 0;
    }
}