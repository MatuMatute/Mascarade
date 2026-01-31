using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class CajaTexto : PanelContainer
{
	[Signal]
	public delegate void DialogoFinalizadoEventHandler();
	const float VELOCIDAD_DE_ESCRITURA = 0.05f;
	enum Estado
	{
		LISTO,
		ESCRIBIENDO,
		FINALIZADO
	}

	[Export]
	private Label Texto;
	[Export]
	private TextureRect Mouse;

	private Estado estadoActual;
	private List<string> Dialogos;
	private Tween Escritor;

	public override void _Ready()
	{
		Dialogos = new List<string>();
		estadoActual = Estado.LISTO;
	}

	public override void _Process(double delta)
	{
		switch (estadoActual)
		{
			case Estado.LISTO:
				if (Dialogos.Count > 0)
				{
					Mouse.Hide();
					Show();
					Texto.Text = Dialogos.First();
					Escritor = CreateTween();
					Escritor.TweenProperty(Texto, "visible_characters", Texto.Text.Length, VELOCIDAD_DE_ESCRITURA * Texto.Text.Length).From(0.0f);
					Escritor.Connect("finished", new Callable(this, "EscrituraFinalizada"), 4);
					Escritor.Play();
					estadoActual = Estado.ESCRIBIENDO;
				}
				else
					if (Visible)
					{
						EmitSignal("DialogoFinalizado");
						Hide();
					}
				break;
			case Estado.ESCRIBIENDO:
				if (Input.IsActionJustPressed("Atacar"))
				{
					Escritor.CustomStep(VELOCIDAD_DE_ESCRITURA * Texto.Text.Length);
					Escritor.Kill();
				}
				break;
			case Estado.FINALIZADO:
				if (Input.IsActionJustPressed("Atacar"))
					estadoActual = Estado.LISTO;
				break;
		}
	}

	public void AgregarDialogo(string nuevoDialogo)
	{
		Dialogos.Add(nuevoDialogo);
	}

	public void MoverArriba()
	{
		SetVSizeFlags(SizeFlags.ShrinkBegin);
	}

	public void MoverAbajo()
	{
		SetVSizeFlags(SizeFlags.ShrinkEnd);
	}
	
	private void EscrituraFinalizada()
	{
		Dialogos.RemoveAt(0);
		Mouse.Show();
		estadoActual = Estado.FINALIZADO;
	}
}