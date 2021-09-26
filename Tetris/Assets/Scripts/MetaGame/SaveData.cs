using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData : IJsonSerializable
{
    public List<HighScoreInfo> HighScores { get { return GetHighScores(); } set { SetHighScores(value); } }

    [SerializeField]
    private List<HighScoreInfoDto> _highScoresDtos;

    public SaveData(List<HighScoreInfo> highScores)
    {
        _highScoresDtos = highScores.Select(highScore => HighScoreInfoDto.From(highScore)).ToList();
    }

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public void LoadFromJson(string jsonString)
    {
        JsonUtility.FromJsonOverwrite(jsonString, this);
    }

    private List<HighScoreInfo> GetHighScores()
    {
        return _highScoresDtos.Select(highScoreDto => highScoreDto.ToNonDto()).ToList();
    }

    private void SetHighScores(List<HighScoreInfo> newHighScores)
    {
        _highScoresDtos = newHighScores.Select(highScore => HighScoreInfoDto.From(highScore)).ToList();
    }

    // Format: CLASS_NAME<Field1: FIELD_1_VALUE, FieldN: FIELD_N_VALUE>
    public override string ToString()
    {
        return $"{typeof(SaveData).Name}<HighScores: {string.Join(", ", _highScoresDtos)}>";
    }
}
