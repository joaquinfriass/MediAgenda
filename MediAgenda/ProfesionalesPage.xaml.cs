using MediAgenda.Data;
using MediAgenda.Models;
using Microsoft.EntityFrameworkCore;

namespace MediAgenda;

public partial class ProfesionalesPage : ContentPage
{
    private readonly AppDbContext _context;

    public ProfesionalesPage()
    {
        InitializeComponent();

        // Inicializar contexto
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "mediagenda.db");
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlite($"Filename={dbPath}");
        _context = new AppDbContext(optionsBuilder.Options);
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await CargarProfesionales();
    }

    private async Task CargarProfesionales()
    {
        try
        {
            // Cargar profesionales CON sus clínicas
            var profesionales = await _context.Profesionales
                .Include(p => p.Clinica)
                .Select(p => new ProfesionalViewModel
                {
                    Nombre = $"Dr/a. {p.Nombre} {p.Apellido}",
                    Especialidad = p.Especialidad,
                    Clinica = p.Clinica.Nombre
                })
                .ToListAsync();

            profesionalesView.ItemsSource = profesionales;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudieron cargar los profesionales: {ex.Message}", "OK");
        }
    }

    // Clase auxiliar para mostrar datos
    public class ProfesionalViewModel
    {
        public string Nombre { get; set; }
        public string Especialidad { get; set; }
        public string Clinica { get; set; }
    }
}