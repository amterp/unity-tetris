using UnityEngine;

/** GameObject Util */
public static class GoUtil
{
    public static GameState FindGameState()
    {
        return GameObject.FindGameObjectWithTag("GameController").GetComponent<GameState>();
    }

    public static SettingsManager FindSettingsManager()
    {
        return GameObject.FindGameObjectWithTag("GameController").GetComponent<SettingsManager>();
    }
}