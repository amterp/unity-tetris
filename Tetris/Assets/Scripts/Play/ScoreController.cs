using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    private const float SCORE_SCALER = 50f;

    public double CurrentPoints { get; private set; }

    [SerializeField] private PlayAreaController _playAreaController;
    [SerializeField] private TextMeshProUGUI _scoreText;

    private GameState _gameState;
    private float _gameStartTimeSeconds;

    void Awake()
    {
        _playAreaController.RowsCompletedEvent += OnRowsCompleted;
        _gameState = GoUtil.FindGameState();
        _gameState.GameStartedEvent += OnGameStarted;
        _gameState.GameOverEvent += OnGameOver;
    }

    private void OnGameStarted()
    {
        _gameStartTimeSeconds = Time.time;
        UpdatePoints(0);
    }

    private void OnGameOver()
    {
        _gameState.SaveScore(new HighScoreInfo("Alex" + DateTimeOffset.Now.ToUnixTimeSeconds(), CurrentPoints, _gameState.Difficulty));
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
