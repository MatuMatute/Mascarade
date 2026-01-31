using Godot;

public partial class Interfaz : Control
{
	[Signal]
	public delegate void IniciarJuegoEventHandler();
	[Export]
	private AnimationPlayer Animaciones;
	[Export]
	private CajaTexto CajaTexto;
	[Export]
	private VBoxContainer Pausa;
	[Export]
	private ColorRect Transicion;
	[Export]
	private Control Controles;
	[Export]
	private Timer TemporizadorControles;
	[Export]
	private HBoxContainer ContenedorVidas;
	[Export]
	private VBoxContainer Creditos;
	[Export]
	private VBoxContainer MenuPrincipal;

	public override void _Ready()
	{
		Animaciones.Play("intro");
	}

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("ui_cancel") && !GetTree().Paused && !Transicion.Visible)
		{
			GetTree().Paused = true;
			Transicion.Show();
			Transicion.Modulate = new Color(0.0f, 0.0f, 0.0f, 0.5f);
			Pausa.Show();
		}
    }

	public async void JugarPresionado()
	{
		Animaciones.Play("transicionJuego");
		await ToSignal(Animaciones, "animation_finished");
		Animaciones.Play("transicionATransparente");
		EmitSignal("IniciarJuego");
	}

	public void MostrarControles()
	{
		Controles.Show();
		TemporizadorControles.Start();
	}

	public void MostrarVidas()
	{
		ContenedorVidas.Show();
	}

	private void EsconderControles() { Controles.Hide(); }

	private void MostrarCreditos()
	{
		MenuPrincipal.Hide();
		Creditos.Show();
	}

	private void VolverMenuPrincipal()
	{
		Creditos.Hide();
		MenuPrincipal.Show();
	}
		
	public CajaTexto GetCajaTexto() {return CajaTexto;}

	public AnimationPlayer GetAnimaciones() {return Animaciones;}

	private void Reanudar()
	{
		GetTree().Paused = false;
		Transicion.Hide();
		Pausa.Hide();
	}
}
