namespace MediAgenda
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Inicia en LoginPage
            MainPage = new NavigationPage(new LoginPage());
        }
    }
}
