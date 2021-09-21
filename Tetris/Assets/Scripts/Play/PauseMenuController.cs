using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    private const bool START_UNPAUSED = false;

    [SerializeField] private GameObject _pauseUiToToggle;

    void Awake()
    {
        GoUtil.FindGameState().GamePausedEvent += OnPauseToggle;
    }

    void Start()
    {
        OnPauseToggle(START_UNPAUSED);
    }

    private void OnPauseToggle(bool isPaused)
    {
        Time.timeScale = isPaused ? 0 : 1;
        _pauseUiToToggle.SetActive(isPaused);
    }
}
