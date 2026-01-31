using Godot;

public partial class EntradaCarpa : Lugar
{
	public override void _Ready()
	{
		Camara.Reparent(Protagonista);
		Camara.Set("position", Vector2.Zero);
		Protagonista.Reparent(this);
	}
}
