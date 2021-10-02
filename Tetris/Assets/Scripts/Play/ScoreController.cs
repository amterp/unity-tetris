using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    private const float SCORE_SCALER = 50f;

    public double CurrentPoints { get; private set; }

    [SerializeField] private TextMeshProUGUI _scoreText;

    private GameState _gameState;
    private float _gameStartTimeSeconds;

    void Awake()
    {
        _gameState = GoUtil.FindGameState();
        _gameState.RowsCompletedEvent += OnRowsCompleted;
        _gameState.GameStartedEvent += OnGameStarted;
    }

    private void OnGameStarted()
    {
        _gameStartTimeSeconds = Time.time;
        UpdatePoints(0);
    }

    private void OnRowsCompleted(int numRowsCompleted)
    {
        ScoreRowCompletions(numRowsCompleted);
    }

    private void AddPoints(double pointsToAdd)
    {
        UpdatePoints(CurrentPoints + pointsToAdd);
    }

    private void UpdatePoints(double newPoints)
    {
        CurrentPoints = newPoints;
        _scoreText.text = CurrentPoints.ToString("N0");
    }

    /** https://www.desmos.com/calculator/umddim59ml */
    private void ScoreRowCompletions(int numRowsCompleted)
    {
        float timeScoreMultiplier = CalculateTimeScoreMultiplier();
        AddPoints(Mathf.Pow(2, numRowsCompleted) * timeScoreMultiplier * 100);
    }

    private float CalculateTimeScoreMultiplier()
    {
        float gameTimeSeconds = Time.time - _gameStartTimeSeconds;
        return Mathf.Pow(gameTimeSeconds, _gameState.Difficulty + 1) / SCORE_SCALER;
    }
}
