using Godot;

public partial class AreaTransicion : Area2D
{
	[Export]
	private int ZonaObjetivo;
	[Export]
	private Vector2 PosicionJugador;

	public void EntradaEntidad(Node2D Entidad)
	{
		if (Entidad is Protagonista)
		{
			GetTree().Root.GetNode<Nucleo>("Nucleo").CambiarArea(ZonaObjetivo, PosicionJugador);
			QueueFree();
		}
	}
}
