using System;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{

    public event Action GameStartedEvent;
    public event Action GameOverEvent;
    public event Action<bool> GamePausedEvent;

    private bool _isGameInProgress = true;
    private bool _isGameOver;
    private bool _isGamePaused = false;

    public void SetGameStarted()
    {
        OnGameStarted();
        EventUtil.SafeInvoke(GameStartedEvent);
    }

    public void SetGameOver()
    {
        OnGameOver();
        EventUtil.SafeInvoke(GameOverEvent);
    }

    public void ToggleGamePaused()
    {
        OnGamePaused();
        EventUtil.SafeInvoke(GamePausedEvent, _isGamePaused);
    }

    public bool IsGameInProgress()
    {
        return _isGameInProgress;
    }

    public bool IsGameOver()
    {
        return _isGameOver;
    }

    public bool IsPaused()
    {
        return _isGamePaused;
    }

    private void OnGameStarted()
    {
        _isGameInProgress = true;
        _isGameOver = false;
        Debug.Log("Game started.");
    }

    private void OnGameOver()
    {
        _isGameInProgress = false;
        _isGameOver = true;
        Debug.Log("Game over.");
    }

    private void OnGamePaused()
    {
        _isGamePaused = !_isGamePaused;
        Debug.Log(_isGamePaused ? "Game paused." : "Game unpaused");
    }
}
