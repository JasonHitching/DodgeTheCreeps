using Godot;
using System;

public partial class Player : Area2D
{
	[Export] // Attribute enables the value to be set in Godot Inspector UI
	public int Speed { get; set; } // How fast player will move (pixels/sec)

	public Vector2 ScreenSize; // Size of game window

	/// <summary>
	/// Called when the node enters the scene tree for the first time.
	/// </summary>
	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size; // Get the current screensize
	}

	/// <summary>
	/// Called every frame. Use this method to update elements in the game that'll change often
	/// </summary>
	/// <param name="delta">Elapsed time since the previous frame.</param>
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
	}
}
