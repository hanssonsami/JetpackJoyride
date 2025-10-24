using Godot;
using System;

public partial class Background : Parallax2D
{
	[Export]
	public float speed = -300;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	var position = ScrollOffset;
    position.X += speed * (float)delta;
    ScrollOffset = position;
	}
}
