using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyController : MonoBehaviour
{
    [Range(0, 1f)]
    public float Difficulty = 0.5f;
    [Range(0, 0.08f)]
    public float SpeedRampupFactor = 0.03f;

    private float _gameStartTimeSeconds;

    void Awake()
    {
        GameState gameState = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameState>();
        gameState.GameStartedEvent += OnGameStarted;
    }

    /** https://www.desmos.com/calculator/ruv4i8ceb4 */
    public float GetBlockGravityDropIntervalSeconds()
    {
        float currentGameDurationSeconds = Time.time - _gameStartTimeSeconds;
        return Math.Min(1, 1 / (SpeedRampupFactor * currentGameDurationSeconds + Difficulty + 1));
    }

    private void OnGameStarted()
    {
        _gameStartTimeSeconds = Time.time;
    }
}
