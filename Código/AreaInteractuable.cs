using Godot;

public partial class AreaInteractuable : Area2D
{
	[Export]
	private string[] Texto;

	protected readonly StringName TipoInteraccion = "Revisar";

	protected CajaTexto CajaTexto;

	protected Protagonista ProtagonistaEnArea;

    public override void _Ready()
    {
		ProtagonistaEnArea = null;
        CajaTexto = GetParent<Lugar>().DarCajaTexto();
    }

    public async override void _Process(double delta)
    {
        if (ProtagonistaEnArea != null)
		{
			if (Input.IsActionJustPressed("Atacar") && ProtagonistaEnArea.PuedeCaminar())
			{
				ProtagonistaEnArea.DeshabilitarMovimiento();
				for (int i = 0; i < Texto.GetLength(0); i++)
					CajaTexto.AgregarDialogo(Texto[i]);
				await ToSignal(CajaTexto, "DialogoFinalizado");
				ProtagonistaEnArea.HabilitarMovimiento();
			}
		}
    }

	protected void EntradaEntidad(Node2D Entidad)
	{
		if (Entidad is Protagonista)
		{
			ProtagonistaEnArea = Entidad as Protagonista;
			ProtagonistaEnArea.MostrarInteraccion(TipoInteraccion);
		}
	}

	protected void SalidaEntidad(Node2D Entidad)
	{
		if (Entidad is Protagonista) 
		{
			ProtagonistaEnArea.EsconderInteraccion();
			ProtagonistaEnArea = null; 
		}
	}
}
