using Godot;

public partial class InteriorBoleteria : Lugar
{
	[Export]
	private CajaBoletos CajaBoletos;
	private readonly NodePath Minijuego = "res://Escenas/Minijuego.tscn";

	private readonly string[] TextoObtuvisteBoleto = [
		"¡Conseguiste un boleto para entrar!",
		"Debajo de la caja hay una carta:",
		"\"Para Mauro\"",
		"\"Vos seguro tenés más idea que yo de como van las ganancias, después de todo soy solo un payaso y vos controlas la caja: ¿es tanto lo que recaudamos?.\"",
		"\"Hace dos semanas, Juan vino con los demás y nos dijo que Octavio ganaba seis veces más que nosotros en este parque.\"",
		"\"Al principio no le creí, sí bien Octavio es el jefe de nuestro show, no me imagino que alguien en este trabajo gane tanto, pero solo un día después despidieron a Juan.\"",
		"\"Parezco paranoico, pero de enserio se está quedando con tanta plata ¿ganamos tanto por función para que le paguen eso?\""
	];

	public override void _Ready()
	{
		Protagonista.Reparent(this);

		if (Esencial.Instancia.TieneBoleto())
			CajaBoletos.QueueFree();
	}

	private void ComenzarMinijuego()
	{
		Protagonista.DeshabilitarMovimiento();
		Minijuego Caja = ResourceLoader.Load<PackedScene>(Minijuego).Instantiate<Minijuego>();
		Caja.Connect("MinijuegoFinalizado", Callable.From(FinalizarMinijuego), 4);
		AddChild(Caja);
		Caja.Set("position", Camara.Get("position"));
	}

	private async void FinalizarMinijuego()
	{
		Esencial.Instancia.ConseguirBoleto();
		CajaBoletos.QueueFree();
		CajaTexto.AgregarDialogo(TextoObtuvisteBoleto[0]);
		CajaTexto.AgregarDialogo(TextoObtuvisteBoleto[1]);
		await ToSignal(CajaTexto, "DialogoFinalizado");
		CajaTexto.MoverArriba();
		for (int i = 2; i < TextoObtuvisteBoleto.GetLength(0); i++)
				CajaTexto.AgregarDialogo(TextoObtuvisteBoleto[i]);
		await ToSignal(CajaTexto, "DialogoFinalizado");
		CajaTexto.MoverAbajo();
		Protagonista.HabilitarMovimiento();
	}
}
