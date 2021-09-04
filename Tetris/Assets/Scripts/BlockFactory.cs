using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockFactory : MonoBehaviour
{

    public GameObject BlockPiecePrefab;

    private DimensionsHandler _dimensions;

    void Start()
    {
        _dimensions = GetComponent<DimensionsHandler>();
    }

    public Block CreateSquareBlock()
    {
        Dictionary<Coordinate, BlockPiece> piecesByCoordinate = new Dictionary<Coordinate, BlockPiece>();

        BlockPiece topLeftPiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        topLeftPiece.Initialize(BlockType.Square, _dimensions.GetCellScale());
        piecesByCoordinate.Add(_dimensions.CreateCoordinate(0, 0, topLeftPiece.transform), topLeftPiece);

        BlockPiece topRightPiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        topRightPiece.Initialize(BlockType.Square, _dimensions.GetCellScale());
        piecesByCoordinate.Add(_dimensions.CreateCoordinate(1, 0, topRightPiece.transform), topRightPiece);

        BlockPiece bottomLeftPiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        bottomLeftPiece.Initialize(BlockType.Square, _dimensions.GetCellScale());
        piecesByCoordinate.Add(_dimensions.CreateCoordinate(0, 1, bottomLeftPiece.transform), bottomLeftPiece);

        BlockPiece bottomRightPiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        bottomRightPiece.Initialize(BlockType.Square, _dimensions.GetCellScale());
        piecesByCoordinate.Add(_dimensions.CreateCoordinate(1, 1, bottomRightPiece.transform), bottomRightPiece);

        return new Block(BlockType.Square, piecesByCoordinate);
    }

    public Block CreateLBlock()
    {
        Dictionary<Coordinate, BlockPiece> piecesByCoordinate = new Dictionary<Coordinate, BlockPiece>();

        BlockPiece topPiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        topPiece.Initialize(BlockType.L, _dimensions.GetCellScale());
        piecesByCoordinate.Add(_dimensions.CreateCoordinate(0, 0, topPiece.transform), topPiece);

        BlockPiece middlePiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        middlePiece.Initialize(BlockType.L, _dimensions.GetCellScale());
        piecesByCoordinate.Add(_dimensions.CreateCoordinate(0, 1, middlePiece.transform), middlePiece);

        BlockPiece bottomPiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        bottomPiece.Initialize(BlockType.L, _dimensions.GetCellScale());
        piecesByCoordinate.Add(_dimensions.CreateCoordinate(0, 2, bottomPiece.transform), bottomPiece);

        BlockPiece bottomRightPiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        bottomRightPiece.Initialize(BlockType.L, _dimensions.GetCellScale());
        piecesByCoordinate.Add(_dimensions.CreateCoordinate(1, 2, bottomRightPiece.transform), bottomRightPiece);

        return new Block(BlockType.L, piecesByCoordinate);
    }
}
