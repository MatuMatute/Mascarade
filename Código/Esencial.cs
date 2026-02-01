using Godot;

public partial class Esencial : Node
{
	public static Esencial Instancia { get; private set; }

	private int Salud;

	private bool PrimerCinematica;
	private bool ConversacionPrimerGuardia;
	private bool ConversacionConBoleteria;
	private bool TenesBoleto;
	private bool PrimerGuardiaTeDejaPasar;

	public override void _Ready()
	{
		Salud = 5;
		Instancia = this;
		PrimerCinematica = false;
		ConversacionPrimerGuardia = false;
		ConversacionConBoleteria = false;
		TenesBoleto = false;
		PrimerGuardiaTeDejaPasar = false;
	}

	public void QuitarSalud()
	{
		if (Salud > 1)
		{
			Salud--;
			GetTree().Root.GetNode<Nucleo>("Nucleo").ActualizarContador();
		}
		else
		{
			Salud = 5;
			GetTree().Root.GetNode<Nucleo>("Nucleo").CambiarArea(6, new Vector2(961.0f, 968.0f));
		}
	}

	public void PrimerCinematicaTerminada() {PrimerCinematica = true;}

	public void HablasteConPrimerGuardia() {ConversacionPrimerGuardia = true;}

	public void HablasteConBoleteria() {ConversacionConBoleteria = true;}

	public void ConseguirBoleto() { TenesBoleto = true; }

	public void PrimerGuardiaSeMovió() {PrimerGuardiaTeDejaPasar = true;}

	public int GetSalud() {return Salud;}

	public bool IntroHaTerminado() { return PrimerCinematica; }

	public bool HasConversadoConPrimerGuardia() {return ConversacionPrimerGuardia;}

	public bool HasConversadoConBoleteria() {return ConversacionConBoleteria;}

	public bool TieneBoleto() {return TenesBoleto;}

	public bool TenesAccesoAlPayaso() {return PrimerGuardiaTeDejaPasar;}
}
