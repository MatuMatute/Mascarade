using Godot;

public partial class JefePayaso : Area2D
{
	[Export]
	private AnimatedSprite2D SpriteCuerpo;
	private int PuntosVida;

	public override void _Ready()
	{
		PuntosVida = 10;
	}

	public override void _Process(double delta)
	{
	}
}
