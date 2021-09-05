using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayAreaController : MonoBehaviour
{
    private Dictionary<Coordinate, GameCell> _cellsByCoordinate;
    private DimensionsHandler _dimensions;

    void Start()
    {
        _cellsByCoordinate = GetComponent<PlayAreaSetupper>().InitializeGameCells();
        _dimensions = GetComponent<DimensionsHandler>();
    }

    void Update()
    {

    }

    public void AddBlock(Block currentBlock)
    {
        new List<Coordinate>(currentBlock.PiecesByCoordinate.Keys)
            .ForEach(coordinate => _cellsByCoordinate[coordinate].BlockPiece = currentBlock.PiecesByCoordinate[coordinate]);
    }

    public void TryMove(Block currentBlock, int xShift, int yShift)
    {
        if (xShift != 0 && yShift != 0)
        {
            throw new InvalidOperationException("Cannot move diagonally!");
        }

        bool canShift = CanShift(currentBlock, xShift, yShift);

        if (IsDownwardsMovement(yShift) && !canShift)
        {
            PlaceBlock(currentBlock);
        }

        if (canShift)
        {
            ShiftBlock(currentBlock, xShift, yShift);
        }
    }

    private void ShiftBlock(Block currentBlock, int xShift, int yShift)
    {
        Dictionary<Coordinate, Coordinate> oldToShiftedCoordinateDict = currentBlock.CalculateShiftedCoordinates(xShift, yShift);
        currentBlock.Shift(oldToShiftedCoordinateDict);

        Dictionary<Coordinate, BlockPiece> piecesByNewCoordinate = new Dictionary<Coordinate, BlockPiece>();

        foreach (Coordinate oldCoordinate in oldToShiftedCoordinateDict.Keys)
        {
            GameCell gameCellAtOldCoordinate = _cellsByCoordinate[oldCoordinate];
            BlockPiece? blockPieceToMove = gameCellAtOldCoordinate.BlockPiece;

            if (blockPieceToMove == null) continue;

            gameCellAtOldCoordinate.BlockPiece = null;
            piecesByNewCoordinate.Add(oldToShiftedCoordinateDict[oldCoordinate], blockPieceToMove);
        }

        foreach (Coordinate newCoordinate in piecesByNewCoordinate.Keys)
        {
            _cellsByCoordinate[newCoordinate].BlockPiece = piecesByNewCoordinate[newCoordinate];
        }
    }

    private bool CoordinateWillStillContainBlockPiece(Coordinate oldCoordinate, Dictionary<Coordinate, Coordinate> oldToShiftedCoordinateDict)
    {
        return oldToShiftedCoordinateDict.Values.Contains(oldCoordinate);
    }

    private void PlaceBlock(Block currentBlock)
    {
        currentBlock.IsPlaced = true;
        // todo run line-completion logic
    }

    private bool CanShift(Block currentBlock, int xShift, int yShift)
    {
        List<Coordinate> shiftedCoordinates = new List<Coordinate>(currentBlock.CalculateShiftedCoordinates(xShift, yShift).Values);
        return AreWithinBounds(shiftedCoordinates) && CoordinatesAreOpen(currentBlock, shiftedCoordinates);
    }

    private bool AreWithinBounds(List<Coordinate> shiftedCoordinates)
    {
        return shiftedCoordinates.TrueForAll(coordinate => coordinate.X < _dimensions.NumberXCells
            && coordinate.X >= 0
            && coordinate.Y < _dimensions.NumberYCells
            && coordinate.Y >= 0);
    }

    private bool CoordinatesAreOpen(Block currentBlock, List<Coordinate> shiftedCoordinates)
    {
        return shiftedCoordinates.TrueForAll(coordinate =>
            _cellsByCoordinate[coordinate].IsEmpty() || currentBlock.PiecesByCoordinate.ContainsKey(coordinate));
    }

    private static bool IsDownwardsMovement(int yShift)
    {
        return yShift > 0;
    }

    private static List<Coordinate> CalculateShiftedCoordinates(List<Coordinate> coordinatesToShift, int xShift, int yShift)
    {
        return coordinatesToShift.Select(coordinate => coordinate.Shifted(xShift, yShift)).ToList();
    }
}
