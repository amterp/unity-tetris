using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{

    private IBlockSpawner _blockSpawner;
    private PlayAreaController _playAreaController;
    private Block? _currentBlock;

    // Start is called before the first frame update
    void Start()
    {
        _blockSpawner = GetComponent<IBlockSpawner>();
        _playAreaController = GetComponent<PlayAreaController>();
    }

    void Update()
    {
        SpawnBlockIfNone();
    }

    public void TryMove(int xShift, int yShift)
    {
        SpawnBlockIfNone();
        _playAreaController.TryMove(_currentBlock, xShift, yShift);

        if (_currentBlock.IsPlaced)
        {
            SpawnNewBlock();
        }
    }

    public void TryRotate(RotationDirection rotationDirection)
    {
        SpawnBlockIfNone();
        _playAreaController.TryRotate(_currentBlock, rotationDirection);
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
