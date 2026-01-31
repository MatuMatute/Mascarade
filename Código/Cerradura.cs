using Godot;

public partial class Cerradura : Area2D
{
	[Export]
	private Timer Temporizador;
	private bool Presionado;

	public void FijarTemporizador(float Tiempo) { Temporizador.Set("wait_time", Tiempo); }

	public bool EstaAbierto() { return Presionado; }

	public void PararTemporizador() { Temporizador.Stop(); }

	private void EventoClic(Node Viewport, InputEvent Evento, int IndiceForma)
	{
		if (Input.IsActionJustPressed("Atacar") && !Presionado)
		{
			Modulate = new Color(0.5f, 0.5f, 0.5f, 1.0f);
			Presionado = true;
			Temporizador.Start();
		}
	}

	private void TiempoTerminado() 
	{ 
		Modulate = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		Presionado = false; 
	}
}
