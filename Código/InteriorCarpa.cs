using Godot;

public partial class InteriorCarpa : Lugar
{
	[Export]
	private JefePayaso JefePayaso;
	public override void _Ready()
	{
		Camara.Reparent(Protagonista);
		Camara.Set("position", Vector2.Zero);
		Protagonista.Reparent(this);
		JefePayaso.FijarProtagonista(Protagonista);
		GetParent<Nucleo>().Vidas();
	}
}
