using MediAgenda.Data;
using MediAgenda.Models;
using Microsoft.EntityFrameworkCore;

namespace MediAgenda;

public partial class ClinicasPage : ContentPage
{
    private readonly AppDbContext _context;

    public ClinicasPage()
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
        await CargarClinicas();
    }

    private async Task CargarClinicas()
    {
        try
        {
            // Cargar clínicas desde la base de datos
            var clinicas = await _context.Clinicas.ToListAsync();
            clinicasView.ItemsSource = clinicas;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudieron cargar las clínicas: {ex.Message}", "OK");
        }
    }
}