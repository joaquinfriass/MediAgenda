namespace MediAgenda
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnEspecialidadClicked(object sender, EventArgs e)
        {
            Button boton = sender as Button;
            string especialidad = boton?.Text;

            await DisplayAlert("Turno", $"Seleccionaste: {especialidad}", "OK");
            // Aquí podés navegar a una página de selección de médico o fecha
        }
    }

}
