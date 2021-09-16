using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{

    public GameObject BlockSpawnerContainer;

    private IBlockSpawner _blockSpawner;
    private PlayAreaController _playAreaController;
    private Block? _currentBlock;
    private GameState _gameState;

    void Awake()
    {
        _blockSpawner = BlockSpawnerContainer.GetComponent<IBlockSpawner>();

        _playAreaController = GetComponent<PlayAreaController>();
        _playAreaController.BlockPlacedEvent += OnBlockPlaced;

        _gameState = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameState>();
        _gameState.GameStartedEvent += OnGameStarted;
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

    private void OnGameStarted()
    {
        _currentBlock = null;
        SpawnBlockIfNone();
    }

    private void OnBlockPlaced()
    {
        SpawnNewBlockIfGameInProgress();
    }

    private void SpawnBlockIfNone()
    {
        if (_currentBlock != null) return;
        SpawnNewBlockIfGameInProgress();
    }

    private void SpawnNewBlockIfGameInProgress()
    {
        if (!_gameState.IsGameInProgress()) return;
        _currentBlock = _blockSpawner.GetNextBlock();
        _playAreaController.AddBlock(_currentBlock);
    }
}
