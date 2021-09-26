using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class JsonDataSaver
{

    private const string filePrefix = "/tnt/saveData";

    public static void Save(IJsonSerializable objectToSave, string fileSuffix = "")
    {
        string absolutePath = ResolveAbsoluteSavePath(fileSuffix);
        string jsonString = objectToSave.ToJson();
        Debug.Log($"Saving to {absolutePath}: {jsonString}");

        new FileInfo(absolutePath).Directory.Create();
        File.WriteAllText(absolutePath, jsonString);
    }

    public static void LoadInto(IJsonSerializable objectToLoadInto, string fileSuffix = "")
    {
        string absolutePath = ResolveAbsoluteSavePath(fileSuffix);
        Debug.Log($"Loading from {absolutePath}.");
        if (File.Exists(absolutePath))
        {
            objectToLoadInto.LoadFromJson(File.ReadAllText(absolutePath));
            Debug.Log($"Loaded: {objectToLoadInto.ToString()}");
        }
        else
        {
            Debug.Log("Cannot load file - doesn't exist");
        }
    }

    private static string ResolveAbsoluteSavePath(string fileSuffix)
    {
        return Path.Combine(Application.persistentDataPath, filePrefix + fileSuffix + ".json");
    }
}
