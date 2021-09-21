using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultySetter : MonoBehaviour
{

    [SerializeField] private Slider _difficultySlider;

    private GameState _gameState;

    void Awake()
    {
        _gameState = GoUtil.FindGameState();
    }

    void Start()
    {
        _difficultySlider.value = _gameState.Difficulty;
    }

    public void SetDifficulty()
    {
        _gameState.Difficulty = _difficultySlider.value;
    }
}
