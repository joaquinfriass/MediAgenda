namespace MediAgenda;

public partial class MenuPage : ContentPage
{
	public MenuPage()
	{
		InitializeComponent();
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
}