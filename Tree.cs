using Godot;
using System;

public partial class Tree : StaticBody2D
{
	[Signal]
	public delegate void OnScoreEventHandler();

	[Signal]
	public delegate void OnHitEventHandler(Node2D body);

[Export]
public float speed = -300;

[Export]
public float acceleration = -50f; // pixels per second squared

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


	public void OnGranDeath(Node2D body)
	{
		EmitSignal(SignalName.OnHit, body);
	}
	
}