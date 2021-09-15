using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityApplier : MonoBehaviour
{
    private const int DOWN = 1;

    private BlockController _blockController;
    private PlayAreaController _playAreaController;
    private float _nextTimeForMovement;
    private GameState _gameState;
    private DifficultyController _difficultyController;

    void Awake()
    {
        _blockController = GetComponent<BlockController>();
        _playAreaController = GetComponent<PlayAreaController>();
        _playAreaController.BlockTransformationEvent += OnBlockShifted;
        _playAreaController.BlockPlacedEvent += OnBlockPlaced;
        _gameState = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameState>();
        _difficultyController = GetComponent<DifficultyController>();

        ResetTimer();
    }

    void Update()
    {
        if (!_gameState.IsGameInProgress()) return;
        MoveIfTimerIsUp();
    }

    private void MoveIfTimerIsUp()
    {
        if (Time.time >= _nextTimeForMovement)
        {
            _blockController.TryMove(0, DOWN);
            ResetTimer();
        }
    }

    private void ResetTimer()
    {
        _nextTimeForMovement = Time.time + _difficultyController.GetBlockGravityDropIntervalSeconds();
    }

    private void OnBlockShifted(BlockTransformation blockTransformation)
    {
        if (DownShiftNotDueToWallKick(blockTransformation))
        {
            ResetTimer();
        }
    }

    private void OnBlockPlaced()
    {
        ResetTimer();
    }

    private static bool DownShiftNotDueToWallKick(BlockTransformation blockTransformation)
    {
        return blockTransformation.RotationDirection == null
            && blockTransformation.ShiftDirection != null
            && blockTransformation.ShiftDirection == ShiftDirection.Down;
    }
}
