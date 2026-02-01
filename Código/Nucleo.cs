using Godot;

public partial class Nucleo : Node
{
	private readonly NodePath[] UbicacionPlaza = [
		"res://Escenas/plaza.tscn", 
		"res://Escenas/Entrada.tscn", 
		"res://Escenas/Fuente.tscn",
		"res://Escenas/CaminoCarpa.tscn",
		"res://Escenas/EntradaCarpa.tscn",
		"res://Escenas/InteriorBoleteria.tscn",
		"res://Escenas/InteriorCarpa.tscn"];
	
	private readonly Vector2 UbicacionInicial = new Vector2(960.0f, 960.0f);

	[Export]
	private Interfaz Interfaz;
	[Export]
	private Protagonista Protagonista;
	[Export]
	private Lugar AreaActual;
	public override void _Ready()
	{
		GetTree().Paused = true;
	}

	public async void CambiarArea(int ID, Vector2 PosicionJugador)
	{
		Protagonista.DeshabilitarMovimiento();
		AnimationPlayer Animaciones = Interfaz.GetAnimaciones();
		Animaciones.Play("transicionANegro");
		await ToSignal(Animaciones, "animation_finished");
		AreaActual.EliminarCamaraLocal();
		Protagonista.Reparent(this);
		Protagonista.Set("position", PosicionJugador);
		AreaActual.QueueFree();
		await ToSignal(GetTree().CreateTimer(0.25f), "timeout");
		Lugar NuevaArea = ResourceLoader.Load<PackedScene>(UbicacionPlaza[ID]).Instantiate<Lugar>();
		NuevaArea.AsignarCajaDeTexto(Interfaz.GetCajaTexto());
		NuevaArea.AsignarProtagonista(Protagonista);
		AddChild(NuevaArea);
		AreaActual = NuevaArea;
		Animaciones.Play("transicionATransparente");
		if (Esencial.Instancia.IntroHaTerminado()) {Protagonista.HabilitarMovimiento();}
	}

	public void Controles()
	{
		Interfaz.MostrarControles();
	}

	public void Vidas()
	{
		Interfaz.MostrarVidas();
	}

	public void ActualizarContador()
	{
		Interfaz.ActualizarVidas();
	}

	private void IniciarPartida()
	{
		GetTree().Paused = false;
		Lugar Plaza = ResourceLoader.Load<PackedScene>(UbicacionPlaza[1]).Instantiate<Lugar>();
		Protagonista.Set("position", UbicacionInicial);
		Plaza.AsignarCajaDeTexto(Interfaz.GetCajaTexto());
		Plaza.AsignarProtagonista(Protagonista);
		AddChild(Plaza);
		AreaActual = Plaza;
	}
}
