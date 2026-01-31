using Godot;

public partial class CajaBoletos : Area2D
{
	[Signal]
	public delegate void IniciarMinijuegoEventHandler();

	private readonly StringName TipoInteraccion = "Revisar";

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
				EmitSignal("IniciarMinijuego");
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
