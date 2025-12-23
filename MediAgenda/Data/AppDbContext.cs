using MediAgenda.Data;
using MediAgenda.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Data
{
    public class AppDbContext : DbContext
    {
        // Estas son las "tablas" de tu base de datos
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Clinica> Clinicas { get; set; }
        public DbSet<Profesional> Profesionales { get; set; }
        public DbSet<Turno> Turnos { get; set; }

        // Constructor
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Configuración de la base de datos
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Ruta donde se guardará la base de datos en el dispositivo
                string dbPath = Path.Combine(FileSystem.AppDataDirectory, "mediagenda.db");
                optionsBuilder.UseSqlite($"Filename={dbPath}");
            }
        }

        // Configuración de relaciones y datos iniciales
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relaciones entre tablas
            modelBuilder.Entity<Turno>()
                .HasOne(t => t.Paciente)
                .WithMany(p => p.Turnos)
                .HasForeignKey(t => t.PacienteId);

            modelBuilder.Entity<Turno>()
                .HasOne(t => t.Profesional)
                .WithMany(pr => pr.Turnos)
                .HasForeignKey(t => t.ProfesionalId);

            modelBuilder.Entity<Profesional>()
                .HasOne(pr => pr.Clinica)
                .WithMany(c => c.Profesionales)
                .HasForeignKey(pr => pr.ClinicaId);

            // DATOS DE PRUEBA (seed data) - Para que tu app no esté vacía
            // Clínicas
            modelBuilder.Entity<Clinica>().HasData(
                new Clinica { Id = 1, Nombre = "Clínica Santa María", Direccion = "Av. Belgrano 1234", Ciudad = "Formosa", Telefono = "370-4445566" },
                new Clinica { Id = 2, Nombre = "Sanatorio del Sol", Direccion = "Calle Rivadavia 567", Ciudad = "Corrientes", Telefono = "379-4447788" },
                new Clinica { Id = 3, Nombre = "Centro Médico Norte", Direccion = "Ruta 11 KM 8", Ciudad = "Resistencia", Telefono = "362-4449900" }
            );

            // Profesionales
            modelBuilder.Entity<Profesional>().HasData(
                new Profesional { Id = 1, Nombre = "María", Apellido = "López", Especialidad = "Pediatría", Matricula = "MP12345", ClinicaId = 1 },
                new Profesional { Id = 2, Nombre = "José", Apellido = "Fernández", Especialidad = "Cardiología", Matricula = "MP23456", ClinicaId = 2 },
                new Profesional { Id = 3, Nombre = "Ana", Apellido = "Gómez", Especialidad = "Dermatología", Matricula = "MP34567", ClinicaId = 3 },
                new Profesional { Id = 4, Nombre = "Carlos", Apellido = "Martínez", Especialidad = "Clínica Médica", Matricula = "MP45678", ClinicaId = 1 }
            );

            // Paciente de ejemplo
            modelBuilder.Entity<Paciente>().HasData(
                new Paciente { Id = 1, Nombre = "Juan", Apellido = "Pérez", DNI = "12345678", Telefono = "370-1234567", Email = "juan@example.com" }
            );
        }
    }
}

