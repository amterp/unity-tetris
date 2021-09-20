using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    private const float SCORE_SCALER = 50f;

    public float CurrentPoints { get; private set; }

    [SerializeField] private PlayAreaController _playAreaController;
    [SerializeField] private DifficultyController _difficultyController;
    [SerializeField] private TextMeshProUGUI _scoreText;

    private float _gameStartTimeSeconds;

    void Awake()
    {
        _playAreaController.RowsCompletedEvent += OnRowsCompleted;
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
        UpdatePoints(CurrentPoints + pointsToAdd);
    }

    private void UpdatePoints(float newPoints)
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
        return Mathf.Pow(gameTimeSeconds, _difficultyController.Difficulty + 1) / SCORE_SCALER;
    }
}
