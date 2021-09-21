using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateProxy : MonoBehaviour, IGameState
{

    private GameState _gameState;

    void Awake()
    {
        _gameState = GoUtil.FindGameState();
    }

    public void SetGameStarted()
    {
        _gameState.SetGameStarted();
    }

    public void SetGameOver()
    {
        _gameState.SetGameOver();
    }

    public void ToggleGamePaused()
    {
        _gameState.ToggleGamePaused();
    }

    public bool IsGameInProgress()
    {
        return _gameState.IsGameInProgress();
    }

    public bool IsGameOver()
    {
        return _gameState.IsGameOver();
    }

    public bool IsPaused()
    {
        return _gameState.IsPaused();
    }
}
