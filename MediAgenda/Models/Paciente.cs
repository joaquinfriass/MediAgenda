using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MediAgenda.MisTurnosPage;

namespace MediAgenda.Models
{
    public class Paciente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string DNI { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }

        // Relación: un paciente puede tener muchos turnos
        public List<Turno> Turnos { get; set; } = new List<Turno>();
    }
}