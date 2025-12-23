using MediAgenda.Data;
using MediAgenda.Models;
using MediAgenda.Services;
using Microsoft.EntityFrameworkCore;

namespace MediAgenda
{
    public partial class MainPage : ContentPage
    {
        private readonly AppDbContext _context;
        private readonly TurnoService _turnoService;
        private List<Profesional> _todosLosProfesionales = new List<Profesional>();
        private const int PACIENTE_ID = 1;

        // Días y horarios permitidos por especialidad
        private Dictionary<string, (DayOfWeek[] dias, int horaInicio, int horaFin)> reglasEspecialidades =
            new Dictionary<string, (DayOfWeek[], int, int)>
            {
                { "Cardiología", (new [] { DayOfWeek.Monday, DayOfWeek.Wednesday, DayOfWeek.Friday }, 9, 15) },
                { "Clínica Médica", (new [] { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday }, 9, 20) },
                { "Dermatología", (new [] { DayOfWeek.Tuesday, DayOfWeek.Thursday, DayOfWeek.Friday }, 16, 20) },
                { "Pediatría", (new [] { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Friday }, 9, 14) }
            };

        public MainPage()
        {
            InitializeComponent();

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "mediagenda.db");
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlite($"Filename={dbPath}");

            _context = new AppDbContext(optionsBuilder.Options);
            _turnoService = new TurnoService();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await CargarEspecialidades();
        }

