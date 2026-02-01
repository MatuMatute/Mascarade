using Godot;

public partial class Mano : Area2D
{
	[Signal]
	public delegate void ActivarVulnerabilidadEventHandler();
	[Export]
	private AnimatedSprite2D Sprite;

	private Vector2 PosicionInicial;

	public override void _Ready()
	{
		PosicionInicial = GlobalPosition;
	}

	public async void Caida(Vector2 PosicionObjetivo)
	{
		Tween Caida = CreateTween();
		Caida.TweenProperty(this, "global_position", PosicionObjetivo, 1.0f);
		Caida.TweenProperty(this, "global_position", PosicionInicial, 1.0f);
		await ToSignal(GetTree().CreateTimer(0.5f), "timeout");
		Sprite.Play("Abajo");
		await ToSignal(Caida, "step_finished");
		EmitSignal("ActivarVulnerabilidad");
		await ToSignal(GetTree().CreateTimer(0.5f), "timeout");
		Sprite.Play("Arriba");
	}

	private void EntradaEntidad(Node2D Entidad)
	{
		if (Entidad is Protagonista)
		{
			Esencial.Instancia.QuitarSalud();
		}
	}
}
