using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SceneSwitcher : MonoBehaviour
{

    [SerializeField]
    private TetrisScene _tetrisScene;

    private GameState _gameState;

    void Awake()
    {
        _gameState = GoUtil.FindGameState();
        GetComponent<Button>().onClick.AddListener(() => SwitchToScene(_tetrisScene));
    }

    public void SwitchToScene(TetrisScene scene)
    {
        _gameState.SwitchToScene(scene);
    }
}

public enum TetrisScene
{
    MainMenu,
    Gameplay,
}

public static class TetrisSceneMethods
{
    public static string Name(this TetrisScene scene)
    {
        switch (scene)
        {
            case TetrisScene.MainMenu: return "MainMenu";
            case TetrisScene.Gameplay: return "Gameplay";
            default: throw new InvalidOperationException("Unknown TetrisScene: " + scene);
        }
    }
}
