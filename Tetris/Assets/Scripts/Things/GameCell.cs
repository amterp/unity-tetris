using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCell : MonoBehaviour
{

    public Color SpawnPillowRoomCellColor;
    public float GhostColorStrength = 0.3f;

    public Coordinate Coordinate { get; private set; }
    public BlockPiece? BlockPiece { get { return _realBlockPiece; } set { _realBlockPiece = value; UpdateRender(); } }
    public BlockPiece? GhostBlockPiece { get { return _ghostBlockPiece; } set { _ghostBlockPiece = value; UpdateRender(); } }

    private BlockPiece? _realBlockPiece;
    private BlockPiece? _ghostBlockPiece;
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

    public void Reset()
    {
        BlockPiece = null;
        GhostBlockPiece = null;
    }

    public bool IsEmpty()
    {
        return BlockPiece == null;
    }

    private void UpdateRender()
    {
        UpdateBaseColor();
        AddGhostColorIfApplicable();
    }

    private void UpdateBaseColor()
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

    private void AddGhostColorIfApplicable()
    {
        if (_ghostBlockPiece != null)
        {
            _spriteRenderer.color = Color.Lerp(_spriteRenderer.color, _ghostBlockPiece.GetColor(), GhostColorStrength);
        }
    }

    private bool IsSpawnPillowRoomCell()
    {
        return Coordinate.Y < 0;
    }
}
