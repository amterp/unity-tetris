using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreEntryController : MonoBehaviour
{

    [SerializeField] private TMP_InputField _newEntryNameInputField;
    [SerializeField] private ScoreController _scoreController;

    private GameState _gameState;
    private bool alreadyAddedCurrentScore = false;

    void Awake()
    {
        _gameState = GoUtil.FindGameState();
        _gameState.GameOverEvent += OnGameOver;
    }

    public void AddNewHighScore()
    {
        if (alreadyAddedCurrentScore) return;

        string playerName = _newEntryNameInputField.text.Trim();
        HighScoreInfo newHighScore = new HighScoreInfo(playerName, _scoreController.CurrentPoints, _gameState.Difficulty);
        _gameState.SaveScore(newHighScore);

        alreadyAddedCurrentScore = true;
        _newEntryNameInputField.DeactivateInputField();
    }

    private void OnGameOver()
    {
        alreadyAddedCurrentScore = false;
        _newEntryNameInputField.ActivateInputField();
    }
}
