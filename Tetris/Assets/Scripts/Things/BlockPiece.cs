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
    public Color TBlockColor;

    private BlockType? _blockType;

    public void Initialize(BlockType blockType)
    {
        if (_blockType != null) throw new InvalidOperationException("Can only init block pieces once!");

        _blockType = blockType;
    }

    public Color GetColor()
    {
        switch (_blockType)
        {
            case BlockType.Line:
                return LineBlockColor;
            case BlockType.L:
                return LBlockColor;
            case BlockType.J:
                return JBlockColor;
            case BlockType.Square:
                return SquareBlockColor;
            case BlockType.S:
                return SBlockColor;
            case BlockType.Z:
                return ZBlockColor;
            case BlockType.T:
                return TBlockColor;
            default:
                throw new InvalidOperationException("Unknown block type: " + _blockType);
        }
    }
}
