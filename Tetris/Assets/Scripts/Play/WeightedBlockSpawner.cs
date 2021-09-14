using System;
using UnityEngine;

public class WeightedBlockSpawner : MonoBehaviour, IBlockSpawner
{
    public float LineBlockWeight = 1f;
    public float LBlockWeight = 1f;
    public float JBlockWeight = 1f;
    public float SquareBlockWeight = 1f;
    public float SBlockWeight = 1f;
    public float ZBlockWeight = 1f;
    public float TBlockWeight = 1f;

    private float _lineBlockCutoff;
    private float _lBlockCutoff;
    private float _jBlockCutoff;
    private float _squareBlockCutoff;
    private float _sBlockCutoff;
    private float _zBlockCutoff;
    private float _tBlockCutoff;

    private BlockFactory _blockFactory;

    void Awake()
    {
        _blockFactory = GetComponent<BlockFactory>();

        float weightsSum = LineBlockWeight + LBlockWeight + JBlockWeight + SquareBlockWeight + SBlockWeight + ZBlockWeight + TBlockWeight;

        _lineBlockCutoff = LineBlockWeight / weightsSum;
        _lBlockCutoff = _lineBlockCutoff + LBlockWeight / weightsSum;
        _jBlockCutoff = _lBlockCutoff + JBlockWeight / weightsSum;
        _squareBlockCutoff = _jBlockCutoff + SquareBlockWeight / weightsSum;
        _sBlockCutoff = _squareBlockCutoff + SBlockWeight / weightsSum;
        _zBlockCutoff = _sBlockCutoff + ZBlockWeight / weightsSum;
        _tBlockCutoff = _zBlockCutoff + TBlockWeight / weightsSum;
    }

    public Block GetNextBlock()
    {
        float randomValue = UnityEngine.Random.value;
        if (randomValue < _lineBlockCutoff)
        {
            return _blockFactory.CreateLineBlock();
        }
        if (randomValue < _lBlockCutoff)
        {
            return _blockFactory.CreateLBlock();
        }
        if (randomValue < _jBlockCutoff)
        {
            return _blockFactory.CreateJBlock();
        }
        if (randomValue < _squareBlockCutoff)
        {
            return _blockFactory.CreateSquareBlock();
        }
        if (randomValue < _sBlockCutoff)
        {
            return _blockFactory.CreateSBlock();
        }
        if (randomValue < _zBlockCutoff)
        {
            return _blockFactory.CreateZBlock();
        }
        if (randomValue <= _tBlockCutoff)
        {
            return _blockFactory.CreateTBlock();
        }
        throw new InvalidProgramException("Bug! Should not be possible to reach this.");
    }
}
