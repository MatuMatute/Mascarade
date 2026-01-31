using Godot;

public partial class Salir : Button
{
	public void Presionado()
	{
		GetTree().Quit();
	}
}
