using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityApplier : MonoBehaviour
{
    private const int DOWN = 1;

    public float CellDropsPerSecond;

    private BlockController _blockController;
    private PlayAreaController _playAreaController;
    private float _nextTimeForMovement;
    private float _dropIntervalSeconds;

    void Start()
    {
        _blockController = GetComponent<BlockController>();
        _playAreaController = GetComponent<PlayAreaController>();
        _playAreaController.BlockTransformationEvent += OnBlockShifted;
        _playAreaController.BlockPlacedEvent += OnBlockPlaced;

        _dropIntervalSeconds = 1 / CellDropsPerSecond;

        ResetTimer();
    }

    void Update()
    {
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
        _nextTimeForMovement = Time.time + _dropIntervalSeconds;
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
