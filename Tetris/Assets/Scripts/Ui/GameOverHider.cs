using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Hidee))]
public class GameOverHider : MonoBehaviour
{

    [SerializeField] private bool _showOnGameOver;
    private Hidee _hidee;

    void Awake()
    {
        _hidee = GetComponent<Hidee>();
        GameState _gameState = GoUtil.FindGameState();
        _gameState.GameStartedEvent += OnGameStarted;
        _gameState.GameOverEvent += OnGameOver;
    }

    private void OnGameStarted()
    {
        if (_showOnGameOver) _hidee.Hide(); else _hidee.Unhide();
    }

    private void OnGameOver()
    {
        if (_showOnGameOver) _hidee.Unhide(); else _hidee.Hide();
    }
}
