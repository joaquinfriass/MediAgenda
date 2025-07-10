namespace MediAgenda;

public partial class ClinicasPage : ContentPage
{
	public ClinicasPage()
	{
		InitializeComponent();
        CargarClinicas();
    }

    public class Clinica
    {
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
    }

    private void CargarClinicas()
    {
        var clinicas = new List<Clinica>
        {
            new Clinica { Nombre = "Clínica Santa María", Direccion = "Av. Belgrano 1234", Ciudad = "Formosa" },
            new Clinica { Nombre = "Sanatorio del Sol", Direccion = "Calle Rivadavia 567", Ciudad = "Corrientes" },
            new Clinica { Nombre = "Centro Médico Norte", Direccion = "Ruta 11 KM 8", Ciudad = "Resistencia" }
        };

        clinicasView.ItemsSource = clinicas;
    }
}