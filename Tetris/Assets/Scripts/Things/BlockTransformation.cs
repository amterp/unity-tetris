using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BlockTransformation
{
    public static readonly BlockTransformation INVALID_TRANSFORMATION =
        new BlockTransformation(null, new Dictionary<Coordinate, Coordinate>(), Vector2Int.zero, null, RotationState.Zero);

    public Block Block { get; private set; }
    public Dictionary<Coordinate, Coordinate> OldToNewCoordinates { get; private set; }
    public Vector2Int LinearTranslation { get; private set; }
    public RotationDirection? RotationDirection { get; private set; }
    public RotationState ResultingRotationState { get; private set; }
    public ShiftDirection? ShiftDirection { get; private set; }

    public BlockTransformation(Block block,
        Dictionary<Coordinate, Coordinate> oldToNewCoordinates,
        Vector2Int linearTranslation,
        RotationDirection? rotationDirection,
        RotationState resultingRotationState)
    {
        Block = block;
        OldToNewCoordinates = oldToNewCoordinates;
        LinearTranslation = linearTranslation;
        RotationDirection = rotationDirection;
        ResultingRotationState = resultingRotationState;
        ShiftDirection = DetermineShiftDirection(oldToNewCoordinates);
    }

    public bool IsValid()
    {
        return OldToNewCoordinates.Count > 0;
    }

    private static ShiftDirection? DetermineShiftDirection(Dictionary<Coordinate, Coordinate> oldToNewCoordinates)
    {
        if (oldToNewCoordinates.Count == 0) return null;

        Coordinate someSampleKey = oldToNewCoordinates.Keys.First();
        Coordinate someSampleValue = oldToNewCoordinates[someSampleKey];
        Vector2Int move = someSampleValue.AsVector2Int() - someSampleKey.AsVector2Int();
        return ShiftDirectionMethods.FromMove(move);
    }
}