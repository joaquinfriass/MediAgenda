using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Models
{
    public class Profesional
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Especialidad { get; set; }
        public string Matricula { get; set; }

        // Clave foránea: pertenece a una clínica
        public int ClinicaId { get; set; }
        public Clinica Clinica { get; set; }

        // Relación: un profesional puede tener muchos turnos
        public List<Turno> Turnos { get; set; } = new List<Turno>();
    }
}