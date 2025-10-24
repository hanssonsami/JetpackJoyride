using Godot;
using System;

public partial class Coin : StaticBody2D
{


	[Signal]
	public delegate void OnHitEventHandler(Node2D body);


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}
	[Export]
	public float speed = -300;

	[Export]
	public float acceleration = -500f; // pixels per second squared

public override void _Process(double delta)
{
    // Increase speed over time
    speed += acceleration * (float)delta;

    var position = Position;
    position.X += speed * (float)delta;
    Position = position;
}
	public void OnScreenExited()
	{
		CallDeferred("queue_free");
		QueueFree();
	}

	public void OnScoreAreaBodyEntered(Node2D body)
	{
		if (body is Gran)
		{

			GD.Print("Gran scored");
			((Gran)body).Test();
			QueueFree();
		}
	}
	
}