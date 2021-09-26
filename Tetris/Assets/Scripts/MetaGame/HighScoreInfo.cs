using System;

public struct HighScoreInfo
{
    public HighScoreInfo(string playerName, double score, float difficulty)
        : this(playerName, score, difficulty, DateTimeOffset.Now.ToUnixTimeMilliseconds())
    {
    }

    public HighScoreInfo(string playerName, double score, float difficulty, long timestampMillis)
    {
        PlayerName = playerName;
        Score = score;
        Difficulty = difficulty;
        TimestampMillis = timestampMillis;
    }

    public string PlayerName;
    public double Score;
    public float Difficulty;
    public long TimestampMillis;

    public override string ToString()
    {
        return $"{typeof(HighScoreInfo).Name}<PlayerName: {PlayerName}, Score: {Score}, Difficulty: {Difficulty}, TimestampMillis: {TimestampMillis}>";
    }
}