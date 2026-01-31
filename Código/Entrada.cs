using Godot;

public partial class Entrada : Lugar
{
	private readonly Vector2 PosicionFinalIntro = new Vector2(960.0f, 0.0f);
	private const float TiempoIntro = 8.0f;
	public override void _Ready()
	{
		Camara.Reparent(Protagonista);
		Camara.Set("position", Vector2.Zero);
		Protagonista.Reparent(this);

		if (!Esencial.Instancia.IntroHaTerminado())
			Protagonista.TweenMovimientoAutomatico(PosicionFinalIntro, TiempoIntro);
	}
}
