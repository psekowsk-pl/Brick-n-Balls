using System;

public class ScoreSystem
{
    public event EventHandler OnScoreChanged;

    private long Score;

    public ScoreSystem()
    {
       Score = 0;
    }

    public void AddPointsToScore(int addScoreAmount)
    {
        Score += addScoreAmount;
        if (OnScoreChanged != null) OnScoreChanged(this, new EventArgs());
    }

    public long GetScore() => Score;
}
