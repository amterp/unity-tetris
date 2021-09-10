using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCell : MonoBehaviour
{

    public Color SpawnPillowRoomCellColor;

    public Coordinate Coordinate { get; private set; }
    public BlockPiece? BlockPiece { get { return _blockPiece; } set { _blockPiece = value; UpdateRender(); } }

    private BlockPiece? _blockPiece;
    private SpriteRenderer _spriteRenderer;
    private Color _defaultEmptyColor;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultEmptyColor = _spriteRenderer.color;
    }

    public void InitCoordinate(Coordinate coordinate)
    {
        if (Coordinate != null) throw new InvalidOperationException("Can only add a coordinate once!");
        Coordinate = coordinate;
        UpdateRender();
    }

    public bool IsEmpty()
    {
        return BlockPiece == null;
    }

    private void UpdateRender()
    {
        if (BlockPiece != null)
        {
            _spriteRenderer.color = BlockPiece.GetColor();
        }
        else if (IsSpawnPillowRoomCell())
        {
            _spriteRenderer.color = SpawnPillowRoomCellColor;
        }
        else if (BlockPiece == null)
        {
            _spriteRenderer.color = _defaultEmptyColor;
        }
    }

    private bool IsSpawnPillowRoomCell()
    {
        return Coordinate.Y < 0;
    }
}
