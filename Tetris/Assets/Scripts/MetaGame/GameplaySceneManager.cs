using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplaySceneManager : MonoBehaviour
{

    private AudioManager _audioManager;

    void Awake()
    {
        _audioManager = GoUtil.FindAudioManager();
        GameState gameState = GoUtil.FindGameState();
        gameState.GameStartedEvent += OnGameStart;
        gameState.GameOverEvent += OnGameOver;
        gameState.SceneSwitchEvent += (ignored) => _audioManager.PlayMusic();
    }

    private void OnGameStart()
    {
        _audioManager.PlayMusic();
    }

    private void OnGameOver()
    {
        _audioManager.StopMusic();
        _audioManager.Play(SoundEnum.GameOver);
    }
}
