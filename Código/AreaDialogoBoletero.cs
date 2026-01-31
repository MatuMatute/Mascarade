using Godot;

public partial class AreaDialogoBoletero : AreaInteractuable
{
	[Signal]
	public delegate void HabilitarPuertaEventHandler();
	[Export]
	private AnimationPlayer AnimacionBoletero;
	private readonly string[] Dialogo = [
		"Buenos días, Sofía",
		"Hola Mauro, no sabia que tenias turno hoy.",
		"Cambio de ultimo momento",
		"Acabo de venir del puesto de churros, preguntan si podés llevarles cambio",
		"Estoy solo, ¡te molesta quedarte hasta que vuelva?",
		"Dale, no hay problema."
	];
	public override void _Ready()
	{
		base._Ready();
	}

    public async override void _Process(double delta)
    {
        if (ProtagonistaEnArea != null)
		{
			if (Input.IsActionJustPressed("Atacar") && ProtagonistaEnArea.PuedeCaminar())
			{
				ProtagonistaEnArea.DeshabilitarMovimiento();
				CajaTexto.MoverArriba();
				CajaTexto.AgregarDialogo(Dialogo[0]);
				await ToSignal(CajaTexto, "DialogoFinalizado");
				CajaTexto.MoverAbajo();
				CajaTexto.AgregarDialogo(Dialogo[1]);
				await ToSignal(CajaTexto, "DialogoFinalizado");
				CajaTexto.MoverArriba();
				CajaTexto.AgregarDialogo(Dialogo[2]);
				await ToSignal(CajaTexto, "DialogoFinalizado");
				CajaTexto.MoverAbajo();
				CajaTexto.AgregarDialogo(Dialogo[3]);
				await ToSignal(CajaTexto, "DialogoFinalizado");
				CajaTexto.MoverArriba();
				CajaTexto.AgregarDialogo(Dialogo[4]);
				await ToSignal(CajaTexto, "DialogoFinalizado");
				CajaTexto.MoverAbajo();
				CajaTexto.AgregarDialogo(Dialogo[5]);
				await ToSignal(CajaTexto, "DialogoFinalizado");
				AnimacionBoletero.Play("MeVoy");
				await ToSignal(AnimacionBoletero, "animation_finished");
				EmitSignal("HabilitarPuerta");
				ProtagonistaEnArea.HabilitarMovimiento();
				QueueFree();
			}
		}
    }
}
