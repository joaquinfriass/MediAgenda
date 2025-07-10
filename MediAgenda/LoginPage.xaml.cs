namespace MediAgenda;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        string email = emailEntry.Text?.Trim();
        string password = passwordEntry.Text;

        // Simulación simple
        if (email == "paciente@mail.com" && password == "1234")
        {
            messageLabel.IsVisible = false;
            await Navigation.PushAsync(new MenuPage());
        }
        else
        {
            messageLabel.Text = "Credenciales incorrectas.";
            messageLabel.IsVisible = true;
        }
    }

}