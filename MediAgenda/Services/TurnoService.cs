using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediAgenda.Data;
using MediAgenda.Models;
using Microsoft.EntityFrameworkCore;

namespace MediAgenda.Services
{
    public class TurnoService
    {
        private readonly AppDbContext _context;

        public TurnoService()
        {
            // Inicializar el contexto de base de datos
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "mediagenda.db");
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlite($"Filename={dbPath}");
            _context = new AppDbContext(optionsBuilder.Options);
        }

        // ✅ CREAR NUEVO TURNO
        public async Task<bool> CrearTurnoAsync(Turno turno)
        {
            try
            {
                _context.Turnos.Add(turno);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al crear turno: {ex.Message}");
                return false;
            }
        }

        // 📋 OBTENER TODOS LOS TURNOS DE UN PACIENTE
        public async Task<List<Turno>> ObtenerTurnosPorPacienteAsync(int pacienteId)
        {
            try
            {
                return await _context.Turnos
                    .Include(t => t.Profesional)
                        .ThenInclude(p => p.Clinica)
                    .Include(t => t.Paciente)
                    .Where(t => t.PacienteId == pacienteId)
                    .OrderBy(t => t.FechaHora)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al obtener turnos: {ex.Message}");
                return new List<Turno>();
            }
        }

        // 📊 OBTENER TURNOS POR ESTADO
        public async Task<List<Turno>> ObtenerTurnosPorEstadoAsync(int pacienteId, string estado)
        {
            try
            {
                return await _context.Turnos
                    .Include(t => t.Profesional)
                        .ThenInclude(p => p.Clinica)
                    .Where(t => t.PacienteId == pacienteId && t.Estado == estado)
                    .OrderBy(t => t.FechaHora)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al filtrar turnos: {ex.Message}");
                return new List<Turno>();
            }
        }

        // ❌ CANCELAR TURNO
        public async Task<bool> CancelarTurnoAsync(int turnoId)
        {
            try
            {
                var turno = await _context.Turnos.FindAsync(turnoId);
                if (turno != null)
                {
                    turno.Estado = "Cancelado";
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al cancelar turno: {ex.Message}");
                return false;
            }
        }

        // 🔄 ACTUALIZAR ESTADO DEL TURNO
        public async Task<bool> ActualizarEstadoTurnoAsync(int turnoId, string nuevoEstado)
        {
            try
            {
                var turno = await _context.Turnos.FindAsync(turnoId);
                if (turno != null)
                {
                    turno.Estado = nuevoEstado;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al actualizar estado: {ex.Message}");
                return false;
            }
        }

        // 🔍 OBTENER TURNOS DISPONIBLES (para validar horarios)
        public async Task<bool> HorarioDisponibleAsync(int profesionalId, DateTime fechaHora)
        {
            try
            {
                // Verificar si ya existe un turno en ese horario
                var turnoExistente = await _context.Turnos
                    .Where(t => t.ProfesionalId == profesionalId
                           && t.FechaHora == fechaHora
                           && t.Estado != "Cancelado")
                    .FirstOrDefaultAsync();

                return turnoExistente == null; // true si está disponible
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al verificar disponibilidad: {ex.Message}");
                return false;
            }
        }

        // 📅 OBTENER PRÓXIMOS TURNOS (para notificaciones)
        public async Task<List<Turno>> ObtenerProximosTurnosAsync(int pacienteId)
        {
            try
            {
                var ahora = DateTime.Now;
                return await _context.Turnos
                    .Include(t => t.Profesional)
                    .Where(t => t.PacienteId == pacienteId
                           && t.FechaHora > ahora
                           && t.Estado == "Aceptado")
                    .OrderBy(t => t.FechaHora)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al obtener próximos turnos: {ex.Message}");
                return new List<Turno>();
            }
        }
    }
}
