using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Hidee))]
public class PlayAgainButton : MonoBehaviour
{
    private Hidee _hidee;

    void Awake()
    {
        _hidee = GetComponent<Hidee>();
        GoUtil.FindGameState().GameOverEvent += OnGameOver;
    }

    private void OnGameOver()
    {
        _hidee.Unhide();
    }
}
