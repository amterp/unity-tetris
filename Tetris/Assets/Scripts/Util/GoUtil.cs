using UnityEngine;

/** GameObject Util */
public static class GoUtil
{
    public static GameState FindGameState()
    {
        return GameObject.FindGameObjectWithTag("GameController").GetComponent<GameState>();
    }
}