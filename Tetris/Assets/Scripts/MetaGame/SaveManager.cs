using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager
{

    public SaveData SaveData { get; private set; }

    public static SaveManager CreateAndLoad()
    {
        SaveData saveData = new SaveData(new List<HighScoreInfo>());
        JsonDataSaver.LoadInto(saveData);
        return new SaveManager(saveData);
    }

    private SaveManager(SaveData saveData)
    {
        SaveData = saveData;
    }

    public void Save()
    {
        Debug.Log("Saving: " + SaveData.ToString());
        JsonDataSaver.Save(SaveData);
    }
}
