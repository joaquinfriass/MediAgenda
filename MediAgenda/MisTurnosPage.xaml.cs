namespace MediAgenda;

public partial class MisTurnosPage : ContentPage
{
	public MisTurnosPage()
	{
		InitializeComponent();
       CargarTurnos(); // Simula datos por ahora
    }

    // Estructura simple de turno
    public class Turno
    {
        public string Descripcion { get; set; }
    }

    private void CargarTurnos()
    {
        // Simulaci�n de datos
        var solicitados = new List<Turno>
        {
            new Turno { Descripcion = "Cardiolog�a - 12/07/2025 - 10:00" }
        };

        var aceptados = new List<Turno>
        {
            new Turno { Descripcion = "Pediatr�a - 15/07/2025 - 15:30" }
        };

        var realizados = new List<Turno>
        {
            new Turno { Descripcion = "Cl�nica M�dica - 01/07/2025 - 09:00" }
        };

        turnosSolicitadosView.ItemsSource = solicitados;
        turnosAceptadosView.ItemsSource = aceptados;
        turnosRealizadosView.ItemsSource = realizados;
    }

    private async void OnCancelarTurnoClicked(object sender, EventArgs e)
    {
        var turno = (sender as Button)?.CommandParameter as Turno;
        if (turno != null)
        {
            bool confirm = await DisplayAlert("Cancelar turno", $"�Deseas cancelar:\n{turno.Descripcion}?", "S�", "No");
            if (confirm)
            {
                await DisplayAlert("Turno cancelado", "El turno fue cancelado con �xito.", "OK");
                // Ac� podr�as eliminarlo de la lista o actualizar la base de datos
            }
        }
    }

}