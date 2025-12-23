using MediAgenda.Models;
using MediAgenda.Services;

namespace MediAgenda;

public partial class MisTurnosPage : ContentPage
{
    private readonly TurnoService _turnoService;
    private const int PACIENTE_ID = 1;

    public MisTurnosPage()
    {
        InitializeComponent();
        _turnoService = new TurnoService();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await CargarTurnos();
    }

    private async Task CargarTurnos()
    {
        try
        {
            var solicitados = await _turnoService.ObtenerTurnosPorEstadoAsync(PACIENTE_ID, "Solicitado");
            var aceptados = await _turnoService.ObtenerTurnosPorEstadoAsync(PACIENTE_ID, "Aceptado");
            var realizados = await _turnoService.ObtenerTurnosPorEstadoAsync(PACIENTE_ID, "Realizado");

            turnosSolicitadosView.ItemsSource = solicitados.Select(t => new TurnoViewModel
            {
                Id = t.Id,
                Descripcion = $"{t.Profesional.Especialidad} - Dr/a. {t.Profesional.Nombre} {t.Profesional.Apellido}\n" +
                             $"{t.FechaHora:dd/MM/yyyy - HH:mm}\n" +
                             $"{t.Profesional.Clinica.Nombre}",
                TurnoOriginal = t
            }).ToList();

            turnosAceptadosView.ItemsSource = aceptados.Select(t => new TurnoViewModel
            {
                Id = t.Id,
                Descripcion = $"{t.Profesional.Especialidad} - Dr/a. {t.Profesional.Nombre} {t.Profesional.Apellido}\n" +
                             $"{t.FechaHora:dd/MM/yyyy - HH:mm}\n" +
                             $"{t.Profesional.Clinica.Nombre}",
                TurnoOriginal = t
            }).ToList();

            turnosRealizadosView.ItemsSource = realizados.Select(t => new TurnoViewModel
            {
                Id = t.Id,
                Descripcion = $"{t.Profesional.Especialidad} - Dr/a. {t.Profesional.Nombre} {t.Profesional.Apellido}\n" +
                             $"{t.FechaHora:dd/MM/yyyy - HH:mm}\n" +
                             $"{t.Profesional.Clinica.Nombre}",
                TurnoOriginal = t
            }).ToList();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudieron cargar los turnos: {ex.Message}", "OK");
        }
    }

    private async void OnCancelarTurnoClicked(object sender, EventArgs e)
    {
        try
        {
            var button = sender as Button;
            var turnoVM = button?.CommandParameter as TurnoViewModel;

            if (turnoVM != null)
            {
                bool confirm = await DisplayAlert(
                    "Cancelar turno",
                    $"¿Deseas cancelar este turno?\n\n{turnoVM.Descripcion}",
                    "Sí",
                    "No"
                );

                if (confirm)
                {
                    bool resultado = await _turnoService.CancelarTurnoAsync(turnoVM.Id);

                    if (resultado)
                    {
                        await DisplayAlert("✅ Turno cancelado", "El turno fue cancelado exitosamente.", "OK");
                        await CargarTurnos();
                    }
                    else
                    {
                        await DisplayAlert("Error", "No se pudo cancelar el turno.", "OK");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
        }
    }

    public class TurnoViewModel
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public Turno? TurnoOriginal { get; set; }
    }
}