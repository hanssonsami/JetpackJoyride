using Godot;
using System;

public partial class Gran : CharacterBody2D
{
		[Signal]
	public delegate void OnScoreEventHandler();
	[Export]
	public float JumpVelocity = -400.0f;
	AnimatedSprite2D sprite;

	public override void _Ready()
	{
		sprite = GetNode<AnimatedSprite2D>("Sprite2D");
		sprite.Play("default");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		velocity += GetGravity() * (float)2 * (float)delta;

		if (Input.IsActionPressed("ui_accept"))
		{
			velocity.Y += JumpVelocity;
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	public void Test()
	{
		EmitSignal(SignalName.OnScore);
	}
}
