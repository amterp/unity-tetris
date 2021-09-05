using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCell : MonoBehaviour
{

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

    void Update()
    {
    }

    public void InitCoordinate(Coordinate coordinate)
    {
        if (Coordinate != null) throw new InvalidOperationException("Can only add a coordinate once!");
        Coordinate = coordinate;
    }

    public bool IsEmpty()
    {
        return BlockPiece == null;
    }

    private void UpdateRender()
    {
        if (BlockPiece == null)
        {
            _spriteRenderer.color = _defaultEmptyColor;
        }
        else
        {
            _spriteRenderer.color = BlockPiece.GetColor();
        }
    }
}
