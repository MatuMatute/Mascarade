using Godot;

public partial class Fuente : Lugar
{
	[Export]
	private AnimatedSprite2D Presentador;
	[Export]
	private CharacterBody2D Acrobata;
	[Export]
	private CharacterBody2D Adivina;
	[Export]
	private CharacterBody2D Mimo;
	[Export]
	private CharacterBody2D Tango;
	[Export]
	private CharacterBody2D Anfitrion;
	[Export]
	private CharacterBody2D Payaso;
	[Export]
	private AnimationPlayer AnimacionesCamara;
	private readonly Vector2 PosicionFinalIntro = new Vector2(960.0f, 832.0f);
	private const float TiempoIntro = 3.0f;
	private readonly string[] DialogosCinematica = [
		"Bienvenidos todos a nuestro Gran Festival de Otoño, este año en Illusion Land!", 
		"Tenemos preparadas distintas actividades que los dejarán al borde de su asiento!",
		"(No reconozco a este presentador.)",
		"Sin embargo, antes de empezar tenemos un problema que resolver.",
		"Como saben, Illusion Land se enorgullece por ser un lugar transparente y un negocio honesto.",
		"Pero ciertas personas se olvidaron de esto",
		"(¡De que habla?, son mis compañeros...)",
		"Aunque estas personas merecen un castigo, el show debe continuar",
		"Para que las acciones de pocos no arruinen la diversión de muchos, utilizando el talento de estos artistas crearé sus reemplazos perfectos",
		"(¡Qué es esta locura?)",
		"Como castigo, cuando este día termine, estos transgresores pasarán la eternidad adornando esta plaza!",
		"Sin más que decir mis queridos visitantes, espero que pasen un hermoso día y recuerden: el Show de payasos va a comenzar!",
		"No puedo dejar que ocurra esto, son mis compañeros, los conozco son buenos y trabajadores, no merecen esto",
		"Los voy ayudar, si les devuelvo esas piedras estoy segura de que van a volver a la normalidad",
		"Se dónde encontrar al primero de esos monstruos, ¡no?  El show de payasos va a comenzar, la carpa se encuentra recto a la derecha."];


	public override void _Ready()
	{
		Camara.Set("position", new Vector2(960.0f, 816.0f));
		Protagonista.Reparent(this);

		if (!Esencial.Instancia.IntroHaTerminado())
		{
			Protagonista.TweenMovimientoAutomatico(PosicionFinalIntro, TiempoIntro);
			Acrobata.Hide();
			Adivina.Hide();
			Mimo.Hide();
			Tango.Hide();
			Anfitrion.Hide();
			Payaso.Hide();
		}
		else
		{
			Presentador.QueueFree();
			Camara.Reparent(Protagonista);
			Camara.Set("position", Vector2.Zero);
		}
	}

	protected override async void ContinuacionCinematica()
	{
		Protagonista.DeshabilitarMovimiento();
		CajaTexto.MoverArriba();
		CajaTexto.AgregarDialogo(DialogosCinematica[0]);
		await ToSignal(CajaTexto, "DialogoFinalizado");
		AnimacionesCamara.Play("MirarPresentador");
		await ToSignal(AnimacionesCamara, "animation_finished");
		CajaTexto.AgregarDialogo(DialogosCinematica[1]);
		await ToSignal(CajaTexto, "DialogoFinalizado");
		CajaTexto.MoverAbajo();
		CajaTexto.AgregarDialogo(DialogosCinematica[2]);
		await ToSignal(CajaTexto, "DialogoFinalizado");
		CajaTexto.MoverArriba();
		CajaTexto.AgregarDialogo(DialogosCinematica[3]);
		CajaTexto.AgregarDialogo(DialogosCinematica[4]);
		await ToSignal(CajaTexto, "DialogoFinalizado");
		Presentador.Play("Apuntando");
		CajaTexto.AgregarDialogo(DialogosCinematica[5]);
		await ToSignal(CajaTexto, "DialogoFinalizado");
		AnimacionesCamara.Play("MostrarCompañeros");
		await ToSignal(AnimacionesCamara, "animation_finished");
		CajaTexto.MoverAbajo();
		CajaTexto.AgregarDialogo(DialogosCinematica[6]);
		await ToSignal(CajaTexto, "DialogoFinalizado");
		CajaTexto.MoverArriba();
		CajaTexto.AgregarDialogo(DialogosCinematica[7]);
		await ToSignal(CajaTexto, "DialogoFinalizado");
		AnimacionesCamara.Play("PetrificarCompañeros");
		await ToSignal(AnimacionesCamara, "animation_finished");
		Presentador.Play("Quieto");
		CajaTexto.AgregarDialogo(DialogosCinematica[8]);
		await ToSignal(CajaTexto, "DialogoFinalizado");
		CajaTexto.MoverAbajo();
		CajaTexto.AgregarDialogo(DialogosCinematica[9]);
		await ToSignal(CajaTexto, "DialogoFinalizado");
		CajaTexto.MoverArriba();
		CajaTexto.AgregarDialogo(DialogosCinematica[10]);
		CajaTexto.AgregarDialogo(DialogosCinematica[11]);
		await ToSignal(CajaTexto, "DialogoFinalizado");
		CajaTexto.MoverAbajo();
		AnimacionesCamara.Play("DesvanecerPresentador");
		await ToSignal(AnimacionesCamara, "animation_finished");
		AnimacionesCamara.PlayBackwards("MirarPresentador");
		await ToSignal(AnimacionesCamara, "animation_finished");
		CajaTexto.AgregarDialogo(DialogosCinematica[12]);
		CajaTexto.AgregarDialogo(DialogosCinematica[13]);
		CajaTexto.AgregarDialogo(DialogosCinematica[14]);
		await ToSignal(CajaTexto, "DialogoFinalizado");
		Presentador.QueueFree();
		Camara.Set("position", Protagonista.Get("position"));
		Camara.Reparent(Protagonista);
		Camara.Set("position", Vector2.Zero);
		Protagonista.HabilitarMovimiento();
		Camara.Set("position_smoothing_enabled", true);
		GetParent<Nucleo>().Controles();
		Esencial.Instancia.PrimerCinematicaTerminada();
	}
}
