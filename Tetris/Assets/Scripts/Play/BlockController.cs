using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IBlockSpawner))]
public class BlockController : MonoBehaviour
{

    public GameObject BlockSpawnerContainer;
    public GameObject BlockStashContainer;

    private IBlockSpawner _blockSpawner;
    private BlockStashController _blockStashController;
    private PlayAreaController _playAreaController;
    private GameState _gameState;
    private GameConstants _gameConstants;
    private Block? _currentBlock;
    private bool _isStashingAvailable;
    private bool _isAwaitingRowCompletion;

    void Awake()
    {
        _blockSpawner = BlockSpawnerContainer.GetComponent<IBlockSpawner>();

        _playAreaController = GetComponent<PlayAreaController>();
        _playAreaController.BlockPlacedEvent += OnBlockPlaced;

        _blockStashController = BlockStashContainer.GetComponent<BlockStashController>();

        _gameState = GoUtil.FindGameState();
        _gameState.GameStartedEvent += OnGameStarted;
        _gameState.RowsCompletedEvent += (ignored) => StartCoroutine(AwaitRowCompletion());

        _gameConstants = GoUtil.FindGameConstants();

        _isAwaitingRowCompletion = false;
    }

    public void TryMove(int xShift, int yShift)
    {
        _playAreaController.TryMove(_currentBlock, xShift, yShift);
    }

    public void TryRotate(RotationDirection rotationDirection)
    {
        _playAreaController.TryRotate(_currentBlock, rotationDirection);
    }

    public void TryBlockStashSwap()
    {
        if (!_isStashingAvailable || _currentBlock == null) return;

        _playAreaController.RemoveBlock(_currentBlock);
        _currentBlock = _blockStashController.SwapBlock(_currentBlock);

        if (_currentBlock == null)
        {
            SpawnBlockIfNone();
        }
        else
        {
            _playAreaController.AddBlock(_currentBlock);
        }

        _isStashingAvailable = false;
    }

    public void InstantPlace()
    {
        _playAreaController.InstantPlace(_currentBlock);
    }

    private void OnGameStarted()
    {
        _currentBlock = null;
        _isStashingAvailable = true;
        SpawnBlockIfNone();
    }

    private void OnBlockPlaced()
    {
        _isStashingAvailable = true;
        SpawnNewBlockIfGameInProgress();
    }

    private IEnumerator AwaitRowCompletion()
    {
        _isAwaitingRowCompletion = true;
        yield return new WaitForSeconds(_gameConstants.LineCompletionPauseSeconds);
        _isAwaitingRowCompletion = false;
        SpawnNewBlockIfGameInProgress();
    }

    private void SpawnBlockIfNone()
    {
        if (_currentBlock != null) return;
        SpawnNewBlockIfGameInProgress();
    }

    private void SpawnNewBlockIfGameInProgress()
    {
        if (!_gameState.IsGameInProgress() || _isAwaitingRowCompletion) return;
        _currentBlock = _blockSpawner.GetNextBlock();
        _playAreaController.AddBlock(_currentBlock);
    }
}
