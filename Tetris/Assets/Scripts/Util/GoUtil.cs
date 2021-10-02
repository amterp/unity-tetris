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

    public static AudioManager FindAudioManager()
    {
        return GameObject.FindGameObjectWithTag("GameController").transform.GetChild(0).GetComponent<AudioManager>();
    }

    public static GameConstants FindGameConstants()
    {
        return GameObject.FindGameObjectWithTag("GameController").GetComponent<GameConstants>();
    }
}