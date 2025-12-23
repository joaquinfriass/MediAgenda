using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public required string Rol { get; set; }

        public int? PacienteId { get; set; }
        public Paciente? Paciente { get; set; }
    }

}


