using MediAgenda.Data;
using MediAgenda.Models;
using Microsoft.EntityFrameworkCore;

namespace MediAgenda.Services
{
    public class AutenticacionService
    {
        private readonly AppDbContext _context;
        public static Paciente? PacienteActual { get; private set; }

        public AutenticacionService()
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "mediagenda.db");
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlite($"Filename={dbPath}");
            _context = new AppDbContext(optionsBuilder.Options);
        }

        public async Task<(bool exito, Paciente? paciente, string mensaje)> IniciarSesionAsync(string email, string password)
        {
            try
            {
                var paciente = await _context.Pacientes
                    .FirstOrDefaultAsync(p => p.Email == email);

                if (paciente == null)
                {
                    return (false, null, "Usuario no encontrado");
                }

                // Por ahora validación simple (en producción usar hash)
                if (paciente.DNI == password) // Usando DNI como contraseña por simplicidad
                {
                    PacienteActual = paciente;
                    return (true, paciente, "Inicio de sesión exitoso");
                }

                return (false, null, "Contraseña incorrecta");
            }
            catch (Exception ex)
            {
                return (false, null, $"Error: {ex.Message}");
            }
        }

        public void CerrarSesion()
        {
            PacienteActual = null;
        }

        public bool EstaAutenticado()
        {
            return PacienteActual != null;
        }
    }
}