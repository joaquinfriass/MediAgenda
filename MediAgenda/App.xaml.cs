using MediAgenda.Data;
using Microsoft.EntityFrameworkCore;

namespace MediAgenda
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            InicializarBaseDeDatos();

            // Iniciar con LoginPage
            MainPage = new NavigationPage(new LoginPage());
        }

        private async void InicializarBaseDeDatos()
        {
            try
            {
                string dbPath = Path.Combine(FileSystem.AppDataDirectory, "mediagenda.db");
                var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
                optionsBuilder.UseSqlite($"Filename={dbPath}");

                using (var context = new AppDbContext(optionsBuilder.Options))
                {
                    await context.Database.EnsureCreatedAsync();
                    Console.WriteLine($"✅ Base de datos creada en: {dbPath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al crear la base de datos: {ex.Message}");
            }
        }
    }
}