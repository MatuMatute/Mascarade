using Godot;

public partial class Personaje : StaticBody2D
{
	[Export]
	private string[] Conversacion;
	[Export]
	private CajaTexto CajaTexto;

	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}

	public void ProtagonistaEnArea(Node2D body)
	{
		if (body is Protagonista)
		{
			for (int i = 0; i < Conversacion.GetLength(0); i++)
				CajaTexto.AgregarDialogo(Conversacion[i]);
		}
	}
}
