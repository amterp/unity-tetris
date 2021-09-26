using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct HighScoreInfoDto
{
    public string PlayerName;
    public double Score;
    public float Difficulty;
    public long TimestampMillis;

    public static HighScoreInfoDto From(HighScoreInfo highScoreInfo)
    {
        return new HighScoreInfoDto(highScoreInfo.PlayerName,
            highScoreInfo.Score,
            highScoreInfo.Difficulty,
            highScoreInfo.TimestampMillis);
    }

    public HighScoreInfo ToNonDto()
    {
        return new HighScoreInfo(PlayerName, Score, Difficulty, TimestampMillis);
    }

    public override string ToString()
    {
        return $"{typeof(HighScoreInfoDto).Name}<PlayerName: {PlayerName}, Score: {Score}, Difficulty: {Difficulty}, TimestampMillis: {TimestampMillis}>";
    }

    private HighScoreInfoDto(string playerName, double score, float difficulty, long timestampMillis)
    {
        PlayerName = playerName;
        Score = score;
        Difficulty = difficulty;
        TimestampMillis = timestampMillis;
    }
}
