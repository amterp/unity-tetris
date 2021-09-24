using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreManager
{
    private readonly List<HighScoreInfo> _highScores;

    public static HighScoreManager Create()
    {
        List<HighScoreInfo> loadedHighScores = LoadHighScores();
        return new HighScoreManager(loadedHighScores);
    }

    private HighScoreManager(List<HighScoreInfo> highScores)
    {
        this._highScores = highScores;
    }

    public void Save(HighScoreInfo highScoreInfo)
    {
        Debug.Log($"Saving #{_highScores.Count + 1}: " + highScoreInfo);
        _highScores.Add(highScoreInfo);
    }

    public List<HighScoreInfo> GetHighScores()
    {
        return new List<HighScoreInfo>(_highScores);
    }

    private static List<HighScoreInfo> LoadHighScores()
    {
        // todo actually load once persistent. Also, use a HighScoreInfoDto to decouple the API from the backend implementation.
        return new List<HighScoreInfo>();
    }
}
