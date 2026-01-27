using Godot;

public partial class Protagonista : CharacterBody2D
{
	const int VELOCIDAD = 300;

	[Export]
	private Mazo Mazo;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Movimiento(delta);
        MoveAndSlide();
	}

	private void Movimiento(double delta)
	{
		Vector2 Velocity = Vector2.Zero;

		if (Input.IsActionPressed("ui_right"))
			Velocity.X += 1.0f;
		if (Input.IsActionPressed("ui_left"))
			Velocity.X -= 1.0f;
		if (Input.IsActionPressed("ui_down"))
			Velocity.Y += 1.0f;
		if (Input.IsActionPressed("ui_up"))
			Velocity.Y -= 1.0f;
		
		Velocity = Velocity.Normalized() * VELOCIDAD;

        Position += Velocity * (float)delta;
	}
}
