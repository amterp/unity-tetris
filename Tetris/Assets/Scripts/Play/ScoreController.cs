using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    private const float SCORE_SCALER = 50f;

    public PlayAreaController PlayAreaController;
    public DifficultyController DifficultyController;
    public TextMeshProUGUI ScoreText;

    private float _gameStartTimeSeconds;
    private float _currentPoints;

    void Awake()
    {
        PlayAreaController.RowsCompletedEvent += OnRowsCompleted;
        GameState gameState = GoUtil.FindGameState();
        gameState.GameStartedEvent += OnGameStarted;
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

    private void AddPoints(float pointsToAdd)
    {
        UpdatePoints(_currentPoints + pointsToAdd);
    }

    private void UpdatePoints(float newPoints)
    {
        _currentPoints = newPoints;
        ScoreText.text = _currentPoints.ToString("N0");
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
        return Mathf.Pow(gameTimeSeconds, DifficultyController.Difficulty + 1) / SCORE_SCALER;
    }
}
