using Godot;

public partial class AreaTexto : Area2D
{
	[Export]
	private string[] Texto;

	protected CajaTexto CajaTexto;

    public override void _Ready()
    {
        CajaTexto = GetParent<Lugar>().DarCajaTexto();
    }

	public virtual async void EntradaEntidad(Node2D Entidad)
	{
		if (Entidad is Protagonista)
		{
			Protagonista Protagonista = Entidad as Protagonista;
			Protagonista.DeshabilitarMovimiento();
			for (int i = 0; i < Texto.GetLength(0); i++)
				CajaTexto.AgregarDialogo(Texto[i]);
			await ToSignal(CajaTexto, "DialogoFinalizado");
			Protagonista.HabilitarMovimiento();
		}
	}
}
