using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Models
{
    public class Turno
    {
        public int Id { get; set; }
        public DateTime FechaHora { get; set; }
        public string Motivo { get; set; }

        // Estados posibles: "Solicitado", "Aceptado", "Realizado", "Cancelado"
        public string Estado { get; set; } = "Solicitado";

        // Claves foráneas
        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; }

        public int ProfesionalId { get; set; }
        public Profesional Profesional { get; set; }

        // Para notificaciones
        public bool NotificacionEnviada24h { get; set; } = false;
        public bool NotificacionEnviada1h { get; set; } = false;
    }
}