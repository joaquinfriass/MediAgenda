using MediAgenda.Services;

namespace MediAgenda;

public partial class MenuPage : ContentPage
{
    private readonly AutenticacionService _authService;

    public MenuPage()
    {
        InitializeComponent();
        _authService = new AutenticacionService();
        MostrarNombreUsuario();
    }

    private void MostrarNombreUsuario()
    {
        var paciente = AutenticacionService.PacienteActual;
        if (paciente != null)
        {
            labelUsuario.Text = $"👤 {paciente.Nombre} {paciente.Apellido}";
        }
    }

    private async void OnNuevoTurnoClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }

    private async void OnMisTurnosClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MisTurnosPage());
    }

    private async void OnClinicasClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ClinicasPage());
    }

    private async void OnProfesionalesClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProfesionalesPage());
    }

    private async void OnCerrarSesionClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert(
            "Cerrar sesión",
            "¿Estás seguro que deseas cerrar sesión?",
            "Sí",
            "No"
        );

        if (confirm)
        {
            _authService.CerrarSesion();
            await Navigation.PopToRootAsync();
        }
    }
}