        private async Task CargarEspecialidades()
        {
            try
            {
                _todosLosProfesionales = await _context.Profesionales
                    .Include(p => p.Clinica)
                    .ToListAsync();

                var especialidades = _todosLosProfesionales
                    .Where(p => !string.IsNullOrWhiteSpace(p.Especialidad))
                    .Select(p => p.Especialidad.Trim())
                    .Distinct()
                    .OrderBy(e => e)
                    .ToList();

                pickerEspecialidad.ItemsSource = especialidades;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error",
                    $"No se pudieron cargar las especialidades: {ex.Message}", "OK");
            }
        }

        private async void OnEspecialidadChanged(object sender, EventArgs e)
        {
            pickerProfesional.SelectedIndex = -1;
            pickerProfesional.IsEnabled = false;

            pickerFecha.ItemsSource = null;
            pickerFecha.IsEnabled = false;

            pickerHora.ItemsSource = null;
            pickerHora.IsEnabled = false;

            if (pickerEspecialidad.SelectedIndex == -1)
                return;

            string especialidad = pickerEspecialidad.SelectedItem.ToString();

            // Aviso de días y horarios disponibles
            var regla = reglasEspecialidades[especialidad];
            string diasTexto = string.Join(", ", regla.dias.Select(TraducirDia));
            string mensaje = $"{especialidad} atiende los días: {diasTexto}\n" +
                             $"Horarios: {regla.horaInicio:00}:00 a {regla.horaFin:00}:00 hs";
            await DisplayAlert("Disponibilidad", mensaje, "OK");

            // Cargar profesionales
            var profesionalesFiltrados = _todosLosProfesionales
                .Where(p => p.Especialidad == especialidad)
                .Select(p => new ProfesionalDisplay
                {
                    Id = p.Id,
                    Nombre = $"Dr/a. {p.Nombre} {p.Apellido} - {p.Clinica.Nombre}"
                })
                .ToList();

            pickerProfesional.ItemsSource = profesionalesFiltrados;
            pickerProfesional.ItemDisplayBinding = new Binding("Nombre");
            pickerProfesional.IsEnabled = true;

            // Cargar fechas válidas
            GenerarFechasDisponibles(especialidad);
        }

        private void GenerarFechasDisponibles(string especialidad)
        {
            pickerFecha.ItemsSource = null;
            pickerFecha.IsEnabled = true;

            var regla = reglasEspecialidades[especialidad];

            List<string> fechas = new List<string>();
            DateTime hoy = DateTime.Today;

            for (int i = 0; i < 30; i++)
            {
                var fecha = hoy.AddDays(i);

                if (regla.dias.Contains(fecha.DayOfWeek))
                {
                    fechas.Add($"{TraducirDia(fecha.DayOfWeek)} {fecha:dd/MM/yyyy}");
                }
            }

            if (fechas.Count == 0)
            {
                DisplayAlert("Sin disponibilidad",
                    "No hay fechas disponibles para esta especialidad", "OK");
            }

            pickerFecha.ItemsSource = fechas;
        }


        private void OnProfesionalChanged(object sender, EventArgs e)
        {
            pickerHora.ItemsSource = null;
            pickerHora.IsEnabled = false;

            if (pickerProfesional.SelectedIndex == -1 ||
                pickerEspecialidad.SelectedIndex == -1 ||
                pickerFecha.SelectedIndex == -1)
                return;

            GenerarHorasDisponibles();
        }

        private void OnFechaChanged(object sender, EventArgs e)
        {
            pickerHora.ItemsSource = null;
            pickerHora.IsEnabled = false;

            if (pickerFecha.SelectedIndex == -1 ||
                pickerEspecialidad.SelectedIndex == -1)
                return;

            if (pickerProfesional.SelectedIndex != -1)
                GenerarHorasDisponibles();
        }

        private void GenerarHorasDisponibles()
        {
            string especialidad = pickerEspecialidad.SelectedItem.ToString();
            var regla = reglasEspecialidades[especialidad];

            List<string> horas = new List<string>();

            for (int h = regla.horaInicio; h < regla.horaFin; h++)
            {
                horas.Add($"{h:00}:00");
                horas.Add($"{h:00}:30");
            }

            pickerHora.ItemsSource = horas;
            pickerHora.IsEnabled = true;
        }

        private async void OnSolicitarTurnoClicked(object sender, EventArgs e)
        {
            try
            {
                if (pickerEspecialidad.SelectedIndex == -1 ||
                    pickerProfesional.SelectedIndex == -1 ||
                    pickerHora.SelectedIndex == -1 ||
                    pickerFecha.SelectedIndex == -1)
                {
                    await DisplayAlert("⚠️ Atención",
                        "Debes completar todos los campos.", "OK");
                    return;
                }

                var profesionalSel = (ProfesionalDisplay)pickerProfesional.SelectedItem;

                string fechaTexto = pickerFecha.SelectedItem.ToString();
                string fechaSolo = fechaTexto.Split(' ')[1];
                DateTime fecha = DateTime.ParseExact(fechaSolo, "dd/MM/yyyy", null);

                TimeSpan hora = TimeSpan.Parse(pickerHora.SelectedItem.ToString());
                DateTime fechaHora = fecha.Add(hora);

                bool disponible = await _turnoService.HorarioDisponibleAsync(
                    profesionalSel.Id, fechaHora);

                if (!disponible)
                {
                    await DisplayAlert("Horario ocupado",
                        "Ese horario ya está reservado.", "OK");
                    return;
                }

                var turno = new Turno
                {
                    PacienteId = PACIENTE_ID,
                    ProfesionalId = profesionalSel.Id,
                    FechaHora = fechaHora,
                    Motivo = string.IsNullOrWhiteSpace(editorMotivo.Text)
                        ? "Consulta general"
                        : editorMotivo.Text,
                    Estado = "Solicitado"
                };

                bool creado = await _turnoService.CrearTurnoAsync(turno);

                if (creado)
                {
                    await DisplayAlert("Turno solicitado",
                        $"Turno reservado para el {fechaHora:dd/MM/yyyy} a las {fechaHora:HH:mm}",
                        "OK");
                    await Navigation.PopAsync();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private string TraducirDia(DayOfWeek d) =>
            d switch
            {
                DayOfWeek.Monday => "Lunes",
                DayOfWeek.Tuesday => "Martes",
                DayOfWeek.Wednesday => "Miércoles",
                DayOfWeek.Thursday => "Jueves",
                DayOfWeek.Friday => "Viernes",
                DayOfWeek.Saturday => "Sábado",
                DayOfWeek.Sunday => "Domingo",
                _ => ""
            };

        public class ProfesionalDisplay
        {
            public int Id { get; set; }
            public string Nombre { get; set; } = string.Empty;
        }
    }
}
