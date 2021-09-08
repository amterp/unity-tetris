using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Block
{
    private static readonly Vector2Int NO_LINEAR_TRANSLATION = Vector2Int.zero;

    public BlockType BlockType;
    public Dictionary<Coordinate, BlockPiece> PiecesByCoordinate;
    public bool IsPlaced;

    private RotationState _rotationState;
    private Vector2 _pivotPosition;

    public Block(BlockType blockType, Dictionary<Coordinate, BlockPiece> piecesByCoordinate)
    {
        BlockType = blockType;
        PiecesByCoordinate = piecesByCoordinate;
        _rotationState = RotationState.Zero;
        _pivotPosition = BlockType.PivotOffset();
    }

    public List<Coordinate> GetCoordinatesCopy()
    {
        return new List<Coordinate>(PiecesByCoordinate.Keys);
    }


    public BlockTransformation CalculateLinearTransformation(int xShift, int yShift)
    {
        Dictionary<Coordinate, Coordinate> oldToNewCoordinateDict = new Dictionary<Coordinate, Coordinate>();
        foreach (Coordinate currentCoordinate in PiecesByCoordinate.Keys)
        {
            oldToNewCoordinateDict.Add(currentCoordinate, currentCoordinate.Shifted(xShift, yShift));
        }
        return new BlockTransformation(this, oldToNewCoordinateDict, new Vector2Int(xShift, yShift), null, _rotationState);
    }

    public BlockTransformation CalculateRotatedCoordinates(Block block,
        RotationDirection rotationDirection,
        Predicate<List<Coordinate>> validCoordinatesPredicate)
    {
        Dictionary<Coordinate, Coordinate> originalRotationResult = CalculateInitialRotation(rotationDirection);
        List<Vector2Int> translationsToTest = BlockType.WallKickTestTranslations(_rotationState, rotationDirection);
        foreach (Vector2Int translationToTest in translationsToTest)
        {
            Dictionary<Coordinate, Coordinate> translatedRotationResult = ApplyTranslation(translationToTest, originalRotationResult);
            if (validCoordinatesPredicate(new List<Coordinate>(translatedRotationResult.Values)))
            {
                return new BlockTransformation(this, translatedRotationResult, translationToTest, rotationDirection, rotationDirection.Rotate(_rotationState));
            }
        }
        return BlockTransformation.INVALID_TRANSFORMATION;
    }

    public void PerformTransformation(BlockTransformation blockTransformation)
    {
        if (IsPlaced) throw new InvalidOperationException("Cannot move placed blocks!");
        if (!blockTransformation.IsValid()) throw new InvalidOperationException("Cannot execute invalid block transformations!");

        Dictionary<Coordinate, BlockPiece> newPiecesByCoordinateDict = new Dictionary<Coordinate, BlockPiece>();
        foreach (Coordinate currentCoordinate in PiecesByCoordinate.Keys)
        {
            Coordinate newCoordinate = blockTransformation.OldToNewCoordinates[currentCoordinate];
            newPiecesByCoordinateDict.Add(newCoordinate, PiecesByCoordinate[currentCoordinate]);
        }

        PiecesByCoordinate = newPiecesByCoordinateDict;
        _pivotPosition += blockTransformation.LinearTranslation;
        _rotationState = blockTransformation.ResultingRotationState;
    }

    private Dictionary<Coordinate, Coordinate> CalculateInitialRotation(RotationDirection rotationDirection)
    {
        Dictionary<Coordinate, Coordinate> oldToRotatedCoordinates = new Dictionary<Coordinate, Coordinate>();
        new List<Coordinate>(PiecesByCoordinate.Keys)
            .ForEach(coordinate => oldToRotatedCoordinates.Add(coordinate, coordinate.Rotated(_pivotPosition)));
        return oldToRotatedCoordinates;
    }

    private static Dictionary<Coordinate, Coordinate> ApplyTranslation(Vector2Int translation,
        Dictionary<Coordinate, Coordinate> originalRotationResult)
    {
        Dictionary<Coordinate, Coordinate> rotationWithTranslation = new Dictionary<Coordinate, Coordinate>();
        foreach (Coordinate nonRotatedCoordinate in originalRotationResult.Keys)
        {
            Coordinate nonTranslatedCoodinate = originalRotationResult[nonRotatedCoordinate];
            rotationWithTranslation.Add(nonRotatedCoordinate, nonTranslatedCoodinate.Shifted(translation.x, translation.y));
        }
        return rotationWithTranslation;
    }
}
