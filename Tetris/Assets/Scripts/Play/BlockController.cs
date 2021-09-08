using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{

    private IBlockSpawner _blockSpawner;
    private PlayAreaController _playAreaController;
    private Block? _currentBlock;

    void Start()
    {
        _blockSpawner = GetComponent<IBlockSpawner>();
        _playAreaController = GetComponent<PlayAreaController>();
        _playAreaController.BlockPlacedEvent += OnBlockPlaced;
        SpawnBlockIfNone();
    }

    public void TryMove(int xShift, int yShift)
    {
        _playAreaController.TryMove(_currentBlock, xShift, yShift);
    }

    public void TryRotate(RotationDirection rotationDirection)
    {
        _playAreaController.TryRotate(_currentBlock, rotationDirection);
    }

    public void InstantPlace()
    {
        _playAreaController.InstantPlace(_currentBlock);
    }

    private void OnBlockPlaced()
    {
        SpawnNewBlock();
    }

    private void SpawnBlockIfNone()
    {
        if (_currentBlock != null) return;
        SpawnNewBlock();
    }

    private void SpawnNewBlock()
    {
        _currentBlock = _blockSpawner.GetNextBlock();
        _playAreaController.AddBlock(_currentBlock);
    }
}
