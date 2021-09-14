using System;
using System.Collections.Generic;
using UnityEngine;

public class BlockFactory : MonoBehaviour
{

    public GameObject BlockPiecePrefab;

    private DimensionsHandler _dimensions;
    private int _line_block_x_center_offset;
    private int _j_block_x_center_offset;
    private int _l_block_x_center_offset;
    private int _square_block_x_center_offset;
    private int _s_block_x_center_offset;
    private int _z_block_x_center_offset;
    private int _t_block_x_center_offset;

    void Awake()
    {
        _dimensions = GetComponent<DimensionsHandler>();

        _line_block_x_center_offset = CalculateCenterOffset(BlockType.L);
        _j_block_x_center_offset = CalculateCenterOffset(BlockType.J);
        _l_block_x_center_offset = CalculateCenterOffset(BlockType.L);
        _square_block_x_center_offset = CalculateCenterOffset(BlockType.Square);
        _s_block_x_center_offset = CalculateCenterOffset(BlockType.S);
        _z_block_x_center_offset = CalculateCenterOffset(BlockType.Z);
        _t_block_x_center_offset = CalculateCenterOffset(BlockType.T);
    }

    public Block CreateLineBlock()
    {
        Dictionary<Coordinate, BlockPiece> piecesByCoordinate = new Dictionary<Coordinate, BlockPiece>();

        BlockPiece leftPiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        leftPiece.Initialize(BlockType.Line);
        piecesByCoordinate.Add(CreateCenteredCoordinate(0, 1, _line_block_x_center_offset), leftPiece);

        BlockPiece leftMiddlePiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        leftMiddlePiece.Initialize(BlockType.Line);
        piecesByCoordinate.Add(CreateCenteredCoordinate(1, 1, _line_block_x_center_offset), leftMiddlePiece);

        BlockPiece rightMiddlePiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        rightMiddlePiece.Initialize(BlockType.Line);
        piecesByCoordinate.Add(CreateCenteredCoordinate(2, 1, _line_block_x_center_offset), rightMiddlePiece);

        BlockPiece rightPiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        rightPiece.Initialize(BlockType.Line);
        piecesByCoordinate.Add(CreateCenteredCoordinate(3, 1, _line_block_x_center_offset), rightPiece);

        return new Block(BlockType.Line, piecesByCoordinate, new Vector2(_line_block_x_center_offset, 0));
    }

    public Block CreateJBlock()
    {
        Dictionary<Coordinate, BlockPiece> piecesByCoordinate = new Dictionary<Coordinate, BlockPiece>();

        BlockPiece leftTopPiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        leftTopPiece.Initialize(BlockType.J);
        piecesByCoordinate.Add(CreateCenteredCoordinate(0, 0, _j_block_x_center_offset), leftTopPiece);

        BlockPiece leftBottomPiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        leftBottomPiece.Initialize(BlockType.J);
        piecesByCoordinate.Add(CreateCenteredCoordinate(0, 1, _j_block_x_center_offset), leftBottomPiece);

        BlockPiece middlePiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        middlePiece.Initialize(BlockType.J);
        piecesByCoordinate.Add(CreateCenteredCoordinate(1, 1, _j_block_x_center_offset), middlePiece);

        BlockPiece rightPiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        rightPiece.Initialize(BlockType.J);
        piecesByCoordinate.Add(CreateCenteredCoordinate(2, 1, _j_block_x_center_offset), rightPiece);

        return new Block(BlockType.J, piecesByCoordinate, new Vector2(_j_block_x_center_offset, 0));
    }

    public Block CreateLBlock()
    {
        Dictionary<Coordinate, BlockPiece> piecesByCoordinate = new Dictionary<Coordinate, BlockPiece>();

        BlockPiece leftPiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        leftPiece.Initialize(BlockType.L);
        piecesByCoordinate.Add(CreateCenteredCoordinate(0, 1, _l_block_x_center_offset), leftPiece);

        BlockPiece middlePiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        middlePiece.Initialize(BlockType.L);
        piecesByCoordinate.Add(CreateCenteredCoordinate(1, 1, _l_block_x_center_offset), middlePiece);

        BlockPiece bottomRightPiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        bottomRightPiece.Initialize(BlockType.L);
        piecesByCoordinate.Add(CreateCenteredCoordinate(2, 1, _l_block_x_center_offset), bottomRightPiece);

        BlockPiece rightTopPiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        rightTopPiece.Initialize(BlockType.L);
        piecesByCoordinate.Add(CreateCenteredCoordinate(2, 0, _l_block_x_center_offset), rightTopPiece);

        return new Block(BlockType.L, piecesByCoordinate, new Vector2(_l_block_x_center_offset, 0));
    }

