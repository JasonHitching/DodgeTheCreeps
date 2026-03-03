using Godot;
using System;

public partial class Player : Area2D
{
	[Export] // Attribute enables the value to be set in Godot Inspector UI
	public int Speed { get; set; } = 400;  // How fast player will move (pixels/sec)

	public Vector2 ScreenSize; // Size of game window

	/// <summary>
	/// Called when the node enters the scene tree for the first time.
	/// </summary>
	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size; // Get the current screensize

		Hide();
	}

	/// <summary>
	/// Called every frame. Use this method to update elements in the game that'll change often
	/// </summary>
	/// <param name="delta">Elapsed time since the previous frame (frame length).
	/// Ensures that movement remains consistent across frame rate changes.</param>
	public override void _Process(double delta)
	{
		var velocity = Vector2.Zero; // Players movement vector set to (0, 0) (not moving)

		if (Input.IsActionPressed("move_right"))
		{
			velocity.X += 1;
		}

		if (Input.IsActionPressed("move_left"))
		{
			velocity.X -= 1;
		}

		if (Input.IsActionPressed("move_down"))
		{
			velocity.Y += 1;
		}

		if (Input.IsActionPressed("move_up"))
		{
			velocity.Y -= 1;
		}

		var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

		if (velocity.Length() > 0)
		{
			velocity = velocity.Normalized() * Speed;
			animatedSprite2D.Play();
		}
		else
		{
			animatedSprite2D.Stop();
		}

		Position += velocity * (float)delta;
		Position = new Vector2(
			x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
			y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y)
		);

		if (velocity.X != 0) 
		{
			animatedSprite2D.Animation = "walk";
			animatedSprite2D.FlipV = false;
			animatedSprite2D.FlipH = velocity.X < 0;
		}
		else if (velocity.Y != 0) 
		{
			animatedSprite2D.Animation = "up";
			animatedSprite2D.FlipV = velocity.Y > 0;
		}
	}
}
