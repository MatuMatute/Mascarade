using Godot;

public partial class Mazo : CharacterBody2D
{
	[Export]
	private AnimatedSprite2D Sprite;
	private CollisionShape2D Colision;
	private Timer Temporizador;
	private bool Habilitado;

	public override void _Ready()
	{
		Temporizador = GetNode<Timer>("Ataque");
		Colision = GetNode<CollisionShape2D>("Colision");
		Esconder();
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("Atacar") && Habilitado)
		{
			LookAt(GetGlobalMousePosition());
			Show();
			Sprite.Play("default");
			Colision.SetDeferred("disabled", false);
			Habilitado = false;
			Temporizador.Start();
		}
	}

	private void Esconder()
	{
		Hide();
		Habilitado = true;
		Colision.SetDeferred("disabled", true);
	}
}
