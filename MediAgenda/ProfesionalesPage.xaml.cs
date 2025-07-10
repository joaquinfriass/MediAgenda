namespace MediAgenda;

public partial class ProfesionalesPage : ContentPage
{
	public ProfesionalesPage()
	{
		InitializeComponent();
        CargarProfesionales();
    }

    public class Profesional
    {
        public string Nombre { get; set; }
        public string Especialidad { get; set; }
        public string Clinica { get; set; }
    }

    private void CargarProfesionales()
    {
        var profesionales = new List<Profesional>
        {
            new Profesional { Nombre = "Dra. Mar�a L�pez", Especialidad = "Pediatr�a", Clinica = "Cl�nica Santa Mar�a" },
            new Profesional { Nombre = "Dr. Jos� Fern�ndez", Especialidad = "Cardiolog�a", Clinica = "Sanatorio del Sol" },
            new Profesional { Nombre = "Dra. Ana G�mez", Especialidad = "Dermatolog�a", Clinica = "Centro M�dico Norte" }
        };

        profesionalesView.ItemsSource = profesionales;
    }
}