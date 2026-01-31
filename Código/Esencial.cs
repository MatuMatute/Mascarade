using Godot;

public partial class Esencial : Node
{
	public static Esencial Instancia { get; private set; }

	private bool PrimerCinematica;
	private bool ConversacionPrimerGuardia;
	private bool ConversacionConBoleteria;
	private bool TenesBoleto;
	private bool PrimerGuardiaTeDejaPasar;

	public override void _Ready()
	{
		Instancia = this;
		PrimerCinematica = false;
		ConversacionPrimerGuardia = false;
		ConversacionConBoleteria = false;
		TenesBoleto = false;
		PrimerGuardiaTeDejaPasar = false;
	}

	public void PrimerCinematicaTerminada() {PrimerCinematica = true;}

	public void HablasteConPrimerGuardia() {ConversacionPrimerGuardia = true;}

	public void HablasteConBoleteria() {ConversacionConBoleteria = true;}

	public void ConseguirBoleto() { TenesBoleto = true; }

	public void PrimerGuardiaSeMovió() {PrimerGuardiaTeDejaPasar = true;}

	public bool IntroHaTerminado() { return PrimerCinematica; }

	public bool HasConversadoConPrimerGuardia() {return ConversacionPrimerGuardia;}

	public bool HasConversadoConBoleteria() {return ConversacionConBoleteria;}

	public bool TieneBoleto() {return TenesBoleto;}

	public bool TenesAccesoAlPayaso() {return PrimerGuardiaTeDejaPasar;}
}
