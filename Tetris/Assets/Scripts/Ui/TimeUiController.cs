using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TimeUiController : MonoBehaviour
{

    public TextMeshProUGUI TimeText;

    private float _gameStartTimeSeconds;
    private GameState _gameState;

    // Start is called before the first frame update
    void Awake()
    {
        _gameState = GoUtil.FindGameState();
        _gameState.GameStartedEvent += OnGameStarted;
    }

    void Update()
    {
        UpdateTimeText();
    }

    private void UpdateTimeText()
    {
        if (!_gameState.IsGameInProgress()) return;
        TimeSpan elapsedTime = TimeSpan.FromSeconds(Time.time - _gameStartTimeSeconds);
        TimeText.text = string.Format("{0:D1}:{1:D2}", elapsedTime.Minutes, elapsedTime.Seconds);
    }

    private void OnGameStarted()
    {
        _gameStartTimeSeconds = Time.time;
    }
}
