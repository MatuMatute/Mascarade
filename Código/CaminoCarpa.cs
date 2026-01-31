using Godot;

public partial class CaminoCarpa : Lugar
{
	[Export]
	private AreaTransicionInteractuable Puerta;
	[Export]
	private AreaDialogoBoletero Boleteria;
	public override void _Ready()
	{
		Camara.Reparent(Protagonista);
		Camara.Set("position", Vector2.Zero);
		Protagonista.Reparent(this);

		if (!Esencial.Instancia.HasConversadoConPrimerGuardia())
			Boleteria.Set("monitoring", false);

		if (Esencial.Instancia.HasConversadoConBoleteria())
		{
			Puerta.Set("monitoring", true);
			Boleteria.QueueFree();
		}
		else
			Puerta.Set("monitoring", false);
	}

	private void HabilitarPuerta()
	{
		Esencial.Instancia.HablasteConBoleteria();
		Puerta.Set("monitoring", true);
	}
}
