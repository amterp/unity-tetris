using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[RequireComponent(typeof(Hidee))]
public class GameOverTextController : MonoBehaviour
{
    private Hidee _hidee;
    [SerializeField] private ScoreController _scoreController;
    [SerializeField] private TextMeshProUGUI _gameOverText;

    void Awake()
    {
        _hidee = GetComponent<Hidee>();
        GameState gameState = GoUtil.FindGameState();
        gameState.GameStartedEvent += () => _hidee.Hide();
        gameState.GameOverEvent += OnGameOver;
    }

    private void OnGameOver()
    {
        SetText();
        _hidee.Unhide();
    }

    private void SetText()
    {
        if (_scoreController.CurrentPoints > 1000)
        {
            _gameOverText.text = "Well done! That's a lotta points!";
        }
        else
        {
            _gameOverText.text = "Hmm, better luck next time!";
        }
    }
}
