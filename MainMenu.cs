using Godot;
using System;

public partial class MainMenu : TextureRect
{
	// Called when the node enters the scene tree for the first time.
	[Export]
	public float speed = -20;

[Export]
public float acceleration = -250f; // pixels per second squared
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
		// Increase speed over time
		speed += acceleration * (float)delta;

		var position = Position;
		position.X += speed * (float)delta;
		Position = position;
	
}
	}

