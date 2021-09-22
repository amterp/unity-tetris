using System;

public readonly struct HighScoreInfo
{
    public HighScoreInfo(string playerName, double score, float difficulty)
    {
        PlayerName = playerName;
        Score = score;
        Difficulty = difficulty;
        TimestampMillis = DateTimeOffset.Now.ToUnixTimeMilliseconds();
    }

    public string PlayerName { get; }
    public double Score { get; }
    public float Difficulty { get; }
    public long TimestampMillis { get; }

    public override string ToString()
    {
        return $"<PlayerName: {PlayerName}, Score: {Score}, Difficulty: {Difficulty}, TimestampMillis: {TimestampMillis}>";
    }
}