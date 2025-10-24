using Godot;
using System;

public partial class ScoreLabel : RichTextLabel
{
    [Signal]
    public delegate void OnScoreChangedEventHandler(int score);

    private int score = 0;

    public void SetScore(int newScore)
    {
        score = newScore;
        Text = score.ToString();
        EmitSignal(SignalName.OnScoreChanged, score);
    }

    public int GetScore()
    {
        return score;
    }
}