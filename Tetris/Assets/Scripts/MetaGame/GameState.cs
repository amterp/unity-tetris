using System;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{

    public event Action GameStartedEvent;
    public event Action GameOverEvent;

    private bool _isGameInProgress = true;
    private bool _isGameOver;

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

    public bool IsGameInProgress()
    {
        return _isGameInProgress;
    }

    public bool IsGameOver()
    {
        return _isGameOver;
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
}
