using Godot;
using System;

public partial class Main : Node
{
	CanvasLayer pauseLayer;
	CanvasLayer menu;

	RichTextLabel distanceLabel;
	RichTextLabel finalScore;
	public bool died = false;
    public int speed = 150;
	public int distance = 0;
	
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("pause") && died == false)
		{
			if (GetTree().Paused == false)
            {
            GetTree().Paused = true;
			pauseLayer.Visible = true;
            }
            else
            {
				GetTree().Paused = false;
				pauseLayer.Visible = false;
            }
		}
		
		distance += (int)(speed * 0.01);
		GD.Print(distance);

	}
	public override void _Ready()
	{
		pauseLayer = GetNode<CanvasLayer>("PauseLayer");
		menu = GetNode<CanvasLayer>("MenuLayer");
		finalScore = GetNode<RichTextLabel>("MenuLayer/VBoxContainer/FinalScore");
		distanceLabel = GetNode<RichTextLabel>("Game/ScoreLayer/Container/Distance");
	}

	public void OnPlayerDeath(int score)
	{
		died = true;
		GetTree().Paused = true;
		menu.Visible = true;
		finalScore.Text = "Final Score: " + score + " Coins ;" + " Distance " + distance + " m";
	}


	public void OnRetry()
	{
		GetTree().Paused = false;
		GetTree().CallDeferred("reload_current_scene");
	}


	public void OnQuit()
	{
		GetTree().Quit();
	}
}
