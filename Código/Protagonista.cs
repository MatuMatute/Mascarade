using System;
using System.Xml.Serialization;
using Godot;

public partial class Protagonista : CharacterBody2D
{
	[Export]
	private AnimatedSprite2D Sprite;
	[Export]
	private AnimatedSprite2D IconoInteraccion;
	private const int VELOCIDAD = 300;
	private bool PoderCaminar;

	public override void _Ready()
	{
		DeshabilitarMovimiento();
		EsconderInteraccion();
	}

	public override void _Process(double delta)
	{
		Movimiento(delta);
        MoveAndSlide();
	}

    public override void _Input(InputEvent @event)
    {
		if (PoderCaminar)
		{
			if (Input.IsActionJustPressed("ui_up"))
				Sprite.Play("Arriba");
			if (Input.IsActionJustPressed("ui_down"))
				Sprite.Play("Abajo");

			if (Input.IsActionJustReleased("ui_up") || Input.IsActionJustReleased("ui_left") || Input.IsActionJustReleased("ui_right") || Input.IsActionJustReleased("ui_down"))
				Sprite.Stop();
		}
    }

	public void TweenMovimientoAutomatico(Vector2 PosicionFinal, float Tiempo)
	{
		Tween Caminata = CreateTween();
		Caminata.TweenProperty(this, "position", PosicionFinal, Tiempo);
		Caminata.Connect("finished", new Callable(GetParent(), "ContinuacionCinematica"), 4);
		Sprite.Play("Arriba");
		Caminata.Play();
	}

	public void MostrarInteraccion(StringName Icono)
	{
		IconoInteraccion.Show();
		IconoInteraccion.Play(Icono);
	}

	public void EsconderInteraccion()
	{
		IconoInteraccion.Hide();
	}

	public void HabilitarMovimiento() {PoderCaminar = true;}

	public void DeshabilitarMovimiento() 
	{
		PoderCaminar = false;
		Sprite.Stop();
	}

	public bool PuedeCaminar() {return PoderCaminar;}

	private void Movimiento(double delta)
	{
		if (PoderCaminar)
		{
			Vector2 Velocity = Vector2.Zero;

			if (Input.IsActionPressed("ui_right"))
				Velocity.X += 1.0f;
			if (Input.IsActionPressed("ui_left"))
				Velocity.X -= 1.0f;
			if (Input.IsActionPressed("ui_down"))
				Velocity.Y += 1.0f;
			if (Input.IsActionPressed("ui_up"))
				Velocity.Y -= 1.0f;
		
			Velocity = Velocity.Normalized() * VELOCIDAD;

        	Position += Velocity * (float)delta;
		}
	}
}
