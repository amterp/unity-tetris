using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(HighScoreListPopulator))]
public class HighScoreListGameOverController : MonoBehaviour
{

    private HighScoreListPopulator _listPopulator;

    void Awake()
    {
        _listPopulator = GetComponent<HighScoreListPopulator>();
        GameState gameState = GoUtil.FindGameState();
        gameState.GameOverEvent += () => _listPopulator.RepopulateEntries();
        gameState.NewHighScoreInfoEvent += (ignored) => _listPopulator.RepopulateEntries();
    }
}
