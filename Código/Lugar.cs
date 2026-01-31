using Godot;

public partial class Lugar : Node2D
{	
	[Export]
	protected Protagonista Protagonista;
	[Export]
	protected Camera2D Camara;

	protected CajaTexto CajaTexto;

	public override void _Process(double delta)
	{
	}

	public void AsignarCajaDeTexto(CajaTexto CajaTexto) {this.CajaTexto = CajaTexto;}

	public void AsignarProtagonista(Protagonista Protagonista)
	{
		this.Protagonista = Protagonista;
	}

	public CajaTexto DarCajaTexto() {return CajaTexto;}

	public void EliminarCamaraLocal() 
	{
		Camara.QueueFree();
	}

	protected virtual void ContinuacionCinematica() {}
}
