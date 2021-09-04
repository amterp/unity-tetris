using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block
{
    private BlockType _blockType;
    private Dictionary<Coordinate, BlockPiece> _piecesByCoordinate;

    public Block(BlockType blockType, Dictionary<Coordinate, BlockPiece> piecesByCoordinate)
    {
        _blockType = blockType;
        _piecesByCoordinate = piecesByCoordinate;
    }
}
