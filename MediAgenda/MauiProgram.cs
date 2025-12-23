using MediAgenda.Data;
using MediAgenda.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MediAgenda
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // 🔥 REGISTRAR LA BASE DE DATOS
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "mediagenda.db");
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite($"Filename={dbPath}"));

            // Registrar el servicio de turnos
            builder.Services.AddSingleton<TurnoService>();

            // Registrar las páginas para navegación
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<MenuPage>();
            builder.Services.AddTransient<MisTurnosPage>();
            builder.Services.AddTransient<ClinicasPage>();
            builder.Services.AddTransient<ProfesionalesPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}