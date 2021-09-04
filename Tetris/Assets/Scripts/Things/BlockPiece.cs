using System;
using System.Collections.Generic;
using UnityEngine;

public class BlockPiece : MonoBehaviour
{
    public Color LineBlockColor;
    public Color LBlockColor;
    public Color JBlockColor;
    public Color SquareBlockColor;
    public Color SBlockColor;
    public Color ZBlockColor;

    private BlockType? _blockType;
    private SpriteRenderer _spriteRenderer;

    public void Initialize(BlockType blockType, Vector3 cellScale)
    {
        if (_blockType != null) throw new InvalidOperationException("Can only init block pieces once!");

        _blockType = blockType;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        transform.localScale = cellScale;

        UpdateRender();
    }

    public void UpdateRender()
    {
        Color color;
        switch (_blockType)
        {
            case BlockType.Line:
                color = LineBlockColor;
                break;
            case BlockType.L:
                color = LBlockColor;
                break;
            case BlockType.J:
                color = JBlockColor;
                break;
            case BlockType.Square:
                color = SquareBlockColor;
                break;
            case BlockType.S:
                color = SBlockColor;
                break;
            case BlockType.Z:
                color = ZBlockColor;
                break;
            default:
                throw new InvalidOperationException("Unknown block type: " + _blockType);
        }
        _spriteRenderer.color = color;
    }
}
