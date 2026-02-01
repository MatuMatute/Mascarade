using Godot;

public partial class Guardia : CharacterBody2D
{
	[Export]
	private StaticBody2D Pared;

	private readonly string[] DialogosSinBoleto = [
		"Disculpe señorita, ¿Puedo ver su boleto?",
		"No tengo, pero trabajo acá",
		"Lo lamento, pero no puedo dejarla entrar así, solo puede pasar libremente al evento en el que trabaja.",
		"(Debo encontrar un boleto, la boletería está cerca de acá.)"
	];

	private readonly string[] DialogosConBoleto = [
		"Disculpe señorita, ¿Puedo ver su boleto?",
		"Tomá",
		"Ahora podés pasar"
	];

	private readonly string DialogoPosterior = "Disfrute la función.";

	private CajaTexto CajaTexto;
	
	public override void _Ready()
	{
		CajaTexto = GetParent<Lugar>().DarCajaTexto();

		if (Esencial.Instancia.TenesAccesoAlPayaso())
			Pared.QueueFree();
	}

	public async void EntradaEntidad(Node2D Entidad)
	{
		if (Entidad is Protagonista)
		{
			Protagonista Protagonista = Entidad as Protagonista;
			Protagonista.DeshabilitarMovimiento();

			if (Esencial.Instancia.TenesAccesoAlPayaso())
			{
				CajaTexto.MoverArriba();
				CajaTexto.AgregarDialogo(DialogoPosterior);
				await ToSignal(CajaTexto, "DialogoFinalizado");
				CajaTexto.MoverAbajo();
				Protagonista.HabilitarMovimiento();
				return;
			}

			if (Esencial.Instancia.TieneBoleto())
			{
				CajaTexto.MoverArriba();
				CajaTexto.AgregarDialogo(DialogosConBoleto[0]);
				await ToSignal(CajaTexto, "DialogoFinalizado");
				CajaTexto.MoverAbajo();
				CajaTexto.AgregarDialogo(DialogosConBoleto[1]);
				await ToSignal(CajaTexto, "DialogoFinalizado");
				CajaTexto.MoverArriba();
				CajaTexto.AgregarDialogo(DialogosConBoleto[2]);
				await ToSignal(CajaTexto, "DialogoFinalizado");	
				Esencial.Instancia.PrimerGuardiaSeMovió();
				Pared.QueueFree();
			}
			else
			{
				CajaTexto.MoverArriba();
				CajaTexto.AgregarDialogo(DialogosSinBoleto[0]);
				await ToSignal(CajaTexto, "DialogoFinalizado");
				CajaTexto.MoverAbajo();
				CajaTexto.AgregarDialogo(DialogosSinBoleto[1]);
				await ToSignal(CajaTexto, "DialogoFinalizado");
				CajaTexto.MoverArriba();
				CajaTexto.AgregarDialogo(DialogosSinBoleto[2]);
				await ToSignal(CajaTexto, "DialogoFinalizado");
				CajaTexto.MoverAbajo();
				CajaTexto.AgregarDialogo(DialogosSinBoleto[3]);
				await ToSignal(CajaTexto, "DialogoFinalizado");
				Esencial.Instancia.HablasteConPrimerGuardia();
			}
			
			Protagonista.HabilitarMovimiento();
		}
	}
}
