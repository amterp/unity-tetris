using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameState
{
    void SetGameStarted();
    void SetGameOver();
    void ToggleGamePaused();
    bool IsGameInProgress();
    bool IsGameOver();
    bool IsPaused();
    void DeleteHighScores();
}
