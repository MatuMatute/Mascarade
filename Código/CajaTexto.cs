using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class CajaTexto : PanelContainer
{
	const float VELOCIDAD_DE_ESCRITURA = 0.05f;
	enum Estado
	{
		LISTO,
		ESCRIBIENDO,
		FINALIZADO
	}

	[Export]
	private Label Texto;

	private Estado estadoActual;
	private List<string> Dialogos;

	public override void _Ready()
	{
		estadoActual = Estado.LISTO;
	}

	public override void _Process(double delta)
	{
		switch (estadoActual)
		{
			case Estado.LISTO:
				if (Dialogos.Count > 0)
					estadoActual = Estado.ESCRIBIENDO;
				else
					Hide();
				break;
			case Estado.ESCRIBIENDO:
				Tween Escritor = CreateTween();
				Texto.Text = Dialogos[0];
				Escritor.TweenProperty(Texto, "visible_characters", Texto.Text.Length, VELOCIDAD_DE_ESCRITURA * Texto.Text.Length);
				break;
			case Estado.FINALIZADO:
				break;
		}
	}
}