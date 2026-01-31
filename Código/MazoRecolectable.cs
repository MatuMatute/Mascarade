using Godot;

public partial class MazoRecolectable : Area2D
{
	private Mazo Mazo;

    public override void _Ready()
    {
        Mazo = ResourceLoader.Load<PackedScene>("res://Escenas/Mazo.tscn").Instantiate<Mazo>();
    }

	public void EntradaEntidad(Node2D Entidad)
	{
		if (Entidad is Protagonista)
		{
			Entidad.CallDeferred("add_child", Mazo);
			QueueFree();
		}
	}
}
