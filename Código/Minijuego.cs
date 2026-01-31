using System;
using Godot;

public partial class Minijuego : Node2D
{
	[Signal]
	public delegate void MinijuegoFinalizadoEventHandler();
	[Export]
	private Node2D ContenedorCerraduras;
	
	private bool Detenido;
	public override void _Ready()
	{
		Detenido = false;
		Godot.Collections.Array<Node> Cerraduras = ContenedorCerraduras.GetChildren();
		float[] Tiempos = DuracionesAleatorias();

		for (int i = 0; i < Cerraduras.Count; i++)
		{
			Cerradura Cerradura = Cerraduras[i] as Cerradura;

			if (Cerradura != null)
				Cerradura.FijarTemporizador(Tiempos[i]);
		}
	}

	public override async void _Process(double delta)
	{
		if (!Detenido)
		{
			Godot.Collections.Array<Node> Cerraduras = ContenedorCerraduras.GetChildren();
			int CerradurasAbiertas = 0;

			for (int i = 0; i < Cerraduras.Count; i++)
			{
				Cerradura Cerradura = Cerraduras[i] as Cerradura;
			
				if (Cerradura != null)
				{
					if (Cerradura.EstaAbierto())
						CerradurasAbiertas++;
				}
			}

			if (CerradurasAbiertas == 7)
			{
				Detenido = true;
				for (int i = 0; i < Cerraduras.Count; i++)
				{
					Cerradura Cerradura = Cerraduras[i] as Cerradura;

					if (Cerradura != null)
						Cerradura.PararTemporizador();
				}

				await ToSignal(GetTree().CreateTimer(1.0), "timeout");
				EmitSignal("MinijuegoFinalizado");
				QueueFree();
			}
		}
	}

	private float[] DuracionesAleatorias()
	{
		float[] Tiempos = [0.5f, 1.0f, 1.5f, 2.0f, 2.5f, 3.0f, 3.5f];
		Random NumeroAleatorio = new Random();

		for (int i = 0; i < Tiempos.GetLength(0); i++)
		{
			int indiceAleatorio = NumeroAleatorio.Next(Tiempos.GetLength(0));
			float Valor1 = Tiempos[i];
			float Valor2 = Tiempos[indiceAleatorio];
			Tiempos[i] = Valor2;
			Tiempos[indiceAleatorio] = Valor1;
		}
		
		return Tiempos;
	}
}
