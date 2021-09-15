using UnityEngine;

public static class GoUtil
{
    public static GameState FindGameState()
    {
        return GameObject.FindGameObjectWithTag("GameController").GetComponent<GameState>();
    }
}