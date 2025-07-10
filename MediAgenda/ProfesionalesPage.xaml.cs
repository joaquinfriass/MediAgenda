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
            new Profesional { Nombre = "Dra. María López", Especialidad = "Pediatría", Clinica = "Clínica Santa María" },
            new Profesional { Nombre = "Dr. José Fernández", Especialidad = "Cardiología", Clinica = "Sanatorio del Sol" },
            new Profesional { Nombre = "Dra. Ana Gómez", Especialidad = "Dermatología", Clinica = "Centro Médico Norte" }
        };

        profesionalesView.ItemsSource = profesionales;
    }
}