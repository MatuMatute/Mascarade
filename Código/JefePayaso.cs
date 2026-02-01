using System.Threading.Tasks;
using Godot;

public partial class JefePayaso : Area2D
{
	private Protagonista Protagonista;

	[Export]
	private AnimatedSprite2D SpriteCuerpo;
	[Export]
	private Mano ManoDerecha;
	[Export]
	private Mano ManoIzquierda;

	private int PuntosVida;

	private bool Vulnerable;

	public override void _Ready()
	{
		PuntosVida = 10;
	}

	public void FijarProtagonista(Protagonista Protagonista) { this.Protagonista = Protagonista; }

	private void PatronDeAtaque()
	{
		Vulnerable = false;
		Vector2 PosicionObjetivo = Protagonista.GlobalPosition;

		if (PosicionObjetivo.X <= Position.X)
		{
			ManoDerecha.Caida(PosicionObjetivo);
		}

		if (PosicionObjetivo.X >= Position.X)
		{
			ManoIzquierda.Caida(PosicionObjetivo);
		}
	}

	private void Vulnerabilidad()
	{
		Vulnerable = true;
	}

	private async void EntidadDetectada(Node2D Entidad)
	{
		if (Entidad is Mazo && Vulnerable)
		{
			if (PuntosVida > 1)
			{
				PuntosVida--;
				Tween RecibirAtaque = CreateTween();
				RecibirAtaque.TweenProperty(SpriteCuerpo, "modulate", new Color(1.0f, 0.0f, 0.0f, 1.0f), 0.5f);
				RecibirAtaque.TweenProperty(SpriteCuerpo, "modulate", new Color(1.0f, 1.0f, 1.0f, 1.0f), 0.5f);
			}
			else
			{
				GetNode<Timer>("Cooldown").Stop();
				Tween Destruccion = CreateTween();
				Destruccion.TweenProperty(SpriteCuerpo, "offset", new Vector2(-4.0f, -4.0f), 0.2f);
				Destruccion.TweenProperty(SpriteCuerpo, "offset", new Vector2(4.0f, 4.0f), 0.2f);
				Destruccion.TweenProperty(SpriteCuerpo, "offset", new Vector2(-4.0f, 4.0f), 0.2f);
				Destruccion.TweenProperty(SpriteCuerpo, "offset", new Vector2(4.0f, -4.0f), 0.2f);
				Destruccion.TweenProperty(SpriteCuerpo, "offset", new Vector2(-4.0f, -4.0f), 0.2f);
				Destruccion.TweenProperty(SpriteCuerpo, "offset", new Vector2(4.0f, 4.0f), 0.2f);
				Destruccion.TweenProperty(SpriteCuerpo, "offset", new Vector2(-4.0f, 4.0f), 0.2f);
				Destruccion.TweenProperty(SpriteCuerpo, "offset", new Vector2(4.0f, -4.0f), 0.2f);
				Destruccion.TweenProperty(this, "modulate", new Color(1.0f, 1.0f, 1.0f, 0.0f), 0.5f);
				await ToSignal(Destruccion, "finished");
				QueueFree();
			}
		}
	}
}