    public Block CreateSquareBlock()
    {
        Dictionary<Coordinate, BlockPiece> piecesByCoordinate = new Dictionary<Coordinate, BlockPiece>();

        BlockPiece topLeftPiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        topLeftPiece.Initialize(BlockType.Square);
        piecesByCoordinate.Add(CreateCenteredCoordinate(0, 0, _square_block_x_center_offset), topLeftPiece);

        BlockPiece topRightPiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        topRightPiece.Initialize(BlockType.Square);
        piecesByCoordinate.Add(CreateCenteredCoordinate(1, 0, _square_block_x_center_offset), topRightPiece);

        BlockPiece bottomLeftPiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        bottomLeftPiece.Initialize(BlockType.Square);
        piecesByCoordinate.Add(CreateCenteredCoordinate(0, 1, _square_block_x_center_offset), bottomLeftPiece);

        BlockPiece bottomRightPiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        bottomRightPiece.Initialize(BlockType.Square);
        piecesByCoordinate.Add(CreateCenteredCoordinate(1, 1, _square_block_x_center_offset), bottomRightPiece);

        return new Block(BlockType.Square, piecesByCoordinate, new Vector2(_square_block_x_center_offset, 0));
    }

    public Block CreateSBlock()
    {
        Dictionary<Coordinate, BlockPiece> piecesByCoordinate = new Dictionary<Coordinate, BlockPiece>();

        BlockPiece leftPiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        leftPiece.Initialize(BlockType.S);
        piecesByCoordinate.Add(CreateCenteredCoordinate(0, 1, _s_block_x_center_offset), leftPiece);

        BlockPiece middleBottomPiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        middleBottomPiece.Initialize(BlockType.S);
        piecesByCoordinate.Add(CreateCenteredCoordinate(1, 1, _s_block_x_center_offset), middleBottomPiece);

        BlockPiece middleTopPiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        middleTopPiece.Initialize(BlockType.S);
        piecesByCoordinate.Add(CreateCenteredCoordinate(1, 0, _s_block_x_center_offset), middleTopPiece);

        BlockPiece rightPiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        rightPiece.Initialize(BlockType.S);
        piecesByCoordinate.Add(CreateCenteredCoordinate(2, 0, _s_block_x_center_offset), rightPiece);

        return new Block(BlockType.S, piecesByCoordinate, new Vector2(_s_block_x_center_offset, 0));
    }

    public Block CreateZBlock()
    {
        Dictionary<Coordinate, BlockPiece> piecesByCoordinate = new Dictionary<Coordinate, BlockPiece>();

        BlockPiece leftPiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        leftPiece.Initialize(BlockType.Z);
        piecesByCoordinate.Add(CreateCenteredCoordinate(0, 0, _z_block_x_center_offset), leftPiece);

        BlockPiece middleTopPiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        middleTopPiece.Initialize(BlockType.Z);
        piecesByCoordinate.Add(CreateCenteredCoordinate(1, 0, _z_block_x_center_offset), middleTopPiece);

        BlockPiece middleBottomPiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        middleBottomPiece.Initialize(BlockType.Z);
        piecesByCoordinate.Add(CreateCenteredCoordinate(1, 1, _z_block_x_center_offset), middleBottomPiece);

        BlockPiece rightPiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        rightPiece.Initialize(BlockType.Z);
        piecesByCoordinate.Add(CreateCenteredCoordinate(2, 1, _z_block_x_center_offset), rightPiece);

        return new Block(BlockType.Z, piecesByCoordinate, new Vector2(_z_block_x_center_offset, 0));
    }

    public Block CreateTBlock()
    {
        Dictionary<Coordinate, BlockPiece> piecesByCoordinate = new Dictionary<Coordinate, BlockPiece>();

        BlockPiece leftPiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        leftPiece.Initialize(BlockType.T);
        piecesByCoordinate.Add(CreateCenteredCoordinate(0, 1, _t_block_x_center_offset), leftPiece);

        BlockPiece middleTopPiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        middleTopPiece.Initialize(BlockType.T);
        piecesByCoordinate.Add(CreateCenteredCoordinate(1, 0, _t_block_x_center_offset), middleTopPiece);

        BlockPiece middleBottomPiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        middleBottomPiece.Initialize(BlockType.T);
        piecesByCoordinate.Add(CreateCenteredCoordinate(1, 1, _t_block_x_center_offset), middleBottomPiece);

        BlockPiece rightPiece = GameObject.Instantiate<GameObject>(BlockPiecePrefab).GetComponent<BlockPiece>();
        rightPiece.Initialize(BlockType.T);
        piecesByCoordinate.Add(CreateCenteredCoordinate(2, 1, _t_block_x_center_offset), rightPiece);

        return new Block(BlockType.T, piecesByCoordinate, new Vector2(_t_block_x_center_offset, 0));
    }

    private Coordinate CreateCenteredCoordinate(int xPosFromLeft, int yPosFromTop, int centerXOffset)
    {
        return _dimensions.CreateCoordinate(xPosFromLeft + centerXOffset, yPosFromTop);
    }

    private int CalculateCenterOffset(BlockType blockType)
    {

        int halfwayXCoordinate = _dimensions.NumberXCells / 2;
        return halfwayXCoordinate - (int)Math.Ceiling(blockType.BoundingBoxDimensions().x / 2f);
    }
}
