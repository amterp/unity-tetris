using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreManager
{
    private readonly SaveManager _saveManager;

    public HighScoreManager(SaveManager saveManager)
    {
        _saveManager = saveManager;
    }

    public void Save(HighScoreInfo highScoreInfo)
    {
        SaveData saveData = _saveManager.SaveData;
        List<HighScoreInfo> highScores = saveData.HighScores;
        highScores.Add(highScoreInfo);
        saveData.HighScores = highScores;

        _saveManager.Save();
    }

    public List<HighScoreInfo> GetHighScores()
    {
        return _saveManager.SaveData.HighScores;
    }

    public void DeleteHighScores()
    {
        Debug.Log("Deleting save data.");
        _saveManager.SaveData.HighScores = new List<HighScoreInfo>();
        _saveManager.Save();
    }
}
