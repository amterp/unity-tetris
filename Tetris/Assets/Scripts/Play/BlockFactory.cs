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

    public Block CreateLineBlock()
    {
        Dictionary<Coordinate, BlockPiece> piecesByCoordinate = new Dictionary<Coordinate, BlockPiece>();

        BlockPiece leftPiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        leftPiece.Initialize(BlockType.Line);
        piecesByCoordinate.Add(_dimensions.CreateCoordinate(0, 1), leftPiece);

        BlockPiece leftMiddlePiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        leftMiddlePiece.Initialize(BlockType.Line);
        piecesByCoordinate.Add(_dimensions.CreateCoordinate(1, 1), leftMiddlePiece);

        BlockPiece rightMiddlePiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        rightMiddlePiece.Initialize(BlockType.Line);
        piecesByCoordinate.Add(_dimensions.CreateCoordinate(2, 1), rightMiddlePiece);

        BlockPiece rightPiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        rightPiece.Initialize(BlockType.Line);
        piecesByCoordinate.Add(_dimensions.CreateCoordinate(3, 1), rightPiece);

        return new Block(BlockType.Line, piecesByCoordinate);
    }

    public Block CreateJBlock()
    {
        Dictionary<Coordinate, BlockPiece> piecesByCoordinate = new Dictionary<Coordinate, BlockPiece>();

        BlockPiece leftTopPiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        leftTopPiece.Initialize(BlockType.J);
        piecesByCoordinate.Add(_dimensions.CreateCoordinate(0, 0), leftTopPiece);

        BlockPiece leftBottomPiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        leftBottomPiece.Initialize(BlockType.J);
        piecesByCoordinate.Add(_dimensions.CreateCoordinate(0, 1), leftBottomPiece);

        BlockPiece middlePiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        middlePiece.Initialize(BlockType.J);
        piecesByCoordinate.Add(_dimensions.CreateCoordinate(1, 1), middlePiece);

        BlockPiece rightPiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        rightPiece.Initialize(BlockType.J);
        piecesByCoordinate.Add(_dimensions.CreateCoordinate(2, 1), rightPiece);

        return new Block(BlockType.J, piecesByCoordinate);
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

    public Block CreateSquareBlock()
    {
        Dictionary<Coordinate, BlockPiece> piecesByCoordinate = new Dictionary<Coordinate, BlockPiece>();

        BlockPiece topLeftPiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        topLeftPiece.Initialize(BlockType.Square);
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

    public Block CreateSBlock()
    {
        Dictionary<Coordinate, BlockPiece> piecesByCoordinate = new Dictionary<Coordinate, BlockPiece>();

        BlockPiece leftPiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        leftPiece.Initialize(BlockType.S);
        piecesByCoordinate.Add(_dimensions.CreateCoordinate(0, 1), leftPiece);

        BlockPiece middleBottomPiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        middleBottomPiece.Initialize(BlockType.S);
        piecesByCoordinate.Add(_dimensions.CreateCoordinate(1, 1), middleBottomPiece);

        BlockPiece middleTopPiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        middleTopPiece.Initialize(BlockType.S);
        piecesByCoordinate.Add(_dimensions.CreateCoordinate(1, 0), middleTopPiece);

        BlockPiece rightPiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        rightPiece.Initialize(BlockType.S);
        piecesByCoordinate.Add(_dimensions.CreateCoordinate(2, 0), rightPiece);

        return new Block(BlockType.S, piecesByCoordinate);
    }

    public Block CreateZBlock()
    {
        Dictionary<Coordinate, BlockPiece> piecesByCoordinate = new Dictionary<Coordinate, BlockPiece>();

        BlockPiece leftPiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        leftPiece.Initialize(BlockType.Z);
        piecesByCoordinate.Add(_dimensions.CreateCoordinate(0, 0), leftPiece);

        BlockPiece middleTopPiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        middleTopPiece.Initialize(BlockType.Z);
        piecesByCoordinate.Add(_dimensions.CreateCoordinate(1, 0), middleTopPiece);

        BlockPiece middleBottomPiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        middleBottomPiece.Initialize(BlockType.Z);
        piecesByCoordinate.Add(_dimensions.CreateCoordinate(1, 1), middleBottomPiece);

        BlockPiece rightPiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        rightPiece.Initialize(BlockType.Z);
        piecesByCoordinate.Add(_dimensions.CreateCoordinate(2, 1), rightPiece);

        return new Block(BlockType.Z, piecesByCoordinate);
    }

    public Block CreateTBlock()
    {
        Dictionary<Coordinate, BlockPiece> piecesByCoordinate = new Dictionary<Coordinate, BlockPiece>();

        BlockPiece leftPiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        leftPiece.Initialize(BlockType.T);
        piecesByCoordinate.Add(_dimensions.CreateCoordinate(0, 1), leftPiece);

        BlockPiece middleTopPiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        middleTopPiece.Initialize(BlockType.T);
        piecesByCoordinate.Add(_dimensions.CreateCoordinate(1, 0), middleTopPiece);

        BlockPiece middleBottomPiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        middleBottomPiece.Initialize(BlockType.T);
        piecesByCoordinate.Add(_dimensions.CreateCoordinate(1, 1), middleBottomPiece);

        BlockPiece rightPiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        rightPiece.Initialize(BlockType.T);
        piecesByCoordinate.Add(_dimensions.CreateCoordinate(2, 1), rightPiece);

        return new Block(BlockType.T, piecesByCoordinate);
    }
}
