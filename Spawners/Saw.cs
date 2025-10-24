using Godot;
using System;

public partial class Saw : StaticBody2D
{
[Export]
public float speed = 300;
[Signal]
public delegate void OnHitEventHandler(Node2D body);


public float rotationSpeed = -180; // degrees per second

	public override void _Process(double delta)
	{
	var position = Position;
	Rotate((float)delta * rotationSpeed);		
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
