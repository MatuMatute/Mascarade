using Godot;

public partial class AreaTransicionInteractuable : Area2D
{
	[Export]
	private int ZonaObjetivo;
	[Export]
	private Vector2 PosicionJugador;
	private readonly StringName TipoInteraccion = "Puerta";

	private Protagonista ProtagonistaEnArea;

    public override void _Ready()
    {
		ProtagonistaEnArea = null;
    }

    public override void _Process(double delta)
    {
        if (ProtagonistaEnArea != null)
		{
			if (Input.IsActionJustPressed("Atacar") && ProtagonistaEnArea.PuedeCaminar())
			{
				GetTree().Root.GetNode<Nucleo>("Nucleo").CambiarArea(ZonaObjetivo, PosicionJugador);
				QueueFree();
			}
		}
    }

	private void EntradaEntidad(Node2D Entidad)
	{
		if (Entidad is Protagonista)
		{
			ProtagonistaEnArea = Entidad as Protagonista;
			ProtagonistaEnArea.MostrarInteraccion(TipoInteraccion);
		}
	}

	private void SalidaEntidad(Node2D Entidad)
	{
		if (Entidad is Protagonista) 
		{
			ProtagonistaEnArea.EsconderInteraccion();
			ProtagonistaEnArea = null; 
		}
	}
}
