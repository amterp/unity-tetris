using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Block
{
    public Dictionary<Coordinate, BlockPiece> PiecesByCoordinate;
    public bool IsPlaced;

    private BlockType _blockType;

    public Block(BlockType blockType, Dictionary<Coordinate, BlockPiece> piecesByCoordinate)
    {
        _blockType = blockType;
        PiecesByCoordinate = piecesByCoordinate;
    }

    /** Returns a Dictionary of <oldCoordinate, shiftedCoordinate>. */
    public Dictionary<Coordinate, Coordinate> CalculateShiftedCoordinates(int xShift, int yShift)
    {
        Dictionary<Coordinate, Coordinate> oldToNewCoordinateDict = new Dictionary<Coordinate, Coordinate>();
        foreach (Coordinate currentCoordinate in PiecesByCoordinate.Keys)
        {
            oldToNewCoordinateDict.Add(currentCoordinate, currentCoordinate.Shifted(xShift, yShift));
        }
        return oldToNewCoordinateDict;
    }

    public void Shift(Dictionary<Coordinate, Coordinate> oldToNewCoordinatesDict)
    {
        if (IsPlaced) throw new InvalidOperationException("Cannot shift placed blocks!");

        Dictionary<Coordinate, BlockPiece> newPiecesByCoordinateDict = new Dictionary<Coordinate, BlockPiece>();
        foreach (Coordinate currentCoordinate in PiecesByCoordinate.Keys)
        {
            Coordinate newCoordinate = oldToNewCoordinatesDict[currentCoordinate];
            newPiecesByCoordinateDict.Add(newCoordinate, PiecesByCoordinate[currentCoordinate]);
        }
        PiecesByCoordinate = newPiecesByCoordinateDict;
    }

    public List<Coordinate> GetCoordinatesCopy()
    {
        return new List<Coordinate>(PiecesByCoordinate.Keys);
    }
}
