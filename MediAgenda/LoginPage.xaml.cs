using MediAgenda.Services;

namespace MediAgenda;

public partial class LoginPage : ContentPage
{
    private readonly AutenticacionService _authService;

    public LoginPage()
    {
        InitializeComponent();
        _authService = new AutenticacionService();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        string? email = emailEntry.Text?.Trim();
        string? password = passwordEntry.Text;

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            messageLabel.Text = "Por favor completa todos los campos";
            messageLabel.IsVisible = true;
            return;
        }

        var (exito, paciente, mensaje) = await _authService.IniciarSesionAsync(email, password);

        if (exito && paciente != null)
        {
            messageLabel.IsVisible = false;
            await DisplayAlert("✅ Bienvenido", $"Hola {paciente.Nombre}!", "OK");
            await Navigation.PushAsync(new MenuPage());
        }
        else
        {
            messageLabel.Text = mensaje;
            messageLabel.IsVisible = true;
        }
    }
}