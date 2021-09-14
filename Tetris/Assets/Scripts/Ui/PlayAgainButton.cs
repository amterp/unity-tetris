using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayAgainButton : MonoBehaviour
{

    private Button _button;
    private UiHider _uiHider;
    private GameState _gameState;

    void Awake()
    {
        _button = GetComponent<Button>();
        _uiHider = GetComponent<UiHider>();
        _gameState = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameState>();
        _gameState.GameOverEvent += OnGameOver;
    }

    void Start()
    {
        Disable();
    }

    public void OnButtonClick()
    {
        Disable();
        _gameState.SetGameStarted();
    }

    private void OnGameOver()
    {
        Enable();
        _uiHider.Unhide(gameObject);
    }

    private void Disable()
    {
        _button.interactable = false;
        _uiHider.Hide(gameObject);
    }

    private void Enable()
    {
        _button.interactable = true;
        _uiHider.Unhide(gameObject);
    }
}
