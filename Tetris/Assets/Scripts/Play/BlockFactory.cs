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
        piecesByCoordinate.Add(_dimensions.CreateCoordinate(0, 0), topLeftPiece);

        BlockPiece topRightPiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        topRightPiece.Initialize(BlockType.Square);
        piecesByCoordinate.Add(_dimensions.CreateCoordinate(1, 0), topRightPiece);

        BlockPiece bottomLeftPiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        bottomLeftPiece.Initialize(BlockType.Square);
        piecesByCoordinate.Add(_dimensions.CreateCoordinate(0, 1), bottomLeftPiece);

        BlockPiece bottomRightPiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        bottomRightPiece.Initialize(BlockType.Square);
        piecesByCoordinate.Add(_dimensions.CreateCoordinate(1, 1), bottomRightPiece);

        return new Block(BlockType.Square, piecesByCoordinate);
    }

    public Block CreateLBlock()
    {
        Dictionary<Coordinate, BlockPiece> piecesByCoordinate = new Dictionary<Coordinate, BlockPiece>();

        BlockPiece leftPiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        leftPiece.Initialize(BlockType.L);
        piecesByCoordinate.Add(_dimensions.CreateCoordinate(0, 1), leftPiece);

        BlockPiece middlePiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        middlePiece.Initialize(BlockType.L);
        piecesByCoordinate.Add(_dimensions.CreateCoordinate(1, 1), middlePiece);

        BlockPiece bottomRightPiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        bottomRightPiece.Initialize(BlockType.L);
        piecesByCoordinate.Add(_dimensions.CreateCoordinate(2, 1), bottomRightPiece);

        BlockPiece rightTopPiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        rightTopPiece.Initialize(BlockType.L);
        piecesByCoordinate.Add(_dimensions.CreateCoordinate(2, 0), rightTopPiece);

        return new Block(BlockType.L, piecesByCoordinate);
    }
}
